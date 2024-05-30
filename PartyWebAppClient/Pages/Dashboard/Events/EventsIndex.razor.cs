using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using PartyWebAppClient.Services.EventService;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppClient.Pages.Dashboard.Events;

public partial class EventsIndex : ComponentBase
{
    [Inject]
    private IEventService EventService { get; set; }
    
    [Inject]
    private IToastService? ToastService { get; set; }
    
    private List<EventDTO> Events { get; set; } = new List<EventDTO>();
    private List<EventDTO> FutureEvents { get; set; } = new List<EventDTO>();
    private List<EventDTO> CurrentEvents { get; set; } = new List<EventDTO>();
    private List<EventDTO> PastEvents { get; set; } = new List<EventDTO>();
    

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
            foreach (var item in Events)
            {
                if (DateTime.Now < item.StartDateTime)
                {
                    FutureEvents.Add(item);
                }
                else if (DateTime.Now > item.EndDateTime)
                {
                    PastEvents.Add(item);
                }
                else
                {
                    CurrentEvents.Add(item);
                }
            }
            
        }
    }
}