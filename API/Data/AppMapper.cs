using AutoMapper;
using API.Models;
using API.Dtos;
using API.Dtos.IdentityUser;

namespace API.Data
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<MachineStatusCatalog, MachineStatusCatalogDto>().ReverseMap();
            CreateMap<MachineStatusCatalog, MachineStatusCatalogIdDto>().ReverseMap();
            CreateMap<AppUser, AppUserDto>().ReverseMap();
            
        }
    }
}
