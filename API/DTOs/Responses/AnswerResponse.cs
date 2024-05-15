using API.Entities;
using API.Enums;

namespace API.DTOs.Responses;

public class AnswerResponse
{
    public required Guid QuestionId { get; set; }
    public InputType InputType { get; set; }
    public string InputTypeText { get => InputType.ToString(); }
    public string? Value { get; set; }
    public List<Choice>? Choices { get; set; }
}
