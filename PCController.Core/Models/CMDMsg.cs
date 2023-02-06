using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.Models
{
    public class CMDMsg
    {
        public CMDMsg(CMDType type, object para)
        {
            Type= type;
            Parameter= para;
        }
        /// <summary>
        /// 指令类型
        /// </summary>
        public CMDType Type { get; set; }
        /// <summary>
        /// 指令参数
        /// </summary>
        public object Parameter { get; set; }
    }
}
