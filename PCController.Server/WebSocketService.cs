using Newtonsoft.Json;
using PCController.Core.Models;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using TouchSocket.Core;
using TouchSocket.Http;
using TouchSocket.Http.WebSockets;
using TouchSocket.Sockets;

namespace PCController.Server
{
    /// <summary>
    /// WebSocket用于给Host发送指令，不作消息接受
    /// </summary>
    public class WebSocketService
    {
        public static WebSocketService Current { get; private set; }
        public HttpService Service { get; private set; }
        public WebSocketService(int port= 7789)
        {
            Current = this;
            Service = new HttpService();
            Service.Setup(new TouchSocketConfig()//加载配置
                .UsePlugin()
                .SetListenIPHosts(new IPHost[] { new IPHost(port) })
                .ConfigureContainer(a =>
                {
                    a.SetSingletonLogger<ConsoleLogger>();
                })
                .ConfigurePlugins(a =>
                {
                    a.Add<WebSocketServerPlugin>()//添加WebSocket功能
                           .SetWSUrl("/ws")
                           .SetCallback(WSCallback);//WSCallback回调函数是在WS收到数据时触发回调的。
                })
                //.SetServiceSslOption(new ServiceSslOption() //Ssl配置，当为null的时候，相当于创建了ws服务器，当赋值的时候，相当于wss服务器。
                //{
                //    Certificate = new X509Certificate2("RRQMSocket.pfx", "RRQMSocket"),
                //    SslProtocols = SslProtocols.Tls12
                //})
                ).Start();

            Console.WriteLine("Http服务器已启动");
            Console.WriteLine($"ws://127.0.0.1:{port}/ws");

        }

        static void WSCallback(ITcpClientBase client, WSDataFrameEventArgs e)
        {
            Console.WriteLine($"接受到消息：{e.DataFrame.ToText()}");
        }


        internal void SendMsg(CMDMsg msg)
        {
            if(msg != null)
            {
                SendMsg(JsonConvert.SerializeObject(msg));
            }
        }
        /// <summary>
        /// 广播消息
        /// 暂设定一个服务器只服务于一台Host电脑
        /// </summary>
        /// <param name="msg"></param>
        internal void SendMsg(string msg)
        {
            var clients = Service.GetClients();
            foreach (var item in clients)
            {
                item.SendWithWS(msg);
            }
        }
    }
}
