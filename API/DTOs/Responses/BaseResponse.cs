using API.Entities;

namespace API.DTOs.Responses;

public class BaseResponse
{
    public ResponseCodes Code { get; set; } = ResponseCodes.Success;
    public bool Status { get; set; } = true;
    public string? Message { get; set; }
}
public class DataResponse<T> : BaseResponse
{
    public T? Data { get; set; }
}
