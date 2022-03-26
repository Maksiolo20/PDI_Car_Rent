namespace Pdi_Car_Rent.Models
{
    public class HomeModel
    {
        public string Name { get; set; } = "ATH Car Rent Service";
        public string Text { get; set; } = "Wypożyczalnia samochodów. Znajdź najbliższy komis i wybierz samochód który Cię intersuje.";
        public List<string> Photos { get; set; } = new List<string>();

        public HomeModel()
        {
            Photos.Add("https://99rent.pl/sites/all/pliki/styles/oddzial/public/zdjecia_oddzialow/img_20201119_145121.jpg?itok=_6_WVZrt&c=aceabefce506c125e5653bc3f6377349");
            Photos.Add("https://99rent.pl/sites/all/pliki/styles/oddzial/public/zdjecia_oddzialow/event_siedziba.jpg?itok=7TpezFFG");
            Photos.Add("https://mobicars.pl/pliki/slider/statyczne/5e70fb8310d2a.jpg");
        }
    }
}
