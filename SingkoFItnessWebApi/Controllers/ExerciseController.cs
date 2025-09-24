using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SingkoFitnessWebApi.Dtos.Exercise;
using SingkoFItnessWebApi.Models;

namespace SingkoFitnessWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly SingkoFitnessWebDbContext _context;
        private readonly IMapper _mapper;

        public ExerciseController(SingkoFitnessWebDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseReadDto>>> GetExercises()
        {
            var exercises = await _context.Exercises.ToListAsync();
            return _mapper.Map<List<ExerciseReadDto>>(exercises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseReadDto>> GetExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
                return NotFound();

            return _mapper.Map<ExerciseReadDto>(exercise);
        }

        [HttpPost]
        public async Task<ActionResult<ExerciseReadDto>> CreateExercise(ExerciseCreateDto dto)
        {
            var exercise = _mapper.Map<Exercise>(dto);

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();

            var readDto = _mapper.Map<ExerciseReadDto>(exercise);

            return CreatedAtAction(nameof(GetExercise), new { id = exercise.ExerciseId }, readDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, ExerciseUpdateDto dto)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
                return NotFound();

            _mapper.Map(dto, exercise); // update fields

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
                return NotFound();

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}