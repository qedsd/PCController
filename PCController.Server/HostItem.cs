using TouchSocket.Http;

namespace PCController.Server
{
    /// <summary>
    /// 主机
    /// </summary>
    public class HostItem : Core.Models.HostItem
    {
        /// <summary>
        /// 主机
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        public HostItem(string name, string password) : base(name, password)
        {
        }

        /// <summary>
        /// WebSocket连接实例
        /// </summary>
        public HttpSocketClient Client { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return Client != null && Client.Online;
        }
    }
}
