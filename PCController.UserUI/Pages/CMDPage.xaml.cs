using Newtonsoft.Json;
using PCController.Core.Enums;
using PCController.Core.Helpers;
using PCController.Core.Models;
using PCController.UserUI.Models;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using TouchSocket.Core;
using TouchSocket.Http.WebSockets;
using TouchSocket.Sockets;

namespace PCController.UserUI.Pages;

public partial class CMDPage : ContentPage
{
	private WebSocketClient webSocket;
    private StringBuilder output = new StringBuilder();
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
        webSocket.Disconnected += Disconnected;
        webSocket.Setup(new TouchSocketConfig().SetRemoteIPHost(Setting.Current.WebSocketIPHost));
        try
        {
            webSocket.Connect();
            Console.WriteLine("�ѽ���socket���ӣ���ʼע����ƶ�");
            string msg = new CMDMsg(Setting.Current.HostName, Md5Helper.ToMd5(Setting.Current.HostPassword), CMDType.UserMgr, null).ToString();
            webSocket.SendWithWS(msg);
        }
        catch(Exception ex)
        {
            await DisplayAlert("Socket����ʧ��", ex.Message, "OK");
        }
    }
    private void OnReceied(WebSocketClient s, WSDataFrame e)
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
                case CMDType.UserMgr://ע����
                    {
                        if(bool.TryParse(cmdMsg.Parameter.ToString(), out bool result))
                        {
                            if(!result)
                            {
                                MainThread.BeginInvokeOnMainThread(async() =>
                                {
                                    await DisplayAlert("ʧ��", "ע����ƶ�ʧ��", "OK");
                                });
                            }
                            else
                            {
                                AppendOutput("��ע����ƶ�");
                            }
                        }
                    }break;
                case CMDType.ExcuteCMDResult:
                    {
                        //CMDִ�н��
                        AppendOutput(cmdMsg.Parameter.ToString());
                    }
                    break;
            }
        }
    }

    private void Disconnected(ITcpClientBase client, DisconnectEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            Console.WriteLine($"�ѶϿ�socket����:{e.Message}");
            await DisplayAlert("�ѶϿ�socket����", e.Message, "OK");
        });
    }

    private void AppendOutput(string str)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ResultLabel.Text = output.AppendLine($"[{DateTime.Now}]{str}").ToString();
        });
    }
    private void ClearOutput()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            output.Clear();
            ResultLabel.Text = string.Empty;
        });
    }
    /// <summary>
    /// ִ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked(object sender, EventArgs e)
    {
        AppendOutput("ִ��ָ��");
    }

    private void ClearButton_Clicked(object sender, EventArgs e)
    {
        ClearOutput();
    }
}