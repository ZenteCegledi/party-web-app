﻿using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.ErrorHandling.ErrorModels;

public class UserHasNoWalletErrorModel
{
    public string Username { get; set; }
    public CurrencyType Currency { get; set; }
}