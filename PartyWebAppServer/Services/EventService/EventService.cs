using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.Services.EventService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PartyWebAppCommon.enums;
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

    //GetAllEvents
    [HttpGet("findall")]
    public async Task<List<Event>> GetAllEvents()
    {
        List<Event> events = DbContext.Events.ToList();
        return events;
    }

    //GetEventById
    [HttpGet("{id}")]
    public async Task<Event> GetEventById(int id)
    {
        if (DbContext.Events.ToList().Where(e => e.Id == id).ToList().Count == 0)
        {
            throw new EventIdNotFoundAppException(id);
        }
        Event eventItem = DbContext.Events.ToList().Where(e => e.Id == id).FirstOrDefault();
        return eventItem;
    }

    //CreateEvent
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
        if (!Enum.IsDefined(typeof(EventType), newEvent.Type))
        {
            throw new EventTypeDoesNotExistAppException(newEvent.Type);
        }
        
        DbContext.Events.Add(newEvent);
        await DbContext.SaveChangesAsync();
        return newEvent;
    }
    
    //EditEvent
    [HttpPut("{id}")]
    public async Task<Event> EditEvent(EditEventRequest request)
    {
        if (DbContext.Events.ToList().Where(e => e.Id == request.Id).ToList().Count == 0)
        {
            throw new EventIdNotFoundAppException(request.Id);
        }

        if (!Enum.IsDefined(typeof(EventType), request.Type))
        {
            throw new EventTypeDoesNotExistAppException(request.Type);
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
            throw new EventIdNotFoundAppException(request.Id);
        }
    }

    //DeleteEvent
    [HttpDelete("delete/{id}")]
    public async Task<Event> DeleteEvent(int id)
    {
        if (DbContext.Events.ToList().Where(e => e.Id == id).ToList().Count == 0)
        {
            throw new EventIdNotFoundAppException(id);
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