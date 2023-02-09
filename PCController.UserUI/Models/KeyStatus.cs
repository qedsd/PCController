using Newtonsoft.Json;
using PCController.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.UserUI.Models
{
    /// <summary>
    /// 按键状态
    /// </summary>
    public class KeyStatus
    {
        public KeyboardItem KeyboardItem { get; set; }
        /// <summary>
        /// 0 按下
        /// 1 松开
        /// </summary>
        public int Status { get; set; }
        [JsonIgnore]
        public string StatusString
        {
            get => Status == 0 ? "按下" : "松开";
        }
    }
}
