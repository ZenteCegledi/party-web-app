using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using PartyWebAppClient.Services.ClientLocationService;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppClient.Pages.Admin.Location;

public partial class Index(IClientLocationService _clientLocationService) : ComponentBase
{
    
    IQueryable<LocationDTO> locations;
    
    protected override async Task OnInitializedAsync()
    {
        List<LocationDTO> locationsList = await GetAllLocations();
        locations = locationsList.AsQueryable();
    }
    
    private async Task<List<LocationDTO>> GetAllLocations()
    {
        return await _clientLocationService.GetAllLocations();
    }

    /*private async Task DeleteLocation(int id)
    {
        await _http.DeleteAsync($"api/locations/{id}");
    }*/
    
}
