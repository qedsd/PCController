using PCController.UserUI.Models;

namespace PCController.UserUI.Pages;

public partial class SettingPage : ContentPage
{
	public SettingPage()
	{
		InitializeComponent();
		WebSocketIPHost.Text = Setting.Current.WebSocketIPHost;
		WebAPIIPHost.Text = Setting.Current.WebAPIIPHost;
        HostName.Text = Setting.Current.HostName;
        Password.Text = Setting.Current.HostPassword;
    }

    private void Confirm_Clicked(object sender, EventArgs e)
    {
        Setting.Current.WebSocketIPHost = WebSocketIPHost.Text;
        Setting.Current.WebAPIIPHost = WebAPIIPHost.Text;
        Setting.Current.HostName = HostName.Text;
        Setting.Current.HostPassword = Password.Text;
        Setting.Current.Save();
    }
}