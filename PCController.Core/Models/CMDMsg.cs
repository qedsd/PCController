using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCController.Core.Enums;

namespace PCController.Core.Models
{
    /// <summary>
    /// 通讯参数
    /// </summary>
    public class CMDMsg
    {
        /// <summary>
        /// 通讯参数
        /// </summary>
        public CMDMsg()
        {

        }
        /// <summary>
        /// 通讯参数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="para"></param>
        public CMDMsg(CMDType type, object para)
        {
            Type= type;
            Parameter= para;
        }
        /// <summary>
        /// 通讯参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pw"></param>
        /// <param name="type"></param>
        /// <param name="para"></param>
        public CMDMsg(string name, string pw, CMDType type, object para)
        {
            Host = new HostItem(name, pw);
            Type = type;
            Parameter = para;
        }

        /// <summary>
        /// 通讯参数
        /// </summary>
        /// <param name="host"></param>
        /// <param name="type"></param>
        /// <param name="para"></param>
        public CMDMsg(HostItem host, CMDType type, object para)
        {
            Host = host;
            Type = type;
            Parameter = para;
        }
        /// <summary>
        /// 目标主机
        /// </summary>
        public HostItem Host { get; set; }
        /// <summary>
        /// 指令类型
        /// </summary>
        public CMDType Type { get; set; }
        /// <summary>
        /// 指令参数
        /// </summary>
        public object Parameter { get; set; }

        /// <summary>
        /// 转成json字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
