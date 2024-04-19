using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
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
    private readonly IIdentityRepository _identityRepository;

    public AuthenticationController(
        UserManager<IdentityUser> userManager,
        IIdentityRepository identityRepository,
        IConfiguration configuration)
    {
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
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto userDto)
    {
        try
        {
            // Validate the incoming request
            if (ModelState.IsValid)
            {
                // Check if the email already exist
                AuthResult result = await _identityRepository.Login(userDto);

                if (result.Result)
                {
                    return Ok(result);
                }
            }

            return BadRequest(new AuthResult()
            {
                Errors = new List<string>()
                {
                    "Email or password is not correct!"
                }
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
    [Route("add-role-admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddRoleAdmin([FromBody] string userId)
    {
        // Validate the incoming request
        if (ModelState.IsValid)
        {
            var result = await _identityRepository.AddRoleAdmin(userId);

            if (result.Result)
            {
                return Ok(result);
            }
        }

        return BadRequest(new AuthResult()
        {
            Errors = new List<string>()
            {
                "Cannot update user role!"
            },
        });
    }
}
