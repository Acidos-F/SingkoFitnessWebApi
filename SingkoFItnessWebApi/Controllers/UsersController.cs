using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SingkoFItnessWebApi.Dtos.UsersDto;
using SingkoFItnessWebApi.Models;

namespace SingkoFItnessWebApi.Controllers
{
    /// <summary>
    /// Controller responsible for managing user accounts within the Singko Fitness system.
    /// Provides CRUD operations such as retrieving, creating, updating, and deleting users.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly SingkoFitnessWebDbContext _context;
        private readonly IMapper _mapper;

        public UsersController(SingkoFitnessWebDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all registered users along with their assigned roles.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersReadDto>>> GetUsers()
        {
            var Users = await _context.Users
                .Include(u => u.Role)
                .ToListAsync();

            return _mapper.Map<List<UsersReadDto>>(Users);
        }

        /// <summary>
        /// Retrieves a specific user by their unique ID.
        /// </summary>
        [HttpGet("UserId")]
        public async Task<ActionResult<UsersReadDto>> GetUserById(int UserId)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == UserId);

            if (user == null)
                return NotFound();

            return _mapper.Map<UsersReadDto>(user);
        }

        /// <summary>
        /// Creates a new user account and stores it in the database.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostUser(UsersCreateDto dto)
        {
            // 1. Check if email already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser != null)
                return BadRequest("Email already registered.");

            // 2. Map DTO to entity
            var user = _mapper.Map<User>(dto);

            // 3. Hash and store password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // 4. Save user to database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // 5. Map to read DTO to exclude sensitive data
            var userReadDto = _mapper.Map<UsersReadDto>(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, userReadDto);
        }

        /// <summary>
        /// Updates an existing user’s information based on their ID.
        /// </summary>
        [HttpPut("UserId")]
        public async Task<IActionResult> UpdateUser(int UserId, UsersUpdateDto Dto)
        {
            var User = await _context.Users.FindAsync(UserId);
            if (User == null)
                return NotFound();

            _mapper.Map(Dto, User);

            if (!string.IsNullOrEmpty(Dto.Password))
            {
                User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(Dto.Password);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Deletes a user account from the database.
        /// </summary>
        [HttpDelete("UserId")]
        public async Task<IActionResult> DeleteUser(int UserId)
        {
            var User = await _context.Users.FindAsync(UserId);
            if (User == null)
                return NotFound();

            _context.Users.Remove(User);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
