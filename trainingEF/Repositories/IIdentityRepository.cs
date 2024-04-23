using trainingEF.Models;
using trainingEF.Models.DTOs;

namespace trainingEF.Repositories;

public interface IIdentityRepository
{
    #region Authentication
    Task<AuthResult> Register(UserRegistrationRequestDto user);
    Task<AuthResult> Login(UserLoginRequestDto user);
    Task<AuthResult> AddRoleAdmin(string userId);
    #endregion

    #region User
    public IEnumerable<UserDto> GetAllUsers();
    public Task<UserDto?> GetUserByEmail(string email);
    public Task<UserDto?> GetUserById(string id);
    public Task<UserDto?> UpdateUser(UserDto user);
    public Task<bool> DeleteUser(string id);
    #endregion
}
