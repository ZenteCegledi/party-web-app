namespace PartyWebAppClient.Layout;

using Microsoft.AspNetCore.Components;

public partial class DashboardLayout : LayoutComponentBase
{
	private async Task SignOutAsync()
	{
		await UserService.SignOutAsync();
		NavigationManager.NavigateTo("/", true);
	}

}