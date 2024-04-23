using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trainingEF.Models.DTOs;
using trainingEF.Repositories;
namespace trainingEF.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly IIdentityRepository userDtoRepository;

    public UserController(IIdentityRepository _userDtoRepository)
    {
        userDtoRepository = _userDtoRepository;
    }

    [HttpGet("all-users")]
    public IActionResult Index()
    {
        return Ok(userDtoRepository.GetAllUsers());
    }

    [HttpGet("{email}")]
    [ActionName("GetUserByEmail")]
    public async Task<IActionResult>? GetUserByEmail(string email)
    {
        return Ok(await userDtoRepository.GetUserByEmail(email));
    }
   
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, UserDto user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        UserDto? updatedUser = await userDtoRepository.UpdateUser(user);

        return updatedUser != null ? Ok(updatedUser) : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        bool isDeleted = await userDtoRepository.DeleteUser(id);

        return isDeleted ? Ok() : BadRequest();
    }
}
