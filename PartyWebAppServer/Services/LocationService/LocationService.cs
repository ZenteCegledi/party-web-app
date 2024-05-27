using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.enums;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.ErrorHandling;
using PartyWebAppServer.ErrorHandling.Exceptions;

namespace PartyWebAppServer.Services;
public class LocationService : ILocationService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public LocationService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<LocationDTO> GetLocation(int id)
    {
        var location = await _dbContext.Locations.FirstOrDefaultAsync(l => l.Id == id);
        if (location == null)
            throw new LocationIdNotFoundAppException(id);
        return _mapper.Map<LocationDTO>(location);
    }

    public async Task<List<LocationDTO>> GetLocations()
    {
        List<LocationDTO> locationDtos = new List<LocationDTO>();
        await _dbContext.Locations.ForEachAsync(l => locationDtos.Add(_mapper.Map<LocationDTO>(l)));
        return locationDtos;
    }

    public async Task<LocationDTO> CreateLocation(CreateLocationRequest request)
    {
        if(!Enum.IsDefined(typeof(LocationType), request.Type))
            throw new LocationTypeDoesNotExistAppException(Convert.ToInt32(request.Type));
        
        await _dbContext.Locations.ForEachAsync(l =>
        {
            if (l.Name == request.Name && l.Address == request.Address)
                throw new LocationAlreadyExistsAppException(request.Name, request.Address);
        });
        
        Location location = new Location
        {
            Name = request.Name,
            Address = request.Address,
            Type = request.Type
        };
        _dbContext.Locations.Add(location);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<LocationDTO>(location);
    }

    public async Task<LocationDTO> DeleteLocation(int id)
    {
        var location = await _dbContext.Locations.FirstOrDefaultAsync(l => l.Id == id);
        if (location == null)
            throw new LocationIdNotFoundAppException(id);
        
        _dbContext.Locations.Remove(location);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<LocationDTO>(location);
    }

    public async Task<LocationDTO> EditLocation(int id, EditLocationRequest request)
    {
        var location = await _dbContext.Locations.FirstOrDefaultAsync(l => l.Id == id);
        if (location == null)
            throw new LocationIdNotFoundAppException(id);
        
        await _dbContext.Locations.ForEachAsync(l =>
        {
            if (l.Name == request.Name && l.Address == request.Address)
                throw new LocationAlreadyExistsAppException(request.Name, request.Address);
        });
        
        if (!string.IsNullOrEmpty(request.Name))
            location.Name = request.Name;
        
        location.Address = request.Address;

        if (request.Type != null)
        {
            if(Enum.IsDefined(typeof(LocationType), request.Type))
                location.Type = request.Type;
            else
                throw new LocationTypeDoesNotExistAppException(Convert.ToInt32(request.Type));
        }

        await _dbContext.SaveChangesAsync();
        return _mapper.Map<LocationDTO>(location);
    }
}
