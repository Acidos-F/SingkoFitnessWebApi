using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SingkoFitnessWebApi.Dtos.Exercise;
using SingkoFItnessWebApi.Models;

namespace SingkoFitnessWebApi.Controllers
{
    /// <summary>
    /// API controller for managing exercise resources.
    /// Provides endpoints to create, read, update, and delete exercises.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly SingkoFitnessWebDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExerciseController"/> class.
        /// </summary>
        /// <param name="context">Database context for accessing exercise data.</param>
        /// <param name="mapper">AutoMapper instance for DTO mapping.</param>
        public ExerciseController(SingkoFitnessWebDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all exercises from the database.
        /// </summary>
        /// <returns>A list of <see cref="ExerciseReadDto"/> objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseReadDto>>> GetExercises()
        {
            var exercises = await _context.Exercises.ToListAsync();
            return _mapper.Map<List<ExerciseReadDto>>(exercises);
        }

        /// <summary>
        /// Retrieves a specific exercise by its ID.
        /// </summary>
        /// <param name="id">The ID of the exercise to retrieve.</param>
        /// <returns>An <see cref="ExerciseReadDto"/> object if found; otherwise, 404 Not Found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseReadDto>> GetExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
                return NotFound();

            return _mapper.Map<ExerciseReadDto>(exercise);
        }

        /// <summary>
        /// Creates a new exercise in the database.
        /// </summary>
        /// <param name="dto">The <see cref="ExerciseCreateDto"/> containing exercise data.</param>
        /// <returns>The created <see cref="ExerciseReadDto"/> with a 201 Created response.</returns>
        [HttpPost]
        public async Task<ActionResult<ExerciseReadDto>> CreateExercise(ExerciseCreateDto dto)
        {
            var exercise = _mapper.Map<Exercise>(dto);
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();

            var readDto = _mapper.Map<ExerciseReadDto>(exercise);
            return CreatedAtAction(nameof(GetExercise), new { id = exercise.ExerciseId }, readDto);
        }

        /// <summary>
        /// Updates an existing exercise by its ID.
        /// </summary>
        /// <param name="id">The ID of the exercise to update.</param>
        /// <param name="dto">The <see cref="ExerciseUpdateDto"/> containing updated data.</param>
        /// <returns>204 No Content if successful; otherwise, 404 Not Found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, ExerciseUpdateDto dto)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
                return NotFound();

            _mapper.Map(dto, exercise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes an exercise by its ID.
        /// </summary>
        /// <param name="id">The ID of the exercise to delete.</param>
        /// <returns>204 No Content if successful; otherwise, 404 Not Found.</returns>
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