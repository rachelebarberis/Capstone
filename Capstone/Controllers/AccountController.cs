using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Capstone.DTOs.Account;
using Capstone.Models;
using Capstone.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly Jwt _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(IOptions<Jwt> jwtOptions, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _jwtSettings = jwtOptions.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var newUser = new ApplicationUser()
            {
                Email = registerRequestDto.Email,
                UserName = registerRequestDto.Email,
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
            };

            var result = await _userManager.CreateAsync(newUser, registerRequestDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(newUser.Email);

            await _userManager.AddToRoleAsync(user, "User");

            return Ok();
        }
        [HttpGet("userinfo")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetUserInfo()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
                return Unauthorized();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("User"))
                return Forbid();

            var userInfo = new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.ImgUserPath
            };

            return Ok(userInfo);
        }

        [HttpPost("uploadprofileimage")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UploadProfileImage([FromForm] UploadImageDto dto)
        {
            var imageFile = dto.ImageFile;

            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("File non valido");

            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return Unauthorized();

            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "users");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
            var filePath = Path.Combine(uploadDir, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            user.ImgUserPath = $"/images/users/{uniqueFileName}";
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.ImgUserPath
            });
        }




        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);

            await _signInManager.PasswordSignInAsync(user, loginRequestDto.Password, false, false);

            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddMinutes(_jwtSettings.ExpiresInMinutes);

            var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claims, expires: expiry, signingCredentials: creds);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new TokenResponse()
            {
                Token = tokenString,
                Expires = expiry
            });
        }
    

    [HttpPost("updateuserinfo")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return Unauthorized();

            user.FirstName = dto.FirstName ?? user.FirstName;
            user.LastName = dto.LastName ?? user.LastName;
            user.Email = dto.Email ?? user.Email;
            user.UserName = dto.Email ?? user.Email;
            user.ImgUserPath = dto.ImgUserPath ?? user.ImgUserPath;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest("Errore durante l'aggiornamento del profilo.");

            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.ImgUserPath
            });
        }
    }
}
