using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.ErrorHandling.Exceptions;

namespace PartyWebAppServer.Services.EventService;

public class EventService(AppDbContext dbContext, IMapper mapper) : IEventService
{
    //GetAllEvents
    public async Task<List<EventDto>> GetAllEvents()
    {
        var events = dbContext.Events.ToList();
        return mapper.Map<List<EventDto>>(events);
    }

    //GetEventById
    public async Task<EventDto> GetEventById(int id)
    {
        var eventItem = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);
        if (eventItem == null)
        {
            throw new EventIdNotFoundAppException(id);
        }

        return mapper.Map<EventDto>(eventItem);
    }

    //GetEventByLocationIds
    public async Task<List<EventDto>> GetEventByLocationIds(EventsByLocationRequest request)
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
        return mapper.Map<List<EventDto>>(events);
    }

    //CreateEvent
    public async Task<EventDto> CreateEvent(CreateEventRequest request)
    {
        if (!Enum.IsDefined(typeof(EventType), request.Type))
        {
            throw new EventTypeDoesNotExistAppException((int)request.Type);
        }
        Event newEvent = new Event
        {
            Name = request.Name,
            Type = request.Type,
            LocationId = (int)request.LocationId,
            Price = request.Price
        };

        dbContext.Events.Add(newEvent);
        await dbContext.SaveChangesAsync();
        return mapper.Map<EventDto>(newEvent);
    }

    //EditEvent
    public async Task<EventDto> EditEvent(EditEventRequest request, int id)
    {
        var eventToUpdate = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);

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
        return mapper.Map<EventDto>(eventToUpdate);
    }

    //DeleteEvent
    public async Task<EventDto> DeleteEvent(int id)
    {
        var eventToDelete = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);
        if (eventToDelete == null)
        {
            throw new EventIdNotFoundAppException(id);
        }

        dbContext.Events.Remove(eventToDelete);
        await dbContext.SaveChangesAsync();

        return mapper.Map<EventDto>(eventToDelete);
    }
}