using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SingkoFItnessWebApi.Dtos;
using SingkoFItnessWebApi.Models;

namespace SingkoFItnessWebApi.Controllers
{
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersReadDto>>> GetUsers()
        {
            var Users = await _context.Users
                .Include(u => u.Role)
                .ToListAsync();
            return _mapper.Map<List<UsersReadDto>>(Users);
        }

        [HttpGet("UserId")]
        public async Task<ActionResult<UsersReadDto>> GetUserById(int UserId)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == UserId);
            if (user == null)
            {
                return NotFound();
            }
            return _mapper.Map<UsersReadDto>(user);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(UsersCreateDto dto)
        {
            // 1. Check if email exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser != null)
                return BadRequest("Email already registered.");

            // 2. Map dto -> entity
            var user = _mapper.Map<User>(dto);

            // 3. Hash password and assign it
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // 4. Save to DB
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // 5. Map to ReadDto (if you want to return user info without password)
            var userReadDto = _mapper.Map<UsersReadDto>(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, userReadDto);
        }

        [HttpPut("UserId")]
        public async Task<IActionResult> UpdateUser(int UserId, UsersUpdateDto Dto)
        {
            var User = await _context.Users.FindAsync(UserId);
            if (User == null)
            {
                return NotFound();
            }

            _mapper.Map(Dto, User);

            if (!string.IsNullOrEmpty(Dto.Password))
            {
                User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(Dto.Password);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("UserId")]
        public async Task<IActionResult> DeleteUser(int UserId)
        {
            var User = await _context.Users.FindAsync(UserId);
            if (User == null)
            {
                return NotFound();
            }

            _context.Users.Remove(User);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}