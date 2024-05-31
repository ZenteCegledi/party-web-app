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
    
    bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        
        var (tmpLocations, error) = await LocationService.GetAllLocations();
        if (error is not null)
        {
            Console.WriteLine(error.Message);
            return;
        }

        _allLocations = tmpLocations;
        await LoadLocations(_allLocations);
        isLoading = false;
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
        await LocationService.DeleteLocation(id);
        _allLocations = _allLocations.Where(l => l.Id != id).ToList();
        await LoadLocations(_allLocations);
        await SearchLocations(new ChangeEventArgs { Value = _searchValue });
        ToastService?.ShowSuccess("Location deleted successfully!");
    }
    
    //Searchbar
    private string _searchValue;

    private async Task SearchLocations(ChangeEventArgs e)
    {
        _searchValue = e.Value?.ToString();
        if (_searchValue != null)
        {
            string searchValue = _searchValue.ToLower();
            List<LocationDto> filteredLcationList = new List<LocationDto>();
            
            filteredLcationList = _allLocations.Where(l => l.Name.ToLower().Contains(searchValue) || l.Address.ToLower().Contains(searchValue) || l.Type.ToString().ToLower().Contains(searchValue)).ToList();
            
            LoadLocations(filteredLcationList);
        }
    }

    private void NavigateToAddLocation()
    {
        Navigation.NavigateTo($"/admin/locations/add");
    }

}