using API.Entities;

namespace API.DTOs.Requests;

public class QuestionRequest
{
    public required string Label { get; set; }
    public IEnumerable<string>? Choices { get; set; }
    public bool EnableOtherOption { get; set; } = false;
    public int? MaxChoices { get; set; }
    public required InputType QuestionType { get; set; }
}
