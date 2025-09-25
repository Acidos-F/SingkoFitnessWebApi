using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SingkoFItnessWebApi.Dtos;
using SingkoFItnessWebApi.Models;

namespace SingkoFItnessWebApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProgressLogsController : ControllerBase {
        private readonly SingkoFitnessWebDbContext _context;
        private readonly IMapper _mapper;

        public ProgressLogsController(SingkoFitnessWebDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        /// <summary>
        /// Gets all progress logs as a list of DTOs. 
        /// </summary>
        public async Task<ActionResult<IEnumerable<ProgressLogsReadDto>>> GetProgressLogs() {
            var logs = await _context.ProgressLogs.ToListAsync();
            return _mapper.Map<List<ProgressLogsReadDto>>(logs);
        }

        [HttpGet("{id}")]
        /// <summary>
        /// Gets a single progress log by ID. 
        /// </summary>
        public async Task<ActionResult<ProgressLogsReadDto>> GetProgressLog(int id) {
            var log = await _context.ProgressLogs.FindAsync(id);
            if (log == null) return NotFound();

            return _mapper.Map<ProgressLogsReadDto>(log);
        }

        [HttpPost]
        /// <summary>
        /// Creates a new progress log if one for the same user and date does not exist. 
        /// </summary>
        public async Task<IActionResult> CreateProgressLog(ProgressLogsCreateDto dto) {
            // Check if a progress log for this user/date already exists
            var existing = await _context.ProgressLogs
                .FirstOrDefaultAsync(p => p.UserId == dto.UserId && p.Date == dto.Date);
            if (existing != null)
                return BadRequest("Progress log for this date already exists.");

            var log = _mapper.Map<ProgressLog>(dto);

            _context.ProgressLogs.Add(log);
            await _context.SaveChangesAsync();

            var readDto = _mapper.Map<ProgressLogsReadDto>(log);

            return CreatedAtAction(nameof(GetProgressLog), new { id = log.ProgressId }, readDto);
        }

        [HttpPut("{id}")]
        /// <summary>
        /// Updates an existing progress log by ID. 
        /// </summary>
        public async Task<IActionResult> UpdateProgressLog(int id, ProgressLogsUpdateDto dto) {
            var log = await _context.ProgressLogs.FindAsync(id);
            if (log == null) return NotFound();

            _mapper.Map(dto, log);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        /// <summary>
        /// Deletes a progress log by ID. 
        /// </summary>
        public async Task<IActionResult> DeleteProgressLog(int id) {
            var log = await _context.ProgressLogs.FindAsync(id);
            if (log == null) return NotFound();

            _context.ProgressLogs.Remove(log);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}