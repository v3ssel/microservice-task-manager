using Microsoft.AspNetCore.Mvc;

namespace tasks.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ILogger<TasksController> _logger;

    public TasksController(ILogger<TasksController> logger)
    {
        _logger = logger;
    }

    [HttpGet("create")]
    public async Task<IActionResult> CreateTask()
    {
        return Ok();
    }
}
