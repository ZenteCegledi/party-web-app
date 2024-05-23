namespace PartyWebAppServer.Profiles;

using PartyWebAppServer.Database;
using PartyWebAppCommon.DTOs;
using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Database.Models.Wallet, WalletDto>();
    }
}