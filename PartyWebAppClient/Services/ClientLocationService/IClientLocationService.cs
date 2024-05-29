using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;

namespace PartyWebAppClient.Services.ClientLocationService;

public interface IClientLocationService
{
    public Task<List<LocationDto>?> GetAllLocations();
    public Task<LocationDto> GetLocation(int id);
    public Task EditLocation(int id, EditLocationRequest request);
    public Task DeleteLocation(int id);

}