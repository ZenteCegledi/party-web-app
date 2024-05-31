using Microsoft.AspNetCore.Components;
using PartyWebAppClient.Services.WalletService;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;

namespace PartyWebAppClient.Components.Dashboard;

public partial class EventCard : ComponentBase
{
    [Parameter]
    public EventDTO Event { get; set; } = new EventDTO();
    
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
    
    protected override async Task OnInitializedAsync()
    {
        Console.WriteLine("EventCard initialized");
        Console.WriteLine(Event.Name);
    }
}