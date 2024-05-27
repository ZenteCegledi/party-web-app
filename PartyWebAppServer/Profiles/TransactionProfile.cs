using AutoMapper;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppServer.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Database.Models.Transaction, TransactionDto>();
    }
}