using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using PartyWebAppClient.Services.TransactionService;
using PartyWebAppCommon.DTOs;

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
    
    protected override async Task OnInitializedAsync()
    {
        var (transactions, error) = await transactionService.GetWalletTransactions(chosenWallet.Id);
        if (error is not null)
        {
            ToastService?.ShowError(error.Message);
            return;
        }

        this.transactions = transactions!;
        
        StateHasChanged();
    }
}