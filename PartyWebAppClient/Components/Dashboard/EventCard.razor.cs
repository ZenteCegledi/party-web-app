using Microsoft.AspNetCore.Components;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppClient.Components.Dashboard;

public partial class EventCard : ComponentBase
{
    [Parameter]
    public EventDTO Event { get; set; } = new EventDTO();
}