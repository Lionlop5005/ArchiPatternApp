using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserSearchService _service;
    public UsersController(UserSearchService service) => _service = service;

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string name)
        => Ok(await _service.Search(name));
}