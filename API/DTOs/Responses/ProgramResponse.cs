namespace API.DTOs.Responses;

public class ProgramResponse
{
    public  Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public List<QuestionResponse>? Questions { get; set; }

}
