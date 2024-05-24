using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.enums;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.ErrorHandling;
using PartyWebAppServer.ErrorHandling.Exceptions;

namespace PartyWebAppServer.Services;
public class LocationService : ILocationService
{
    private AppDbContext DbContext { get; set; }

    public LocationService(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<Location> GetLocation(int id)
    {
        Location location = await DbContext.Locations.FindAsync(id);
        if (location == null)
            throw new LocationIdNotFoundAppException(id);
        return location;
    }

    public async Task<List<Location>> GetLocations()
    {
        return await DbContext.Locations.ToListAsync();
    }

    public async Task<Location> CreateLocation(CreateLocationRequest request)
    {
        if(!Enum.IsDefined(typeof(LocationType), request.Type))
            throw new LocationTypeDoesNotExistAppException(request.Type);
        
        Location location = new Location
        {
            Name = request.Name,
            Address = request.Address,
            Type = request.Type
        };
        DbContext.Locations.Add(location);
        await DbContext.SaveChangesAsync();
        return location;
    }

    public async Task<Location> DeleteLocation(int id)
    {
        Location location = await DbContext.Locations.FindAsync(id);
        if (location == null)
            throw new LocationIdNotFoundAppException(id);
        
        DbContext.Locations.Remove(location);
        await DbContext.SaveChangesAsync();
        return location;
    }

    public async Task<Location> EditLocation(int id, EditLocationRequest request)
    {
        Location location = await DbContext.Locations.FindAsync(id);
        if (location == null)
            throw new LocationIdNotFoundAppException(id);
        
        if (!string.IsNullOrEmpty(request.Name))
            location.Name = request.Name;
        if (!string.IsNullOrEmpty(request.Address))
            location.Address = request.Address;

        if (request.Type != null)
        {
            if(Enum.IsDefined(typeof(LocationType), request.Type))
                location.Type = request.Type;
            else
                throw new LocationTypeDoesNotExistAppException(request.Type);
        }

        await DbContext.SaveChangesAsync();
        return location;
    }
}
