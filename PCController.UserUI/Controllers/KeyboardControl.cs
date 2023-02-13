using Newtonsoft.Json;
using PCController.Core.Helpers;
using PCController.Core.Models;
using PCController.Core.MsgParameter;
using PCController.UserUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.UserUI.Controllers
{
    internal static class KeyboardControl
    {
        internal static void Click(KeyboardItem item)
        {
            Down(item);
            Up(item);
        }
        internal static void Down(KeyboardItem item)
        {
            KeyboardParameter parameter = new KeyboardParameter();
            if (item.Code.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                //十六进制
                parameter.BVk = (byte)Convert.ToInt32(item.Code, 16);
            }
            else
            {
                //十进制
                parameter.BVk = (byte)Convert.ToInt32(item.Code, 10);
            }
            parameter.BScan = 0;
            parameter.DwFlags = 0;
            parameter.DwExtraInfo = 0;
            Set(parameter);
        }
        internal static void Up(KeyboardItem item)
        {
            KeyboardParameter parameter= new KeyboardParameter();
            if(item.Code.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                //十六进制
                parameter.BVk = (byte)Convert.ToInt32(item.Code, 16);
            }
            else
            {
                //十进制
                parameter.BVk = (byte)Convert.ToInt32(item.Code, 10);
            }
            parameter.BScan = 0;
            parameter.DwFlags = 1;
            parameter.DwExtraInfo = 0;
            Set(parameter);
        }
        internal static async void Set(KeyboardParameter parameter)
        {
            parameter.HostName = Setting.Current.HostName;
            parameter.HostPassword = Setting.Current.HostPassword;
            await HttpHelper.PostJsonAsync($"{Setting.Current.WebAPIIPHost}/keyboard", JsonConvert.SerializeObject(parameter));
        }
    }
}
