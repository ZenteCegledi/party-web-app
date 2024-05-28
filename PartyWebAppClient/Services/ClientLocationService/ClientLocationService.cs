using System.Net;
using System.Net.Http.Json;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppClient.Services.ClientLocationService;

public class ClientLocationService(HttpClient _http)
{
    
    private async Task<List<LocationDTO>> GetAllLocations()
    {
        return await _http.GetFromJsonAsync<List<LocationDTO>>("api/locations");
    }

}