using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.enums;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/events")]
public class EventService
{
    private AppDbContext DbContext { get; set; }
    
    public EventService(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }
    
    // GetEventById
    [HttpGet("{id}")]
    public async Task<Event> GetEventById(int id)
    {
        Event events = DbContext.Events.ToList().Where(e => e.Id == id).First();
        return events;
    }
    
    // GetEventsByLocation
    [HttpGet("geteventsbylocation")]
    public async Task<List<Event>> GetEventsByLocation([FromQuery]EventsByLocationRequest request)
    {
        List<int> locationIds = request.LocationIds;
        if (locationIds.Count == 0 || locationIds[0] == 0)
        {
            return DbContext.Events.ToList();
        }
        else
        {
            return DbContext.Events.ToList().Where(e => locationIds.Contains(e.LocationId)).ToList();
        }
    }
    
    // CreateEvent
    [HttpPost("create")]
    public async Task<Event> CreateEvent(CreateEventRequest request)
    {
        Event newEvent = new Event()
        {
            Name = request.Name,
            Type = request.Type,
            LocationId = request.LocationId,
            Price = request.Price
        };
        DbContext.Events.Add(newEvent);
        DbContext.SaveChanges();
        return newEvent;
    }
    
    // DeleteEvent
    [HttpDelete("delete/{id}")]
    public async Task<Event> DeleteEvent(int id)
    {
        Event events = DbContext.Events.ToList().Where(e => e.Id == id).ToList().First();
        DbContext.Events.Remove(events);
        DbContext.SaveChanges();
        return events;
    }
    
    //EditEvent
    [HttpPut("edit/")]
    public async Task<Event> EditEvent(EditEventRequest request)
    {
        Event eventToUpdate = await DbContext.Events.FindAsync(request.Id);
        if (eventToUpdate != null)
        {
            if (request.Name != null)
            {
                eventToUpdate.Name = request.Name;
            }
            if (request.Type != 0 && request.Type != null)
            {
                eventToUpdate.Type = request.Type;
            }
            if (request.LocationId != 0 && request.LocationId != null)
            {
                eventToUpdate.LocationId = request.LocationId;
            }
            if (request.Price != 0 && request.Price != null)
            {
                eventToUpdate.Price = request.Price;
            }
            await DbContext.SaveChangesAsync();
        }

        return eventToUpdate;
    }
    
}