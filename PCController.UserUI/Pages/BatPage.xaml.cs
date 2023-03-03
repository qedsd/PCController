using Newtonsoft.Json;
using PCController.Core.Helpers;
using PCController.Core.Models;
using PCController.Core.MsgParameter;
using PCController.UserUI.Models;

namespace PCController.UserUI.Pages;

public partial class BatPage : ContentPage
{
	public BatPage()
	{
		InitializeComponent();
		UpdateList();
		SelectedBat = null;
    }
	private List<string> SourceBatList;
    private List<string> ShowBatList;
    private async void UpdateList()
	{
		MsgParameter parameter = new MsgParameter();
        parameter.HostName = Setting.Current.HostName;
        parameter.HostPassword = Setting.Current.HostPassword;
        string result = await HttpHelper.PostJsonAsync($"{Setting.Current.WebAPIIPHost}/batlist", JsonConvert.SerializeObject(parameter));
		if (result != null)
		{
            RequestResult requestResult = JsonConvert.DeserializeObject<RequestResult>(result);
			if(requestResult != null && requestResult.Successful)
			{
                var list = JsonConvert.DeserializeObject<List<string>>(requestResult.Msg);
                if(list != null && list.Count != 0)
                {
                    SourceBatList = list;
                    ShowBatList = new List<string>();
                    foreach(var p in list)
                    {
                        //ShowBatList.Add(System.IO.Path.GetFileNameWithoutExtension(p));
                        ShowBatList.Add(p.Substring(p.LastIndexOf('\\') + 1));
                    }
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        BatList.ItemsSource = ShowBatList;
                    });
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        BatList.ItemsSource = null;
                    });
                }
            }
		}
	}

	public string SelectedBat;
    private async void BatList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		Console.WriteLine("Ñ¡Ôñ½Å±¾");
        var index = ShowBatList.IndexOf((sender as ListView).SelectedItem.ToString());
        if(index >= 0)
        {
            SelectedBat = SourceBatList[index];
            await Navigation.PopAsync();
        }
    }

    private void Page_NavigatingFrom(object sender, NavigatingFromEventArgs e)
    {
        
    }
}