using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.MsgParameter
{
    /// <summary>
    /// 设置前台窗口
    /// </summary>
    public class SetForegroundWindowParameter: MsgParameter
    {
        /// <summary>
        /// HWND句柄
        /// </summary>
        public IntPtr IntPtr { get; set; }
    }
}
