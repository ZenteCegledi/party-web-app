﻿using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.ErrorHandling.ErrorModels;

public class WalletNotFoundModel
{
    public string Username { get; set; }
    public CurrencyType Currency { get; set; }
}