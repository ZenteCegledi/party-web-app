using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Services.JwtService;
using PartyWebAppServer.Services.LocationService;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController(IServerLocationService _locationService, IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
{
    private readonly HttpContext httpContext = httpContextAccessor.HttpContext;

    [HttpGet("{id}")]
    public async Task<LocationDTO> GetLocation(int id)
    {
        if (!jwtService.IsAuthorized(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be authorized.");

        return await _locationService.GetLocation(id);
    }


    [HttpGet()]
    public async Task<List<LocationDTO>> GetLocations()
    {
        if (!jwtService.IsAuthorized(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be authorized.");

        return await _locationService.GetLocations();
    }

    [HttpPost()]
    public async Task<LocationDTO> CreateLocation(CreateLocationRequest request)
    {
        if (!jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin.");

        return await _locationService.CreateLocation(request);
    }

    [HttpDelete("{id}")]
    public async Task<LocationDTO> DeleteLocation(int id)
    {
        if (!jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin.");

        return await _locationService.DeleteLocation(id);
    }

    [HttpPut("{id}")]
    public async Task<LocationDTO> EditLocation(int id, EditLocationRequest request)
    {
        if (!jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin.");

        return await _locationService.EditLocation(id, request);
    }
}