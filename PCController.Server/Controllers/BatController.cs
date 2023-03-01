using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PCController.Core.Enums;
using PCController.Core.Models;
using PCController.Core.MsgParameter;
using System.Collections.Concurrent;

namespace PCController.Server.Controllers
{
    /// <summary>
    /// bat控制器
    /// </summary>
    [ApiController]
    [EnableCors("AllowCors")]
    public class BatController : Controller
    {
        /// <summary>
        /// 向Host发送执行bat请求
        /// </summary>
        /// <param name="parameter">参数</param>
        [Route("excutebat")]
        [HttpPost]
        [Produces("application/json")]
        public RequestResult Post([FromBody] BatParameter parameter)
        {
            if (HostServer.Current.IsValid(parameter.HostName, parameter.GetHostMd5Password()))
            {
                WebSocketService.Current.SendMsg(new CMDMsg(parameter.HostName, parameter.GetHostMd5Password(), CMDType.ExcuteBat, parameter));
                return new RequestResult(true);
            }
            else
            {
                return new RequestResult(false, "主机名称密码不匹配");
            }
        }

        /// <summary>
        /// 请求获取Host上的bat列表
        /// </summary>
        /// <returns></returns>
        [Route("batlist")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<RequestResult> GetList([FromBody] MsgParameter parameter)
        {
            if (HostServer.Current.IsValid(parameter.HostName, parameter.GetHostMd5Password()))
            {
                WebSocketService.Current.OnReceivedBatList += Current_OnReceivedBatList;
                WebSocketService.Current.SendMsg(new CMDMsg(parameter.HostName, parameter.GetHostMd5Password(), CMDType.GetBatList, parameter));
                int count = 0;
                while (count < 20)
                {
                    if(BatMsg != null &&BatMsg.TryGetValue(parameter.HostName,out var msg))
                    {
                        return new RequestResult(true, msg.Parameter.ToString());
                    }
                    else
                    {
                        await Task.Delay(100);
                    }
                }
                return new RequestResult(false);
            }
            else
            {
                return new RequestResult(false, "主机名称密码不匹配");
            }
        }

        private ConcurrentDictionary<string, CMDMsg> BatMsg;
        private void Current_OnReceivedBatList(CMDMsg msg)
        {
            if(BatMsg == null)
            {
                BatMsg = new ConcurrentDictionary<string, CMDMsg>();
            }
            BatMsg.TryAdd(msg.Host.Name, msg);
        }
    }
}
