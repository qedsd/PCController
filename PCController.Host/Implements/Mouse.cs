using Newtonsoft.Json;
using PCController.Core.MsgParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Host.Implements
{
    internal class Mouse
    {
        public static int Execute(string parameter)
        {
            var para = JsonConvert.DeserializeObject<MouseMoveParameter>(parameter);
            return Execute(para);
        }
        public static int Execute(object para) => Execute(JsonConvert.DeserializeObject<MouseMoveParameter>(JsonConvert.SerializeObject(para)));
        public static int Execute(MouseMoveParameter para)
        {
            if (para != null)
            {
                return DllImporter.mouse_event(para.GetDwFlags(), para.Dx, para.Dy, para.DwData, para.DwExtraInfo);
            }
            else
            {
                return -1;
            }
        }
    }
}
