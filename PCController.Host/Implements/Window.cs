using Newtonsoft.Json;
using PCController.Core.MsgParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Windows.Win32;
using Windows.Win32.Foundation;

namespace PCController.Host.Implements
{
    internal class Window
    {
        public static bool SetForegroundWindow(object para) => SetForegroundWindow(para as SetForegroundWindowParameter);
        public static bool SetForegroundWindow(SetForegroundWindowParameter para)
        {
            return para != null && SetForegroundWindow(para.IntPtr);
        }
        public static bool SetForegroundWindow(IntPtr hWnd)
        {
            return DllImporter.SetForegroundWindow(hWnd);
        }

        public static bool ShowWindow(object para) => ShowWindow(JsonConvert.DeserializeObject<ShowWindowParameter>(JsonConvert.SerializeObject(para)));
        public static bool ShowWindow(ShowWindowParameter para)
        {
            return para != null && ShowWindow(para.HWnd, para.NCmdShow);
        }
        public static bool ShowWindow(IntPtr hWnd, int nCmdShow)
        {
            return DllImporter.ShowWindow(hWnd, nCmdShow);
        }

        public static void EnumWindows()
        {
            DllImporter.EnumWindows(EnumWindows,0);
        }
        public static bool EnumWindows(IntPtr hwnd, int lParam)
        {
            //if(ShouldWindowBeDisplayed(hwnd))
            //{
            //    string title = GetWindowTitle(hwnd);
            //    if(!string.IsNullOrEmpty(title))
            //    {
            //        Console.WriteLine($"{hwnd} {lParam} {GetWindowTitle(hwnd)}");
            //    }
            //}
            if(IsAltTabWindow(hwnd))
            {
                string title = GetWindowTitle(hwnd);
                if (!string.IsNullOrEmpty(title))
                    Console.WriteLine($"{hwnd} {lParam} {title}");
            }
            return true;
        }
        private static bool ShouldWindowBeDisplayed(IntPtr window)
        {
            //https://stackoverflow.com/questions/210504/enumerate-windows-like-alt-tab-does
            //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowinfo
            //int windowStyles = PInvoke.GetWindowLong(window, Windows.Win32.UI.WindowsAndMessaging.WINDOW_LONG_PTR_INDEX.GWL_STYLE);
            long windowStyles = DllImporter.GetWindowLong(window, -16);
            //if (((uint)0x10000000L & windowStyles) != (uint)0x10000000L ||
            //    ((uint)0x00040000L & windowStyles) != (uint)0x00040000L)
            //{
            //    return true;
            //}
            if ((268435456 & windowStyles) != 268435456 ||
                (262144 & windowStyles) != 262144)
            {
                return true;
            }
            return false;
        }

        private static string GetWindowTitle(IntPtr hWnd)
        {
            //PInvoke.GetWindowText()
            int length = DllImporter.GetWindowTextLength(hWnd);
            StringBuilder windowName = new StringBuilder(length + 1);
            DllImporter.GetWindowText(hWnd, windowName, windowName.Capacity);
            return windowName.ToString();
        }

        private static bool IsAltTabWindow(IntPtr hwnd)
        {
            // Start at the root owner
            IntPtr hwndWalk = DllImporter.GetAncestor(hwnd, 3);
            // See if we are the last active visible popup
            IntPtr hwndTry;
            while ((hwndTry = DllImporter.GetLastActivePopup(hwndWalk)) != hwndTry)
            {
                if (DllImporter.IsWindowVisible(hwndTry)) break;
                hwndWalk = hwndTry;
            }
            return hwndWalk == hwnd;
        }
    }
}
