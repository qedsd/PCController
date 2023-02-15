namespace PCController.UserUI.Pages;

public partial class PortraitKeyboardPage : ContentPage
{
	public PortraitKeyboardPage()
	{
		InitializeComponent();
	}

    private void Button_Pressed(object sender, EventArgs e)
    {
        var keyName = (sender as Button).Text;
        //TODO:匹配按键编码，发出指令
    }

    private void Button_Released(object sender, EventArgs e)
    {
        var keyName = (sender as Button).Text;
        //TODO:匹配按键编码，发出指令
    }
}