using System.Net;
using System.Net.Http.Json;
using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;

namespace PartyWebAppClient.Services.LocationService;

public class LocationService(IAppHttpClient _http) : ILocationService
{
    
    public async Task<(List<LocationDto>?, AppErrorModel?)> GetAllLocations() =>
        await _http.GetAsync<List<LocationDto>>("api/location");

    public async Task<(LocationDto?, AppErrorModel?)> GetLocation(int id) =>
        await _http.GetAsync<LocationDto>($"api/location/{id}");
    
    public async Task<(LocationDto?, AppErrorModel?)> CreateLocation(CreateLocationRequest request) =>
        await _http.PostAsync<LocationDto>("api/location", request);
    
    public async Task<(LocationDto?, AppErrorModel?)> EditLocation(int id, EditLocationRequest request) =>
        await _http.PutAsync<LocationDto>($"api/location/{id}", request);
    
    public async Task<(LocationDto?, AppErrorModel?)> DeleteLocation(int id) =>
        await _http.DeleteAsync<LocationDto>($"api/location/{id}");
    

}