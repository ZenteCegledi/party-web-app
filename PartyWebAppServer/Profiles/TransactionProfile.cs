using AutoMapper;
using PartyWebAppCommon.DTOs;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, TransactionDTO>();
    }
}