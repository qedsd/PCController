using TouchSocket.Core;

namespace PCController.Server
{
    /// <summary>
    /// 控制端用户管理服务
    /// </summary>
    public class UserServer
    {
        private static UserServer current;
        /// <summary>
        /// 单例实例
        /// </summary>
        public static UserServer Current
        {
            get
            {
                if (current == null)
                {
                    current = new UserServer();
                }
                return current;
            }
        }

        /// <summary>
        /// key为Host的name
        /// </summary>
        private readonly Dictionary<string, UserItem> Users = new Dictionary<string, UserItem>();

        /// <summary>
        /// 添加控制端
        /// 若已存在则替换
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        internal bool Add(string hostName, UserItem item)
        {
            Users.AddOrUpdate(hostName, item);
            if (Users.ContainsKey(hostName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 移除控制端
        /// 同步释放通讯
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal bool Remove(string name)
        {
            if (Users.TryGetValue(name, out var item))
            {
                if (Users.Remove(name))
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

        internal UserItem Find(string hostName)
        {
            if(Users.TryGetValue(hostName, out var v))
            {
                return v;
            }
            else
            {
                return null;
            }
        }
    }
}
