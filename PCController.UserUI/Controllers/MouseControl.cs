using Newtonsoft.Json;
using PCController.Core.Helpers;
using PCController.Core.MsgParameter;
using PCController.UserUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.UserUI.Controllers
{
    internal static class MouseControl
    {
        internal static void RightClick()
        {
            RightDown();
            RightUp();
        }
        internal static void RightUp()
        {
            MouseMoveParameter parameter = new MouseMoveParameter()
            {
                DwFlags = new Core.Enums.DwFlag[] { Core.Enums.DwFlag.MOUSEEVENTF_RIGHTUP },
                Dx = 0,
                Dy = 0,
                DwData = 0,
                DwExtraInfo = 0
            };
            Set(parameter);
        }
        internal static void RightDown()
        {
            MouseMoveParameter parameter = new MouseMoveParameter()
            {
                DwFlags = new Core.Enums.DwFlag[] { Core.Enums.DwFlag.MOUSEEVENTF_RIGHTDOWN },
                Dx = 0,
                Dy = 0,
                DwData = 0,
                DwExtraInfo = 0
            };
            Set(parameter);
        }
        internal static void LeftClick()
        {
            LeftDown();
            LeftUp();
        }
        internal static void LeftUp()
        {
            MouseMoveParameter parameter = new MouseMoveParameter()
            {
                DwFlags = new Core.Enums.DwFlag[] { Core.Enums.DwFlag.MOUSEEVENTF_LEFTUP },
                Dx = 0,
                Dy = 0,
                DwData = 0,
                DwExtraInfo = 0
            };
            Set(parameter);
        }
        internal static void LeftDown()
        {
            MouseMoveParameter parameter = new MouseMoveParameter()
            {
                DwFlags = new Core.Enums.DwFlag[] { Core.Enums.DwFlag.MOUSEEVENTF_LEFTDOWN },
                Dx = 0,
                Dy = 0,
                DwData = 0,
                DwExtraInfo = 0
            };
            Set(parameter);
        }
        /// <summary>
        /// 相对位移移动鼠标
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        internal static void Move(int dx, int dy)
        {
            MouseMoveParameter parameter = new MouseMoveParameter()
            {
                DwFlags = new Core.Enums.DwFlag[] { Core.Enums.DwFlag.MOUSEEVENTF_MOVE },
                Dx = dx,
                Dy = dy,
                DwData = 0,
                DwExtraInfo = 0
            };
            Set(parameter);
        }
        /// <summary>
        /// 移动鼠标到指定位置
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        internal static void SetPos(int dx,int dy)
        {
            MouseMoveParameter parameter = new MouseMoveParameter()
            {
                DwFlags = new Core.Enums.DwFlag[] { Core.Enums.DwFlag.MOUSEEVENTF_ABSOLUTE, Core.Enums.DwFlag.MOUSEEVENTF_MOVE },
                Dx = dx,
                Dy = dy,
                DwData = 0,
                DwExtraInfo = 0
            };
            Set(parameter);
        }
        /// <summary>
        /// 按指定参数控制鼠标
        /// </summary>
        /// <param name="parameter"></param>
        internal static async void Set(MouseMoveParameter parameter)
        {
            parameter.HostName = Setting.Current.HostName;
            parameter.HostPassword= Setting.Current.HostPassword;
            await HttpHelper.PostJsonAsync($"{Setting.Current.WebAPIIPHost}/mousemove", JsonConvert.SerializeObject(parameter));
        }
    }
}
