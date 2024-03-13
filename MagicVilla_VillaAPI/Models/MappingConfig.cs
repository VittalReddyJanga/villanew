using AutoMapper;
using MagicVilla_VillaAPI.Models.DTOS;

namespace MagicVilla_VillaAPI.Models
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();
            CreateMap<Villa, VillaDto>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberCreateDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberDto>().ReverseMap();

        }
    }
}
