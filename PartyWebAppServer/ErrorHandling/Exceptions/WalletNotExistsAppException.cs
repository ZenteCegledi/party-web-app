namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class WalletNotExistsAppException : Exception
{
    public WalletNotExistsAppException() : base("Wallet not found")
    {
    }

    public WalletNotExistsAppException(string message) : base(message)
    {
    }

    public WalletNotExistsAppException(string message, Exception innerException) : base(message, innerException)
    {
    }
}