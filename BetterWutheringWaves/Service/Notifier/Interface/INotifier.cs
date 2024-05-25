using System.Net.Http;
using System.Threading.Tasks;

namespace BetterWutheringWaves.Service.Notifier.Interface;

public interface INotifier
{
    string Name { get; }

    // TODO: replace HttpContent with another data structure
    Task SendNotificationAsync(HttpContent content);
}
