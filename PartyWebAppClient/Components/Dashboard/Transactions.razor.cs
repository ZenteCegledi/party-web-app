using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using PartyWebAppClient.Services.TransactionService;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;

namespace PartyWebAppClient.Components.Dashboard;

public partial class Transactions : ComponentBase
{
    [Inject]
    private ITransactionService transactionService { get; set; }

    [Inject]
    private IToastService ToastService { get; set; }
    
    private List<TransactionDto> transactions = new List<TransactionDto>();
    
    [Parameter]
    public WalletDto chosenWallet { get; set; }

    public Random Random = new Random();
    
    protected override async Task OnInitializedAsync()
    {
        var (_transactions, error) = await transactionService.GetWalletTransactions(chosenWallet.Id);
        if (error is not null)
        {
            ToastService?.ShowError(error.Message);
            return;
        }

        this.transactions = _transactions!;
        
        StateHasChanged();
    }
}