using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using trainingEF.Configuration;
using trainingEF.Entities;
using trainingEF.Models;
using trainingEF.Models.DTOs;
using trainingEF.Repositories;

namespace trainingEF.Controllers;

[Route("api/[controller]")] // api/authentication
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly string secretTokenKey;
    private readonly IIdentityRepository _identityRepository;

    public AuthenticationController(
        UserManager<IdentityUser> userManager,
        IIdentityRepository identityRepository,
        IConfiguration configuration)
    {

        _userManager = userManager;
        secretTokenKey = configuration.GetSection("JwtConfig:Secret").Value;

        _identityRepository = identityRepository;
    }

    [HttpPost]
    [Route("Register")] // api/authentication/register
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
    {
        // Validate the incoming request
        if (ModelState.IsValid)
        {
            AuthResult result = await _identityRepository.Register(requestDto);

            if (result.Result)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        else
        {
            return BadRequest(new AuthResult()
            {
                Result = false,
                Errors = new List<string>()
                {
                    "Invalid input!"
                }
            });
        }
    }

    [HttpPost]
    [Route("Login")] // api/authentication/login
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto requestDto)
    {
        try
        {
            // Validate the incoming request
            if (ModelState.IsValid)
            {
                // Check if the email already exist
                var user_exist = await _userManager.FindByEmailAsync(requestDto.Email);

                if (user_exist == null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Email does not exist!"
                        }
                    });
                }

                bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user_exist, requestDto.Password);

                if (isPasswordCorrect)
                {
                    // Generate the token
                    var token = GenerateJwtToken(user_exist);

                    return Ok(new AuthResult()
                    {
                        Result = true,
                        Token = token,
                    });
                }
            }

            return BadRequest(new AuthResult()
            {
                Errors = new List<string>()
                {
                    "Email or password is not correct!"
                },
                Result = false
            });

        }
        catch (Exception)
        {
            return BadRequest(new AuthResult()
            {
                Errors = new List<string>()
                {
                    "Server error!"
                },
                Result = false
            });
        }
    }

    [HttpPost]
    [Route("Change-role-admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangeUserRoleToAdmin([FromBody] string userId)
    {
        try
        {
            // Validate the incoming request
            if (ModelState.IsValid)
            {
                // Check if the email already exist
                var user_exist = await _userManager.FindByIdAsync(userId);

                if (user_exist == null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "User does not exist!"
                        }
                    });
                }
                var userRoles = _userManager.GetRolesAsync(user_exist).Result;

                if (!userRoles.Contains(Roles.Admin.ToString())) 
                {
                    await _userManager.RemoveFromRoleAsync(user_exist, Roles.User.ToString());
                    await _userManager.AddToRoleAsync(user_exist, Roles.Admin.ToString());
                    await _userManager.UpdateAsync(user_exist);

                    return Ok(user_exist);
                }
            }

            return BadRequest(new AuthResult()
            {
                Errors = new List<string>()
                {
                    "Cannot update user role!"
                },
                Result = false
            });

        }
        catch (Exception)
        {
            return BadRequest(new AuthResult()
            {
                Errors = new List<string>()
                {
                    "Cannot update user role!"
                },
                Result = false
            });
        }
    }

    private async Task<List<Claim>> CreateClaimAsync(IdentityUser user)
    {
        List<Claim> claims = new()
        {
            new(type: "Id", value: user.Id),
            new(type: JwtRegisteredClaimNames.Sub, value: user.Email),
            new(type: JwtRegisteredClaimNames.Email, value: user.Email),
            new(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new(type: JwtRegisteredClaimNames.Iat, value: DateTime.Now.ToUniversalTime().ToString())
        };

        var userRoles = await _userManager.GetRolesAsync(user);

        claims.AddRange(userRoles.Select(x => new Claim(type: ClaimTypes.Role, value: x)));

        return claims;
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.UTF8.GetBytes(secretTokenKey);

        var claims = CreateClaimAsync(user).Result;

        // Token descriptor. (JWT payload)
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            
            Expires = DateTime.Now.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);

        return jwtToken;
    }
}
