using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Services.EventService;
using PartyWebAppServer.Services.JwtService;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController(IServerEventService eventService, IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
{
    private readonly HttpContext httpContext = httpContextAccessor.HttpContext;

    [HttpGet()]
    public async Task<List<EventDTO>> GetAllEvents()
    {
        if (!jwtService.IsAuthorized(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be authorized.");

        return await eventService.GetAllEvents();
    }

    [HttpGet("{id}")]
    public async Task<EventDTO> GetEventById(int id)
    {
        if (!jwtService.IsAuthorized(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be authorized.");

        return await eventService.GetEventById(id);
    }

    [HttpGet("bylocationids")]
    public async Task<List<EventDTO>> GetEventByLocationIds([FromQuery] EventsByLocationRequest request)
    {
        if (!jwtService.IsAuthorized(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be authorized.");

        return await eventService.GetEventByLocationIds(request);
    }

    [HttpPost()]
    public async Task<EventDTO> CreateEvent(CreateEventRequest request)
    {
        if (!jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin to create an event.");

        return await eventService.CreateEvent(request);
    }

    [HttpPut("{id}")]
    public async Task<EventDTO> EditEvent(EditEventRequest request, int id)
    {
        if (!jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin to edit an event.");

        return await eventService.EditEvent(request, id);
    }

    [HttpDelete("{id}")]
    public async Task<EventDTO> DeleteEvent(int id)
    {
        if (!jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin to delete an event.");

        return await eventService.DeleteEvent(id);
    }
}