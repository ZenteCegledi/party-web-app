using BitzArt.Blazor.Auth;

namespace PartyWebAppClient.Layout.AdminLayout;

using Microsoft.AspNetCore.Components;

public partial class AdminLayout : LayoutComponentBase
{
    [Inject]
    IUserService UserService { get; set; }
    
    [Inject]
    NavigationManager NavigationManager { get; set; }
    
    private async Task SignOutAsync()
    {
        await UserService.SignOutAsync();
        NavigationManager.NavigateTo("/", true);
    }
}