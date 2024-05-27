using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Services;

public interface ILocationService
{
    public Task<LocationDTO> GetLocation(int id);
    
    public Task<List<LocationDTO>> GetLocations();
    
    public Task<LocationDTO> CreateLocation(CreateLocationRequest request);
    
    public Task<LocationDTO> DeleteLocation(int id);
    
    public Task<LocationDTO> EditLocation(int id, EditLocationRequest request);
    
    
}