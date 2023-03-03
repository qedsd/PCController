using Newtonsoft.Json;
using PCController.Core.Enums;
using PCController.Core.Helpers;
using PCController.Core.Models;
using PCController.Core.MsgParameter;
using PCController.UserUI.Controllers;
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
        DeviceDisplay.Current.MainDisplayInfoChanged += Current_MainDisplayInfoChanged;
    }

    private void Current_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        SetContent();
    }
    private void SetContent()
    {
        if (DeviceDisplay.Current.MainDisplayInfo.Orientation == DisplayOrientation.Portrait)
        {
            //竖屏
            Content1.SetValue(Grid.ColumnSpanProperty, 2);
            Content1.SetValue(Grid.RowSpanProperty, 1);
            Content2.SetValue(Grid.RowProperty, 1);
            Content2.SetValue(Grid.ColumnProperty, 0);
            Content2.SetValue(Grid.ColumnSpanProperty, 2);
            Content2.SetValue(Grid.RowSpanProperty, 1);
        }
        else
        {
            //横盘
            Content1.SetValue(Grid.ColumnSpanProperty, 1);
            Content1.SetValue(Grid.RowSpanProperty, 2);
            Content2.SetValue(Grid.ColumnProperty, 1);
            Content2.SetValue(Grid.ColumnSpanProperty, 1);
            Content2.SetValue(Grid.RowProperty, 0);
            Content2.SetValue(Grid.RowSpanProperty, 2);
        }
    }

    private void CMDPage_Loaded(object sender, EventArgs e)
    {
        SetContent();
        InitWebSocket();
        UseShellExecute.IsChecked = Setting.Current.UseShellExecute;
        RedirectStandardInput.IsChecked = Setting.Current.RedirectStandardInput;
        RedirectStandardOutput.IsChecked = Setting.Current.RedirectStandardOutput;
        RedirectStandardError.IsChecked = Setting.Current.RedirectStandardError;
        CreateNoWindow.IsChecked = Setting.Current.CreateNoWindow;
        Admin.IsChecked = Setting.Current.Admin;
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
            Console.WriteLine("已建立socket连接，开始注册控制端");
            string msg = new CMDMsg(Setting.Current.HostName, Md5Helper.ToMd5(Setting.Current.HostPassword), CMDType.UserMgr, null).ToString();
            webSocket.SendWithWS(msg);
        }
        catch(Exception ex)
        {
            await DisplayAlert("Socket连接失败", ex.Message, "OK");
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
                case CMDType.UserMgr://注册结果
                    {
                        if(bool.TryParse(cmdMsg.Parameter.ToString(), out bool result))
                        {
                            if(!result)
                            {
                                MainThread.BeginInvokeOnMainThread(async() =>
                                {
                                    await DisplayAlert("失败", "注册控制端失败", "OK");
                                });
                            }
                            else
                            {
                                AppendOutput("已注册控制端");
                            }
                        }
                    }break;
                case CMDType.ExcuteCMDResult:
                    {
                        //CMD执行结果
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
            Console.WriteLine($"已断开socket连接:{e.Message}");
            await DisplayAlert("已断开socket连接", e.Message, "OK");
        });
    }

    private void AppendOutput(string str)
    {
        MainThread.BeginInvokeOnMainThread(async() =>
        {
            ResultLabel.Text = output.AppendLine($"[{DateTime.Now}]{str}").ToString();
            await ResultLabelScrollView.ScrollToAsync(ResultLabelScrollView.ScrollX, ResultLabelScrollView.ContentSize.Height * 2, true);
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
    /// 执行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Button_Clicked(object sender, EventArgs e)
    {
        (sender as Button).IsEnabled = false;
        CMDParameter parameter = new CMDParameter()
        {
            FileName = FileName.Text,
            UseShellExecute = UseShellExecute.IsChecked,
            RedirectStandardInput = RedirectStandardInput.IsChecked,
            RedirectStandardOutput = RedirectStandardOutput.IsChecked,
            RedirectStandardError = RedirectStandardError.IsChecked,
            CreateNoWindow = CreateNoWindow.IsChecked,
            Admin = Admin.IsChecked,
            CMD = InputEditor.Text
        };
        var result = await CMDControllercs.ExcuteAsync(parameter);
        if(result != null)
        {
            AppendOutput(result.Successful ? "执行指令成功": $"执行指令失败:{result.Msg}");
        }
        else
        {
            AppendOutput("执行指令失败，请检查网络连接");
        }
        (sender as Button).IsEnabled = true;
    }

    private void ClearButton_Clicked(object sender, EventArgs e)
    {
        ClearOutput();
    }

    private void SettingButton_Clicked(object sender, EventArgs e)
    {
        SettingGrid.IsVisible = !SettingGrid.IsVisible;
    }

    private void UseShellExecute_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Setting.Current.UseShellExecute = e.Value;
        Setting.Current.Save();
    }

    private void RedirectStandardInput_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Setting.Current.RedirectStandardInput = e.Value;
        Setting.Current.Save();
    }

    private void RedirectStandardOutput_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Setting.Current.RedirectStandardOutput = e.Value;
        Setting.Current.Save();
    }

    private void RedirectStandardError_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Setting.Current.RedirectStandardError = e.Value;
        Setting.Current.Save();
    }

    private void CreateNoWindow_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Setting.Current.CreateNoWindow = e.Value;
        Setting.Current.Save();
    }

    private void Admin_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Setting.Current.Admin = e.Value;
        Setting.Current.Save();
    }

    private async void BatButton_Clicked(object sender, EventArgs e)
    {
        BatPage page = new BatPage();
        page.NavigatedFrom += Page_NavigatedFrom;
        await Navigation.PushAsync(page);
    }

    private void Page_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        var page = sender as BatPage;
        if(page.SelectedBat != null)
        {
            FileName.Text = page.SelectedBat;
        }
    }
}