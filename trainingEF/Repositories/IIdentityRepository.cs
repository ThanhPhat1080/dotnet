using trainingEF.Models;
using trainingEF.Models.DTOs;

namespace trainingEF.Repositories
{
    public interface IIdentityRepository
    {
        Task<AuthResult> Register(UserRegistrationRequestDto user);
        Task<AuthResult> Login(UserLoginRequestDto user);
        Task<AuthResult> AddRoleAdmin(string userId);
    }
}
