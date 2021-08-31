using AutoMapper;
using API.Entities;
using API.DTOs;
using System.Linq;
using API.Extensions;

namespace API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                //Set DefaultPhoto URL into the field "photoUrl"
                //destination is PhotoUrlField, That maps from Photos where isMain is set to true 
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.isMain).url))
                //Sets value Age (MemberDto) by calculating it
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo,PhotoDto>();
        }
    }
}