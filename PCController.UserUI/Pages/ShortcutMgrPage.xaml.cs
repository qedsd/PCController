using PCController.UserUI.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PCController.UserUI.Pages;

public partial class ShortcutMgrPage : ContentPage
{
    /// <summary>
    /// 数据源
    /// </summary>
    private ObservableCollection<Shortcut> Shortcuts;
    public ShortcutMgrPage()
	{
		InitializeComponent();
        Init();
        Collection.ItemsSource = Shortcuts;
        BindingContext = this;
    }

    #region 编辑
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

    #region 新增
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
    /// 给拖动添加数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
    {
        e.Data.Properties.Add("Shortcut", (sender as DragGestureRecognizer).DragStartingCommandParameter);
    }
    /// <summary>
    /// 处理完成拖放动作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
    {
        var b = (sender as Element).Parent as Button;
        var shortcut = b.BindingContext as Shortcut;
        var targetData = (sender as DropGestureRecognizer).DropCommandParameter as Shortcut;//要被替换的数据
        if(e.Data.Properties.TryGetValue("Shortcut", out var data))//拖动到此的数据
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
            await DisplayAlert("?", "请选择要删除的快捷键", "OK");
        }
        else
        {
            if(await DisplayAlert("?", $"是否确认删除{Collection.SelectedItems.Count}个快捷方式?", "是", "否"))
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