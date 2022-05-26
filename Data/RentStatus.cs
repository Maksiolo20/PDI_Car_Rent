using System.ComponentModel.DataAnnotations;

namespace Pdi_Car_Rent.Data
{
    public class RentStatus
    {
        [Key]
        public int RentStatusId { get; set; }
        public string RentStatusName { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
