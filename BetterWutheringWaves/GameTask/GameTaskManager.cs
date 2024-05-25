using BetterWutheringWaves.Core.Config;
using BetterWutheringWaves.Core.Recognition.OpenCv;
using BetterWutheringWaves.GameTask.AutoPick.Assets;
using BetterWutheringWaves.GameTask.AutoSkip.Assets;
using BetterWutheringWaves.GameTask.Common.Element.Assets;
using BetterWutheringWaves.GameTask.GameLoading;
using BetterWutheringWaves.GameTask.GameLoading.Assets;
using BetterWutheringWaves.View.Drawable;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using OpenCvSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BetterWutheringWaves.GameTask;

internal class GameTaskManager
{
    public static Dictionary<string, ITaskTrigger>? TriggerDictionary { get; set; }

    /// <summary>
    /// 一定要在任务上下文初始化完毕后使用
    /// </summary>
    /// <returns></returns>
    public static List<ITaskTrigger> LoadTriggers()
    {
        ReloadAssets();
        TriggerDictionary = new Dictionary<string, ITaskTrigger>()
        {
            // { "GameLoading", new GameLoadingTrigger() },
            // { "AutoPick", new AutoPick.AutoPickTrigger() },
            { "AutoSkip", new AutoSkip.AutoSkipTrigger() },
        };

        var loadedTriggers = TriggerDictionary.Values.ToList();

        loadedTriggers.ForEach(i => i.Init());

        loadedTriggers = loadedTriggers.OrderByDescending(i => i.Priority).ToList();
        return loadedTriggers;
    }

    public static void RefreshTriggerConfigs()
    {
        if (TriggerDictionary is { Count: > 0 })
        {
            // TriggerDictionary["AutoPick"].Init();
            TriggerDictionary["AutoSkip"].Init();
            // 清理画布
            VisionContext.Instance().DrawContent.ClearAll();
        }
        ReloadAssets();
    }

    public static void ReloadAssets()
    {
        AutoPickAssets.DestroyInstance();
        AutoSkipAssets.DestroyInstance();
        ElementAssets.DestroyInstance();
        GameLoadingAssets.DestroyInstance();
    }

    /// <summary>
    /// 获取素材图片并缩放
    /// todo 支持多语言
    /// </summary>
    /// <param name="featName">任务名称</param>
    /// <param name="assertName">素材文件名</param>
    /// <param name="flags"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static Mat LoadAssetImage(string featName, string assertName, ImreadModes flags = ImreadModes.Color)
    {
        var info = TaskContext.Instance().SystemInfo;
        var assetsFolder = Global.Absolute($@"GameTask\{featName}\Assets\{info.GameScreenSize.Width}x{info.GameScreenSize.Height}");
        if (!Directory.Exists(assetsFolder))
        {
            assetsFolder = Global.Absolute($@"GameTask\{featName}\Assets\1920x1080");
        }

        if (!Directory.Exists(assetsFolder))
        {
            throw new FileNotFoundException($"未找到{featName}的素材文件夹");
        }

        var filePath = Path.Combine(assetsFolder, assertName);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"未找到{featName}中的{assertName}文件");
        }

        var mat = Mat.FromStream(File.OpenRead(filePath), flags);
        if (info.GameScreenSize.Width != 1920)
        {
            mat = ResizeHelper.Resize(mat, info.AssetScale);
        }

        return mat;
    }
}
