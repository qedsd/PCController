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
		var ls = new string[] { "����", "�ɿ�" };
        KeyStatusList.ItemsSource = new string[] { "����", "�ɿ�" };
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
	/// �Ƿ���˱��水ť
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
			await DisplayAlert("��Ϣȱʧ", "����д��������", "ȷ��");
			return false;
		}
        if (string.IsNullOrEmpty(KeyCode.Text))
        {
            await DisplayAlert("��Ϣȱʧ", "����д��������", "ȷ��");
            return false;
        }
        return true;
	}
}