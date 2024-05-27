using PartyWebAppCommon.DTOs;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Profiles;
using AutoMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>();
    }
}