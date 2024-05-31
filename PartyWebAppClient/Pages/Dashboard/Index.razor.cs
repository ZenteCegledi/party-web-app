using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.FluentUI.AspNetCore.Components;
using PartyWebAppClient.Services.UserService;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppClient.Pages.Dashboard;

public partial class Index : ComponentBase {
	private UserDto _user = new UserDto();
	
	[CascadingParameter]
	private Task<AuthenticationState>? authenticationState { get; set; }
	
	[Inject]
	IUserService userService { get; set; }
	
	[Inject]
	IToastService ToastService { get; set; }
	
	[Inject]
	NavigationManager NavigationManager { get; set; }
    
	protected override async Task OnInitializedAsync() {
		var authState = await authenticationState!;
		var user      = authState?.User;

		if (user == null) return;

		if (user.FindFirst("IsAdmin").Value == "true")
		{
			NavigationManager.NavigateTo("/admin");
			return;
		}

		
		var (userDto, error) = await userService.GetUser(user.FindFirst("Username")!.Value);
		if (error is not null) {
			ToastService.ShowError(error.Message);
			return;
		}

		if (userDto is not null) _user = userDto;
		
		StateHasChanged();
	}
}