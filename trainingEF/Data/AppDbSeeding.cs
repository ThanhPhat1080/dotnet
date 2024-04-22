using Microsoft.AspNetCore.Identity;
using trainingEF.Entities;
using trainingEF.Models.DTOs;

namespace trainingEF.Data;

public class AppDbSeeding
{
    public static async Task SeedingData(
        RoleManager<IdentityRole> roleManager,
        UserManager<UserDto> userManager,
        AppDbContext appDb,
        IConfiguration configuration)
    {

        await using var transaction = await appDb.Database.BeginTransactionAsync();

        try
        {
            #region Seeding Role Data
            if (!await roleManager.RoleExistsAsync(Roles.Admin.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            }
            if (!await roleManager.RoleExistsAsync(Roles.User.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
            }
            #endregion

            #region Seeding Admin user
            var userExist = await userManager.FindByEmailAsync("admin@admin.com");

            if (userExist == null)
            {
                var newUser = new UserDto()
                {
                    Email = configuration.GetSection("SeedingData:DefaultAdminEmail").Value,
                    UserName = "AdminDefault"
                };

                // TODO: add pass to appSetting key
                await userManager.CreateAsync(newUser, configuration.GetSection("SeedingData:DefaultAdminPassword").Value);
                await userManager.AddToRoleAsync(newUser, Roles.Admin.ToString());

                await transaction.CommitAsync();
            }
            #endregion

        } catch (Exception)
        {
            await transaction.RollbackAsync();
            throw new Exception("Cannot add default data");
        }

    }
}
