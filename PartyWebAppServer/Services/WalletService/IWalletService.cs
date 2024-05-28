using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.Services.WalletService;

public interface IWalletService
{
    List<WalletDto> GetWallets(string username);
    WalletDto GetWallet(string username, CurrencyType currency);
}