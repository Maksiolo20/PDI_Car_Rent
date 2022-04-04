using AutoMapper;
using Pdi_Car_Rent.Models;
using Pdi_Car_Rent.Data;

namespace Pdi_Car_Rent.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Car, CarIndexViewModel>()
                .ForMember(r => r.CarType, opt => opt.MapFrom<string>(r => r.CarType.Name))
                .ForMember(x=>x.CarId,opt =>opt.MapFrom<int>(x=>x.Id));
            CreateMap<Car, CarDetailsViewModel>()
                .ForMember(r => r.CarType, opt => opt.MapFrom<string>(r => r.CarType.Name))
                .ForMember(x => x.CarId, opt => opt.MapFrom<int>(x => x.Id));
            CreateMap<Car, CarEditViewModel>();
                //.ForMember(r => r.CarType, opt => opt.MapFrom<string>(r => r.CarType.Name))
                //.ForMember(x => x.CarId, opt => opt.MapFrom<int>(x => x.Id)); ;
            CreateMap<Car, CarCreateViewModel>();
                //.ForMember(r => r.CarType, opt => opt.MapFrom<string>(r => r.CarType.Name))
                //.ForMember(x => x.CarId, opt => opt.MapFrom<int>(x => x.Id)); ;
        }
    }
}
