using PartyWebAppCommon.DTOs;

namespace PartyWebAppClient.Services.ClientLocationService;

public interface IClientLocationService
{
    public Task<List<LocationDTO>> GetAllLocations();

}