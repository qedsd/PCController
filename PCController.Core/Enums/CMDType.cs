using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.Enums
{
    /// <summary>
    /// 指令类型
    /// </summary>
    public enum CMDType
    {
        /// <summary>
        /// 管理Host
        /// </summary>
        HostMgr,
        /// <summary>
        /// 获取光标位置
        /// </summary>
        GetCursor, 
        /// <summary>
        /// 设置光标位置
        /// </summary>
        SetCursor, 
        /// <summary>
        /// 键盘
        /// </summary>
        Keyboard,
        /// <summary>
        /// 鼠标
        /// </summary>
        Mouse, 
        /// <summary>
        /// 设置前台窗口
        /// </summary>
        SetForegroundWindow,
        /// <summary>
        /// 显示窗口
        /// </summary>
        ShowWindow,
        /// <summary>
        /// 执行bat脚本
        /// </summary>
        ExcuteBat,
        /// <summary>
        /// 获取bat脚本列表
        /// </summary>
        GetBatList,
    }
}
