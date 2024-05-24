﻿using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.Services;
using PartyWebAppServer.Services.LocationService;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/locations")]
public class LocationController(ILocationService _locationService)
{

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