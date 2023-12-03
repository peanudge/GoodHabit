namespace GoodHabits.HabitService.Controllers;

using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class HabitController : ControllerBase
{
    private ILogger<HabitController> _logger;
    private readonly IHabitService _habitService;

    public HabitController(
        ILogger<HabitController> logger,
        IHabitService habitService
        )
    {
        _logger = logger;
        _habitService = habitService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var habit = await _habitService.GetById(id);

        if (habit is null)
        {
            return Problem("Not found habit by id");
        }

        return Ok(habit);
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync() => Ok(await _habitService.GetAll());

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateHabitDto request) => Ok(await _habitService.Create(request.Name, request.Description));
}
