using Microsoft.Extensions.Hosting;
using TouchSocket.Http;

namespace PCController.Server
{
    /// <summary>
    /// 主机管理服务
    /// </summary>
    public class HostServer
    {
        //TODO:是否需要保存主机列表到本地，下次启动自动读取

        private static HostServer current;
        /// <summary>
        /// 单例实例
        /// </summary>
        public static HostServer Current
        {
            get
            {
                if(current ==null)
                {
                    current = new HostServer();
                }
                return current;
            }
        }

        private readonly Dictionary<string, HostItem> Hosts = new Dictionary<string, HostItem>();

        /// <summary>
        /// 是否存在目标主机、密码是否正确
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pw"></param>
        /// <returns></returns>
        public bool IsValid(string name, string pw)
        {
            if (Hosts.TryGetValue(name, out var item))
            {
                return item.IsValidPW(pw);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否存在目标主机、密码是否正确
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public bool IsValid(HostItem host)
        {
            return IsValid(host.Name, host.Password);
        }

        /// <summary>
        /// 添加主机
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(HostItem item)
        {
            if(item != null)
            {
                return Add(item.Name, item.Password);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 添加主机
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pw"></param>
        /// <returns></returns>
        public bool Add(string name, string pw)
        {
            if (Hosts.TryGetValue(name, out var item))
            {
                if (item.IsConnected())
                {
                    return false;
                }
                else
                {
                    Hosts.Remove(item.Name);
                    Hosts.Add(pw, new HostItem(name, pw));
                    return true;
                }
            }
            else
            {
                return Hosts.TryAdd(name, new HostItem(name, pw));
            }
        }

        /// <summary>
        /// 移除主机
        /// 同步释放通讯
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Remove(string name)
        {
            if(Hosts.TryGetValue(name, out var item))
            {
                if(Hosts.Remove(name))
                {
                    if (item.Client != null)
                    {
                        item.Client.Close();
                        item.Client.Dispose();
                        item.Client = null;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// 获取主机列表
        /// </summary>
        /// <returns></returns>
        public string[] GetList()
        {
            return Hosts.Keys.ToArray();
        }

        /// <summary>
        /// 赋值client
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pw"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool ConfigClient(string name, string pw, HttpSocketClient client)
        {
            if (Hosts.TryGetValue(name, out var item))
            {
                if(item.IsValidPW(pw))
                {
                    item.Client = client;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否在线
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsOnline(string name)
        {
            if(Hosts.TryGetValue(name, out var item))
            {
                return item.IsConnected();
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 按host名称获取实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public HostItem Get(string name)
        {
            return Hosts.TryGetValue(name, out var item) ? item : null;
        }
    }
}
