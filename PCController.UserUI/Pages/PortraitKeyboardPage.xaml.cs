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
        //TODO:ƥ�䰴�����룬����ָ��
    }

    private void Button_Released(object sender, EventArgs e)
    {
        var keyName = (sender as Button).Text;
        //TODO:ƥ�䰴�����룬����ָ��
    }
}