using Newtonsoft.Json;
using PCController.Core.MsgParameter;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
