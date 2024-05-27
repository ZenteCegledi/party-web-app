using AutoMapper;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;

namespace PartyWebAppServer.Profiles;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<CreateEventRequest, EventDTO>();
    }
}