using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using PartyWebAppClient.Services.TransactionService;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;

namespace PartyWebAppClient.Components.Dashboard;

public partial class Transactions : ComponentBase
{
    private List<TransactionDto> tempTransactions = new List<TransactionDto>();
    
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
        for (int i = 0; i < 5; i++)
        {
            tempTransactions.Add(new TransactionDto
            {
                SpentCurrency = Random.Next(100, 1000),
                WalletId = 1,
                Date = DateTime.Now.ToUniversalTime(),
                Id = i,
                TransactionType = (TransactionType)(i),
                Count = 1,
            });
        }
        
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