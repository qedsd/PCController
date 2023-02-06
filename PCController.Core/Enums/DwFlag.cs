using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.Enums
{
    /// <summary>
    /// 鼠标状态标记
    /// </summary>
    public enum DwFlag
    {
        /// <summary>
        /// 移动鼠标
        /// </summary>
        [Description("移动鼠标")]
        MOUSEEVENTF_MOVE = 1,

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        [Description("鼠标左键按下")]
        MOUSEEVENTF_LEFTDOWN = 2,

        /// <summary>
        /// 鼠标左键抬起
        /// </summary>
        [Description("鼠标左键抬起")]
        MOUSEEVENTF_LEFTUP = 4,

        /// <summary>
        /// 鼠标右键按下
        /// </summary>
        [Description("鼠标右键按下")]
        MOUSEEVENTF_RIGHTDOWN = 8,

        /// <summary>
        /// 鼠标右键抬起
        /// </summary>
        [Description("鼠标右键抬起")]
        MOUSEEVENTF_RIGHTUP = 16,

        /// <summary>
        /// 鼠标中键按下
        /// </summary>
        [Description("鼠标中键按下")]
        MOUSEEVENTF_MIDDLEDOWN = 32,

        /// <summary>
        /// 鼠标中键抬起
        /// </summary>
        [Description("鼠标中键抬起")]
        MOUSEEVENTF_MIDDLEUP = 64,

        /// <summary>
        /// 采用绝对坐标
        /// </summary>
        [Description("采用绝对坐标")]
        MOUSEEVENTF_ABSOLUTE = 32768,
    }
}
