using System.Text.Json.Serialization;
using BetterWutheringWaves.Service.Notification.Model.Enum;

namespace BetterWutheringWaves.Service.Notification.Model;

public record LifecycleNotificationData : INotificationData
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public NotificationEvent Event { get; set; }

    public static LifecycleNotificationData Test()
    {
        return new LifecycleNotificationData() { Event = NotificationEvent.Test };
    }
}
