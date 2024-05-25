using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace BetterWutheringWaves.Core.Config;

/// <summary>
///     鸣潮启动配置
/// </summary>
[Serializable]
public partial class GenshinStartConfig : ObservableObject
{
    /// <summary>
    ///     自动点击月卡
    /// </summary>
    [ObservableProperty]
    private bool _autoClickBlessingOfTheWelkinMoonEnabled;

    /// <summary>
    ///     自动进入游戏（开门）
    /// </summary>
    [ObservableProperty]
    private bool _autoEnterGameEnabled = true;

    /// <summary>
    ///     鸣潮启动参数
    /// </summary>
    [ObservableProperty]
    private string _genshinStartArgs = "";

    /// <summary>
    ///     鸣潮安装路径
    /// </summary>
    [ObservableProperty]
    private string _installPath = "";

    /// <summary>
    ///     联动启动鸣潮本体
    /// </summary>
    [ObservableProperty]
    private bool _linkedStartEnabled = true;
}
