using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppClient.Services.EventService;

public interface IEventService
{
    public Task<(List<EventDTO>, AppErrorModel)> GetEvents();
}