using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.Services;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/locations")]
public class LocationController
{
    
    private ILocationService _locationService;
    
    private readonly IMapper _mapper;
    
    public LocationController(ILocationService locationService, IMapper mapper)
    {
        _locationService = locationService;
        _mapper = mapper;
    }
    
    [HttpGet("{id}")]
    public async Task<LocationDTO> GetLocation(int id)
    {
        return await _locationService.GetLocation(id);
    }
    
    
    [HttpGet()]
    public async Task<List<LocationDTO>> GetLocations()
    {
        return await _locationService.GetLocations();
    }

    [HttpPost()]
    public async Task<LocationDTO> CreateLocation(CreateLocationRequest request)
    {
        return await _locationService.CreateLocation(request);
    }

    [HttpDelete("{id}")]
    public async Task<LocationDTO> DeleteLocation(int id)
    {
        return await _locationService.DeleteLocation(id);
    }

    [HttpPut("{id}")]
    public async Task<LocationDTO> EditLocation(int id, EditLocationRequest request)
    {
        return await _locationService.EditLocation(id, request);
    }
}