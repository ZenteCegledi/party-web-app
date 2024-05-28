﻿using System.Net;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class UserHasNoWalletAppException : AppException
{
    public UserHasNoWalletAppException(UserDto user, WalletDto wallet)
    {
        Message = $"User '{user.Username}' has no wallet with currency type '{wallet.Currency.ToString()}'";
        ErrorObject = new UserHasNoWalletErrorModel{Username = user.Username, Currency = wallet.Currency};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}