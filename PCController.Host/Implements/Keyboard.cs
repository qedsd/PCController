using Newtonsoft.Json;
using PCController.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Host.Implements
{
    internal class Keyboard
    {
        public static void Execute(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo)
        {
            DllImporter.keybd_event(bVk, bScan, dwFlags, dwExtraInfo);
        }
        public static void Execute(object para) => Execute(JsonConvert.DeserializeObject<KeyboardParameter>(JsonConvert.SerializeObject(para)));
        public static void Execute(KeyboardParameter para)
        {
            if (para != null)
            {
                DllImporter.keybd_event(para.BVk, para.BScan, para.DwFlags, para.DwExtraInfo);
            }
        }
    }
}
