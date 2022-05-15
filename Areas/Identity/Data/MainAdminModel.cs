namespace Pdi_Car_Rent.Areas.Identity.Data
{
    public class MainAdminModel
    {
        public AddingRoleModel RoleModel { get; set; }
        public ManageRentPlaceModel RentPlaceModel { get; set; }
        public bool roleAction { get; set; } = false;
        public bool rentAction { get; set; } = false;
    }
}
