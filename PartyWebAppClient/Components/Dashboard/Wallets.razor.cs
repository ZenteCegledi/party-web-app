using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using Microsoft.FluentUI.AspNetCore.Components;
using PartyWebAppCommon.Requests;

namespace PartyWebAppClient.Components.Dashboard;

public partial class Wallets : ComponentBase
{
    private bool modalSubmitting = false;

    private Modal DepositModal;
    private decimal depositAmount;

    private Modal WithdrawModal;
    private decimal withdrawAmount;

    private Modal CreateWalletModal;
    private string CreateWalletCurrency { get; set; }
    private decimal CreateWalletAmount { get; set; }
    private List<CurrencyType> CreateWalletCurrencyOptions = new List<CurrencyType>();

    public async Task ShowModal(Modal modal) => await modal?.ShowAsync()!;
    public async Task HideModal(Modal modal) => await modal?.HideAsync()!;

    [CascadingParameter]
    private Task<AuthenticationState>? authenticationState { get; set; }

    [Inject]
    private IToastService? ToastService { get; set; }

    [Inject]
    private IWalletService walletService { get; set; }

    private List<WalletDto> wallets = new List<WalletDto>();
    private readonly Dictionary<CurrencyType, (string symbol, string image)> currencyMap = Enum.GetValues<CurrencyType>().ToDictionary(c => c, c => c switch
    {
        CurrencyType.USD => ("$", "images/flags/us.svg"),
        CurrencyType.EUR => ("€", "images/flags/eu.svg"),
        CurrencyType.HUF => ("Ft", "images/flags/hu.svg"),
        CurrencyType.CREDIT => ("C", "images/flags/credit.svg"),
    });
    private WalletDto chosenWallet = new WalletDto();

    protected override async Task OnInitializedAsync()
    {
        var authState = await authenticationState!;
        var user = authState?.User;

        if (user == null) return;

        var (_wallets, error) = await walletService.GetUserWallets(new GetWalletRequest { Username = user.FindFirst("username")?.Value! });
        if (error is not null)
        {
            ToastService?.ShowError(error.Message);
            return;
        }

        if (_wallets!.Count > 0) chosenWallet = _wallets.Find(w => w.IsPrimary) ?? _wallets[0];
        wallets = _wallets.OrderBy(w => w.IsPrimary ? 0 : 1).ToList();

        CreateWalletCurrencyOptions = Enum.GetValues<CurrencyType>().Except(wallets.Select(w => w.Currency)).ToList();

        StateHasChanged();
    }


    private async Task Deposit()
    {
        modalSubmitting = true;

        var (wallet, error) = await walletService.DepositToWallet(new DepositToWalletRequest
        {
            Username = chosenWallet.Username,
            Currency = chosenWallet.Currency,
            Amount = depositAmount
        });
        if (error is not null) ToastService?.ShowError(error.Message);

        var index = wallets.FindIndex(w => w.Currency == wallet.Currency);
        wallets[index] = wallet;
        chosenWallet = wallet;

        ToastService?.ShowSuccess("Deposit successful!");

        modalSubmitting = false;

        StateHasChanged();

        await HideModal(DepositModal);
    }

    private async Task Withdraw()
    {
        modalSubmitting = true;

        var (wallet, error) = await walletService.WithdrawFromWallet(new WithdrawFromWalletRequest
        {
            Username = chosenWallet.Username,
            Currency = chosenWallet.Currency,
            Amount = withdrawAmount
        });
        if (error is not null)
        {
            ToastService?.ShowError(error.Message);
            return;
        }

        var index = wallets.FindIndex(w => w.Currency == wallet.Currency);
        wallets[index] = wallet;
        chosenWallet = wallet;

        ToastService?.ShowSuccess("Withdrawal successful!");

        modalSubmitting = false;

        StateHasChanged();

        await HideModal(WithdrawModal);
    }

    private async Task CreateWallet()
    {
        modalSubmitting = true;

        var authState = await authenticationState!;
        var user = authState?.User;

        var newWallet = new WalletDto
        {
            Username = user.FindFirst("Username")?.Value!,
            Currency = Enum.Parse<CurrencyType>(CreateWalletCurrency),
            Amount = CreateWalletAmount,
            IsPrimary = wallets.Count == 0
        };

        var (wallet, error) = await walletService.CreateWallet(new CreateWalletRequest { Username = newWallet.Username, Currency = newWallet.Currency, Amount = newWallet.Amount, IsPrimary = newWallet.IsPrimary });
        if (error is not null)
        {
            ToastService?.ShowError(error.Message);
            return;
        }

        if (wallet is not null)
        {
            wallets.Add(wallet);
            CreateWalletCurrencyOptions.Remove(wallet.Currency);
        }

        ToastService?.ShowSuccess("Wallet created successfully");

        modalSubmitting = false;

        StateHasChanged();

        await HideModal(CreateWalletModal);
    }
}
