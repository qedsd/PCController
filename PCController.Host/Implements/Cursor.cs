using Newtonsoft.Json;
using PCController.Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Host.Implements
{
    internal static class Cursor
    {
        public static Point GetCursorPos()
        {
            Point pot = new Point();
            DllImporter.GetCursorPos(ref pot);
            return pot;
        }
        public static void SetCursorPos(string json)
        {
            var para = JsonConvert.DeserializeObject<SetCursorPosParameter>(json);
            SetCursorPos(para);
        }
        public static void SetCursorPos(object para)
        {
            SetCursorPos(JsonConvert.DeserializeObject<SetCursorPosParameter>(JsonConvert.SerializeObject(para)));
        }
        public static void SetCursorPos(SetCursorPosParameter para)
        {
            if (para != null)
            {
                SetCursorPos(para.X, para.Y);
            }
        }
        public static void SetCursorPos(int x, int y)
        {
            DllImporter.SetCursorPos(x, y);
        }
    }
}
