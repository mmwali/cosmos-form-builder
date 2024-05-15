using System.ComponentModel.DataAnnotations.Schema;

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
