namespace PdI_Car_Rent.Data
{
    public class Car
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public int RentPriceForHour { get; set; }
        public string CarInfo { get; set; }
        public List<string> Photos { get; set; } = new List<string>();
    }
}