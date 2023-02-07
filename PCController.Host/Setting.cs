using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Host
{
    internal class Setting
    {
        public string WebSocketIPHost { get; set; } = "ws://127.0.0.1:7789/ws";
        public string WebAPIIPHost { get; set; } = "https://localhost:7106";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = Guid.NewGuid().ToString().Split('-').First();
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = Guid.NewGuid().ToString().Split('-').Last();

        public static Setting Load(string path = null)
        {
            if(string.IsNullOrEmpty(path))
            {
                path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            }
            if(System.IO.File.Exists(path))
            {
                string content = System.IO.File.ReadAllText(path);
                if(!string.IsNullOrEmpty(content))
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
    }
}
