using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.Services;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/locations")]
public class LocationController
{
    
    private ILocationService LocationService;

    public LocationController(ILocationService locationService)
    {
        LocationService = locationService;
    }

    
    [HttpGet("{id}")]
    public async Task<Location> GetLocation(int id)
    {
        return await LocationService.GetLocation(id);
    }
    
    
    [HttpGet()]
    public async Task<List<Location>> GetLocations()
    {
        return await LocationService.GetLocations();
    }

    [HttpPost()]
    public async Task<Location> CreateLocation(CreateLocationRequest request)
    {
        return await LocationService.CreateLocation(request);
    }

    [HttpDelete("{id}")]
    public async Task<Location> DeleteLocation(int id)
    {
        return await LocationService.DeleteLocation(id);
    }

    [HttpPut("{id}")]
    public async Task<Location> EditLocation(int id, EditLocationRequest request)
    {
        return await LocationService.EditLocation(id, request);
    }
}