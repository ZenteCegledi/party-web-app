using PartyWebAppServer.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;

namespace PartyWebAppServer.Services.EventService;

public interface IEventService
{
    public Task<List<EventDTO>> GetAllEvents();
    public Task<List<EventDTO>> GetAllEventsWithLocations();
    public Task<EventDTO> GetEventById(GetEventRequest request);
    public Task<List<EventDTO>> GetEventByLocationIds(EventsByLocationRequest request);
    public Task<EventDTO> CreateEvent(CreateEventRequest request);
    public Task<EventDTO> EditEvent(EditEventRequest request, int id);
    public Task<EventDTO> DeleteEvent(int id);
}

