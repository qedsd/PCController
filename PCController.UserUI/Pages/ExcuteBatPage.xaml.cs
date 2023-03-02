using Microsoft.Maui.LifecycleEvents;
using Newtonsoft.Json;
using PCController.Core.Helpers;
using PCController.Core.Models;
using PCController.Core.MsgParameter;
using PCController.UserUI.Models;

namespace PCController.UserUI.Pages;

public partial class ExcuteBatPage : ContentPage
{
	public ExcuteBatPage(string name)
	{
		InitializeComponent();
		BatName.Text = name;
	}
	public BatParameter GetBatParameter()
	{
		return new BatParameter()
		{
			BatPath = BatName.Text,
			UseShellExecute = UseShellExecute.IsToggled,
			CreateNoWindow = CreateNoWindow.IsToggled,
			Admin = Admin.IsToggled,
		};
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		var p = GetBatParameter();
		p.HostName = Setting.Current.HostName;
		p.HostPassword= Setting.Current.HostPassword;
        ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true };
        var result = await HttpHelper.PostJsonAsync($"{Setting.Current.WebAPIIPHost}/excutebat", JsonConvert.SerializeObject(p));
		activityIndicator.IsRunning = false;
		if(string.IsNullOrEmpty(result))
		{
            await DisplayAlert("失败", "未成功执行脚本，请检查网络连接", "OK");
        }
		else
		{
			var rq = JsonConvert.DeserializeObject<RequestResult>(result);
			if(rq == null)
			{
                await DisplayAlert("失败", "未成功执行脚本，未知错误", "OK");
            }
			else
			{
				if(rq.Successful)
				{
                    ToolTipProperties.SetText(sender as Button, "成功执行脚本");
                    await Navigation.PopAsync();
                }
				else
				{
                    await DisplayAlert("失败", rq.Msg, "OK");
                }
			}
		}
    }
}