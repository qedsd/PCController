using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.MsgParameter
{
    /// <summary>
    /// 显示窗口
    /// </summary>
    public class ShowWindowParameter: MsgParameter
    {
        /// <summary>
        /// 窗口句柄
        /// </summary>
        public IntPtr HWnd { get; set; }
        /// <summary>
        /// 指定窗口如何显示
        /// 0 关闭窗口
        /// 1 正常大小显示窗口
        /// 2 最小化窗口
        /// 3 最大化窗口
        /// </summary>
        public int NCmdShow { get; set; }
    }
}
