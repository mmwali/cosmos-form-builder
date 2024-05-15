using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

}
public class Program : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public List<Question>? Questions { get; set; }
}

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

public class Choice: BaseEntity
{
    public required string Name { get; set; }
}

public enum InputType
{
    Paragraph = 0,
    YesNo = 1,
    Dropdown = 2,
    MultipleChoice = 3,
    Date = 4,
    Number = 5
}
public enum QuestionType
{
    Personal = 0,
    Additional = 1,
}
public enum ResponseCodes
{
    NoData,
    Success
}
