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
        public async Task<ActionResult<IEnumerable<UsersReadDto>>> getUsers()
        {
            // var Users = await _context.Users.ToListAsync();
            var Users = await _context.Users
                .Include(u => u.Role) // join Roles
                .ToListAsync();
            return _mapper.Map<List<UsersReadDto>>(Users);
        }

        [HttpGet("UserId")]
        public async Task<IActionResult> getUsers(int UserId)
        {
            var user = await _context.Users
                                     .Include(u => u.Role)
                                     .SingleOrDefaultAsync(u => u.UserId == UserId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UsersReadDto>(user));
        }

        [HttpPost]
        public async Task<ActionResult<UsersReadDto>> Register(UsersCreateDto dto)
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

            return CreatedAtAction(nameof(getUsers), new { id = user.UserId }, userReadDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSingkobank(int UserId, UsersUpdateDto Dto)
        {
            var User = await _context.Users.FindAsync(UserId);
            if (User == null)
            {
                return NotFound();
            }
            _mapper.Map(Dto, User);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSingkobank(int UserId)
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