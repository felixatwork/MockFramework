using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MockFramework.Models;
using MockFramework.Services;

namespace MockFramework.Controllers;

[Route("api/mock")]
[ApiController]
public class MockController : Controller {

    private readonly MockRepository _mockRepository;

    public MockController(MockRepository mockRepository)
    {
        _mockRepository = mockRepository;
    }

    
    [HttpGet()]
    public async Task<IActionResult> GetAsync()
    {
       // Get request path 
       string path = HttpContext.Request.Path;

       return GetResultFromMockResponse(await _mockRepository.GetMockAsync(path));
    }

    [HttpPost]
    public async Task<IActionResult> GetMock([FromBody] JsonElement payload)
    {
        // Get request path
        string path = HttpContext.Request.Path;

        return GetResultFromMockResponse(await _mockRepository.GetMockAsync(path, payload.ToString()));
    }

    private IActionResult GetResultFromMockResponse(MockResponses responses)
    {
        if (responses != null)
        {
            if (responses.DelayTime > 0)
            {
                Thread.Sleep(responses.DelayTime);
            }

            return responses.HttpStatusCode switch
            {
                200 => Ok(responses.Payload),
                400 => BadRequest(),
                403 => Forbid(),
                404 => NotFound(),
                500 => StatusCode(500),
                _ => Ok()
            };
        }

        return NotFound("Mock response not found");
    }
}