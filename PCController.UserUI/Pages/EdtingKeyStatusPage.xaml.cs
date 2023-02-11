using PCController.UserUI.Models;
using System.Collections;

namespace PCController.UserUI.Pages;

public partial class EdtingKeyStatusPage : ContentPage
{
	public KeyStatus KeyStatus;
    public EdtingKeyStatusPage(KeyStatus keyStatus)
	{
		KeyStatus = keyStatus;
		InitializeComponent();
		var ls = new string[] { "按下", "松开" };
        KeyStatusList.ItemsSource = new string[] { "按下", "松开" };
		if(keyStatus != null)
		{
			KeyStatusList.SelectedItem = ls[keyStatus.Status];
			KeyName.Text = keyStatus.KeyboardItem?.Name;
			KeyCode.Text = keyStatus.KeyboardItem?.Code;
        }
		else
		{
            KeyStatusList.SelectedItem = ls.First();
        }
	}
	/// <summary>
	/// 是否点了保存按钮
	/// </summary>
	public bool Saved { get;private set; }
    private async void Confirm_Clicked(object sender, EventArgs e)
    {
		if(!await Valid())
		{
			return;
		}
		Saved = true;
        if (KeyStatus == null)
		{
            KeyStatus = new KeyStatus();
			KeyStatus.KeyboardItem = new Core.Models.KeyboardItem();
        }
        KeyStatus.KeyboardItem.Name = KeyName.Text;
        KeyStatus.KeyboardItem.Code = KeyCode.Text;
		KeyStatus.Status = KeyStatusList.SelectedItem.Equals((KeyStatusList.ItemsSource as string[]).First()) ? 0 : 1;
		await Navigation.PopAsync();
    }
	private async Task<bool> Valid()
	{
		if(string.IsNullOrEmpty(KeyName.Text))
		{
			await DisplayAlert("信息缺失", "请填写按键名称", "确认");
			return false;
		}
        if (string.IsNullOrEmpty(KeyCode.Text))
        {
            await DisplayAlert("信息缺失", "请填写按键编码", "确认");
            return false;
        }
        return true;
	}
}