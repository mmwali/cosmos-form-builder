namespace API.DTOs.Responses;

public class ApplicationResponse
{
    public  Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public List<AnswerResponse> Answers { get; set; }

}
