﻿using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.enums;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/events")]
public class EventService
{
    private AppDbContext DbContext { get; set; }
    
    public EventService(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }
    
    [HttpGet("{id}")]
    public async Task<Event> GetEventById(int id)
    {
        Event events = DbContext.Events.ToList().Where(e => e.Id == id).ToList().First();
        return events;
    }
    
    // GetEventsByLocation
    [HttpGet()]
    public async Task<List<Event>> GetEventsByLocation()
    {

        return new List<Event>();
    }
    

    [HttpPost()]
    public async Task<Event> CreateEvent(Event createEventRequest)
    {
        Event newEvent = createEventRequest;
        DbContext.Events.Add(newEvent);
        DbContext.SaveChanges();
        return newEvent;
    }
    
    // DeleteEvent
    [HttpGet("DeleteEvent/{id}")]
    public async Task<Event> DeleteEvent(int id)
    {
        Event events = DbContext.Events.ToList().Where(e => e.Id == id).ToList().First();
        DbContext.Events.Remove(events);
        DbContext.SaveChanges();
        return events;
    }
    
    //EditEvent
    [HttpPost("EditEvent/{id}")]
    public async Task<Event> EditEvent(int id, Event EditEventRequest)
    {
        Event events = DbContext.Events.ToList().Where(e => e.Id == id).ToList().First();
        events = EditEventRequest;
        return events;
    }
    
}