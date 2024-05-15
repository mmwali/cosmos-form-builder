using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProgramsController : ControllerBase
{
    private readonly IProgramService _programService;

    public ProgramsController(IProgramService programService)
    {
        _programService = programService;
    }

    [HttpPost]
    public async Task<ActionResult<DataResponse<Guid>>> CreateAsync(CreateProgramRequest request)
    
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new DataResponse<Guid>()
            {
                Message = string.Join(" | ", ModelState.Values
                   .SelectMany(v => v.Errors)
                   .Select(e => e.ErrorMessage)),
                Status = false
            });
        }

        var response = await _programService.CreateProgramAsync(request);
        return response.Status? Ok(response) : StatusCode(500, response);
    }

    [HttpPost("{programId}")]
    public async Task<ActionResult<BaseResponse>> CreateQuestionAsync(Guid programId, QuestionRequest request)
    
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new BaseResponse()
            {
                Message = string.Join(" | ", ModelState.Values
                   .SelectMany(v => v.Errors)
                   .Select(e => e.ErrorMessage)),
                Status = false
            });
        }

        var response = await _programService.CreateQuestionAsync(programId, request);
        return response.Status? Ok(response) : StatusCode(500, response);
    }

    [HttpPut("{questionId}")]
    public async Task<ActionResult<BaseResponse>> UpdateQuestionAsync(Guid questionId, QuestionRequest request)
    
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new BaseResponse()
            {
                Message = string.Join(" | ", ModelState.Values
                   .SelectMany(v => v.Errors)
                   .Select(e => e.ErrorMessage)),
                Status = false
            });
        }

        var response = await _programService.UpdateQuestionAsync(questionId, request);
        return response.Status? Ok(response) : StatusCode(500, response);
    }

    [HttpDelete("{questionId}")]
    public async Task<ActionResult<BaseResponse>> DeleteQuestionAsync(Guid questionId)
    
    {
        var response = await _programService.DeleteQuestionAsync(questionId);
        return response.Status? Ok(response) : StatusCode(500, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse>> GetProgramAsync(Guid id)
    
    {
        var response = await _programService.ViewProgramAsync(id);
        return response.Status? Ok(response) : StatusCode(500, response);
    }

}
