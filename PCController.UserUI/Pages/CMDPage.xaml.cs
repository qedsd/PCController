using Newtonsoft.Json;
using PCController.Core.Enums;
using PCController.Core.Helpers;
using PCController.Core.Models;
using PCController.UserUI.Models;
using TouchSocket.Core;
using TouchSocket.Http.WebSockets;
using TouchSocket.Sockets;

namespace PCController.UserUI.Pages;

public partial class CMDPage : ContentPage
{
	private WebSocketClient webSocket;

    public CMDPage()
	{
		InitializeComponent();
        Loaded += CMDPage_Loaded;
	}

    private void CMDPage_Loaded(object sender, EventArgs e)
    {
        InitWebSocket();
    }

    private async void InitWebSocket()
	{
		webSocket = new WebSocketClient();
		webSocket.Received += OnReceied;
        webSocket.Setup(new TouchSocketConfig().SetRemoteIPHost(Setting.Current.WebSocketIPHost));
        try
        {
            webSocket.Connect();
            Console.WriteLine("已建立socket连接，开始注册控制端");
            string msg = new CMDMsg(Setting.Current.HostName, Md5Helper.ToMd5(Setting.Current.HostPassword), CMDType.UserMgr, null).ToString();
            webSocket.SendWithWS(msg);
        }
        catch(Exception ex)
        {
            await DisplayAlert("Socket连接失败", ex.Message, "OK");
        }
    }
    async void OnReceied(WebSocketClient s, WSDataFrame e)
	{
        string msg = e.ToText();
        if (string.IsNullOrEmpty(msg))
        {
            return;
        }
        Console.WriteLine($"Received {msg}");
        var cmdMsg = JsonConvert.DeserializeObject<CMDMsg>(msg);
        if(cmdMsg != null)
        {
            switch(cmdMsg.Type)
            {
                case CMDType.UserMgr://注册结果
                    {
                        if(bool.TryParse(cmdMsg.Parameter as string, out bool result))
                        {
                            if(result)
                            {
                                await DisplayAlert("失败", "注册控制端失败", "OK");
                            }
                            else
                            {
                                await DisplayAlert("成功", "已注册控制端", "OK");
                            }
                        }
                    }break;
                case CMDType.ExcuteCMDResult:
                    {
                        //CMD执行结果
                        //TODO:输出
                    }
                    break;
            }
        }
    }
}