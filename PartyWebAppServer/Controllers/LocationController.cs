using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Services.JwtService;
using PartyWebAppServer.Services.LocationService;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController(ILocationService locationService, IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    [HttpGet("{id}")]
    public async Task<LocationDto> GetLocation(int id)
    {
        if (!jwtService.IsAuthorized(_httpContext.Request))
            throw new UnauthorizedAccessException("You need to be authorized.");

        return await locationService.GetLocation(id);
    }


    [HttpGet()]
    public async Task<List<LocationDto>> GetLocations()
    {
        if (!jwtService.IsAuthorized(_httpContext.Request))
            throw new UnauthorizedAccessException("You need to be authorized.");

        return await locationService.GetLocations();
    }

    [HttpPost()]
    public async Task<LocationDto> CreateLocation(CreateLocationRequest request)
    {
        if (!jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin.");

        return await locationService.CreateLocation(request);
    }

    [HttpDelete("{id}")]
    public async Task<LocationDto> DeleteLocation(int id)
    {
        if (!jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin.");

        return await locationService.DeleteLocation(id);
    }

    [HttpPut("{id}")]
    public async Task<LocationDto> EditLocation(int id, EditLocationRequest request)
    {
        if (!jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin.");

        return await locationService.EditLocation(id, request);
    }
}