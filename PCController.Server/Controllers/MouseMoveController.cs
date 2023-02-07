using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PCController.Core.Enums;
using PCController.Core.Models;
using PCController.Core.MsgParameter;
using System.Reflection;

namespace PCController.Server.Controllers
{
    /// <summary>
    /// 鼠标移动控制器
    /// </summary>
    [ApiController]
    [EnableCors("AllowCors")]
    public class MouseMoveController : Controller
    {
        /// <summary>
        /// 鼠标移动控制器
        /// </summary>
        /// <param name="parameter">移动参数</param>
        [Route("mousemove")]
        [HttpPost]
        [Produces("application/json")]
        public RequestResult Post([FromBody] MouseMoveParameter parameter)
        {
            if (HostServer.Current.IsValid(parameter.HostName, parameter.GetHostMd5Password()))
            {
                WebSocketService.Current.SendMsg(new CMDMsg(parameter.HostName, parameter.GetHostMd5Password(), CMDType.Mouse, parameter));
                return new RequestResult(true);
            }
            else
            {
                return new RequestResult(false, "主机名称密码不匹配");
            }
        }
    }
}
