using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    public UsersController(IUserService service) => _service = service;

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string name)
        => Ok(await _service.SearchAsync(name));
}