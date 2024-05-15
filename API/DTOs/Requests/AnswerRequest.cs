using API.Entities;

namespace API.DTOs.Requests;

public class AnswerRequest
{
    public required Guid QuestionId { get; set; }
    public string? Value { get; set; }
    public List<Choice>? Choices { get; set; }
}
