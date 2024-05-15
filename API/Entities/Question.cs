using API.Enums;

namespace API.Entities;

public class Question : BaseEntity
{
    public required Guid ProgramId { get; set; }
    public required string Label { get; set; }
    public List<Choice>? Choices { get; set; }
    public bool EnableOtherOption { get; set; } = false;
    public bool IsMandatory { get; set; } = false;
    public bool? IsHidden { get; set; }
    public bool? IsInternal { get; set; }
    public int? MaxChoices { get; set; }
    public QuestionType QuestionType { get; set; } = QuestionType.Additional;
    public InputType InputType { get; set; }
}

