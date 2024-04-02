using trainingEF.Models;

namespace trainingEF.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<UserModel> GetAllUsers();
    }
}
