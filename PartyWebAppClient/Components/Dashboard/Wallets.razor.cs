using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppClient.Services;
using BlazorBootstrap;
using Microsoft.FluentUI.AspNetCore.Components;

namespace PartyWebAppClient.Components.Dashboard;

public partial class Wallets : ComponentBase
{
    [CascadingParameter]
    private Task<AuthenticationState>? authenticationState { get; set; }

    [Inject]
    private AppHttpClient? Http { get; set; }

    [Inject]
    private IToastService? ToastService { get; set; }

    private List<WalletDto> wallets = new List<WalletDto>();
    private readonly Dictionary<CurrencyType, (string symbol, string image)> currencyMap = Enum.GetValues<CurrencyType>().ToDictionary(c => c, c => c switch
    {
        CurrencyType.USD => ("$", "images/flags/us.svg"),
        CurrencyType.EUR => ("€", "images/flags/eu.svg"),
        CurrencyType.HUF => ("Ft", "images/flags/hu.svg"),
        CurrencyType.CREDIT => ("C", "images/flags/hu.svg"),
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
        else wallets = _wallets ?? new List<WalletDto>();

        if (wallets.Count > 0) chosenWallet = wallets.Find(w => w.IsPrimary) ?? wallets[0];

        StateHasChanged();
    }
}
