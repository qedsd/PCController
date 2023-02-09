using PCController.UserUI.Models;

namespace PCController.UserUI.Pages;

public partial class EdtingShortcutPage : ContentPage
{
    private Shortcut Shortcut;
    public EdtingShortcutPage(Shortcut shortcut)
	{
		InitializeComponent();
        Shortcut = shortcut;
    }

    private async void AddKey_Clicked(object sender, EventArgs e)
    {
        EdtingKeyStatusPage page  = new EdtingKeyStatusPage((sender as Button).BindingContext as KeyStatus);
        await Navigation.PushAsync(page);
        if(page.KeyStatus != null)
        {
            Shortcut.KeyStatuses.Add(page.KeyStatus);
        }
    }

    private void RemoveKeyStatus_Clicked(object sender, EventArgs e)
    {

    }
}