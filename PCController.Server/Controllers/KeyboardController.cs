using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PCController.Core.Enums;
using PCController.Core.Models;
using PCController.Core.MsgParameter;

namespace PCController.Server.Controllers
{
    /// <summary>
    /// 键盘控制器
    /// </summary>
    [ApiController]
    [EnableCors("AllowCors")]
    public class KeyboardController : Controller
    {
        /// <summary>
        /// 键盘控制器
        /// </summary>
        /// <param name="parameter">键盘参数</param>
        [Route("keyboard")]
        [HttpPost]
        [Produces("application/json")]
        public RequestResult Post([FromBody] KeyboardParameter parameter)
        {
            if(HostServer.Current.IsValid(parameter.HostName, parameter.GetHostMd5Password()))
            {
                WebSocketService.Current.SendMsg(new CMDMsg(parameter.HostName, parameter.GetHostMd5Password(), CMDType.Keyboard, parameter));
                return new RequestResult(true);
            }
            else
            {
                return new RequestResult(false,"主机名称密码不匹配");
            }
        }
    }
}
