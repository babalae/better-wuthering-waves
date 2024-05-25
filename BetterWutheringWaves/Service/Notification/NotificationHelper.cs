using System;
using System.Drawing;
using BetterWutheringWaves.GameTask.Common;
using BetterWutheringWaves.Service.Notification.Builder;
using BetterWutheringWaves.Service.Notification.Model;

namespace BetterWutheringWaves.Service.Notification;

public class NotificationHelper
{
    public static void Notify(INotificationData notificationData)
    {
        NotificationService.Instance().NotifyAllNotifiers(notificationData);
    }

    public static void SendTaskNotificationUsing(Func<TaskNotificationBuilder, INotificationData> builderFunc)
    {
        var builder = new TaskNotificationBuilder();
        Notify(builderFunc(builder));
    }

    public static void SendTaskNotificationWithScreenshotUsing(Func<TaskNotificationBuilder, INotificationData> builderFunc)
    {
        var builder = new TaskNotificationBuilder();
        var screenShot = (Bitmap)TaskControl.GetRectAreaFromDispatcher().SrcBitmap.Clone();
        Notify(builderFunc(builder.WithScreenshot(screenShot)));
    }
}
