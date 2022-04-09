using Microsoft.AspNetCore.Identity;
using Pdi_Car_Rent.Data;

namespace Pdi_Car_Rent.Services
{
    public class UserRoleService
    {
        private readonly Pdi_Car_RentUserContext _userContext;
        public UserRoleService(Pdi_Car_RentUserContext userContext)
        {
            _userContext = userContext;
            //StartingSetup(_userContext);

        }
        public void StartingSetup(Pdi_Car_RentUserContext userContext)
        {
            userContext.Roles.Add(new IdentityRole { Name = "Administrator", NormalizedName = "Administrator".ToUpper() });
            userContext.Roles.Add(new IdentityRole { Name = "Uzytkownik", NormalizedName = "Uzytkownik".ToUpper() });
            userContext.Roles.Add(new IdentityRole { Name = "Pracownik", NormalizedName = "Pracownik".ToUpper() });
            //create admin
            userContext.SaveChanges();
        }
    }
}
