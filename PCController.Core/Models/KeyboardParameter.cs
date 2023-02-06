using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.Models
{
    /// <summary>
    /// 模拟键盘参数
    /// </summary>
    public class KeyboardParameter
    {
        /// <summary>
        /// 键盘代码
        /// </summary>
        public byte BVk { get; set; }
        /// <summary>
        /// 扫描码，一般不用设置，用 0 代替就行
        /// </summary>
        public byte BScan { get; set; }
        /// <summary>
        /// 选项标志，如果为 keydown 则置0即可，如果为 keyup 则设成1
        /// </summary>
        public uint DwFlags { get; set; }
        /// <summary>
        /// 与键笔划关联的附加值，一般置 0 即可
        /// </summary>
        public uint DwExtraInfo { get; set; }
    }
}
