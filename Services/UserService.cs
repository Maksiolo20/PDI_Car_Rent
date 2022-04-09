using Microsoft.AspNetCore.Identity;
using Pdi_Car_Rent.Areas.Identity.Data;
using Pdi_Car_Rent.Data;

namespace Pdi_Car_Rent.Services
{
    public class UserService : IUserService
    {
        private readonly Pdi_Car_RentUserContext _userContext;
        public UserService(Pdi_Car_RentUserContext userContext)
        {
            _userContext = userContext;
        }
        public void Add(User user)
        {
            _userContext.Users.Add(user);
            _userContext.SaveChanges();
        }

        public void Delete(User user)
        {
            _userContext.Users.Remove(user);
            _userContext.SaveChanges();
        }

        public void ChangeRole(User user, int roleId)
        {
            var result = _userContext.UserRoles.FirstOrDefault(x => x.UserId == user.Id);
            if (result != null)
            {
                _userContext.UserRoles.Remove(result);
                _userContext.UserRoles.Add(new IdentityUserRole<string>()
                {
                    UserId = user.Id,
                    RoleId = roleId.ToString()
                });

                _userContext.SaveChanges();
            }
        }
    }
}
