using BetterWutheringWaves.Core.Config;
using BetterWutheringWaves.GameTask;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BetterWutheringWaves.ViewModel.Windows;

public class AutoPickWhiteListViewModel : FormViewModel<string>
{
    public AutoPickWhiteListViewModel()
    {
        var blackListJson = Global.ReadAllTextIfExist(@"User\pick_white_lists.json");
        if (!string.IsNullOrEmpty(blackListJson))
        {
            var blackList = JsonSerializer.Deserialize<List<string>>(blackListJson) ?? new List<string>();
            AddRange(blackList);
        }
    }

    public new void OnSave()
    {
        var blackListJson = JsonSerializer.Serialize(List.ToList());
        Global.WriteAllText(@"User\pick_white_lists.json", blackListJson);
        GameTaskManager.RefreshTriggerConfigs();
    }
}