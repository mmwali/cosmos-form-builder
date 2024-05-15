using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationsController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public ApplicationsController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpPost("{programId}")]
    public async Task<ActionResult<BaseResponse>> CreateAsync(Guid programId, CreateApplicationRequest request)

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
        var response = await _applicationService.CreateApplicationAsync(programId, request);
        return response.Status ? Ok(response) : StatusCode(500, response);
    }
}
