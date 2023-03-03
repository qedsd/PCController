using Newtonsoft.Json;
using PCController.Core.Models;
using PCController.UserUI.Models;
using System.Reflection;
using System.Text;

namespace PCController.UserUI.Pages;

public partial class PickKeyPage : ContentPage
{
    public List<KeyStatus> KeyStatuses { get; set; }
    private static List<KeyboardItem> keyboardItems;
	public PickKeyPage()
	{
		InitializeComponent();
        Init();
    }
    private async void Init()
    {
        if(keyboardItems == null)
        {
            string localPath = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "keylist.csv");
            if (!System.IO.File.Exists(localPath))
            {
                string content;
                using (Stream stream = await FileSystem.OpenAppPackageFileAsync("keylist.csv"))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        content = reader.ReadToEnd();
                    }
                }
                System.IO.File.WriteAllText(localPath, content);
            }
            var lines = System.IO.File.ReadAllLines(localPath);
            if (lines != null && lines.Length != 0)
            {
                keyboardItems = lines.Select(p => KeyboardItem.FromCsv(p)).ToList();
            }
        }
        KeyList.ItemsSource = keyboardItems?.ToList();
    }

    /// <summary>
    /// ȷ��ѡ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Confirm_Clicked(object sender, EventArgs e)
    {
        if(KeyList.ItemsSource != null)
        {
            if (KeyList.SelectedItems.Count != 0)
            {
                KeyStatuses = new List<KeyStatus>();
                foreach(KeyboardItem item in KeyList.SelectedItems)
                {
                    //�������ɿ�
                    KeyStatuses.Add(new KeyStatus()
                    {
                        KeyboardItem = item,
                        Status = 0
                    });
                    KeyStatuses.Add(new KeyStatus()
                    {
                        KeyboardItem = item,
                        Status = 1
                    });
                }
            }
        }
        await Navigation.PopAsync();
    }
}