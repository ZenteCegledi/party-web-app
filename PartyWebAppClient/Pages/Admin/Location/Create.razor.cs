using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.VisualBasic;
using PartyWebAppClient.Services.LocationService;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppCommon.Requests;

namespace PartyWebAppClient.Pages.Admin.Location;

public partial class Create : ComponentBase
{
    [Inject]
    private NavigationManager Navigation { get; set; }
    [Inject]
    private IToastService? ToastService { get; set; }
    [Inject]
    private ILocationService LocationService { get; set; }
    
    private LocationDto _location = new LocationDto();
    
    private List<LocationDto> _allLocations;

    private LocationType _selectedType;

    private string SelectedType
    {
        get => _selectedType.ToString();
        set => _selectedType = (LocationType) Enum.Parse(typeof(LocationType), value);
    }

    protected override async Task OnInitializedAsync()
    {
        var (tempLocations, error) = await LocationService.GetAllLocations();
        if (error is not null)
        {
            ToastService?.ShowError(error.Message);
            return;
        }
        _allLocations = tempLocations;
    }

    private void ValidateForm()
    {
        if(String.IsNullOrEmpty(_location.Name.Trim()) || String.IsNullOrEmpty(_location.Address.Trim()) || _selectedType == null)
        {
            ToastService?.ShowError("Please fill in all fields!");
            return;
        }

        CreateLocation();
    }
    
    private async Task CreateLocation()
    {
        CreateLocationRequest createRequest = new CreateLocationRequest()
        {
            Name = _location.Name.Trim(),
            Address = _location.Address.Trim(),
            Type = _selectedType
        };
        
        if(_allLocations.Any(l => l.Name == createRequest.Name && l.Address.ToLower().Equals(createRequest.Address.ToLower())))
        {
            ToastService?.ShowError("Location with this name and address already exists!");
            return;
        }
        
        await LocationService.CreateLocation(createRequest);
        NavigateToIndex();
        ToastService?.ShowSuccess("Location created successfully!");
    }
    
    private void NavigateToIndex()
    {
        Navigation.NavigateTo("/admin/locations");
    }
}