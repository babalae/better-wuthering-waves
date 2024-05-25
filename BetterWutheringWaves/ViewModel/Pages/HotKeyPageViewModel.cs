using BetterWutheringWaves.Core.Config;
using BetterWutheringWaves.GameTask;
using BetterWutheringWaves.Helpers.Extensions;
using BetterWutheringWaves.Model;
using BetterWutheringWaves.Service.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using HotKeySettingModel = BetterWutheringWaves.Model.HotKeySettingModel;

namespace BetterWutheringWaves.ViewModel.Pages;

public partial class HotKeyPageViewModel : ObservableObject, IViewModel
{
    private readonly ILogger<HotKeyPageViewModel> _logger;
    private readonly TaskSettingsPageViewModel _taskSettingsPageViewModel;
    public AllConfig Config { get; set; }

    [ObservableProperty]
    private ObservableCollection<HotKeySettingModel> _hotKeySettingModels = new();

    public HotKeyPageViewModel(IConfigService configService, ILogger<HotKeyPageViewModel> logger, TaskSettingsPageViewModel taskSettingsPageViewModel)
    {
        _logger = logger;
        _taskSettingsPageViewModel = taskSettingsPageViewModel;
        // 获取配置
        Config = configService.Get();

        // 构建快捷键配置列表
        BuildHotKeySettingModelList();

        foreach (var hotKeyConfig in HotKeySettingModels)
        {
            hotKeyConfig.RegisterHotKey();
            hotKeyConfig.PropertyChanged += (sender, e) =>
            {
                if (sender is HotKeySettingModel model)
                {
                    // 反射更新配置

                    // 更新快捷键
                    if (e.PropertyName == "HotKey")
                    {
                        Debug.WriteLine($"{model.FunctionName} 快捷键变更为 {model.HotKey}");
                        var pi = Config.HotKeyConfig.GetType().GetProperty(model.ConfigPropertyName, BindingFlags.Public | BindingFlags.Instance);
                        if (null != pi && pi.CanWrite)
                        {
                            var str = model.HotKey.ToString();
                            if (str == "< None >")
                            {
                                str = "";
                            }

                            pi.SetValue(Config.HotKeyConfig, str, null);
                        }
                    }

                    // 更新快捷键类型
                    if (e.PropertyName == "HotKeyType")
                    {
                        Debug.WriteLine($"{model.FunctionName} 快捷键类型变更为 {model.HotKeyType.ToChineseName()}");
                        model.HotKey = HotKey.None;
                        var pi = Config.HotKeyConfig.GetType().GetProperty(model.ConfigPropertyName + "Type", BindingFlags.Public | BindingFlags.Instance);
                        if (null != pi && pi.CanWrite)
                        {
                            pi.SetValue(Config.HotKeyConfig, model.HotKeyType.ToString(), null);
                        }
                    }

                    RemoveDuplicateHotKey(model);
                    model.UnRegisterHotKey();
                    model.RegisterHotKey();
                }
            };
        }
    }

    /// <summary>
    /// 移除重复的快捷键配置
    /// </summary>
    /// <param name="current"></param>
    private void RemoveDuplicateHotKey(HotKeySettingModel current)
    {
        if (current.HotKey.IsEmpty)
        {
            return;
        }

        foreach (var hotKeySettingModel in HotKeySettingModels)
        {
            if (hotKeySettingModel.HotKey.IsEmpty)
            {
                continue;
            }

            if (hotKeySettingModel.ConfigPropertyName != current.ConfigPropertyName && hotKeySettingModel.HotKey == current.HotKey)
            {
                hotKeySettingModel.HotKey = HotKey.None;
            }
        }
    }

    private void BuildHotKeySettingModelList()
    {
        var bgiEnabledHotKeySettingModel = new HotKeySettingModel(
            "启动停止 BetterWW",
            nameof(Config.HotKeyConfig.BgiEnabledHotkey),
            Config.HotKeyConfig.BgiEnabledHotkey,
            Config.HotKeyConfig.BgiEnabledHotkeyType,
            (_, _) => { WeakReferenceMessenger.Default.Send(new PropertyChangedMessage<object>(this, "SwitchTriggerStatus", "", "")); }
        );
        HotKeySettingModels.Add(bgiEnabledHotKeySettingModel);

        // var takeScreenshotHotKeySettingModel = new HotKeySettingModel(
        //     "游戏截图（开发者）",
        //     nameof(Config.HotKeyConfig.TakeScreenshotHotkey),
        //     Config.HotKeyConfig.TakeScreenshotHotkey,
        //     Config.HotKeyConfig.TakeScreenshotHotkeyType,
        //     (_, _) => { TaskTriggerDispatcher.Instance().TakeScreenshot(); }
        // );
        // HotKeySettingModels.Add(takeScreenshotHotKeySettingModel);

        // var autoPickEnabledHotKeySettingModel = new HotKeySettingModel(
        //     "自动拾取开关",
        //     nameof(Config.HotKeyConfig.AutoPickEnabledHotkey),
        //     Config.HotKeyConfig.AutoPickEnabledHotkey,
        //     Config.HotKeyConfig.AutoPickEnabledHotkeyType,
        //     (_, _) =>
        //     {
        //         TaskContext.Instance().Config.AutoPickConfig.Enabled = !TaskContext.Instance().Config.AutoPickConfig.Enabled;
        //         _logger.LogInformation("切换{Name}状态为[{Enabled}]", "自动拾取", ToChinese(TaskContext.Instance().Config.AutoPickConfig.Enabled));
        //     }
        // );
        // HotKeySettingModels.Add(autoPickEnabledHotKeySettingModel);

        var autoSkipEnabledHotKeySettingModel = new HotKeySettingModel(
            "自动剧情开关",
            nameof(Config.HotKeyConfig.AutoSkipEnabledHotkey),
            Config.HotKeyConfig.AutoSkipEnabledHotkey,
            Config.HotKeyConfig.AutoSkipEnabledHotkeyType,
            (_, _) =>
            {
                TaskContext.Instance().Config.AutoSkipConfig.Enabled = !TaskContext.Instance().Config.AutoSkipConfig.Enabled;
                _logger.LogInformation("切换{Name}状态为[{Enabled}]", "自动剧情", ToChinese(TaskContext.Instance().Config.AutoSkipConfig.Enabled));
            }
        );
        HotKeySettingModels.Add(autoSkipEnabledHotKeySettingModel);

        HotKeySettingModels.Add(new HotKeySettingModel(
            "自动按下跳过按钮开关",
            nameof(Config.HotKeyConfig.AutoSkipPressSkipEnabledHotkey),
            Config.HotKeyConfig.AutoSkipPressSkipEnabledHotkey,
            Config.HotKeyConfig.AutoSkipPressSkipEnabledHotkeyType,
            (_, _) =>
            {
                TaskContext.Instance().Config.AutoSkipConfig.PressSkipEnabled = !TaskContext.Instance().Config.AutoSkipConfig.PressSkipEnabled;
                _logger.LogInformation("切换{Name}状态为[{Enabled}]", "自动按下跳过按钮", ToChinese(TaskContext.Instance().Config.AutoSkipConfig.PressSkipEnabled));
            }
        ));
    }

    private string ToChinese(bool enabled)
    {
        return enabled.ToChinese();
    }
}
