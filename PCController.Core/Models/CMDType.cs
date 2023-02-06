using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.Models
{
    /// <summary>
    /// 指令类型
    /// </summary>
    public enum CMDType
    {
        GetCursor,SetCursor,Keyboard,Mouse, SetForegroundWindow, ShowWindow,
    }
}
