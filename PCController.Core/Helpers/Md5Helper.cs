using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.Helpers
{
    /// <summary>
    /// Md5Helper
    /// </summary>
    public static class Md5Helper
    {
        /// <summary>
        /// 将字符串转为md5
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ToMd5(string data, string key = "PCController")
        {
            var bytes = HMACMD5.HashData(System.Text.Encoding.Default.GetBytes(key),System.Text.Encoding.Default.GetBytes(data));
            return System.Text.Encoding.Default.GetString(bytes);
        }
    }
}
