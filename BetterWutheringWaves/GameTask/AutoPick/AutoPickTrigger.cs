using Microsoft.Extensions.Logging;

namespace BetterWutheringWaves.GameTask.AutoPick;

public class AutoPickTrigger : ITaskTrigger
{
    private readonly ILogger<AutoPickTrigger> _logger = App.GetLogger<AutoPickTrigger>();
    public string Name => "自动拾取";
    public bool IsEnabled { get; set; }
    public int Priority => 30;
    public bool IsExclusive => false;

    public void Init()
    {
        IsEnabled = TaskContext.Instance().Config.AutoPickConfig.Enabled;
    }

    public void OnCapture(CaptureContent content)
    {
    }
}
