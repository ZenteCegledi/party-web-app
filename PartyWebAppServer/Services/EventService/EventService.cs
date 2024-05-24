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

    public async Task<List<Event>> GetAllEvents()
    {
        List<Event> events = DbContext.Events.ToList();
        return events;
    }

    public async Task<Event> GetEventById(int id)
    {
        if (DbContext.Events.ToList().Where(e => e.Id == id).ToList().Count == 0)
        {
            throw new EventIdNotFoundException(id);
        }
        Event eventItem = DbContext.Events.ToList().Where(e => e.Id == id).FirstOrDefault();
        return eventItem;
    }

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
        await DbContext.SaveChangesAsync();
        return newEvent;
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
            eventToUpdate.Name = request.Name;
            eventToUpdate.Type = request.Type;
            eventToUpdate.LocationId = request.LocationId;
            eventToUpdate.Price = request.Price;
            await DbContext.SaveChangesAsync();
            return eventToUpdate;
        }
        else
        {
            throw new EventIdNotFoundException(request.Id);
        }
    }

    public async Task<Event> DeleteEvent(int id)
    {
        if (DbContext.Events.ToList().Where(e => e.Id == id).ToList().Count == 0)
        {
            throw new EventIdNotFoundException(id);
        }
        Event eventToDelete = DbContext.Events.ToList().Where(e => e.Id == id).FirstOrDefault();
        if (eventToDelete != null)
        {
            DbContext.Events.Remove(eventToDelete);
            DbContext.SaveChanges();
        }
        return eventToDelete;
    }
}