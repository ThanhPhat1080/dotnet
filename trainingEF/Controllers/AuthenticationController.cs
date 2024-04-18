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

namespace trainingEF.Controllers;

[Route("api/[controller]")] // api/authentication
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly string secretTokenKey;

    public AuthenticationController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {

        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        secretTokenKey = configuration.GetSection("JwtConfig:Secret").Value;
    }

    [HttpPost]
    [Route("Register")] // api/authentication/register
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
    {
        // Validate the incoming request
        if (ModelState.IsValid)
        {
            // Check if the email already exist
            var user_exist = await _userManager.FindByEmailAsync(requestDto.Email);

            if (user_exist != null)
            {
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Email already exist!"
                    }
                });
            }

            var new_user = new IdentityUser()
            {
                Email = requestDto.Email,
                UserName = requestDto.Name
            };

            var is_created = await _userManager.CreateAsync(new_user, requestDto.Password);

            if (is_created.Succeeded)
            {
                // Generate the token
                var token = GenerateJwtToken(new_user);
                var assignRoleResult = await _userManager.AddToRoleAsync(new_user, Roles.User.ToString());

                if (!assignRoleResult.Succeeded)
                {
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>()
                        {
                            "Cannot assign user role!"
                        },
                        Result = false
                    });
                }

                return Ok(new AuthResult()
                {
                    Result = true,
                    Token = token,
                });
            }

            return BadRequest(new AuthResult()
            {
                Errors = new List<string>()
                {
                    "Server error!"
                },
                Result = false
            });
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("Login")] // api/authentication/login
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
