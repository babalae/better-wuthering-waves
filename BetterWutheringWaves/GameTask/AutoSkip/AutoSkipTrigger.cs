using BetterWutheringWaves.Core.Simulator;
using BetterWutheringWaves.GameTask.AutoSkip.Assets;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using BetterWutheringWaves.GameTask.Common;
using BetterWutheringWaves.View.Drawable;
using Vanara.PInvoke;

namespace BetterWutheringWaves.GameTask.AutoSkip;

/// <summary>
/// 自动剧情有选项点击
/// </summary>
public class AutoSkipTrigger : ITaskTrigger
{
    private readonly ILogger<AutoSkipTrigger> _logger = App.GetLogger<AutoSkipTrigger>();

    public string Name => "自动剧情";
    public bool IsEnabled { get; set; }
    public int Priority => 20;
    public bool IsExclusive => false;

    private readonly PostMessageSimulator _simulator = Simulation.PostMessage(TaskContext.Instance().GameHandle);

    private readonly Random _random = new();

    public void Init()
    {
        IsEnabled = TaskContext.Instance().Config.AutoSkipConfig.Enabled;
    }

    private DateTime _prevExecute = DateTime.MinValue;

    public void OnCapture(CaptureContent content)
    {
        if ((DateTime.Now - _prevExecute).TotalMilliseconds <= 200)
        {
            return;
        }

        _prevExecute = DateTime.Now;

        if (TaskContext.Instance().Config.AutoSkipConfig.PressSkipEnabled && SystemControl.IsGameActive())
        {
            var skipRa = content.CaptureRectArea.Find(AutoSkipAssets.Instance.SkipButtonRo);
            if (skipRa.IsExist())
            {
                skipRa.Click();
                TaskControl.Sleep(1000);
                using var captureRa = TaskControl.CaptureToRectArea();
                captureRa.Find(AutoSkipAssets.Instance.NotPromptAgainButtonRo, region =>
                {
                    region.Click();
                });
                TaskControl.Sleep(300);
                captureRa.Find(AutoSkipAssets.Instance.ConfirmButtonRo, region =>
                {
                    region.Click();
                });
                VisionContext.Instance().DrawContent.ClearAll();
                return;
            }
        }

        bool inStory = true;
        var r1 = content.CaptureRectArea.Find(AutoSkipAssets.Instance.StartAutoButtonRo);
        if (r1.IsEmpty())
        {
            var r2 = content.CaptureRectArea.Find(AutoSkipAssets.Instance.StopAutoButtonRo);
            if (r2.IsEmpty())
            {
                inStory = false;
            }
        }

        if (inStory)
        {
            // 随机上下键 保证剧情不死循环
            int n = _random.Next(1, 4);
            if (n == 2)
            {
                // Simulation.SendInput.Keyboard.KeyPress(User32.VK.VK_UP);
                _simulator.KeyPressBackground(User32.VK.VK_W);
                // _logger.LogInformation("上");
                // Thread.Sleep(500);
            }
            else if (n == 3)
            {
                // Simulation.SendInput.Keyboard.KeyPress(User32.VK.VK_DOWN);
                _simulator.KeyPressBackground(User32.VK.VK_S);
                // _logger.LogInformation("下");
                // Thread.Sleep(500);
            }
            _simulator.KeyPressBackground(User32.VK.VK_F);
        }
    }
}
