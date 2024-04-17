using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trainingEF.Repositories;
namespace trainingEF.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public UserController(IUserRepository _userRepository)
    {
        userRepository = _userRepository;
    }

    [HttpGet("All-users")]
    //[Authorize(Roles = "User")]
    //[AllowAnonymous]
    public IActionResult Index()
    {
        return Ok(userRepository.GetAllUsers().ToList());    
    }

    [HttpGet("{id}")]
    [ActionName("GetUserById")]
    //[Authorize(Roles = "Admin")]
    public UserModel? GetUserById(int id)
    {
        return userRepository.GetUserById(id);
    }

    [HttpPost]
    public async Task<ActionResult<UserModel>> Create(UserModel user)
    {
        await userRepository.CreateUser(user);

        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Edit(int id, UserModel user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        bool isUpdated = await userRepository.UpdateUser(user);

        return isUpdated ? Ok() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        bool isDeleted = await userRepository.DeleteUser(id);

        return isDeleted ? Ok() : BadRequest();
    }
}
