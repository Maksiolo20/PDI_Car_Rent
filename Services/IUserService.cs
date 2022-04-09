using Pdi_Car_Rent.Areas.Identity.Data;
using System.Linq.Expressions;

namespace Pdi_Car_Rent.Services
{
    public interface IUserService
    {
        
        void Add(User entity);
        void Delete(User entity);
        void ChangeRole(User entity, int roleId);

        

    }
}
