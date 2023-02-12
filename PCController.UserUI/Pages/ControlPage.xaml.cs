using Newtonsoft.Json;
using PCController.UserUI.Models;
using System.Collections.ObjectModel;

namespace PCController.UserUI.Pages;

public partial class ControlPage : ContentPage
{
	public ControlPage()
	{
		InitializeComponent();
        InitShortcut();
    }
    #region Êó±ê¿ØÖÆ
    private ImageButton MouseImageButton;
    private void ImageButton_Loaded(object sender, EventArgs e)
    {
        MouseImageButton = sender as ImageButton;
    }
    private void OnMousePanUpdated(object sender, PanUpdatedEventArgs e)
    {
        MouseImageButton.TranslationX = e.TotalX;
        MouseImageButton.TranslationY = e.TotalY;
        Controllers.MouseControl.Move((int)e.TotalX, (int)e.TotalY);
    }

    private async void MouseOuterArea_Tapped(object sender, EventArgs e)
    {
        Controllers.MouseControl.RightClick();
        await MousePanLayoutAnimation();
        Console.WriteLine("Êó±êÓÒ¼üµ¥»÷");
    }

    private async void MouseOuterArea_Tapped2(object sender, EventArgs e)
    {
        Controllers.MouseControl.RightClick();
        Controllers.MouseControl.RightClick();
        await MousePanLayoutAnimation();
        Console.WriteLine("Êó±êÓÒ¼üµ¥Ë«»÷");
    }

    private async void MousePan_Tapped2(object sender, EventArgs e)
    {
        Controllers.MouseControl.LeftClick();
        await MousePanAnimation();
        Console.WriteLine("Êó±ê×ó¼üË«»÷");
    }

    private async void MousePan_Tapped(object sender, EventArgs e)
    {
        Controllers.MouseControl.LeftClick();
        Controllers.MouseControl.LeftClick();
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
    #endregion

    #region ¿ì½Ý¼ü
    private async void EditShortcut_Clicked(object sender, EventArgs e)
    {
        ShortcutMgrPage page = new ShortcutMgrPage();
        page.NavigatedFrom += Page_NavigatedFrom;
        await Navigation.PushAsync(page);
    }

    private void Page_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        InitShortcut();
    }

    private void Shortcut_Clicked(object sender, EventArgs e)
    {
        Shortcut shortcut = (sender as Button).BindingContext as Shortcut;
        if (shortcut != null)
        {
            Console.WriteLine(shortcut.Name);
        }
    }

    /// <summary>
    /// ³õÊ¼»¯¿ì½Ý¼ü
    /// </summary>
    private void InitShortcut()
    {
        string path = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "shortcuts.json");
        if (System.IO.File.Exists(path))
        {
            string content = System.IO.File.ReadAllText(path);
            if (!string.IsNullOrEmpty(content))
            {
                ShortcutCollection.ItemsSource = JsonConvert.DeserializeObject<ObservableCollection<Shortcut>>(content);
            }
        }
    }
    #endregion
}