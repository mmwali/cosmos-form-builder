using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

public class Answer : BaseEntity
{
    public required Guid QuestionId { get; set; }

    [ForeignKey(nameof(QuestionId))]
    public Question? Question { get; set; }
    public string? Value { get; set; }
    public List<Choice>? Choices { get; set; }
}

