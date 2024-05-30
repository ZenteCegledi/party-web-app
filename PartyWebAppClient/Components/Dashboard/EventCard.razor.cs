using Microsoft.AspNetCore.Components;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppClient.Components.Dashboard;

public partial class EventCard : ComponentBase
{
    [Parameter]
    public EventDTO Event { get; set; } = new EventDTO();
    
    protected override async Task OnInitializedAsync()
    {
        Console.WriteLine("EventCard initialized");
        Console.WriteLine(Event.Name);
    }
}