using PartyWebAppCommon.DTOs;

namespace PartyWebAppServer.ErrorHandling.ErrorModels;

public class UserNotExistsErrorModel
{
    public UserDto User { get; set; }
}