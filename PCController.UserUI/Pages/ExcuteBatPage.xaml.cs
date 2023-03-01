using Newtonsoft.Json;
using PCController.Core.Helpers;
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
        await HttpHelper.PostJsonAsync($"{Setting.Current.WebAPIIPHost}/excutebat", JsonConvert.SerializeObject(p));
        await Navigation.PopAsync();
    }
}