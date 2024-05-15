using API.Data;
using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Entities;
using API.Enums;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class ApplicationService : IApplicationService
{
    private readonly AppDbContext _context;

    public ApplicationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse> CreateApplicationAsync(Guid programId, CreateApplicationRequest request)
    {
        var programExists = await _context.Programs.AnyAsync(p => p.Id == programId);
        if (programExists)
        {
            return new()
            {
                Code = ResponseCodes.NoData,
                Message = "Program not found",
                Status = false
            };
        }
        var questions = await _context.Questions
            .Where(c => c.ProgramId == programId && c.IsHidden != true)
            .ToListAsync();

        var requiredCheck = questions.Where(c => c.IsMandatory).Select(c =>  c.Id)
            .All(c => request.Answers.Select(a => a.QuestionId).Contains(c));
        
        if (!requiredCheck)
        {
            return new()
            {
                Code = ResponseCodes.BadRequest,
                Message = "Required questions not answered",
                Status = false
            };
        }

        var app = new Application()
        {
            ProgramId = programId,
            Answers = new List<Answer>()
        };

        var answers = request.Answers.Select(a => new Answer()
        {
            QuestionId = a.QuestionId,
            Choices = a.Choices,
            Value = a.Value,
        }).ToList();

        app.Answers.AddRange(answers);

        await _context.Applications.AddAsync(app);

        var isSaved = await _context.SaveChangesAsync() > 0;
        if (isSaved)
        {
            return new();
        }
        return new()
        {
            Message = "Unable to create application",
            Status = false
        };

    }

}
