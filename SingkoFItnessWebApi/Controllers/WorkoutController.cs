using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SingkoFItnessWebApi.Dtos;
using SingkoFItnessWebApi.Models;

namespace SingkoFItnessWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsController : ControllerBase
    {
        private readonly SingkoFitnessWebDbContext _context;
        private readonly IMapper _mapper;

        public WorkoutsController(SingkoFitnessWebDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// See all workouts with their exercises
        /// </summary>
        /// <returns>List of WorkoutReadDto objects</returns>
        // GET: api/workouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutReadDto>>> GetWorkouts()
        {
            var workouts = await _context.Workouts
                .Include(w => w.Exercises)
                .ToListAsync();

            return _mapper.Map<List<WorkoutReadDto>>(workouts);
        }

        /// <summary>
        /// See a specific workout by ID with its exercises
        /// </summary>
        /// <param name="WorkoutId">The ID of the workout to be shown</param>
        /// <returns>A specific list of WorkoutReadDto objects if found, otherwise, NotFound</returns>
        // GET: api/workouts/5
        [HttpGet("{WorkoutId}")]
        public async Task<ActionResult<WorkoutReadDto>> GetWorkoutById(int WorkoutId)
        {
            var workout = await _context.Workouts
                .Include(w => w.Exercises)
                .FirstOrDefaultAsync(w => w.WorkoutId == WorkoutId);

            if (workout == null)
            {
                return NotFound();
            }

            return _mapper.Map<WorkoutReadDto>(workout);
        }

        /// <summary>
        /// Creates a new workout and adds it to the database.
        /// </summary>
        /// <param name="dto">WorkoutCreateDto containing the details of the workout to create.</param>
        /// <returns>The created WorkoutReadDto</returns>
        // POST: api/workouts
        [HttpPost]
        public async Task<IActionResult> PostWorkout(WorkoutCreateDto dto)
        {
            var workout = _mapper.Map<Workout>(dto);

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            var workoutReadDto = _mapper.Map<WorkoutReadDto>(workout);

            return CreatedAtAction(nameof(GetWorkoutById), new { WorkoutId = workout.WorkoutId }, workoutReadDto);
        }

        /// <summary>
        /// Updates an existing workout by ID.
        /// </summary>
        /// <param name="WorkoutId">The ID of the workout to update.</param>
        /// <param name="dto">WorkoutUpdateDto containing updated workout details.</param>
        /// <returns>NoContent if updated successfully; otherwise, NotFound.</returns>
        // PUT: api/workouts/5
        [HttpPut("{WorkoutId}")]
        public async Task<IActionResult> UpdateWorkout(int WorkoutId, WorkoutUpdateDto dto)
        {
            var workout = await _context.Workouts
                .Include(w => w.Exercises)
                .FirstOrDefaultAsync(w => w.WorkoutId == WorkoutId);

            if (workout == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, workout);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Deletes a workout by ID.
        /// </summary>
        /// <param name="WorkoutId">The ID of the workout to delete.</param>
        /// <returns>NoContent if deleted successfully; otherwise, NotFound.</returns>
        // DELETE: api/workouts/5
        [HttpDelete("{WorkoutId}")]
        public async Task<IActionResult> DeleteWorkout(int WorkoutId)
        {
            var workout = await _context.Workouts.FindAsync(WorkoutId);
            if (workout == null)
            {
                return NotFound();
            }

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}