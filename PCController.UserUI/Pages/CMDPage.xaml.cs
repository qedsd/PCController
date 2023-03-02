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
            Console.WriteLine("�ѽ���socket���ӣ���ʼע����ƶ�");
            string msg = new CMDMsg(Setting.Current.HostName, Md5Helper.ToMd5(Setting.Current.HostPassword), CMDType.UserMgr, null).ToString();
            webSocket.SendWithWS(msg);
        }
        catch(Exception ex)
        {
            await DisplayAlert("Socket����ʧ��", ex.Message, "OK");
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
                case CMDType.UserMgr://ע����
                    {
                        if(bool.TryParse(cmdMsg.Parameter as string, out bool result))
                        {
                            if(result)
                            {
                                await DisplayAlert("ʧ��", "ע����ƶ�ʧ��", "OK");
                            }
                            else
                            {
                                await DisplayAlert("�ɹ�", "��ע����ƶ�", "OK");
                            }
                        }
                    }break;
                case CMDType.ExcuteCMDResult:
                    {
                        //CMDִ�н��
                        //TODO:���
                    }
                    break;
            }
        }
    }
}