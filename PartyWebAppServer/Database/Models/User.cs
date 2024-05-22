﻿using System.ComponentModel.DataAnnotations;

namespace PartyWebAppServer.Database.Models;

public class User
{
    [Key]
    public string Username { get; set; }

    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }

    public List<Wallet> Wallets { get; set; }
}