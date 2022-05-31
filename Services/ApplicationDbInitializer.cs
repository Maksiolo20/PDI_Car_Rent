using Microsoft.AspNetCore.Identity;
using Pdi_Car_Rent.Data;
using Pdi_Car_Rent.Models;

namespace Pdi_Car_Rent.Services
{
    public class ApplicationDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DatabaseContext _context;
        public ApplicationDbInitializer(UserManager<IdentityUser> userManager, DatabaseContext context)
        {
            _userManager = userManager;
            _context = context;
            //SeedUsers(_userManager);
        }
        public void SeedRentStatuses()
        {
            List<RentStatus> rentStatuses = new()
            {
                new RentStatus {/*RentStatusId=0,*/RentStatusName = "Dostępny" },
                new RentStatus {/*RentStatusId=1,*/RentStatusName = "Rezerwacja" },
                new RentStatus {/*RentStatusId=2,*/RentStatusName = "Wypożyczony" },
            };
            foreach (var item in rentStatuses)
            {
                _context.RentStatuses.Add(item);
            }
            _context.SaveChanges();
        }
        public void SeedRentPlaces()
        {
            List<CarRentPlaceViewModel> Places = new()
            {
                new CarRentPlaceViewModel { PlaceName = "Bielsko-Biała", Address = "Willowa 2", WorkerId = _userManager.FindByNameAsync("Pracownik").Result.Id},
                new CarRentPlaceViewModel { PlaceName = "Katowice", Address = "Kościuszki 96" },
            };
            foreach (var item in Places)
            {
                _context.CarRentPlace.Add(item);
            }
            _context.SaveChanges();
        }
        public void SeedCars()
        {
            List<Car> Cars = new()
            {
                new Car {Name = "Wołga", CarInfo = "Czarna", CarRentPlaceID = 0, CarTypeId = 0, RentPriceForHour = 30, RentStatusID = 1 },
                new Car {Name = "Mazda", CarInfo = "MX-5", CarRentPlaceID = 1, CarTypeId = 1, RentPriceForHour = 40, RentStatusID = 1 },

            };
            foreach (var item in Cars)
            {
                _context.Cars.Add(item);
            }
            _context.SaveChanges();
        }
        public void SeedCarTypes()
        {
            List<CarType> Types = new()
            {
                new CarType { /*CarTypeId = 1,*/ Name = "Combi" },
                new CarType { /*CarTypeId = 2,*/ Name = "Sedan" },
            };
            foreach (var item in Types)
            {
                _context.CarTypes.Add(item);
            }
            _context.SaveChanges();
        }
        public void SeedUsers()
        {
            if (_userManager.FindByEmailAsync("Administrator").Result == null)
            {
                List<IdentityUser> identityUsers = new()
                {
                    new IdentityUser
                    {
                        UserName = "Administrator",
                        Email = "Administrator"
                    },

                    new IdentityUser
                    {
                        UserName = "Pracownik",
                        Email = "Pracownik"
                    },

                    new IdentityUser
                    {
                        UserName = "Uzytkownik",
                        Email = "Uzytkownik"
                    },
                };
                string password = "P@$$w0rd";
                foreach (var item in identityUsers)
                {
                    IdentityResult Result = _userManager.CreateAsync(item, password).Result;
                    if (Result.Succeeded)
                    {
                        _userManager.AddToRoleAsync(item, item.UserName.ToString()).Wait();
                    }

                }
            }
        }

    }
}
