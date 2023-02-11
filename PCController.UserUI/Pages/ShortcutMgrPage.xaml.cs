using PCController.UserUI.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PCController.UserUI.Pages;

public partial class ShortcutMgrPage : ContentPage
{
    /// <summary>
    /// ����Դ
    /// </summary>
    private ObservableCollection<Shortcut> Shortcuts;
    public ShortcutMgrPage()
	{
		InitializeComponent();
        Init();
        Collection.ItemsSource = Shortcuts;
        BindingContext = this;
    }

    #region �༭
    private async void Button_Clicked(object sender, EventArgs e)
    {
        if(EditGrid.IsVisible)
        {
            return;
        }
        Shortcut shortcut = (sender as Button).BindingContext as Shortcut;
        EdtingShortcutPage page = new EdtingShortcutPage(shortcut);
        page.NavigatedFrom += Edit_NavigatedFrom;
        await Navigation.PushAsync(page);
    }
    private void Edit_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        EdtingShortcutPage page = sender as EdtingShortcutPage;
        page.NavigatedFrom -= Edit_NavigatedFrom;
        if(page.Saved)
        {
            int index = Shortcuts.IndexOf(page.Shortcut);
            Shortcuts.RemoveAt(index);
            Shortcuts.Insert(index, page.Shortcut);
            Save();
        }
    }
    #endregion

    #region ����
    private async void AddShortcut_Clicked(object sender, EventArgs e)
    {
        EdtingShortcutPage page = new EdtingShortcutPage(null);
        page.NavigatedFrom += Add_NavigatedFrom;
        await Navigation.PushAsync(page);
    }
    private void Add_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        EdtingShortcutPage page = sender as EdtingShortcutPage;
        page.NavigatedFrom -= Add_NavigatedFrom;
        if (page.Saved)
        {
            Shortcuts.Add(page.Shortcut);
            Save();
        }
    }
    #endregion

    private void Init()
    {

        Shortcuts = new ObservableCollection<Shortcut>
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
    }
    private void Save()
    {

    }

    /// <summary>
    /// ���϶��������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
    {
        e.Data.Properties.Add("Shortcut", (sender as DragGestureRecognizer).DragStartingCommandParameter);
    }
    /// <summary>
    /// ��������ϷŶ���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
    {
        var b = (sender as Element).Parent as Button;
        var shortcut = b.BindingContext as Shortcut;
        var targetData = (sender as DropGestureRecognizer).DropCommandParameter as Shortcut;//Ҫ���滻������
        if(e.Data.Properties.TryGetValue("Shortcut", out var data))//�϶����˵�����
        {
            var sourceData = data as Shortcut;
            if (targetData != sourceData)
            {
                int index = Shortcuts.IndexOf(targetData);
                Shortcuts.Remove(sourceData);
                Shortcuts.Insert(index, sourceData);
                Save();
            }
        }
    }

    private void Edit_Clicked(object sender, EventArgs e)
    {
        Collection.SelectionMode = SelectionMode.Multiple;
        MgrGrid.IsVisible = false;
        EditGrid.IsVisible = true;
    }

    private async void Remove_Clicked(object sender, EventArgs e)
    {
        if(Collection.SelectedItems.Count == 0)
        {
            await DisplayAlert("?", "��ѡ��Ҫɾ���Ŀ�ݼ�", "OK");
        }
        else
        {
            if(await DisplayAlert("?", $"�Ƿ�ȷ��ɾ��{Collection.SelectedItems.Count}����ݷ�ʽ?", "��", "��"))
            {
                foreach(var item in Collection.SelectedItems)
                {
                    Shortcuts.Remove(item as Shortcut);
                }
            }
        }
    }

    private void OutEdit_Clicked(object sender, EventArgs e)
    {
        Collection.SelectionMode = SelectionMode.None;
        MgrGrid.IsVisible = true;
        EditGrid.IsVisible = false;
    }
}