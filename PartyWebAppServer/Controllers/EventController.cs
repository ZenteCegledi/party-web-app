using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.Services.EventService;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/events")]
public class EventController(IEventService eventService)
{
    [HttpGet()]
    public async Task<List<EventDTO>> GetAllEvents()
    {
        return await eventService.GetAllEvents();
    }
    
    [HttpGet("{id}")]
    public async Task<EventDTO> GetEventById(int id)
    {
        return await eventService.GetEventById(id);
    }
    
    [HttpGet("bylocationids")]
    public async Task<List<EventDTO>> GetEventByLocationIds([FromQuery]EventsByLocationRequest request)
    {
        return await eventService.GetEventByLocationIds(request);
    }
    
    [HttpPost()]
    public async Task<EventDTO> CreateEvent(CreateEventRequest request)
    {
        return await eventService.CreateEvent(request);
    }
    
    [HttpPut()]
    public async Task<EventDTO> EditEvent(EditEventRequest request)
    {
        return await eventService.EditEvent(request);
    }
    
    [HttpDelete("{id}")]
    public async Task<EventDTO> DeleteEvent(int id)
    {
        return await eventService.DeleteEvent(id);
    }
}