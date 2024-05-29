using AutoMapper;
using PartyWebAppCommon.DTOs;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}