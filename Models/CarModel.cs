namespace PdI_Car_Rent.Models
{
    public class CarModel
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public int RentPrice { get; set; }
        public string CarInfo { get; set; }
        public List<string> Photos { get; set; }
    }
}