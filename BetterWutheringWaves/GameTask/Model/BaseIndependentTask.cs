using BetterWutheringWaves.Helpers;
using Vanara.PInvoke;

namespace BetterWutheringWaves.GameTask.Model;

public class BaseIndependentTask
{
    protected SystemInfo Info => TaskContext.Instance().SystemInfo;
    protected RECT CaptureRect => TaskContext.Instance().SystemInfo.CaptureAreaRect;
    protected double AssetScale => TaskContext.Instance().SystemInfo.AssetScale;
}
