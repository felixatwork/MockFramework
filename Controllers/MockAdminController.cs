using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MockFramework.Models;
using MockFramework.Services;

namespace MockFramework.Controllers;

[Route("api/mock/admin")]
[ApiController]
public class MockAdminController : Controller {

    private readonly MockRepository _mockRepository;

    public MockAdminController(MockRepository mockRepository)
    {
        _mockRepository = mockRepository;
    }

    [HttpPost("create")]
    //Create
    public IActionResult CreateMock([FromBody] MockCreateDto mockCreateDto)
    {
        _mockRepository.InsertMockAsync(mockCreateDto);
        return Ok();
    }

    //Update
    [HttpPatch("update")]
    public IActionResult UpdateMock(string requestPath, JsonPatchDocument<MockResponse> patchDoc)
    {
        var result = _mockRepository.GetMockAsync(requestPath);

        if (result.Result.Payload != null) {
            patchDoc.ApplyTo(result.Result);

            return Ok();
        }

        return NotFound();
    }

    [HttpDelete("remove")]
    public IActionResult RemoveMock(string requestPath)
    {
        _mockRepository.DeleteMockAsync(requestPath);

        return Ok();
    }
}