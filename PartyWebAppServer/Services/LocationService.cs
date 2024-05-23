using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Services;

[ApiController]
[Route("api/locations")]
public class LocationService
{

    private AppDbContext DbContext { get; set; }

    public LocationService(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    [HttpGet("{id}")]
    public async Task<Location> GetLocation(int id)
    {
        if(id != null && DbContext.Locations.Find(id) != null)
            return DbContext.Locations.Find(id);
        throw new Exception();
    }

    [HttpGet()]
    public async Task<List<Location>> GetLocations()
    {
        return DbContext.Locations.ToList();
    }

    [HttpPost()]
    public async Task<Location> CreateLocation(CreateLocationRequest request)
    {
        Location location = new Location
        {
            Name = request.Name,
            Address = request.Address,
            Type = request.Type
        };
        DbContext.Locations.Add(location);
        DbContext.SaveChanges();
        return location;
    }

    [HttpDelete("{id}")]
    public async Task<Location> DeleteLocation(int id)
    {
        if (id != null && DbContext.Locations.Find(id) != null)
        {
            Location location = DbContext.Locations.Find(id);
            DbContext.Locations.Remove(location);
            DbContext.SaveChanges();
            return location;
        }

        throw new Exception();
    }

    [HttpPut("{id}")]
    public async Task<Location> EditLocation(int id, EditLocationRequest request)
    {
        if (id != null && DbContext.Locations.Find(id) != null)
        {
            Location location = DbContext.Locations.Find(id);

            if (!String.IsNullOrEmpty(request.Name))
                location.Name = request.Name;
            if (!String.IsNullOrEmpty(request.Address))
                location.Address = request.Address;
            if (Enum.IsDefined(request.Type))
                location.Type = request.Type;

            DbContext.SaveChanges();
            return location;
        }

        throw new Exception();

    }
}