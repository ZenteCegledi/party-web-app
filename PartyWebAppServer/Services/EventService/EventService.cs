using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.Services.EventService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PartyWebAppServer.ErrorHandling.Exceptions;

namespace PartyWebAppServer.Services.EventService;

[ApiController]
[Route("api/events")]
public class EventService : IEventService
{
    private AppDbContext DbContext { get; set; }

    public EventService(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<IEnumerable<Event>> GetAllEventsAsync()
    {
        return DbContext.Events.ToList();
        if (DbContext.Events.ToList().Where(e => e.Id == id).ToList().Count == 0)
        {
            throw new EventIdNotFoundException(id);
        }
        Event events = DbContext.Events.ToList().Where(e => e.Id == id).First();
        return events;
    }

    public async Task<Event> GetEventByIdAsync(string id)
    {
        int eventId = int.Parse(id);
        Event eventItem = DbContext.Events.ToList().Where(e => e.Id == eventId).FirstOrDefault();
        if (eventItem == null)
        {
            throw new EventIdNotFoundException(eventId);
        }
        return eventItem;
    }

    public async Task<Event> CreateEventAsync(Event newEvent)
    {
        DbContext.Events.Add(newEvent);
        await DbContext.SaveChangesAsync();
        return newEvent;
    }

    public async Task UpdateEventAsync(Event updatedEvent)
    {
        Event eventToUpdate = await DbContext.Events.FindAsync(updatedEvent.Id);
        if (DbContext.Events.ToList().Where(e => e.Id == id).ToList().Count == 0)
        {
        }
            throw new EventIdNotFoundException(id);
        Event events = DbContext.Events.ToList().Where(e => e.Id == id).ToList().First();
        DbContext.Events.Remove(events);
        await DbContext.SaveChangesAsync();
        return events;
    }
    
    //EditEvent
    [HttpPut("edit/")]
    public async Task<Event> EditEvent(EditEventRequest request)
    {
        if (DbContext.Events.ToList().Where(e => e.Id == request.Id).ToList().Count == 0)
        {
            throw new EventIdNotFoundException(request.Id);
        }
        Event eventToUpdate = await DbContext.Events.FindAsync(request.Id);
        if (eventToUpdate != null)
        {
            eventToUpdate.Name = updatedEvent.Name;
            eventToUpdate.Type = updatedEvent.Type;
            eventToUpdate.LocationId = updatedEvent.LocationId;
            eventToUpdate.Price = updatedEvent.Price;
            await DbContext.SaveChangesAsync();
        }
        else
        {
            throw new EventIdNotFoundException(updatedEvent.Id);
        }
    }

    public async Task DeleteEventAsync(string id)
    {
        int eventId = int.Parse(id);
        Event eventToDelete = DbContext.Events.ToList().Where(e => e.Id == eventId).FirstOrDefault();
        if (eventToDelete != null)
        {
            DbContext.Events.Remove(eventToDelete);
            DbContext.SaveChanges();
        }
        else
        {
            throw new EventIdNotFoundException(eventId);
        }
    }
}