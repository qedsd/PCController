using TouchSocket.Http;

namespace PCController.Server
{
    /// <summary>
    /// 控制端用户
    /// </summary>
    public class UserItem
    {
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
