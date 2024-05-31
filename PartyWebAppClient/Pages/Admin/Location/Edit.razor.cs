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
    
    private string _selectedType;
    
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
        
        var (tempLocation, getLocationError) = await LocationService.GetLocation(Id);
        if (getLocationError is not null)
        {
            ToastService?.ShowError(getLocationError.Message);
            return;
        }
        _location = tempLocation;
        _selectedType = Convert.ToString(_location.Type);
        
        isLoading = false;
    }
    
    private Task<(LocationDto?, AppErrorModel?)> GetLocation()
    {
        return LocationService.GetLocation(Id);
    }
    
    private async Task EditLocation()
    {
        EditLocationRequest editRequest = new EditLocationRequest()
        {
            Name = _location.Name,
            Address = _location.Address,
            Type = Enum.Parse<LocationType>(_selectedType)
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