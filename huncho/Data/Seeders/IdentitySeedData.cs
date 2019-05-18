using huncho.Data.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace huncho.Data.Seeders
{
    public static class IdentitySeedData
    {
        private const string AdminUserName = "Admin";
        private const string AdminPassword = "Admin_123";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>())
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
    }
}
