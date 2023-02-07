using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PCController.Core.Enums;
using PCController.Core.Models;
using PCController.Core.MsgParameter;

namespace PCController.Server.Controllers
{
    /// <summary>
    /// 光标位置控制器
    /// </summary>
    [ApiController]
    [EnableCors("AllowCors")]
    public class SetCursorrController : Controller
    {
        /// <summary>
        /// 光标位置控制器
        /// </summary>
        /// <param name="parameter">移动参数</param>
        [Route("setcursor")]
        [HttpPost]
        [Produces("application/json")]
        public RequestResult Post([FromBody] SetCursorPosParameter parameter)
        {
            if (HostServer.Current.IsValid(parameter.HostName, parameter.GetHostMd5Password()))
            {
                WebSocketService.Current.SendMsg(new CMDMsg(parameter.HostName, parameter.GetHostMd5Password(), CMDType.SetCursor, parameter));
                return new RequestResult(true);
            }
            else
            {
                return new RequestResult(false, "主机名称密码不匹配");
            }
        }
    }
}
