using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.enums;
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
        Event events = DbContext.Events.ToList().Where(e => e.Id == id).ToList().First();
        return events;
    }
    
    // GetEventsByLocation
    [HttpGet("geteventsbylocation/{locationIds}")]
    public async Task<List<Event>> GetEventsByLocation(List<int> locationIds)
    {
        if (locationIds.Count == 0)
        {
            return DbContext.Events.ToList();
        }
        else
        {
            return DbContext.Events.ToList().Where(e => locationIds.Contains(e.LocationId)).ToList();
        }
    }
    
    // CreateEvent
    [HttpPost("createevent")]
    public async Task<Event> CreateEvent(Event createEventRequest)
    {
        Event newEvent = createEventRequest;
        DbContext.Events.Add(newEvent);
        DbContext.SaveChanges();
        return newEvent;
    }
    
    // DeleteEvent
    [HttpGet("deleteevent/{id}")]
    public async Task<Event> DeleteEvent(int id)
    {
        Event events = DbContext.Events.ToList().Where(e => e.Id == id).ToList().First();
        DbContext.Events.Remove(events);
        DbContext.SaveChanges();
        return events;
    }
    
    //EditEvent
    [HttpPut("editevent/{id}")]
    public async Task<Event> EditEvent(int id, Event EditEventRequest)
    {
        Event eventToUpdate = await DbContext.Events.FindAsync(id);
        if (eventToUpdate != null)
        {
            eventToUpdate.Name = EditEventRequest.Name;
            eventToUpdate.Type = EditEventRequest.Type;
            eventToUpdate.LocationId = EditEventRequest.LocationId;
            eventToUpdate.Price = EditEventRequest.Price;

            await DbContext.SaveChangesAsync();
        }

        return eventToUpdate;
    }
    
}