using Newtonsoft.Json;
using PCController.Core.Helpers;
using PCController.Core.Models;
using PCController.Core.MsgParameter;
using PCController.UserUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.UserUI.Controllers
{
    internal static class CMDControllercs
    {
        internal static async Task<RequestResult> ExcuteAsync(CMDParameter parameter)
        {
            parameter.HostName = Setting.Current.HostName;
            parameter.HostPassword = Setting.Current.HostPassword;
            string result = await HttpHelper.PostJsonAsync($"{Setting.Current.WebAPIIPHost}/excutecmd", JsonConvert.SerializeObject(parameter));
            if(!string.IsNullOrEmpty(result))
            {
                return JsonConvert.DeserializeObject<RequestResult>(result);
            }
            else
            {
                return null;
            }
        }
    }
}
