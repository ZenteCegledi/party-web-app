using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.Services.EventService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.enums;
using PartyWebAppServer.ErrorHandling.Exceptions;

namespace PartyWebAppServer.Services.EventService;

public class EventService(AppDbContext dbContext, IMapper mapper) : IEventService
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
        if (dbContext.Events.Where(e => e.Id == id).ToList().Count == 0)
        {
            throw new EventIdNotFoundAppException(id);
        }
        Event eventItem = dbContext.Events.Where(e => e.Id == id).FirstOrDefault();
        
        return mapper.Map<EventDTO>(eventItem);
    }
    
    //GetEventByLocationIds
    public async Task<List<EventDTO>> GetEventByLocationIds([FromQuery]EventsByLocationRequest request)
    {
        List<Event> events;
        if (request.LocationIds.Count > 0)
        {
            events = await dbContext.Events
                .Where(e => request.LocationIds.Contains(e.LocationId))
                .ToListAsync();

            List<EventDTO> eventDto = events.Select(e => new EventDTO()
            {
                Id = e.Id,
                Name = e.Name,
                Type = e.Type,
                LocationId = e.LocationId,
                Price = e.Price
            }).ToList();

            return eventDto;
        }
        events = dbContext.Events.ToList().Where(e => request.LocationIds.Contains(e.LocationId)).ToList();
        return mapper.Map<List<EventDTO>>(events);
    }
    
    //CreateEvent
    public async Task<EventDTO> CreateEvent(CreateEventRequest request)
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
        
        dbContext.Events.Add(newEvent);
        await dbContext.SaveChangesAsync();
        return mapper.Map<EventDTO>(newEvent);
    }
    
    //EditEvent
    public async Task<EventDTO> EditEvent(EditEventRequest request)
    {
        Event eventToUpdate = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == request.Id);

        if (eventToUpdate == null)
        {
            throw new EventIdNotFoundAppException(request.Id);
        }

        if (!Enum.IsDefined(typeof(EventType), request.Type))
        {
            throw new EventTypeDoesNotExistAppException(request.Type);
        }

        eventToUpdate.Name = request.Name;
        eventToUpdate.Type = (EventType)request.Type;
        eventToUpdate.LocationId = (int)request.LocationId;
        eventToUpdate.Price = (int)request.Price;

        await dbContext.SaveChangesAsync();

        return mapper.Map<EventDTO>(eventToUpdate);
    }

    //DeleteEvent
    public async Task<EventDTO> DeleteEvent(int id)
    {
        if (dbContext.Events.ToList().Where(e => e.Id == id).ToList().Count == 0)
        {
            throw new EventIdNotFoundAppException(id);
        }
        Event eventToDelete = dbContext.Events.ToList().Where(e => e.Id == id).FirstOrDefault();
        if (eventToDelete != null)
        {
            dbContext.Events.Remove(eventToDelete);
            dbContext.SaveChanges();
        }
        return mapper.Map<EventDTO>(eventToDelete);
    }
}