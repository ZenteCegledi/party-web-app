using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.FluentUI.AspNetCore.Components;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;

namespace PartyWebAppClient.Components.Dashboard;

public partial class Wallets : ComponentBase
{
    [CascadingParameter]
    private Task<AuthenticationState>? authenticationState { get; set; }

    [Inject]
    private HttpClient? Http { get; set; }

    private List<WalletDto> wallets = new List<WalletDto>();
    private readonly Dictionary<CurrencyType, (string symbol, string image)> currencyMap = Enum.GetValues<CurrencyType>().ToDictionary(c => c, c => c switch
    {
        CurrencyType.USD => ("$", "images/flags/us.svg"),
        CurrencyType.EUR => ("€", "images/flags/eu.svg"),
        CurrencyType.HUF => ("Ft", "images/flags/hu.svg"),
        CurrencyType.CREDIT => ("C", "images/flags/hu.svg"),
    });
    private WalletDto chosenWallet = new WalletDto();
    
    FluentHorizontalScroll _walletsContainer = default!;

    protected override async Task OnInitializedAsync()
    {
        var authState = await authenticationState!;
        var user = authState?.User;

        if (user == null) return;

        wallets = await Http?.GetFromJsonAsync<List<WalletDto>>($"http://localhost:5259/api/Wallet/{user.FindFirst("Username")?.Value}")! ?? new List<WalletDto>();
    
        if (wallets.Count > 0) chosenWallet = wallets.First();
            
        StateHasChanged();
    }
}
