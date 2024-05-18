using Auto.Data.Entities;
using Auto.Models;
using AutoMapper;

namespace Auto.Mapping;

public class AppMappingProfile: Profile
{
    public AppMappingProfile()
    {			
        CreateMap<AppUser, UserEditModel>().ReverseMap();
        CreateMap<AppUser, UserRegisterModel>().ReverseMap();
    }
}