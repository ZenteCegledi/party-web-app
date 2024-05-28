using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;

namespace PartyWebAppServer.Services.LocationService;

public interface IServerLocationService
{
    public Task<LocationDto> GetLocation(int id);
    public Task<List<LocationDto>> GetLocations();
    public Task<LocationDto> CreateLocation(CreateLocationRequest request);
    public Task<LocationDto> DeleteLocation(int id);
    public Task<LocationDto> EditLocation(int id, EditLocationRequest request);
}