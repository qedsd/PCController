using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PCController.Core.Enums;
using PCController.Core.Extensions;
using PCController.Core.Models;
using PCController.Core.MsgParameter;

namespace PCController.Server.Controllers
{
    /// <summary>
    /// 主机管理控制器
    /// </summary>
    [ApiController]
    [EnableCors("AllowCors")]
    public class HostController : Controller
    {
        /// <summary>
        /// 添加主机
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [Route("addhost")]
        [HttpPost]
        [Produces("application/json")]
        public RequestResult Post([FromBody] CMDMsg parameter)
        {
            if(HostServer.Current.Add(parameter.Host.DepthClone<HostItem>()))
            {
                return new RequestResult(true, null);
            }
            else
            {
                return new RequestResult(false, "主机名不可重复");
            }
        }

        /// <summary>
        /// 移除主机
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [Route("removehost")]
        [HttpDelete]
        [Produces("application/json")]
        public RequestResult Delete([FromBody] CMDMsg parameter)
        {
            if (HostServer.Current.Remove(parameter.Host.Name))
            {
                return new RequestResult(true, null);
            }
            else
            {
                return new RequestResult(false, "请确认主机是否存在");
            }
        }

        /// <summary>
        /// 获取主机列表
        /// </summary>
        /// <returns></returns>
        [Route("gethosts")]
        [HttpGet]
        [Produces("application/json")]
        public string[] GetList()
        {
            return HostServer.Current.GetList();
        }

        /// <summary>
        /// 获取主机是否在线
        /// </summary>
        /// <returns></returns>
        [Route("hostonline")]
        [HttpGet]
        [Produces("application/json")]
        public RequestResult Online(string name)
        {
            return new RequestResult(HostServer.Current.IsOnline(name), null);
        }
    }
}
