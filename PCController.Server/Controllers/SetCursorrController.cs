using Microsoft.AspNetCore.Mvc;
using PCController.Core.Models;

namespace PCController.Server.Controllers
{
    public class SetCursorrController : Controller
    {
        /// <summary>
        /// 鼠标移动控制器
        /// </summary>
        /// <param name="parameter">移动参数</param>
        /// <remarks>
        /// Post示例:
        ///
        ///     {
        ///        "X": 1,
        ///        "Y": 1,
        ///     }
        ///
        /// </remarks>
        [Route("setcursor")]
        [HttpPost]
        [Produces("application/json")]
        public void Post([FromBody] Core.Models.SetCursorPosParameter parameter)
        {
            WebSocketService.Current.SendMsg(new CMDMsg(CMDType.SetCursor, parameter));
        }
    }
}
