using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;

namespace PartyWebAppClient.Services.EventService;

public class EventService(IAppHttpClient http) : IEventService
{
    
    public async Task<(List<EventDTO>?, AppErrorModel?)> GetEvents() =>
        await http.GetAsync<List<EventDTO>>("http://localhost:5259/api/event");
    
    public async Task<(EventDTO?, AppErrorModel?)> GetEventById(GetEventRequest req) =>
        await http.GetAsync<EventDTO>($"http://localhost:5259/api/event/{req.Id}");
    
    
    
}