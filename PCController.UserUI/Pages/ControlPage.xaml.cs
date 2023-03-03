using Newtonsoft.Json;
using PCController.UserUI.Controllers;
using PCController.UserUI.Models;
using System.Collections.ObjectModel;

namespace PCController.UserUI.Pages;

public partial class ControlPage : ContentPage
{
	public ControlPage()
	{
		InitializeComponent();
        InitShortcut();
        InitMouseStep();
        Loaded += ControlPage_Loaded;
        DeviceDisplay.Current.MainDisplayInfoChanged += Current_MainDisplayInfoChanged;
    }

    private void ControlPage_Loaded(object sender, EventArgs e)
    {
        SetContent();
    }

    private void Current_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        SetContent();
    }
    private void SetContent()
    {
        if (DeviceDisplay.Current.MainDisplayInfo.Orientation == DisplayOrientation.Portrait)
        {
            //ÊúÆÁ
            Content1.SetValue(Grid.ColumnSpanProperty, 2);
            Content1.SetValue(Grid.RowSpanProperty, 1);
            Content2.SetValue(Grid.RowProperty, 1);
            Content2.SetValue(Grid.ColumnProperty, 0);
            Content2.SetValue(Grid.ColumnSpanProperty, 2);
            Content2.SetValue(Grid.RowSpanProperty, 1);
            MouseStepGrid.IsVisible = true;
        }
        else
        {
            //ºáÅÌ
            Content1.SetValue(Grid.ColumnSpanProperty, 1);
            Content1.SetValue(Grid.RowSpanProperty, 2);
            Content2.SetValue(Grid.ColumnProperty, 1);
            Content2.SetValue(Grid.ColumnSpanProperty, 1);
            Content2.SetValue(Grid.RowProperty, 0);
            Content2.SetValue(Grid.RowSpanProperty, 2);
            MouseStepGrid.IsVisible = false;
        }
    }
    #region Êó±ê¿ØÖÆ
    private ImageButton MouseImageButton;
    private void ImageButton_Loaded(object sender, EventArgs e)
    {
        MouseImageButton = sender as ImageButton;
    }
    private double lastX = 0;
    private double lastY = 0;
    private void OnMousePanUpdated(object sender, PanUpdatedEventArgs e)
    {
        double totalX = Math.Max(-100, Math.Min(100, e.TotalX));
        double totalY = Math.Max(-100, Math.Min(100, e.TotalY));
        Console.WriteLine($"total {totalX} {totalY}");
        MouseImageButton.TranslationX = totalX;
        MouseImageButton.TranslationY = totalY;
        int x = (int)(totalX / 100 * mouseStep);
        int y = (int)(totalY / 100 * mouseStep);
        lastX = x; lastY = y;
        //int x = (int)(e.TotalX > 0 ? mouseStep : -mouseStep);
        //int y = (int)(e.TotalY > 0 ? mouseStep : -mouseStep);
        Console.WriteLine($"act {x} {y}");
        Controllers.MouseControl.Move(x, y);
    }

    private async void MouseOuterArea_Tapped(object sender, EventArgs e)
    {
        Controllers.MouseControl.RightClick();
        await MousePanLayoutAnimation();
        Console.WriteLine("Êó±êÓÒ¼üµ¥»÷");
        HapticFeedback.Default.Perform(HapticFeedbackType.Click);
    }

    private async void MouseOuterArea_Tapped2(object sender, EventArgs e)
    {
        Controllers.MouseControl.RightClick();
        Controllers.MouseControl.RightClick();
        await MousePanLayoutAnimation();
        Console.WriteLine("Êó±êÓÒ¼üµ¥Ë«»÷");
        HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
    }

    private async void MousePan_Tapped2(object sender, EventArgs e)
    {
        Controllers.MouseControl.LeftClick();
        await MousePanAnimation();
        Console.WriteLine("Êó±ê×ó¼üË«»÷");
        HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
    }

    private async void MousePan_Tapped(object sender, EventArgs e)
    {
        Controllers.MouseControl.LeftClick();
        Controllers.MouseControl.LeftClick();
        await MousePanAnimation();
        HapticFeedback.Default.Perform(HapticFeedbackType.Click);
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

    private async void Shortcut_Clicked(object sender, EventArgs e)
    {
        Shortcut shortcut = (sender as Button).BindingContext as Shortcut;
        if (shortcut != null)
        {
            Console.WriteLine(shortcut.Name);
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            foreach (var key in shortcut.KeyStatuses)
            {
                if(key.Status == 0)
                {
                    await KeyboardControl.DownAsync(key.KeyboardItem);
                }
                else
                {
                    await KeyboardControl.UpAsync(key.KeyboardItem);
                }
            }
        }
    }

    /// <summary>
    /// ³õÊ¼»¯¿ì½Ý¼ü
    /// </summary>
    private void InitShortcut()
    {
        EditShortcutButton2.IsVisible = true;
        ShortcutCollection.IsVisible = false;
        
        string path = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "shortcuts.json");
        if (System.IO.File.Exists(path))
        {
            string content = System.IO.File.ReadAllText(path);
            if (!string.IsNullOrEmpty(content))
            {
                var items = JsonConvert.DeserializeObject<ObservableCollection<Shortcut>>(content);
                ShortcutCollection.ItemsSource = items;
                if(items.Count != 0)
                {
                    EditShortcutButton2.IsVisible = false;
                    ShortcutCollection.IsVisible = true;
                }
            }
        }
    }
    #endregion


    #region Êó±êÒÆ¶¯²½½ø
    private double mouseStep;
    private void InitMouseStep()
    {
        mouseStep = Setting.Current.MouseStep;
        MouseStepSlider.Value = mouseStep;
        MouseStepSlider.ValueChanged += OnSliderValueChanged;
    }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        mouseStep = e.NewValue;
        Setting.Current.MouseStep = mouseStep;
        HapticFeedback.Default.Perform(HapticFeedbackType.Click);
        Setting.Current.Save();
    }
    #endregion
}