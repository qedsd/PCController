using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.Models
{
    /// <summary>
    /// 主机信息
    /// </summary>
    public class HostItem
    {
        /// <summary>
        /// 主机信息
        /// </summary>
        public HostItem()
        {

        }
        /// <summary>
        /// 主机信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        public HostItem(string name, string password)
        {
            Name = name;
            Password = password;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 密码是否匹配
        /// </summary>
        /// <param name="pw"></param>
        /// <returns></returns>
        public bool IsValidPW(string pw)
        {
            return Password == pw;
        }
    }
}
