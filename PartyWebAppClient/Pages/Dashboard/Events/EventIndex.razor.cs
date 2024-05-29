using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using PartyWebAppClient.Services.EventService;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppClient.Pages.Dashboard.Events;

public partial class EventIndex : ComponentBase
{
    [Inject]
    private IEventService EventService { get; set; }
    
    [Inject]
    private IToastService? ToastService { get; set; }
    
    private List<EventDTO> Events { get; set; } = new List<EventDTO>();

    protected override async Task OnInitializedAsync()
    {
        var (events, error) = await EventService.GetEvents();
        if (error != null)
        {
            ToastService.ShowError("Can't get events: " + error.Message);
            Console.WriteLine(error.Message);
        }
        else
        {
            Events = events;
        }
    }
}