using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trainingEF.Repositories;
namespace trainingEF.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : Controller
{
    private readonly IUserRepository userRepository;

    public UserController(IUserRepository _userRepository)
    {
        userRepository = _userRepository;
    }

    [HttpGet("")]
    [ActionName("GetAllUsers")]
    public IEnumerable<UserModel> Index()
    {
        return userRepository.GetAllUsers();
    }

    [HttpGet("{id}")]
    [ActionName("GetUserById")]
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
