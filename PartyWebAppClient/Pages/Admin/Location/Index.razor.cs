using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using PartyWebAppClient.Services.ClientLocationService;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppClient.Pages.Admin.Location;

public partial class Index : ComponentBase
{
    [Inject] 
    private IClientLocationService? _clientLocationService {  get; set; }

    private List<LocationDto>? _allLocations;
    
    private IQueryable<LocationDto>? _locations;
    
    [Inject]
    private NavigationManager? _navigation { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _allLocations = await GetAllLocations();
        await LoadLocations(_allLocations);
    }
    
    private async Task LoadLocations(List<LocationDto> locationList)
    {
        _locations = locationList.AsQueryable(); 
        StateHasChanged();
    }
    
    private async Task<List<LocationDto>> GetAllLocations()
    {
        return await _clientLocationService.GetAllLocations();
    }
    
    private void NavigateToEditLocation(int id)
    {
        _navigation.NavigateTo($"/admin/locations/edit/{id}");
    }
    
    private async Task DeleteLocation(int id)
    {
        await _clientLocationService.DeleteLocation(id);
        await LoadLocations(_locations.ToList());
    }
    
    //Searchbar
    private string _searchValue;
    
    private async Task SearchLocations(ChangeEventArgs e)
    {
        _searchValue = e.Value.ToString();
        if (_searchValue != null)
        {
            string searchValue = _searchValue.ToLower();
            List<LocationDto> filteredLcationList = new List<LocationDto>();
            _allLocations.ToList().ForEach(l =>
            {
                if (l.Id.ToString() == searchValue || l.Name.ToLower().Contains(searchValue) || l.Address.ToLower().Contains(searchValue) || l.Type.ToString().ToLower().Contains(searchValue))
                {
                    filteredLcationList.Add(l);
                }
            });
            LoadLocations(filteredLcationList);
        }
    }

    private void NavigateToAddLocation()
    {
        _navigation.NavigateTo($"/admin/locations/add");
    }

}