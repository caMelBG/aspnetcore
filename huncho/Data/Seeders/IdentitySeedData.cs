using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace huncho.Data.Seeders
{
    public static class IdentitySeedData
    {
        private const string AdminUserName = "Admin";
        private const string AdminPassword = "Admin_123";

        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(AdminUserName);
            if (user == null)
            {
                user = new IdentityUser(AdminUserName);
                await userManager.CreateAsync(user, AdminPassword);
            }
        }
    }
}
