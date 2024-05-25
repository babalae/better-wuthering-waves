﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace BetterWutheringWaves.GameTask.AutoPick
{
    /// <summary>
    /// 非16:9分辨率下可能无法正常工作
    /// </summary>
    [Serializable]
    public partial class AutoPickConfig : ObservableObject
    {
        /// <summary>
        /// 触发器是否启用
        /// </summary>
        [ObservableProperty] private bool _enabled = true;

        /// <summary>
        /// 1080p下拾取文字左边的起始偏移
        /// </summary>
        [ObservableProperty] private int _itemIconLeftOffset = 60;

        /// <summary>
        /// 1080p下拾取文字的起始偏移
        /// </summary>
        [ObservableProperty] private int _itemTextLeftOffset = 115;

        /// <summary>
        /// 1080p下拾取文字的终止偏移
        /// </summary>
        [ObservableProperty] private int _itemTextRightOffset = 400;

        /// <summary>
        /// 急速模式
        /// 无视文字识别结果，直接拾取
        /// </summary>

        [ObservableProperty] private bool _fastModeEnabled = false;
    }
}
