using Microsoft.AspNetCore.Identity;

namespace Pdi_Car_Rent.Services
{
    public class ApplicationDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        public ApplicationDbInitializer(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            //SeedUsers(_userManager);
        }
        public void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("Administrator").Result == null)
            {
                IdentityUser admin = new IdentityUser
                {
                    UserName = "Administrator",
                    Email = "Administrator"
                };

                IdentityUser worker = new IdentityUser
                {
                    UserName = "Pracownik",
                    Email = "Pracownik"
                };

                IdentityUser user = new IdentityUser
                {
                    UserName = "Uzytkownik",
                    Email = "Uzytkownik"
                };

                string password = "P@$$w0rd";
                IdentityResult adminResult = userManager.CreateAsync(admin, password).Result;
                IdentityResult workerResult = userManager.CreateAsync(worker, password).Result;
                IdentityResult userResult = userManager.CreateAsync(user, password).Result;

                if (adminResult.Succeeded)
               {
                    userManager.AddToRoleAsync(admin, "Administrator").Wait();
                }
                if (workerResult.Succeeded)
                {
                    userManager.AddToRoleAsync(worker, "Pracownik").Wait();
                }
                if (userResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Uzytkownik").Wait();
                }
            }
        }
    }
}
