namespace PartyWebAppServer.ErrorHandling;

public class AppError<T>
{
    public string Id { get; set; }
    public string Message { get; set; }
    public string DetailedMessage { get; set; }
    public T ErrorObject { get; set; }
}