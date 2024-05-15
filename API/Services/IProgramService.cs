using API.Data;
using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IProgramService
{
    Task<DataResponse<Guid>> CreateProgramAsync(CreateProgramRequest request);
    Task<BaseResponse> CreateQuestionAsync(Guid programId, QuestionRequest request);
    Task<BaseResponse> UpdateQuestionAsync(Guid questionId, QuestionRequest request);
    Task<BaseResponse> DeleteQuestionAsync(Guid questionId);
}

public class ProgramService : IProgramService
{
    private readonly AppDbContext _context;
    public ProgramService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<DataResponse<Guid>> CreateProgramAsync(CreateProgramRequest request)
    {
        var program = new Entities.Program()
        {
            Description = request.Description,
            Title = request.Title
        };
        program.Questions.AddRange(new List<Question>()
        {
            new()
            {
                Label = "First Name",
                QuestionType = QuestionType.Personal,
                InputType = InputType.Paragraph,
                IsMandatory = true,
            },
            new()
            {
                Label = "Last Name",
                QuestionType = QuestionType.Personal,
                InputType = InputType.Paragraph,
                IsMandatory = true,
            },
            new()
            {
                Label = "Email",
                InputType = InputType.Paragraph,
                QuestionType = QuestionType.Personal,
                IsMandatory = true,
            },
            new()
            {
                Label = "Phone",
                QuestionType = QuestionType.Personal,
                InputType = InputType.Paragraph,
                IsHidden = request.Phone.IsHidden,
                IsInternal = request.Phone.IsInternal,
            },
            new()
            {
                Label = "Nationality",
                QuestionType = QuestionType.Personal,
                InputType = InputType.Paragraph,
                IsHidden = request.Nationality.IsHidden,
                IsInternal = request.Nationality.IsInternal,
            },
            new()
            {
                Label = "Current Residence",
                InputType = InputType.Paragraph,
                QuestionType = QuestionType.Personal,
                IsHidden = request.Country.IsHidden,
                IsInternal = request.Country.IsInternal,
            },
            new()
            {
                Label = "Id Number",
                InputType = InputType.Paragraph,
                IsHidden = request.IdNumber.IsHidden,
                QuestionType = QuestionType.Personal,
                IsInternal = request.IdNumber.IsInternal,
            },
            new()
            {
                Label = "Date Of Birth",
                InputType = InputType.Date,
                QuestionType = QuestionType.Personal,
                IsHidden = request.DateOfBirth.IsHidden,
                IsInternal = request.DateOfBirth.IsInternal,
            },
            new()
            {
                Label = "Gender",
                
                InputType = InputType.Dropdown,
                IsHidden = request.Gender.IsHidden,
                IsInternal = request.Gender.IsInternal,
                EnableOtherOption = true,
                QuestionType = QuestionType.Personal,
                Choices = new List<Choice>
                {
                    new()
                    {
                        Name = "Male",
                    },
                    new()
                    {
                        Name = "Female",
                    }
                }
            },
        });
        await _context.Programs.AddAsync(program);

        var isSaved = await _context.SaveChangesAsync() > 0;
        if (isSaved)
        {
            return new()
            {
                Data = program.Id
            };
        }
        return new()
        {
            Message = "Unable to create program",
            Status = false
        };
    }

    public async Task<BaseResponse> CreateQuestionAsync(Guid programId, QuestionRequest request)
    {
        var program = await _context.Programs.SingleOrDefaultAsync(p => p.Id == programId);
        if (program is null)
        {
            return new()
            {
                Code = ResponseCodes.NoData,
                Message = "Program not found",
                Status = false
            };
        }

        var question = new Question()
        {
            Label = request.Label,
            InputType = request.QuestionType
        };
        switch (question.InputType)
        {
            case InputType.MultipleChoice:
                question.Choices = request.Choices?.Select(c => new Choice() { Name = c }).ToList();
                question.MaxChoices = request.MaxChoices;
                question.EnableOtherOption = request.EnableOtherOption;
                break;
            case InputType.Dropdown:
                question.Choices = request.Choices?.Select(c => new Choice() { Name = c }).ToList();
                question.EnableOtherOption = request.EnableOtherOption;
                break;
        }
        if (program.Questions is null)
        {
            program.Questions = new List<Question>();
        }
        program.Questions.Add(question);
        var isSaved = await _context.SaveChangesAsync() > 0;
        if (isSaved)
        {
            return new();
        }
        return new()
        {
            Message = "Unable to create question",
            Status = false
        };
    }

    public async Task<BaseResponse> DeleteQuestionAsync(Guid questionId)
    {
        var question = await _context.Questions.SingleOrDefaultAsync(p => p.Id == questionId);
        if (question is null)
        {
            return new()
            {
                Code = ResponseCodes.NoData,
                Message = "Question not found",
                Status = false
            };
        }
        _context.Questions.Remove(question);
        
        var isSaved = await _context.SaveChangesAsync() > 0;
        if (isSaved)
        {
            return new();
        }
        return new()
        {
            Message = "Unable to update question",
            Status = false
        };
    }

    public async Task<BaseResponse> UpdateQuestionAsync(Guid questionId, QuestionRequest request)
    {
        var question = await _context.Questions.SingleOrDefaultAsync(p => p.Id == questionId);
        if (question is null)
        {
            return new()
            {
                Code = ResponseCodes.NoData,
                Message = "Question not found",
                Status = false
            };
        }

        question.Label = request.Label;
        question.InputType = request.QuestionType;
        switch (request.QuestionType)
        {
            case InputType.MultipleChoice:
                question.Choices = request.Choices?.Select(c => new Choice() { Name = c }).ToList();
                question.MaxChoices = request.MaxChoices;
                question.EnableOtherOption = request.EnableOtherOption;
                break;
            case InputType.Dropdown:
                question.Choices = request.Choices?.Select(c => new Choice() { Name = c }).ToList();
                question.EnableOtherOption = request.EnableOtherOption;
                break;
        }
        var isSaved = await _context.SaveChangesAsync() > 0;
        if (isSaved)
        {
            return new();
        }
        return new()
        {
            Message = "Unable to update question",
            Status = false
        };
    }
}
