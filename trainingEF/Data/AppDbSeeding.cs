using Microsoft.AspNetCore.Identity;
using trainingEF.Entities;
using trainingEF.Models;

namespace trainingEF.Data;

public class AppDbSeeding
{
    public static async Task SeedingData(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
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
            var newUser = new IdentityUser()
            {
                Email = "admin@admin.com",
                UserName = "AdminDefault"
            };

            // TODO: add pass to appSetting key
            await userManager.CreateAsync(newUser, "Admin@123");
            await userManager.AddToRoleAsync(newUser, Roles.Admin.ToString());
        }
        #endregion

    }
}
