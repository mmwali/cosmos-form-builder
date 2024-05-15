using API.Data;
using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Entities;
using API.Enums;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

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
            Title = request.Title,
            Questions = new List<Question>()
        };
        program.Questions.AddRange(new List<Question>()
        {
            new()
            {
                Label = "First Name",
                QuestionType = QuestionType.Personal,
                InputType = InputType.Paragraph,
                IsMandatory = true,
                ProgramId = program.Id
            },
            new()
            {
                Label = "Last Name",
                QuestionType = QuestionType.Personal,
                InputType = InputType.Paragraph,
                IsMandatory = true,
                ProgramId = program.Id

            },
            new()
            {
                Label = "Email",
                InputType = InputType.Paragraph,
                QuestionType = QuestionType.Personal,
                ProgramId = program.Id,
                IsMandatory = true,
            },
            new()
            {
                Label = "Phone",
                ProgramId = program.Id,
                QuestionType = QuestionType.Personal,
                InputType = InputType.Paragraph,
                IsHidden = request.Phone.IsHidden,
                IsInternal = request.Phone.IsInternal,
            },
            new()
            {
                Label = "Nationality",
                ProgramId = program.Id,
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
                ProgramId = program.Id,
                IsInternal = request.Country.IsInternal,
            },
            new()
            {
                Label = "Id Number",
                InputType = InputType.Paragraph,
                IsHidden = request.IdNumber.IsHidden,
                QuestionType = QuestionType.Personal,
                IsInternal = request.IdNumber.IsInternal,
                ProgramId = program.Id
            },
            new()
            {
                Label = "Date Of Birth",
                InputType = InputType.Date,
                QuestionType = QuestionType.Personal,
                IsHidden = request.DateOfBirth.IsHidden,
                IsInternal = request.DateOfBirth.IsInternal,
                ProgramId = program.Id
            },
            new()
            {
                Label = "Gender",
                
                ProgramId = program.Id,
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
            InputType = request.QuestionType,
            ProgramId = programId,
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

    public async Task<DataResponse<ProgramResponse>> ViewProgramAsync(Guid id)
    {
        var program = await _context.Programs.AsNoTracking()
            //.Include(c => c.Questions)
            .SingleOrDefaultAsync(p => p.Id == id);
        if (program is null)
        {
            return new()
            {
                Code = ResponseCodes.NoData,
                Message = "Program not found",
                Status = false
            };
        }

        return new DataResponse<ProgramResponse>()
        {
            Data = new()
            {
                Description = program.Description,
                Title = program.Title,
                Id = id,
                Questions = _context.Questions?.AsNoTracking().Where(c => c.ProgramId == id).Select(c => new QuestionResponse()
                {
                    Label = c.Label,
                    Id = c.Id,
                    Choices = c.Choices,
                    EnableOtherOption = c.EnableOtherOption,
                    InputType = c.InputType,
                    IsHidden = c.IsHidden,
                    IsInternal = c.IsInternal,
                    IsMandatory = c.IsMandatory,
                    MaxChoices = c.MaxChoices,
                    QuestionType = c.QuestionType
                }).OrderBy(c => c.QuestionType).ToList()
            }
        };
    }
}
