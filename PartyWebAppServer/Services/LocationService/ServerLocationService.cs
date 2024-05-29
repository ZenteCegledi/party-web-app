using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.ErrorHandling.Exceptions;

namespace PartyWebAppServer.Services.LocationService;

public class ServerLocationService(AppDbContext _dbContext, IMapper _mapper) : IServerLocationService
{
    public async Task<LocationDTO> GetLocation(int id)
    {
        var location = await _dbContext.Locations.FirstOrDefaultAsync(l => l.Id == id);
        if (location == null)
            throw new LocationIdNotFoundAppException(id);
        return _mapper.Map<LocationDTO>(location);
    }

    public async Task<List<LocationDTO>> GetLocations()
    {
        return _mapper.Map<List<LocationDTO>>(await _dbContext.Locations.ToListAsync());
    }

    public async Task<LocationDTO> CreateLocation(CreateLocationRequest request)
    {
        if (!Enum.IsDefined(typeof(LocationType), request.Type))
            throw new LocationTypeDoesNotExistAppException(Convert.ToInt32(request.Type));

        Location? existingLocation = await _dbContext.Locations.FirstOrDefaultAsync(l => l.Name == request.Name && l.Address == request.Address);

        if (existingLocation != null)
            throw new LocationAlreadyExistsAppException(request.Name, request.Address);

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

        Location? existingLocation = await _dbContext.Locations.FirstOrDefaultAsync(l => l.Name == request.Name && l.Address == request.Address);

        if (existingLocation != null && existingLocation.Id != id)
            throw new LocationAlreadyExistsAppException(request.Name, request.Address);

        if (!string.IsNullOrEmpty(request.Name))
            location.Name = request.Name;

        location.Address = request.Address;

        if (request.Type != null)
        {
            if (Enum.IsDefined(typeof(LocationType), request.Type))
                location.Type = request.Type;
            else
                throw new LocationTypeDoesNotExistAppException(Convert.ToInt32(request.Type));
        }

        await _dbContext.SaveChangesAsync();
        return _mapper.Map<LocationDTO>(location);
    }
}
