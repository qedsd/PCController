namespace PCController.UserUI.Pages;

public partial class KeyboardPage : ContentPage
{
	public KeyboardPage()
	{
		InitializeComponent();
        DeviceDisplay.Current.MainDisplayInfoChanged += Current_MainDisplayInfoChanged;

    }

    private void Current_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        
    }
}