using Microsoft.AspNetCore.Mvc;

namespace trainingEF.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<UserModel> GetAllUsers();
        public UserModel? GetUserById(int id);
        public Task<ActionResult<UserModel>> CreateUser(UserModel user);
        public Task<bool> UpdateUser(UserModel user);
        public Task<bool> DeleteUser(int id);
    }
}
