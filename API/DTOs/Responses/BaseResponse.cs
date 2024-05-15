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
public class ProgramResponse
{
    public  Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public List<QuestionResponse>? Questions { get; set; }

}
public class QuestionResponse
{
    public  Guid Id { get; set; }
    public required string Label { get; set; }
    public List<Choice>? Choices { get; set; }
    public bool EnableOtherOption { get; set; } = false;
    public bool IsMandatory { get; set; } = false;
    public bool? IsHidden { get; set; }
    public bool? IsInternal { get; set; }
    public int? MaxChoices { get; set; }
    public QuestionType QuestionType { get; set; }
    public InputType InputType { get; set; }
    public string QuestionTypeText { get => QuestionType.ToString();}
    public string InputTypeText { get => InputType.ToString(); }
}
