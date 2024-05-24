using PartyWebAppServer.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartyWebAppServer.Services.EventService;
public interface IEventService
{
    Task<IEnumerable<Event>> GetAllEventsAsync();
    Task<Event> GetEventByIdAsync(string id);
    Task<Event> CreateEventAsync(Event newEvent);
    Task UpdateEventAsync(Event updatedEvent);
}