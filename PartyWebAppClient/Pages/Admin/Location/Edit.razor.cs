using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using PartyWebAppClient.Models;
using PartyWebAppClient.Services.LocationService;
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
    private ILocationService LocationService { get; set; }
    
    [Inject]
    private IToastService? ToastService { get; set; }
    
    [Inject]
    private NavigationManager Navigation { get; set; }
    
    private LocationDto _location;

    private List<LocationDto> _allLocations;
    
    private LocationType? _selectedType;

    private string SelectedType
    {
        get => _selectedType?.ToString();
        set => _selectedType = (LocationType) Enum.Parse(typeof(LocationType), value);
    }
    bool isLoading = true;
    protected override async Task OnInitializedAsync()
    {
        var (tempLocations, getAllLocationsError) = await LocationService.GetAllLocations();
        if (getAllLocationsError is not null)
        {
            ToastService?.ShowError(getAllLocationsError.Message);
            return;
        }
        _allLocations = tempLocations;
        
        LocationDto? tempLocation = _allLocations.Find(l => l.Id == Id);
        if (tempLocation is null)
        {
            ToastService?.ShowError("Location not found");
            isLoading = false;
            return;
        }
        _location = tempLocation;
        _selectedType = _location.Type;
        
        isLoading = false;
    }
    
    private Task<(LocationDto?, AppErrorModel?)> GetLocation()
    {
        return LocationService.GetLocation(Id);
    }
    
    private void ValidateForm()
    {
        if(String.IsNullOrEmpty(_location.Name.Trim()) || String.IsNullOrEmpty(_location.Address.Trim()) || _selectedType == null)
        {
            ToastService?.ShowError("Please fill in all fields!");
            return;
        }

        EditLocation();
    }
    
    private async Task EditLocation()
    {
        EditLocationRequest editRequest = new EditLocationRequest()
        {
            Name = _location.Name,
            Address = _location.Address,
            Type = _selectedType
        };
        
        if(_allLocations.Any(l =>l.Id != Id && l.Name == editRequest.Name && l.Address.ToLower().Equals(editRequest.Address.ToLower())))
        {
            ToastService?.ShowError("Location with this name and address already exists!");
            return;
        }
        
        await LocationService.EditLocation(Id, editRequest);
        NavigateToIndex();
        ToastService?.ShowSuccess("Changes saved successfully!");
    }
    
    private void NavigateToIndex()
    {
        Navigation.NavigateTo("/admin/locations");
    }
}