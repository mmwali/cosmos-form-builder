using API.Entities;

namespace API.DTOs.Requests;

public class CreateProgramRequest
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required PersonalQuestionRequest Phone { get; set; }
    public required PersonalQuestionRequest Nationality { get; set; }
    public required PersonalQuestionRequest Country { get; set; }
    public required PersonalQuestionRequest IdNumber { get; set; }
    public required PersonalQuestionRequest DateOfBirth { get; set; }
    public required PersonalQuestionRequest Gender { get; set; }

}
public class PersonalQuestionRequest
{
    public bool IsInternal { get; set; } = false;
    public bool IsHidden { get; set; } = false;

}
public class QuestionRequest
{
    public required string Label { get; set; }
    public IEnumerable<string>? Choices { get; set; }
    public bool EnableOtherOption { get; set; } = false;
    public int? MaxChoices { get; set; }
    public required InputType QuestionType { get; set; }
}
