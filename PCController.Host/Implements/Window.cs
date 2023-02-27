using Newtonsoft.Json;
using PCController.Core.MsgParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
        public static bool EnumWindows(int hwnd, int lParam)
        {
            Console.WriteLine($"{hwnd} {lParam}");
            return true;
        }
        private static bool ShouldWindowBeDisplayed(IntPtr window)
        {
            //https://stackoverflow.com/questions/210504/enumerate-windows-like-alt-tab-does
            return false;
        }
    }
}
