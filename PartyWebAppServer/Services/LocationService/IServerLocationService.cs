using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;

namespace PartyWebAppServer.Services.LocationService;

public interface IServerLocationService
{
    public Task<LocationDTO> GetLocation(int id);
    public Task<List<LocationDTO>> GetLocations();
    public Task<LocationDTO> CreateLocation(CreateLocationRequest request);
    public Task<LocationDTO> DeleteLocation(int id);
    public Task<LocationDTO> EditLocation(int id, EditLocationRequest request);
}