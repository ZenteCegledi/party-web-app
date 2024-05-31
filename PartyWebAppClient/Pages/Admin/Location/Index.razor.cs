using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using PartyWebAppClient.Models;
using PartyWebAppClient.Services.LocationService;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppClient.Pages.Admin.Location;

public partial class Index : ComponentBase
{
    [Inject] 
    private ILocationService? LocationService {  get; set; }
    [Inject]
    private NavigationManager? Navigation { get; set; }
    [Inject]
    private IToastService? ToastService { get; set; }

    private List<LocationDto>? _allLocations;
    
    private IQueryable<LocationDto>? _locations;
    
    bool _isLoading = true;
    
    private string _searchValue;

    protected override async Task OnInitializedAsync()
    {
        
        var (tmpLocations, error) = await LocationService.GetAllLocations();
        if (error is not null)
        {
            ToastService?.ShowError(error.Message);
            return;
        }

        _allLocations = tmpLocations.OrderBy(l => l.Id).ToList();
        await LoadLocations(_allLocations);
        _isLoading = false;
    }
    
    private async Task LoadLocations(List<LocationDto> locationList)
    {
        _locations = locationList.AsQueryable(); 
        StateHasChanged();
    }
    
    private async Task<(List<LocationDto>?, AppErrorModel?)> GetAllLocations()
    {
        return await LocationService.GetAllLocations();
    }
    
    private void NavigateToEditLocation(int id)
    {
        Navigation.NavigateTo($"/admin/locations/edit/{id}");
    }
    
    private async Task DeleteLocation(int id)
    {
        var(deletedLocation, deleteError) = await LocationService.DeleteLocation(id);
        if(deleteError is not null)
        {
            ToastService?.ShowError(deleteError.Message);
            return;
        }

        var(locationsAfterDeletion, getAllLocationsError) = await GetAllLocations();
        if (getAllLocationsError is not null)
        {
            ToastService?.ShowError(getAllLocationsError.Message);
            return;
        }

        _allLocations = locationsAfterDeletion;
        await SearchLocations(new ChangeEventArgs { Value = _searchValue });
        ToastService?.ShowSuccess("Location deleted successfully!");
    }
    
    //Searchbar
    private async Task SearchLocations(ChangeEventArgs e)
    {
        _searchValue = e.Value?.ToString();
        if (_searchValue != null)
        {
            string searchValue = _searchValue.ToLower();

            var(filteredLocationList, error) = await GetAllLocations();
            if (error is not null)
            {
                ToastService?.ShowError(error.Message);
                return;
            }
            
            LoadLocations(filteredLocationList.Where(l => l.Name.ToLower().Contains(searchValue) || l.Address.ToLower().Contains(searchValue) || l.Type.ToString().ToLower().Contains(searchValue)).ToList());
        }
    }

    private void NavigateToAddLocation()
    {
        Navigation.NavigateTo($"/admin/locations/add");
    }

}