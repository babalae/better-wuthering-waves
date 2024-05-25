﻿using System;
using System.Drawing;
using static Vanara.PInvoke.Gdi32;
using Vanara.PInvoke;

namespace BetterWutheringWaves.Helpers
{
    public class PrimaryScreen
    {
        /// <summary>
        /// 获取屏幕分辨率当前物理大小
        /// </summary>
        public static Size WorkingArea
        {
            get
            {
                var hdc = User32.GetDC(IntPtr.Zero);
                var size = new Size
                {
                    Width = Gdi32.GetDeviceCaps(hdc, DeviceCap.HORZRES),
                    Height = Gdi32.GetDeviceCaps(hdc, DeviceCap.VERTRES)
                };
                User32.ReleaseDC(IntPtr.Zero, hdc);
                return size;
            }
        }
    }

}
