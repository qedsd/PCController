using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PCController.Core.Enums;
using PCController.Core.Models;
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
        /// <remarks>
        /// Post示例:
        ///
        ///     {
        ///        "dwflags": 0x0001,
        ///        "dx": 1,
        ///        "dy": 2,
        ///        "dwdata": 0,
        ///        "DwExtraInfo": 0,
        ///     }
        ///
        /// </remarks>
        [Route("mousemove")]
        [HttpPost]
        [Produces("application/json")]
        public void Post([FromBody] Core.Models.MouseMoveParameter parameter)
        {
            WebSocketService.Current.SendMsg(new CMDMsg(CMDType.Mouse, parameter));
        }
    }
}
