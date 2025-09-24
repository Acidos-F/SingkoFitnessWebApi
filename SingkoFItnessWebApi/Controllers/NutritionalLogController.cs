using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SingkoFItnessWebApi.Dtos;
using SingkoFItnessWebApi.Dtos.NutrionalLogDto;
using SingkoFItnessWebApi.Models;


namespace SingkoFItnessWebApi.Controllers
{
    public class NutritionalLogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
[ApiController]
[Route("api/[controller]")]
public class NutritionLogsController : ControllerBase
{
    private readonly SingkoFitnessWebDbContext _context;
    private readonly IMapper _mapper;

    public NutritionLogsController(SingkoFitnessWebDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: Whole Nutritional Log
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NutritionLogReadDto>>> GetNutritionLogs()
    {
        var logs = await _context.NutritionLogs.ToListAsync();
        return _mapper.Map<List<NutritionLogReadDto>>(logs);
    }

    // GET: Nutritional Log by Id
    [HttpGet("{id}")]
    public async Task<ActionResult<NutritionLogReadDto>> GetNutritionLog(int id)
    {
        var log = await _context.NutritionLogs.FindAsync(id);
        if (log == null) return NotFound();

        return _mapper.Map<NutritionLogReadDto>(log);
    }

    // POST: Create Nutritional Log
    [HttpPost]
    public async Task<IActionResult> CreateNutritionLog(NutritionLogCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Optional: Prevent duplicate meal log for same date/user
        var existing = await _context.NutritionLogs
            .FirstOrDefaultAsync(n => n.Date == dto.Date && n.MealType == dto.MealType);
        if (existing != null)
            return BadRequest("Nutrition log for this meal already exists.");

        var log = _mapper.Map<NutritionLog>(dto);

        _context.NutritionLogs.Add(log);
        await _context.SaveChangesAsync();

        var readDto = _mapper.Map<NutritionLogReadDto>(log);

        return CreatedAtAction(nameof(GetNutritionLog), new { id = log.NutritionId }, readDto);
    }

    // PUT: Update nutritional Log
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNutritionLog(int id, NutritionLogUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var log = await _context.NutritionLogs.FindAsync(id);
        if (log == null) return NotFound();

        _mapper.Map(dto, log);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: Delete Nutritional Log
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNutritionLog(int id)
    {
        var log = await _context.NutritionLogs.FindAsync(id);
        if (log == null) return NotFound();

        _context.NutritionLogs.Remove(log);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

