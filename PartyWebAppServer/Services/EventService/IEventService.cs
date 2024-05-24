using PartyWebAppServer.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.Requests;

namespace PartyWebAppServer.Services.EventService;
public interface IEventService
{
    public Task<List<Event>> GetAllEvents();
    public Task<Event> GetEventById(int id);
    public Task<List<Event>> GetEventByLocationIds([FromQuery]EventsByLocationRequest request);
    public Task<Event> CreateEvent(CreateEventRequest request);
    public Task<Event> EditEvent(EditEventRequest request);
    public Task<Event> DeleteEvent(int id);
}

