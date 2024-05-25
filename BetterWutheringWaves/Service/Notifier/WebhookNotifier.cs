using System.Net.Http;
using System.Threading.Tasks;
using BetterWutheringWaves.Service.Notifier.Interface;
using BetterWutheringWaves.Service.Notifier.Exception;

namespace BetterWutheringWaves.Service.Notifier;

public class WebhookNotifier : INotifier
{
    public string Name { get; set; } = "Webhook";

    public string Endpoint { get; set; }

    private readonly HttpClient _httpClient;

    public WebhookNotifier(HttpClient httpClient, string endpoint = "")
    {
        _httpClient = httpClient;
        Endpoint = endpoint;
    }

    public async Task SendNotificationAsync(HttpContent content)
    {
        try
        {
            var response = await _httpClient.PostAsync(Endpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new NotifierException($"Webhook call failed with code: {response.StatusCode}");
            }
        }
        catch (NotifierException)
        {
            throw;
        }
        catch (System.Exception ex)
        {
            throw new NotifierException($"Error sending webhook: {ex.Message}");
        }
    }
}
