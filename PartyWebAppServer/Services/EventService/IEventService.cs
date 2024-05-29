using PartyWebAppServer.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;

namespace PartyWebAppServer.Services.EventService;

public interface IEventService
{
    public Task<List<EventDto>> GetAllEvents();
    public Task<EventDto> GetEventById(int id);
    public Task<List<EventDto>> GetEventByLocationIds(EventsByLocationRequest request);
    public Task<EventDto> CreateEvent(CreateEventRequest request);
    public Task<EventDto> EditEvent(EditEventRequest request, int id);
    public Task<EventDto> DeleteEvent(int id);
}

