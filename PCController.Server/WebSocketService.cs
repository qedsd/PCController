using Newtonsoft.Json;
using PCController.Core.Enums;
using PCController.Core.Models;
using PCController.Core.MsgParameter;
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
    /// WebSocket用于给Host发送、接收指令，解决Host可能的无公网IP问题
    /// </summary>
    public class WebSocketService
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static WebSocketService Current { get; private set; }
        /// <summary>
        /// sebsocket服务
        /// </summary>
        public HttpService Service { get; private set; }
        /// <summary>
        /// WebSocketService
        /// </summary>
        /// <param name="port"></param>
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

            Console.WriteLine($"WebSocket服务器已启动:ws://127.0.0.1:{port}/ws");

        }

        static void WSCallback(ITcpClientBase client, WSDataFrameEventArgs e)
        {
            Console.WriteLine($"接受到消息：{e.DataFrame.ToText()}");
            var msg = JsonConvert.DeserializeObject<CMDMsg>(e.DataFrame.ToText());
            if(msg != null)
            {
                switch(msg.Type)
                {
                    case Core.Enums.CMDType.HostMgr:
                        {
                            //此处只能为第一次建立连接时确认
                            var host = JsonConvert.DeserializeObject<HostItem>(JsonConvert.SerializeObject(msg.Host));
                            if(host != null)
                            {
                                HostServer.Current.ConfigClient(host.Name, host.Password, client as HttpSocketClient);
                            }
                        }
                        break;
                    case Core.Enums.CMDType.GetBatList:
                        {
                            //主机回复bat列表
                            //var host = JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(msg.Parameter));
                            Current.OnReceivedBatList?.Invoke(msg);
                        }
                        break;
                    case Core.Enums.CMDType.UserMgr:
                        {
                            //注册控制端
                            //控制端注册时也是传HostItem类型，用以校验
                            var host = JsonConvert.DeserializeObject<HostItem>(JsonConvert.SerializeObject(msg.Host));
                            if (host != null)
                            {
                                if(HostServer.Current.IsValid(host))
                                {
                                    bool su = UserServer.Current.Add(host.Name, new UserItem() { Client = client as HttpSocketClient });
                                    CMDMsg backToUserMsg = new CMDMsg(CMDType.UserMgr,su);
                                    (client as HttpSocketClient)?.SendWithWS(JsonConvert.SerializeObject(backToUserMsg));
                                }
                            }
                        }
                        break;
                }
            }
        }
        internal delegate void ReceiveBatList(CMDMsg msg);
        internal event ReceiveBatList OnReceivedBatList;

        internal void SendMsg(CMDMsg msg)
        {
            if(msg != null)
            {
                var host = HostServer.Current.Get(msg.Host.Name);
                if(host != null)
                {
                    if(host.IsValidPW(msg.Host.Password))
                    {
                        host.Client?.SendWithWS(JsonConvert.SerializeObject(msg));
                    }
                } 
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
