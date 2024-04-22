using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trainingEF.Models;
using trainingEF.Models.DTOs;
using trainingEF.Repositories;

namespace trainingEF.Controllers;

[Route("api/[controller]")] // api/authentication
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IIdentityRepository identityRepository;

    public AuthenticationController(IIdentityRepository _identityRepository)
    {
        identityRepository = _identityRepository;
    }

    [HttpPost]
    [Route("Register")] // api/authentication/register
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
    {
        // Validate the incoming request
        if (ModelState.IsValid)
        {
            AuthResult result = await identityRepository.Register(requestDto);

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
                AuthResult result = await identityRepository.Login(userDto);

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
            var result = await identityRepository.AddRoleAdmin(userId);

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
