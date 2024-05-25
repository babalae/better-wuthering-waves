using BetterWutheringWaves.Service.Notification.Model;

namespace BetterWutheringWaves.Service.Notification.Builder;

public interface INotificationDataBuilder<TNotificationData> where TNotificationData : INotificationData
{
    TNotificationData Build();
}
