namespace PCController.UserUI.Pages;

public partial class KeyboardPage : ContentPage
{
	public KeyboardPage()
	{
		InitializeComponent();
	}


    private void MoreKeyboard_Clicked(object sender, EventArgs e)
    {
        MainKeyboard.IsVisible = false;
        MoreKeyboard.IsVisible = true;
    }

    private void MainKeyboard_Clicked(object sender, EventArgs e)
    {
        MainKeyboard.IsVisible = true;
        MoreKeyboard.IsVisible = false;
    }
}