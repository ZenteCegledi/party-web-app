using System.Net;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.Controllers;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class LocationTypeDoesNotExistAppException : AppException
{
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }

    public LocationTypeDoesNotExistAppException(int? type)
    {
        Message = $"Location type {type} does not exist";
        HttpStatusCode = HttpStatusCode.BadRequest;
        ErrorObject = new LocationTypeDoesNotExistErrorModel { TypeId = (int)type };
    }
}