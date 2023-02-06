//受控制的电脑
//TODO:websocket或长连接tcp监听服务器指令

using Newtonsoft.Json;
using PCController.Core.Models;
using PCController.Host;
using PCController.Host.Implements;
using TouchSocket.Core;
using TouchSocket.Http.WebSockets;
using TouchSocket.Sockets;

Setting setting = Setting.Load();

WebSocketClient myWSClient = new WebSocketClient();
myWSClient.Received += OnReceied;
//myWSClient.Handshaked += this.MyWSClient_Handshaked;

myWSClient.Setup(new TouchSocketConfig()
    .SetRemoteIPHost(setting.IPHost));
try
{
    myWSClient.Connect();
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}

while (true)
{
    Console.ReadLine();
}

void OnReceied(WebSocketClient s, WSDataFrame e)
{
    string msg = e.ToText();
    if(string.IsNullOrEmpty(msg))
    {
        return;
    }
    var cmdMsg = JsonConvert.DeserializeObject<CMDMsg>(msg);
    if(cmdMsg != null)
    {
        switch(cmdMsg.Type)
        {
            case CMDType.GetCursor: Cursor.GetCursorPos();break;
            case CMDType.SetCursor: Cursor.SetCursorPos(cmdMsg.Parameter); break;
            case CMDType.Keyboard: Keyboard.Execute(cmdMsg.Parameter); break;
            case CMDType.Mouse: Mouse.Execute(cmdMsg.Parameter); break;
            case CMDType.SetForegroundWindow: Window.SetForegroundWindow(cmdMsg.Parameter); break;
            case CMDType.ShowWindow: Window.ShowWindow(cmdMsg.Parameter); break;
        }
    }
}