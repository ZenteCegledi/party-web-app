using AutoMapper;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppServer.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Database.Models.User, UserDTO>();

    }
}