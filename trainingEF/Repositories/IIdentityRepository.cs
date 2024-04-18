using trainingEF.Models;
using trainingEF.Models.DTOs;

namespace trainingEF.Repositories
{
    public interface IIdentityRepository
    {
        public Task<AuthResult> Register(UserRegistrationRequestDto user);
    }
}
