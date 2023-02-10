using Newtonsoft.Json;
using PCController.Core.Models;
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
        public static Setting Load()
        {
            string localPath = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "appsettings.json");
            if(!System.IO.File.Exists(localPath))
            {
                string content;
                using (Stream stream = FileSystem.OpenAppPackageFileAsync("appsettings.json").Result)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        content = reader.ReadToEnd();
                    }
                }
                System.IO.File.WriteAllText(localPath, content);
            }
            string content2 = System.IO.File.ReadAllText(localPath);
            if (!string.IsNullOrEmpty(content2))
            {
                return string.IsNullOrEmpty(content2) ? new Setting() : JsonConvert.DeserializeObject<Setting>(content2);
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
                path = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "appsettings.json");
            }
            string json = JsonConvert.SerializeObject(this);
            System.IO.File.WriteAllText(path, json);
        }
    }
}
