using BetterWutheringWaves.Core.Simulator;
using BetterWutheringWaves.GameTask;
using BetterWutheringWaves.Model;
using Gma.System.MouseKeyHook;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Vanara.PInvoke;
using Timer = System.Timers.Timer;

namespace BetterWutheringWaves.Core;

public class MouseKeyMonitor
{
    private IKeyboardMouseEvents? _globalHook;
    private IntPtr _hWnd;

    public void Subscribe(IntPtr gameHandle)
    {
        _hWnd = gameHandle;
        // Note: for the application hook, use the Hook.AppEvents() instead
        _globalHook = Hook.GlobalEvents();

        _globalHook.KeyDown += GlobalHookKeyDown;
        _globalHook.KeyUp += GlobalHookKeyUp;
        _globalHook.MouseDownExt += GlobalHookMouseDownExt;
        _globalHook.MouseUpExt += GlobalHookMouseUpExt;
        //_globalHook.KeyPress += GlobalHookKeyPress;
    }

    private void GlobalHookKeyDown(object? sender, KeyEventArgs e)
    {
        // Debug.WriteLine("KeyDown: \t{0}", e.KeyCode);

        // 热键按下事件
        HotKeyDown(sender, e);
    }

    private void GlobalHookKeyUp(object? sender, KeyEventArgs e)
    {
        // Debug.WriteLine("KeyUp: \t{0}", e.KeyCode);

        // 热键松开事件
        HotKeyUp(sender, e);
    }

    private void HotKeyDown(object? sender, KeyEventArgs e)
    {
        if (KeyboardHook.AllKeyboardHooks.TryGetValue(e.KeyCode, out var hook)) hook.KeyDown(sender, e);
    }

    private void HotKeyUp(object? sender, KeyEventArgs e)
    {
        if (KeyboardHook.AllKeyboardHooks.TryGetValue(e.KeyCode, out var hook)) hook.KeyUp(sender, e);
    }

    //private void GlobalHookKeyPress(object? sender, KeyPressEventArgs e)
    //{
    //    Debug.WriteLine("KeyPress: \t{0}", e.KeyChar);
    //}

    private void GlobalHookMouseDownExt(object? sender, MouseEventExtArgs e)
    {
        // Debug.WriteLine("MouseDown: {0}; \t Location: {1};\t System Timestamp: {2}", e.Button, e.Location, e.Timestamp);

        if (e.Button != MouseButtons.Left)
            if (MouseHook.AllMouseHooks.TryGetValue(e.Button, out var hook))
                hook.MouseDown(sender, e);
    }

    private void GlobalHookMouseUpExt(object? sender, MouseEventExtArgs e)
    {
        // Debug.WriteLine("MouseUp: {0}; \t Location: {1};\t System Timestamp: {2}", e.Button, e.Location, e.Timestamp);

        if (e.Button != MouseButtons.Left)
            if (MouseHook.AllMouseHooks.TryGetValue(e.Button, out var hook))
                hook.MouseUp(sender, e);
    }

    public void Unsubscribe()
    {
        if (_globalHook != null)
        {
            _globalHook.KeyDown -= GlobalHookKeyDown;
            _globalHook.KeyUp -= GlobalHookKeyUp;
            _globalHook.MouseDownExt -= GlobalHookMouseDownExt;
            _globalHook.MouseUpExt -= GlobalHookMouseUpExt;
            //_globalHook.KeyPress -= GlobalHookKeyPress;
            _globalHook.Dispose();
        }
    }
}
