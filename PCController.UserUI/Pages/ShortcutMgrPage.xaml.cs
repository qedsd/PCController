using Newtonsoft.Json;
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
        page.NavigatingFrom += Page_NavigatingFrom;
        await Navigation.PushAsync(page);
    }
    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        var b = sender as Button;
        var shortcut = b.BindingContext as Shortcut;
        EdtingShortcutPage page = new EdtingShortcutPage(shortcut);
        page.NavigatedFrom += Edit_NavigatedFrom;
        await Navigation.PushAsync(page);
    }

    private void Page_NavigatingFrom(object sender, NavigatingFromEventArgs e)
    {
        EdtingShortcutPage page = sender as EdtingShortcutPage;
        //page.NavigatedFrom -= Add_NavigatedFrom;
        if (page.Saved)
        {
            Shortcuts.Add(page.Shortcut);
            Save();
        }
    }

    private void Add_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
       
    }
    #endregion

    private void Init()
    {
        string path = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "shortcuts.json");
        if(System.IO.File.Exists(path))
        {
            string content = System.IO.File.ReadAllText(path);
            if (!string.IsNullOrEmpty(content))
            {
                Shortcuts =  JsonConvert.DeserializeObject<ObservableCollection<Shortcut>>(content);
            }
            else
            {
                Shortcuts = new ObservableCollection<Shortcut>();
            }
        }
        else
        {
            Shortcuts = new ObservableCollection<Shortcut>();
        }
    }
    private void Save()
    {
        string path = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "shortcuts.json");
        string json = JsonConvert.SerializeObject(Shortcuts);
        System.IO.File.WriteAllText(path, json);
    }

    /// <summary>
    /// 给拖动添加数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
    {
        RemoveButton.IsVisible = true;
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

    private async void RemoveDrop_Drop(object sender, DropEventArgs e)
    {
        if(e.Data.Properties.TryGetValue("Shortcut", out var data))
        {
            var sourceData = data as Shortcut;
            if (await DisplayAlert("是否确认", $"确认删除快捷方式:{sourceData.Name}?", "是", "否"))
            {
                Shortcuts.Remove(sourceData);
                Save();
            }
        }
    }

    private void DragGestureRecognizer_DropCompleted(object sender, DropCompletedEventArgs e)
    {
        RemoveButton.IsVisible = false;
    }
}