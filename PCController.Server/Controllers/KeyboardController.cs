using Microsoft.AspNetCore.Mvc;
using PCController.Core.Models;

namespace PCController.Server.Controllers
{
    /// <summary>
    /// 键盘控制器
    /// </summary>
    public class KeyboardController : Controller
    {
        /// <summary>
        /// 键盘控制器
        /// </summary>
        /// <param name="parameter">键盘参数</param>
        /// <remarks>
        /// Post示例:
        ///
        ///     {
        ///        "BVk": 1,
        ///        "BScan": 0,
        ///        "DwFlags": 0,
        ///        "DwExtraInfo": 0,
        ///     }
        ///
        /// </remarks>
        [Route("keyboard")]
        [HttpPost]
        [Produces("application/json")]
        public void Post([FromBody] Core.Models.KeyboardParameter parameter)
        {
            WebSocketService.Current.SendMsg(new CMDMsg(CMDType.Keyboard, parameter));
        }
    }
}
