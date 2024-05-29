namespace PartyWebAppServer.Profiles;

using PartyWebAppCommon.DTOs;
using AutoMapper;

public class WalletProfile : Profile
{
    public WalletProfile()
    {
        CreateMap<Database.Models.Wallet, WalletDTO>();
    }
}