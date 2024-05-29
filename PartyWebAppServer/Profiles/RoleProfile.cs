using AutoMapper;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppServer.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Database.Models.Role, RoleDTO>();
    }
}