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

    #region ����Ԥ�谴ť
    /// <summary>
    /// ���Ԥ�谴��
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
    /// ɾ����ť
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void RemoveKeyStatus_Clicked(object sender, EventArgs e)
    {
        var key = (sender as Button).BindingContext as KeyStatus;
        if(await DisplayAlert("ɾ������?", $"�Ƿ�ɾ��������{key.KeyboardItem.Name}", "��", "��"))
        {
            Shortcut.KeyStatuses.Remove(key);
        }
    }
    public bool Saved;
    /// <summary>
    /// �����ݼ�
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Confirm_Clicked(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(ShortcutName.Text))
        {
            await DisplayAlert("��Ϣȱʧ", "����д��ݼ�����", "ȷ��");
            return;
        }
        if (Shortcut.KeyStatuses.Count == 0)
        {
            await DisplayAlert("��Ϣȱʧ", "��������Ϊ��", "ȷ��");
            return;
        }
        Shortcut.Name = ShortcutName.Text;
        Saved = true;
        //���ս����Shortcut��ȡ
        if (SourceShortcut != null)
        {
            SourceShortcut.Name = Shortcut?.Name;
            SourceShortcut.KeyStatuses = Shortcut?.KeyStatuses;
            Shortcut = SourceShortcut;
        }
        await Navigation.PopAsync();
    }
    #region �༭ѡ����
    private async void KeyStatusItem_Tapped(object sender, EventArgs e)
    {
        EdtingKeyStatusPage page = new EdtingKeyStatusPage((sender as BindableObject).BindingContext as KeyStatus);
        page.NavigatingFrom += EditedKeyStatus;
        await Navigation.PushAsync(page);
    }
    /// <summary>
    /// �༭���
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

    #region ����Զ��尴ť
    /// <summary>
    /// ����Զ��尴��
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
    /// ����Զ��尴�����
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