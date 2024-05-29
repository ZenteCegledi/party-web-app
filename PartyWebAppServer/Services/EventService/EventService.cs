using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.ErrorHandling.Exceptions;

namespace PartyWebAppServer.Services.EventService;

public class ServerEventService(AppDbContext dbContext, IMapper mapper) : IServerEventService
{
    //GetAllEvents
    public async Task<List<EventDTO>> GetAllEvents()
    {
        List<Event> events = dbContext.Events.ToList();
        return mapper.Map<List<EventDTO>>(events);
    }

    //GetEventById
    public async Task<EventDTO> GetEventById(int id)
    {
        Event eventItem = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);
        if (eventItem == null)
        {
            throw new EventIdNotFoundAppException(id);
        }

        return mapper.Map<EventDTO>(eventItem);
    }

    //GetEventByLocationIds
    public async Task<List<EventDTO>> GetEventByLocationIds(EventsByLocationRequest request)
    {
        List<Event> events;
        if (request.LocationIds.Count > 0)
        {
            events = await dbContext.Events
                .Where(e => request.LocationIds.Contains(e.LocationId))
                .ToListAsync();
        }
        else
        {
            events = dbContext.Events.ToList();
        }
        return mapper.Map<List<EventDTO>>(events);
    }

    //CreateEvent
    public async Task<EventDTO> CreateEvent(CreateEventRequest request)
    {
        if (!Enum.IsDefined(typeof(EventType), request.Type))
        {
            throw new EventTypeDoesNotExistAppException((int)request.Type);
        }
        Event newEvent = new Event()
        {
            Name = request.Name,
            Type = (EventType)request.Type,
            LocationId = (int)request.LocationId,
            Price = (int)request.Price
        };

        dbContext.Events.Add(newEvent);
        await dbContext.SaveChangesAsync();
        return mapper.Map<EventDTO>(newEvent);
    }

    //EditEvent
    public async Task<EventDTO> EditEvent(EditEventRequest request, int id)
    {
        Event eventToUpdate = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);

        if (eventToUpdate == null)
        {
            throw new EventIdNotFoundAppException(id);
        }

        if (!Enum.IsDefined(typeof(EventType), request.Type))
        {
            throw new EventTypeDoesNotExistAppException((int)request.Type);
        }

        if (request.Name != null)
        {
            eventToUpdate.Name = request.Name;
        }
        if (request.Type != null)
        {
            eventToUpdate.Type = (EventType)request.Type;
        }
        if (request.LocationId != null)
        {
            eventToUpdate.LocationId = (int)request.LocationId;
        }
        if (request.Price != null)
        {
            eventToUpdate.Price = (int)request.Price;
        }

        await dbContext.SaveChangesAsync();

        return mapper.Map<EventDTO>(eventToUpdate);
    }

    //DeleteEvent
    public async Task<EventDTO> DeleteEvent(int id)
    {
        Event eventToDelete = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);
        if (eventToDelete == null)
        {
            throw new EventIdNotFoundAppException(id);
        }

        dbContext.Events.Remove(eventToDelete);
        dbContext.SaveChanges();

        return mapper.Map<EventDTO>(eventToDelete);
    }
}