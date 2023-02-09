namespace PCController.UserUI.Pages;

public partial class ControlPage : ContentPage
{
	public ControlPage()
	{
		InitializeComponent();
	}
    private ImageButton MouseImageButton;
    private void ImageButton_Loaded(object sender, EventArgs e)
    {
        MouseImageButton = sender as ImageButton;
    }
    private void OnMousePanUpdated(object sender, PanUpdatedEventArgs e)
    {
        MouseImageButton.TranslationX = e.TotalX;
        MouseImageButton.TranslationY = e.TotalY;
    }

    private async void MouseOuterArea_Tapped(object sender, EventArgs e)
    {
        await MousePanLayoutAnimation();
        Console.WriteLine("Êó±êÓÒ¼üµ¥»÷");
    }

    private async void MouseOuterArea_Tapped2(object sender, EventArgs e)
    {
        await MousePanLayoutAnimation();
        Console.WriteLine("Êó±êÓÒ¼üµ¥Ë«»÷");
    }

    private async void MousePan_Tapped2(object sender, EventArgs e)
    {
        await MousePanAnimation();
        Console.WriteLine("Êó±ê×ó¼üË«»÷");
    }

    private async void MousePan_Tapped(object sender, EventArgs e)
    {
        await MousePanAnimation();
        Console.WriteLine("Êó±ê×ó¼üµ¥»÷");
    }
    private async Task MousePanAnimation()
    {
        await MouseImageButton.ScaleTo(1.1, 50);
        await MouseImageButton.ScaleTo(1, 50);
    }
    private async Task MousePanLayoutAnimation()
    {
        await MousePanLayout.ScaleTo(1.1, 50);
        await MousePanLayout.ScaleTo(1, 50);
    }

    private HorizontalStackLayout MousePanLayout;
    private void HorizontalStackLayout_Loaded(object sender, EventArgs e)
    {
        MousePanLayout = sender as HorizontalStackLayout;
    }

    private async void EditShortcut_Clicked(object sender, EventArgs e)
    {
        ShortcutMgrPage page = new ShortcutMgrPage();
        await Navigation.PushAsync(page);
    }
}