using AutoMapper;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppServer.Profiles
{
    public class RepourProfile : Profile
    {
        public RepourProfile()
        {
            CreateMap<RepourProvider, RepourProviderDto>();
        }
    }
}