using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trainingEF.Data;
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

        public UserModel? GetUserById(int id)
        {
            return context.UserDbSet.Find(id);
        }

        public async Task<ActionResult<UserModel>> CreateUser(UserModel user)
        {
            context.UserDbSet.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UpdateUser(UserModel user)
        {
            UserModel? foundUser = await context.UserDbSet.FindAsync(user.Id);
            if (foundUser == null)
            {
                return false;
            }

            foundUser.Name = user.Name;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            UserModel? foundUser = await context.UserDbSet.FindAsync(id);
            if (foundUser == null)
            {
                return false;
            }

            try
            {
                context.UserDbSet.Remove(foundUser);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }
    }
}
