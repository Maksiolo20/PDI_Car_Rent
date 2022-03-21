using AutoMapper;
using PdI_Car_Rent.Models;
using PdI_Car_Rent.Data;

namespace Pdi_Car_Rent.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Car, CarIndexViewModel>()
                .ForMember(r => r.CarType, opt => opt.MapFrom<string>(r => r.CarType.Name));
            CreateMap<Car, CarDetailsViewModel>()
                .ForMember(r => r.CarType, opt => opt.MapFrom<string>(r => r.CarType.Name));
        }
    }
}
