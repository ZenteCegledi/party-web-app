using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using PartyWebAppClient.Services.ClientLocationService;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppCommon.Requests;
using ToastService = Microsoft.FluentUI.AspNetCore.Components.ToastService;

namespace PartyWebAppClient.Pages.Admin.Location;

public partial class Edit : ComponentBase
{
    [Parameter] 
    public int Id { get; set; }
    [Inject]
    private IClientLocationService _clientLocationService { get; set; }
    
    private LocationDto _location;

    private string _selectedType;

    protected override async Task OnInitializedAsync()
    {   
        _location = await GetLocation();
        _selectedType = Convert.ToString(_location.Type);
    }
    
    private Task<LocationDto> GetLocation()
    {
        return _clientLocationService.GetLocation(Id);
    }
    
    private async Task EditLocation()
    {
        try
        {
            EditLocationRequest request = new EditLocationRequest()
            {
                Name = _location.Name,
                Address = _location.Address,
                Type = Enum.Parse<LocationType>(_selectedType)
            };
            await _clientLocationService.EditLocation(Id, request);
            Navigation.NavigateTo("/admin/locations");
        }
        catch (Exception e)
        {
            ToastService.ShowToast(ToastIntent.Error, "Nagy a baj");
        }

    }
    
    private void NavigateToIndex()
    {
        Navigation.NavigateTo("/admin/locations");
    }
}