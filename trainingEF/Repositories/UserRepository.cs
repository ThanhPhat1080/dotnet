using trainingEF.Models;

namespace trainingEF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return context.UserDbSet.ToList();
        }
    }
}
