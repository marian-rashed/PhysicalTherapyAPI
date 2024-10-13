using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PhysicalTherapyAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PhysicalTherapyAPI.DTOs;
using System.Text;

namespace PhysicalTherapyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = registerDTO.Name,
                    PasswordHash = registerDTO.Password
                };
                IdentityResult result = await _userManager.CreateAsync(applicationUser, registerDTO.Password);

                if (result.Succeeded)
                {
                    return Created();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return BadRequest(ModelState);
        }


        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LoginDTO logInDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser? UserFromDB = await _userManager.FindByNameAsync(logInDTO.Username);
                if (UserFromDB != null)
                {
                    bool res = await _userManager.CheckPasswordAsync(UserFromDB, logInDTO.Password);
                    if (res)
                    {
                        //create token
                        List<Claim> claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name,UserFromDB.UserName),
                            new Claim (ClaimTypes.NameIdentifier,UserFromDB.Id),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        };

                        SymmetricSecurityKey mySignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes($"{_configuration["JWT:Secret"]}"));
                        SigningCredentials Credintials = new SigningCredentials(mySignKey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer: $"{_configuration["JWT:ValidIssuer"]}",
                            audience: $"{_configuration["JWT:ValidAudience"]}",
                            expires: DateTime.Now.AddDays(1),
                            claims: claims,
                            signingCredentials: Credintials
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(myToken),
                            expiredate = myToken.ValidTo
                        });

                    }
                    return BadRequest("Invalid Password");

                }
                return BadRequest("Invalid user");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("GenerateResetToken")]
        public async Task<IActionResult> GenerateResetToken(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("Invalid username.");
            }
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            return Ok(new { Token = resetToken });
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _userManager.FindByNameAsync(resetPasswordDTO.Username);
            if (user == null)
            {
                return BadRequest("Invalid username.");
            }

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.Token, resetPasswordDTO.NewPassword);

            if (resetPassResult.Succeeded)
            {
                return Ok("Password reset successful.");
            }

            foreach (var error in resetPassResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

    }
}
