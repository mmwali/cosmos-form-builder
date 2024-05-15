namespace API.Entities;

public class Application
{
    public required Guid ProgramId { get; set; }
    public List<Answer>? Answers { get; set; }
}

