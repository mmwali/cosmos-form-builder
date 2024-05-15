namespace API.DTOs.Requests;

public class PersonalQuestionRequest
{
    public bool IsInternal { get; set; } = false;
    public bool IsHidden { get; set; } = false;

}
