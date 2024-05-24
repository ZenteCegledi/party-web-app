using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.Services.EventService;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/events")]
public class EventController
{
    private IEventService EventService { get; set; }

    public EventController(IEventService eventService)
    {
        EventService = eventService;
    }
    
    [HttpGet()]
    public async Task<List<Event>> GetAllEvents()
    {
        return await EventService.GetAllEvents();
    }
    
    [HttpGet("{id}")]
    public async Task<Event> GetEventById(int id)
    {
        return await EventService.GetEventById(id);
    }
    
    [HttpGet("findbylocationids")]
    public async Task<List<Event>> GetEventByLocationIds(EventsByLocationRequest request)
    {
        return await EventService.GetEventByLocationIds(request);
    }
    
    [HttpPost()]
    public async Task<Event> CreateEvent(CreateEventRequest request)
    {
        return await EventService.CreateEvent(request);
    }
    
    [HttpPut()]
    public async Task<Event> EditEvent(EditEventRequest request)
    {
        return await EventService.EditEvent(request);
    }
    
    [HttpDelete("{id}")]
    public async Task<Event> DeleteEvent(int id)
    {
        return await EventService.DeleteEvent(id);
    }
}