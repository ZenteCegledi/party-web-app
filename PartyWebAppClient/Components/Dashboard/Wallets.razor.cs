using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using Microsoft.FluentUI.AspNetCore.Components;
using PartyWebAppClient.Services;

namespace PartyWebAppClient.Components.Dashboard;

public partial class Wallets : ComponentBase
{
    private Modal DepositModal;
    private Modal WithdrawModal;
    private Modal CreateWalletModal;

    public async Task ShowModal(Modal modal) => await modal?.ShowAsync();
    public async Task HideModal(Modal modal) => await modal?.HideAsync();

    [CascadingParameter]
    private Task<AuthenticationState>? authenticationState { get; set; }

    [Inject]
    private IAppHttpClient? Http { get; set; }

    [Inject]
    private IToastService? ToastService { get; set; }

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

        var walletService = new ClientWalletService(Http!);

        var (_wallets, error) = await walletService.GetUserWallets(user.FindFirst("username")?.Value!);
        if (error is not null) ToastService?.ShowError(error.Message);

        if (_wallets!.Count > 0) chosenWallet = _wallets.Find(w => w.IsPrimary) ?? _wallets[0];
        wallets = _wallets.OrderBy(w => w.IsPrimary ? 0 : 1).ToList();

        StateHasChanged();
    }
}
