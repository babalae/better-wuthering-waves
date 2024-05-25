using System;
using System.Windows;
using BetterWutheringWaves.Core.Config;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace BetterWutheringWaves.ViewModel.Windows;

public partial class MapViewerViewModel : ObservableObject
{
    [ObservableProperty]
    private Rect _bigMapRect = new(0, 0, 0, 0);

    [ObservableProperty]
    private string _mapPath = Global.Absolute(@"Assets\Map\mainMap100Block.png");

    public MapViewerViewModel()
    {
        WeakReferenceMessenger.Default.Register<PropertyChangedMessage<object>>(this, (sender, msg) =>
        {
            if (msg.PropertyName == "UpdateBigMapRect")
            {
                BigMapRect = (Rect)msg.NewValue;
            }
        });
    }
}
