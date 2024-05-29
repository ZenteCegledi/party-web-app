using PartyWebAppClient.Models;

namespace PartyWebAppClient.Services;

public interface IAppHttpClient
{
    Task<(T?, AppErrorModel?)> SendAsync<T>(HttpRequestMessage message);
    Task<(T?, AppErrorModel?)> GetWithoutAuthAsync<T>(string query);
    Task<(T?, AppErrorModel?)> GetAsync<T>(string query);
    Task<(T?, AppErrorModel?)> PutAsync<T>(string query, object? content = null);
    Task<(byte[]?, AppErrorModel?)> PutAsync(string query, object? content = null);
    Task<(T?, AppErrorModel?)> PostAsync<T>(string query, object content);
    Task<(T?, AppErrorModel?)> PostWithoutAuthAsync<T>(string query, object? content = null);
    Task<(T?, AppErrorModel?)> PostAsync<T>(string query, HttpContent? content = null);
    Task<(byte[]?, AppErrorModel?)> PostAsync(string query, object? content = null);
    Task<(T?, AppErrorModel?)> DeleteAsync<T>(string query);
}
