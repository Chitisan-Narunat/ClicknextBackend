using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kanbanboardAPI.Models;
using kanbanboardAPI.Data;
using kanbanboardAPI.Register;
using kanbanboardAPI.Login;

namespace kanbanboardAPI.Controllers{  //requirement 1
    [ApiController, Route("api/[controller]")]
    public class AuthenController : ControllerBase {
        private readonly AppDbContext _context;
        public AuthenController(AppDbContext context){
            _context = context;
        }
        

        [HttpPost("Register")] //requirement 1 สมัคร
        public async Task<IActionResult> Register(RegisterRQ dto){
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password)){
                return BadRequest("Username, Email and Password are required");
            }
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest("Email already exists");
            }

            var user = new User{
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return Ok(new { message = "Register successed" });
        }

        [HttpPost("Login")]  //requirement 1 ล็อคอิน
        public async Task<IActionResult> Login(LoginRQ dto){
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password)){
                return BadRequest("Email and Password are required");
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid Email or Password" });
            }
            
            return Ok(new{message = "Login Successful" , user = new{
                user.Id,
                user.Username,
                user.Email,
            }});
        }
    }
}