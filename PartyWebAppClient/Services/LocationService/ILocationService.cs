using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;

namespace PartyWebAppClient.Services.LocationService;

public interface ILocationService
{
    public Task<(List<LocationDto>?, AppErrorModel?)> GetAllLocations();
    public Task<(LocationDto?, AppErrorModel?)> GetLocation(int id);
    public Task<(LocationDto?, AppErrorModel?)> CreateLocation(CreateLocationRequest request);
    public Task<(LocationDto?, AppErrorModel?)> EditLocation(int id, EditLocationRequest request);
    public Task<(LocationDto?, AppErrorModel?)> DeleteLocation(int id);

}