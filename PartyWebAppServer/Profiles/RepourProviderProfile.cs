using AutoMapper;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppServer.Profiles
{
    public class RepourProviderProfile : Profile
    {
        public RepourProviderProfile()
        {
            CreateMap<RepourProvider, RepourProviderDto>();
        }
    }
}