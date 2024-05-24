using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Services;

public interface ILocationService
{
    public Task<Location> GetLocation(int id);
    
    public Task<List<Location>> GetLocations();
    
    public Task<Location> CreateLocation(CreateLocationRequest request);
    
    public Task<Location> DeleteLocation(int id);
    
    public Task<Location> EditLocation(int id, EditLocationRequest request);
    
    
}