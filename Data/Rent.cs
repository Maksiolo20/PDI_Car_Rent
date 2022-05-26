using Pdi_Car_Rent.Data;
using System.ComponentModel.DataAnnotations;

namespace Pdi_Car_Rent.Data
{
    public class Rent
    {
        [Key]
        public int RentId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ClientId { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }
    }
}
