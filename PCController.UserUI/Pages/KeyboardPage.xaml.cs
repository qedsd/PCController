using PCController.UserUI.Views;

namespace PCController.UserUI.Pages;

public partial class KeyboardPage : ContentPage
{
	public KeyboardPage()
	{
		InitializeComponent();
        DeviceDisplay.Current.MainDisplayInfoChanged += Current_MainDisplayInfoChanged;
        SetContent();
    }

    private void Current_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        SetContent();
    }

    private PortraitKeyboardContent portraitKeyboard;
    private LandscapeKeyboardContent landscapeKeyboard;

    private void SetContent()
    {
        if(DeviceDisplay.Current.MainDisplayInfo.Orientation == DisplayOrientation.Portrait)
        {
            if(portraitKeyboard == null)
            {
                portraitKeyboard = new PortraitKeyboardContent();
            }
            Content = portraitKeyboard;
        }
        else
        {
            if(landscapeKeyboard == null)
            {
                landscapeKeyboard = new LandscapeKeyboardContent();
            }
            Content = landscapeKeyboard;
        }
    }
}