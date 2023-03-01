//受控制的电脑

using Newtonsoft.Json;
using PCController.Core.Enums;
using PCController.Core.Helpers;
using PCController.Core.Models;
using PCController.Host;
using PCController.Host.Implements;
using TouchSocket.Core;
using TouchSocket.Http.WebSockets;
using TouchSocket.Sockets;

//CMD.ExcuteBat(@"E:\Code\QEDSD\PCController\PCController.Host\bin\Debug\net6.0\CMD\test.bat",false,false,false);
//return;
Setting setting = Setting.Load();

WebSocketClient myWSClient = new WebSocketClient();
myWSClient.Received += OnReceied;
//myWSClient.Handshaked += this.MyWSClient_Handshaked;

myWSClient.Setup(new TouchSocketConfig()
    .SetRemoteIPHost(setting.WebSocketIPHost));
try
{
    myWSClient.Connect();
    Console.WriteLine("已建立socket连接，开始注册主机");
    string msg = new CMDMsg(setting.Name, Md5Helper.ToMd5(setting.Password), CMDType.HostMgr, null).ToString();
    string reslut = await HttpHelper.PostJsonAsync($"{setting.WebAPIIPHost}/addhost", msg);
    if(!string.IsNullOrEmpty(reslut))
    {
        var res = JsonConvert.DeserializeObject<RequestResult>(reslut);
        if(res != null && res.Successful)
        {
            Console.WriteLine("已成功注册主机，开始确认websocket");
            myWSClient.SendWithWS(msg);
            int tryTime = 0;
            while (tryTime++ != 10)
            {
                Thread.Sleep(1000);
                string reslut2 = await HttpHelper.GetAsync($"{setting.WebAPIIPHost}/hostonline?name={setting.Name}");
                if (!string.IsNullOrEmpty(reslut2))
                {
                    var res2 = JsonConvert.DeserializeObject<RequestResult>(reslut);
                    if (res != null && res.Successful)
                    {
                        Console.WriteLine("websocket确认成功");
                        Console.WriteLine("系统初始化完成！");
                        break;
                    }
                }
                Console.WriteLine("websocket确认失败，继续尝试中...");
            }
            if(tryTime == 10)
            {
                Console.WriteLine("系统初始化失败");
            }
        }
    }
    else
    {
        Console.WriteLine("注册主机失败");
    }
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
    Console.WriteLine($"Received {msg}");
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
            case CMDType.ExcuteBat: CMD.ExcuteBat(cmdMsg.Parameter);break;
            case CMDType.GetBatList:
                {
                    var ls = CMD.GetBatList();
                    string backMsg = new CMDMsg(setting.Name, Md5Helper.ToMd5(setting.Password), CMDType.GetBatList, ls).ToString();
                    myWSClient.SendWithWS(backMsg);
                }
                break;
        }
    }
}