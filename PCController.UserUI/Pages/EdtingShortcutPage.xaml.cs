using PCController.Core.Extensions;
using PCController.UserUI.Models;
using System.Collections.ObjectModel;

namespace PCController.UserUI.Pages;

public partial class EdtingShortcutPage : ContentPage
{
    private Shortcut SourceShortcut;
    public Shortcut Shortcut;
    public EdtingShortcutPage(Shortcut shortcut)
	{
		InitializeComponent();
        SourceShortcut = shortcut;
        if(SourceShortcut != null )
        {
            Shortcut = SourceShortcut.DepthClone<Shortcut>();
        }
        else
        {
            Shortcut = new Shortcut();
        }
        if (Shortcut.KeyStatuses == null)
        {
            Shortcut.KeyStatuses = new ObservableCollection<KeyStatus>();
        }
        ShortcutName.Text = Shortcut.Name;
        KeyStatusList.ItemsSource = Shortcut.KeyStatuses;
    }

    #region 增加预设按钮
    /// <summary>
    /// 添加预设按键
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void AddKey_Clicked(object sender, EventArgs e)
    {
        PickKeyPage page = new PickKeyPage();
        page.NavigatingFrom += PickedKeyStatus;
        await Navigation.PushAsync(page);
    }
    private void PickedKeyStatus(object sender, NavigatingFromEventArgs e)
    {
        var page = sender as PickKeyPage;
        page.NavigatingFrom -= PickedKeyStatus;
        if (page.KeyStatuses != null)
        {
            foreach (var keyStatus in page.KeyStatuses)
            {
                Shortcut.KeyStatuses.Add(keyStatus);
            }
        }
    }
    #endregion

    /// <summary>
    /// 删除按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void RemoveKeyStatus_Clicked(object sender, EventArgs e)
    {
        var key = (sender as Button).BindingContext as KeyStatus;
        if(await DisplayAlert("删除按键?", $"是否删除按键：{key.KeyboardItem.Name}", "是", "否"))
        {
            Shortcut.KeyStatuses.Remove(key);
        }
    }
    public bool Saved;
    /// <summary>
    /// 保存快捷键
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Confirm_Clicked(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(ShortcutName.Text))
        {
            await DisplayAlert("信息缺失", "请填写快捷键名称", "确认");
            return;
        }
        if (Shortcut.KeyStatuses.Count == 0)
        {
            await DisplayAlert("信息缺失", "按键不可为空", "确认");
            return;
        }
        Shortcut.Name = ShortcutName.Text;
        Saved = true;
        //最终结果从Shortcut获取
        if (SourceShortcut != null)
        {
            SourceShortcut.Name = Shortcut?.Name;
            SourceShortcut.KeyStatuses = Shortcut?.KeyStatuses;
            Shortcut = SourceShortcut;
        }
        await Navigation.PopAsync();
    }
    #region 编辑选中项
    private async void KeyStatusItem_Tapped(object sender, EventArgs e)
    {
        EdtingKeyStatusPage page = new EdtingKeyStatusPage((sender as BindableObject).BindingContext as KeyStatus);
        page.NavigatingFrom += EditedKeyStatus;
        await Navigation.PushAsync(page);
    }
    /// <summary>
    /// 编辑结果
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EditedKeyStatus(object sender, NavigatingFromEventArgs e)
    {
        var page = sender as EdtingKeyStatusPage;
        page.NavigatingFrom -= EditedKeyStatus;
        if (page.KeyStatus != null && page.Saved)
        {
            var index = Shortcut.KeyStatuses.IndexOf(page.KeyStatus);
            Shortcut.KeyStatuses.Remove(page.KeyStatus);
            Shortcut.KeyStatuses.Insert(index, page.KeyStatus);
        }
    }
    #endregion

    #region 添加自定义按钮
    /// <summary>
    /// 添加自定义按键
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void AddMyKey_Clicked(object sender, EventArgs e)
    {
        EdtingKeyStatusPage page = new EdtingKeyStatusPage(null);
        page.NavigatingFrom += AddedKeyStatus;
        await Navigation.PushAsync(page);
    }

    /// <summary>
    /// 添加自定义按键结果
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddedKeyStatus(object sender, NavigatingFromEventArgs e)
    {
        var page = sender as EdtingKeyStatusPage;
        page.NavigatingFrom -= AddedKeyStatus;
        if (page.KeyStatus != null)
        {
            Shortcut.KeyStatuses.Add(page.KeyStatus);
        }
    }
    #endregion
}