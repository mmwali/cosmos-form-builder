using API.DTOs.Requests;
using API.DTOs.Responses;

namespace API.Services;

public interface IProgramService
{
    Task<DataResponse<Guid>> CreateProgramAsync(CreateProgramRequest request);
    Task<DataResponse<ProgramResponse>> ViewProgramAsync(Guid id);
    Task<BaseResponse> CreateQuestionAsync(Guid programId, QuestionRequest request);
    Task<BaseResponse> UpdateQuestionAsync(Guid questionId, QuestionRequest request);
    Task<BaseResponse> DeleteQuestionAsync(Guid questionId);
}
