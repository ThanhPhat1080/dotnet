using Microsoft.AspNetCore.Identity;
using trainingEF.Entities;

namespace trainingEF.Data;

public class AppDbSeeding
{
    public static async Task SeedingData(RoleManager<IdentityRole> roleManager)
    {
        #region Seeding Data
        if (!await roleManager.RoleExistsAsync(Roles.Admin.ToString()))
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        }
        if (!await roleManager.RoleExistsAsync(Roles.User.ToString()))
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
        }
        #endregion

    }
}
