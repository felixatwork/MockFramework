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
    public IActionResult Get()
    {
        // Your logic here
        return Ok("Hello, World!");
    }

    [HttpPost]
    public async Task<IActionResult> GetMock([FromBody] JsonElement payload) {
        
    // Get request path
        string path = HttpContext.Request.Path;

        // Call DB to get the mock response
        MockResponses responses = await _mockRepository.GetMockAsync(path, payload.ToString());

        // Use the response object
        if (responses != null)
        {
            if (responses.DelayTime > 0 ) {
                Thread.Sleep(responses.DelayTime);
            }

            return Ok(responses); // return the mock response
        }

        return NotFound("Mock response not found");

    }
}