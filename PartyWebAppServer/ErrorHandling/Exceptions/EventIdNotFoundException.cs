﻿using System.Net;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class EventIdNotFoundException : AppException
{
        public override string Message { get; }
        
        public override HttpStatusCode HttpStatusCode { get; }

        public EventIdNotFoundException(int eventId)
        {
            Message = $"Event with id {eventId} not found";
            HttpStatusCode = HttpStatusCode.BadRequest;
            ErrorObject = new EventIdNotFoundErrorModel { Id = eventId };
        }
        
}