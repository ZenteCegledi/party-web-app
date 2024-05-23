using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
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
    public async Task<List<EventDTO>> GetAllEvents()
    {
        return await EventService.GetAllEvents();
    }
    
    [HttpGet("{id}")]
    public async Task<EventDTO> GetEventById(int id)
    {
        return await EventService.GetEventById(id);
    }
    
    [HttpGet("bylocationids")]
    public async Task<List<EventDTO>> GetEventByLocationIds([FromQuery]EventsByLocationRequest request)
    {
        return await EventService.GetEventByLocationIds(request);
    }
    
    [HttpPost()]
    public async Task<EventDTO> CreateEvent(CreateEventRequest request)
    {
        return await EventService.CreateEvent(request);
    }
    
    [HttpPut()]
    public async Task<EventDTO> EditEvent(EditEventRequest request)
    {
        return await EventService.EditEvent(request);
    }
    
    [HttpDelete("{id}")]
    public async Task<EventDTO> DeleteEvent(int id)
    {
        return await EventService.DeleteEvent(id);
    }
}