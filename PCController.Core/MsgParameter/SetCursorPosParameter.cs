using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.MsgParameter
{
    /// <summary>
    /// 设置光标位置
    /// </summary>
    public class SetCursorPosParameter: MsgParameter
    {
        /// <summary>
        /// X
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y
        /// </summary>
        public int Y { get; set; }
    }
}
