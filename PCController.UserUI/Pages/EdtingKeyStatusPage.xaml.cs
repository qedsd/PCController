using PCController.UserUI.Models;

namespace PCController.UserUI.Pages;

public partial class EdtingKeyStatusPage : ContentPage
{
	public KeyStatus KeyStatus;
    public EdtingKeyStatusPage(KeyStatus keyStatus)
	{
		KeyStatus = keyStatus;
		InitializeComponent();
	}
}