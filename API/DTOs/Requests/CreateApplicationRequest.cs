namespace API.DTOs.Requests;

public class CreateApplicationRequest
{
    public required Guid ProgramId { get; set; }
    public required List<AnswerRequest> Answers { get; set; }
}
