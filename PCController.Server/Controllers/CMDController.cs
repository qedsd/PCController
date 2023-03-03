using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PCController.Core.Enums;
using PCController.Core.Models;
using PCController.Core.MsgParameter;

namespace PCController.Server.Controllers
{
    /// <summary>
    /// 指令控制器
    /// </summary>
    [ApiController]
    [EnableCors("AllowCors")]
    public class CMDController : Controller
    {
        /// <summary>
        /// 向Host发送执行指令请求
        /// </summary>
        /// <param name="parameter">参数</param>
        [Route("excutecmd")]
        [HttpPost]
        [Produces("application/json")]
        public RequestResult Post([FromBody] CMDParameter parameter)
        {
            if (HostServer.Current.IsValid(parameter.HostName, parameter.GetHostMd5Password()))
            {
                WebSocketService.Current.SendMsg(new CMDMsg(parameter.HostName, parameter.GetHostMd5Password(), CMDType.ExcuteCMD, parameter));
                return new RequestResult(true);
            }
            else
            {
                return new RequestResult(false, "主机名称密码不匹配");
            }
        }
    }
}
