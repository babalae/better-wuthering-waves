using BetterWutheringWaves.Service.Notification.Model.Enum;
using System.Text.Json.Serialization;

namespace BetterWutheringWaves.Service.Notification.Model;

public interface INotificationData
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public NotificationEvent Event { get; set; }
}
