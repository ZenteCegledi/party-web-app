using System.Net;
using System.Net.Http.Json;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;

namespace PartyWebAppClient.Services.ClientLocationService;

public class ClientLocationService(HttpClient _http) : IClientLocationService
{
    
    public async Task<List<LocationDto>> GetAllLocations()
    {
        return await _http.GetFromJsonAsync<List<LocationDto>>("api/location");
    }

    public async Task<LocationDto> GetLocation(int id)
    {
        return await _http.GetFromJsonAsync<LocationDto>($"api/location/{id}");
    }
    
    public async Task EditLocation(int id, EditLocationRequest request)
    {
        await _http.PutAsJsonAsync($"api/location/{id}", request);
    }
    
    public async Task DeleteLocation(int id)
    {
        await _http.DeleteAsync($"api/location/{id}");
    }

}