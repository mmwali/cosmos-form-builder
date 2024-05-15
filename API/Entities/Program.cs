using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Entities;
public class Program : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public List<Question>? Questions { get; set; }
}

