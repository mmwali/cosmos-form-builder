namespace API.Entities;

public class Application: BaseEntity
{
    public required Guid ProgramId { get; set; }
    public List<Answer>? Answers { get; set; }
}

