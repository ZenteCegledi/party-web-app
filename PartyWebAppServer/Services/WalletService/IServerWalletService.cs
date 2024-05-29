using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.Services.WalletService;

public interface IServerWalletService
{
    public WalletDTO CreateWallet(WalletDTO wallet);
    public WalletDTO DeleteWallet(string username, CurrencyType currency);
    public WalletDTO DepositToWallet(WalletDTO wallet, decimal amount);
    public WalletDTO WithdrawFromWallet(WalletDTO wallet, decimal amount);
    public WalletDTO SetPrimaryWallet(WalletDTO wallet);
}