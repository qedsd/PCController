using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.MsgParameter
{
    /// <summary>
    /// webapi传递参数基类
    /// </summary>
    public class MsgParameter
    {
        /// <summary>
        /// 目标名称
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// 目标密码
        /// </summary>
        public string HostPassword { get; set; }

        /// <summary>
        /// 获取md5格式的密码
        /// </summary>
        /// <returns></returns>
        public string GetHostMd5Password()
        {
            return Helpers.Md5Helper.ToMd5(HostPassword);
        }
    }
}
