using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.Models
{
    /// <summary>
    /// 请求返回结果
    /// </summary>
    public class RequestResult
    {
        /// <summary>
        /// 请求返回结果
        /// </summary>
        public RequestResult()
        {

        }
        /// <summary>
        /// 请求返回结果
        /// </summary>
        /// <param name="successful"></param>
        /// <param name="msg"></param>
        public RequestResult(bool successful, string msg = null)
        {
            Successful = successful;
            Msg = msg;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Successful { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public string Msg { get; set; }
    }
}
