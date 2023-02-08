using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.UserUI.Models
{
    class Setting
    {
        public string WebSocketIPHost { get; set; } = "ws://127.0.0.1:7789/ws";
        public string WebAPIIPHost { get; set; } = "https://localhost:7106";
        /// <summary>
        /// 名称
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string HostPassword { get; set; }
        public static Setting Current { get; set; }
        public static Setting Load(string path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            }
            if (System.IO.File.Exists(path))
            {
                string content = System.IO.File.ReadAllText(path);
                if (!string.IsNullOrEmpty(content))
                {
                    return string.IsNullOrEmpty(content) ? new Setting() : JsonConvert.DeserializeObject<Setting>(content);
                }
                else
                {
                    return new Setting();
                }
            }
            else
            {
                return new Setting();
            }
        }
        public void Save(string path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            }
            string json = JsonConvert.SerializeObject(this);
            System.IO.File.WriteAllText(path, json);
        }
    }
}
