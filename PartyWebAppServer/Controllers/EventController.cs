using Microsoft.AspNetCore.Mvc;
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
    
    /*
    [HttpGet("findall")]
    public async Task<List<Event>> FindAllEvents()
    {
        List<Event> events = EventService.GetAllEvents();
        return events;
    }*/
    
    
    
   
}