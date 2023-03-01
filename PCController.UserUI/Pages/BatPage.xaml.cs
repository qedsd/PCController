using Newtonsoft.Json;
using PCController.Core.Helpers;
using PCController.Core.Models;
using PCController.Core.MsgParameter;
using PCController.UserUI.Models;
using System.Reflection.Metadata;

namespace PCController.UserUI.Pages;

public partial class BatPage : ContentPage
{
	public BatPage()
	{
		InitializeComponent();
		UpdateList();
    }
	private async void UpdateList()
	{
		List<string> ls = new List<string>()
		{
			"111",
			"222",
			"333",
			"444"
		};
		MainThread.BeginInvokeOnMainThread(() =>
		{
			BatList.ItemsSource = ls;
		});
		return;
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
            }
		}
	}

    private async void BatList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		Console.WriteLine("Ñ¡Ôñ½Å±¾");
		string bat = (sender as ListView).SelectedItem.ToString();
        (sender as ListView).SelectedItem = null;
        ExcuteBatPage page = new ExcuteBatPage(bat);
        //page.NavigatingFrom += Page_NavigatingFrom;
        await Navigation.PushAsync(page);
    }

    private void Page_NavigatingFrom(object sender, NavigatingFromEventArgs e)
    {
        
    }
}