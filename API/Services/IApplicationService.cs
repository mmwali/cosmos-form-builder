using API.DTOs.Requests;
using API.DTOs.Responses;

namespace API.Services;

public interface IApplicationService
{
    Task<BaseResponse> CreateApplicationAsync(Guid programId, CreateApplicationRequest request);
}
