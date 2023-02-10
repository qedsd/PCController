using PCController.UserUI.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PCController.UserUI.Pages;

public partial class ShortcutMgrPage : ContentPage
{
    //public ICommand EditItemCommand { get; private set; } = new Command<Shortcut>((item) =>
    //{

    //});
    public ICommand EditItemCommand { get; private set; }
    public ShortcutMgrPage()
	{
		InitializeComponent();
        List<Shortcut> shortcuts = new List<Shortcut>
        {
            new Shortcut()
            {
                Name = "111",
                KeyStatuses = new ObservableCollection<KeyStatus>()
                {
                    new KeyStatus() { KeyboardItem = new Core.Models.KeyboardItem(), Status = 0},
                    new KeyStatus() { KeyboardItem = new Core.Models.KeyboardItem(), Status = 0}
                }
            },
            new Shortcut()
            {
                Name = "222",
                KeyStatuses = new ObservableCollection<KeyStatus>()
                {
                    new KeyStatus() { KeyboardItem = new Core.Models.KeyboardItem(), Status = 0},
                    new KeyStatus() { KeyboardItem = new Core.Models.KeyboardItem(), Status = 0}
                }
            },
            new Shortcut()
            {
                Name = "333",
                KeyStatuses = new ObservableCollection<KeyStatus>()
                {
                    new KeyStatus() { KeyboardItem = new Core.Models.KeyboardItem(), Status = 0},
                    new KeyStatus() { KeyboardItem = new Core.Models.KeyboardItem(), Status = 0}
                }
            },
            new Shortcut()
            {
                Name = "444",
                KeyStatuses = new ObservableCollection<KeyStatus>()
                {
                    new KeyStatus() { KeyboardItem = new Core.Models.KeyboardItem(), Status = 0},
                    new KeyStatus() { KeyboardItem = new Core.Models.KeyboardItem(), Status = 0}
                }
            }
        };
        Collection.ItemsSource = shortcuts;
        EditItemCommand = new Command(() =>
        {

        });
        BindingContext = this;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        Shortcut shortcut = (sender as Button).BindingContext as Shortcut;
        await Navigation.PushAsync(new EdtingShortcutPage(shortcut));
    }

    private async void AddShortcut_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EdtingShortcutPage(null));
    }
}