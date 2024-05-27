using AutoMapper;
using PartyWebAppCommon.DTOs;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, LocationDTO>();
    }
}