using API.Entities;
using API.Enums;

namespace API.DTOs.Responses;

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
