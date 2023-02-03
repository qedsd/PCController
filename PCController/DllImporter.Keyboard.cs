using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PCController
{
    public static partial class DllImporter
    {
        /// <summary>
        /// 键盘事件
        /// https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-keybd_event
        /// https://learn.microsoft.com/zh-cn/windows/win32/inputdev/virtual-key-codes
        /// </summary>
        /// <param name="bVk">按键的虚拟键值</param>
        /// <param name="bScan">扫描码，一般不用设置，用 0 代替就行</param>
        /// <param name="dwFlags">选项标志，如果为 keydown 则置 0 即可，如果为 keyup 则设成"KEYEVENTF_KEYUP"</param>
        /// <param name="dwExtraInfo">与键笔划关联的附加值，一般置 0 即可</param>
        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        const int KEYEVENTF_KEYUP = 0x0002;
    }
}
