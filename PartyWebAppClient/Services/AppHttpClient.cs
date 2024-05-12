using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using PartyWebAppClient.Models;

namespace PartyWebAppClient.Services;

public class AppHttpClient : IAppHttpClient
{
    private readonly HttpClient _httpClient;

    public AppHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(T?, AppErrorModel?)> SendAsync<T>(HttpRequestMessage message)
    {
        return await GetResponseMessage<T>(async () => await _httpClient.SendAsync(message));
    }

    public async Task<(T?, AppErrorModel?)> GetAsync<T>(string query)
    {
        return await GetResponseMessage<T>(async () => await _httpClient.GetAsync(query));
    }

    public async Task<(T?, AppErrorModel?)> GetWithoutAuthAsync<T>(string query)
    {
        return await GetResponseMessage<T>(async () => await _httpClient.GetAsync(query), false);
    }

    public async Task<(T?, AppErrorModel?)> PutAsync<T>(string query, object? content = null)
    {
        var stringContent = new StringContent(
            JsonConvert.SerializeObject(content),
            Encoding.UTF8, MediaTypeNames.Application.Json);
        return await GetResponseMessage<T>(async () => await _httpClient.PutAsync(query, stringContent));
    }

    public async Task<(byte[]?, AppErrorModel?)> PutAsync(string query, object? content = null)
    {
        try
        {
            var stringContent = new StringContent(
                JsonConvert.SerializeObject(content),
                Encoding.UTF8, MediaTypeNames.Application.Json);
            var responseMessage = await _httpClient.PostAsync(query, stringContent);
            if (responseMessage.IsSuccessStatusCode)
                return (await responseMessage.Content.ReadAsByteArrayAsync(), null);
            return (default, await responseMessage.Content.ReadFromJsonAsync<AppErrorModel>());
        }
        catch (Exception)
        {
            return (default, null);
        }
    }
    
    public async Task<(T?, AppErrorModel?)> PostAsync<T>(string query, object content)
    {
        var stringContent = new StringContent(
            JsonConvert.SerializeObject(content),
            Encoding.UTF8, MediaTypeNames.Application.Json);
        return await GetResponseMessage<T>(async () => await _httpClient.PostAsync(query, stringContent));
    }

    public async Task<(T?, AppErrorModel?)> PostWithoutAuthAsync<T>(string query, object? content = null)
    {
        var stringContent = content is not null
            ? new StringContent(
                JsonConvert.SerializeObject(content),
                Encoding.UTF8, MediaTypeNames.Application.Json)
            : null;
        return await GetResponseMessage<T>(async () => await _httpClient.PostAsync(query, stringContent), false);
    }

    public async Task<(T?, AppErrorModel?)> PostAsync<T>(string query, HttpContent? content = null)
    {
        return await GetResponseMessage<T>(async () => await _httpClient.PostAsync(query, content));
    }

    public async Task<(byte[]?, AppErrorModel?)> PostAsync(string query, object? content = null)
    {
        try
        {
            var stringContent = new StringContent(
                JsonConvert.SerializeObject(content),
                Encoding.UTF8, MediaTypeNames.Application.Json);
            var responseMessage = await _httpClient.PostAsync(query, stringContent);
            if (responseMessage.IsSuccessStatusCode)
                return (await responseMessage.Content.ReadAsByteArrayAsync(), null);
            return (default, await responseMessage.Content.ReadFromJsonAsync<AppErrorModel>());
        }
        catch (Exception)
        {
            return (default, null);
        }
    }

    public async Task<(T?, AppErrorModel?)> DeleteAsync<T>(string query)
    {
        return await GetResponseMessage<T>(async () => await _httpClient.DeleteAsync(query));
    }

    private async Task<(T?, AppErrorModel?)> GetResponseMessage<T>(Func<Task<HttpResponseMessage>> responseProvider,
        bool authCheck = true)
    {
        try
        {
            var responseMessage = await responseProvider.Invoke();
            if (responseMessage.IsSuccessStatusCode)
                return (JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync()), null);
            return (default, await responseMessage.Content.ReadFromJsonAsync<AppErrorModel>());
        }
        catch (Exception)
        {
            return (default, null);
        }
    }
}
