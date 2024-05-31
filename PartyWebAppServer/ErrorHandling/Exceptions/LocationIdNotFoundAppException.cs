using System.Net;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class LocationIdNotFoundAppException : AppException
{
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }

    public LocationIdNotFoundAppException(int? id)
    {
        Message = $"Location with id {id} not found";
        HttpStatusCode = HttpStatusCode.BadRequest;
        ErrorObject = new LocationIdNotFoundErrorModel { Id = id };
    }
    
}
