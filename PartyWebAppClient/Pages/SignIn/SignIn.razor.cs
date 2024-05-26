using Microsoft.AspNetCore.Components;

namespace PartyWebAppClient.Pages.SignIn;

public partial class SignIn : ComponentBase
{
	private SignInModel _model = new SignInModel();
	private bool loading = false;

	private async Task OnValidSubmit()
	{
		if (loading) return;

		loading = true;

		var response = await UserService.SignInAsync(_model);

		if (!response.IsSuccess) ToastService.ShowError(response.ErrorMessage ?? "An error occurred while signing in.");
		else NavigationManager.NavigateTo("/dashboard", true);

		loading = false;
	}
}