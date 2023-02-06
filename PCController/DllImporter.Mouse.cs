using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PCController
{
    public static partial class DllImporter
    {
        /// <summary>
        /// 鼠标移动和点击
        /// </summary>
        /// <param name="dwFlags">
        /// 标志位集，指定点击按钮和鼠标动作的多种情况。此参数里的各位可以是下列值的任何合理组合
        /// MOUSEEVENTF_ABSOLUTE：表明参数dX，dy含有规范化的绝对坐标。如果不设置此位，参数含有相对数据
        /// MOUSEEVENTF_MOVE：表明发生移动
        /// MOUSEEVENTF_LEFTDOWN：表明接按下鼠标左键
        /// MOUSEEVENTF_LEFTUP：表明松开鼠标左键
        /// MOUSEEVENTF_RIGHTDOWN：表明按下鼠标右键
        /// MOUSEEVENTF_RIGHTUP：表明松开鼠标右键
        /// MOUSEEVENTF_MIDDLEDOWN：表明按下鼠标中键
        /// MOUSEEVENTF_MIDDLEUP：表明松开鼠标中键
        /// MOUSEEVENTF_WHEEL：在Windows NT中如果鼠标有一个轮，表明鼠标轮被移动。移动的数量由dwData给出
        /// </param>
        /// <param name="dx">
        /// dx：指定鼠标沿x轴的绝对位置或者从上次鼠标事件产生以来移动的数量，依赖于MOUSEEVENTF_ABSOLUTE的设置。
        /// 给出的绝对数据作为鼠标的实际X坐标；给出的相对数据作为移动的mickeys数。一个mickey表示鼠标移动的数量，表明鼠标已经移动</param>
        /// <param name="dy">同dx，表示y轴</param>
        /// <param name="dwData">如果dwFlags为MOUSEEVENTF_WHEEL，则dwData指定鼠标轮移动的数量。
        /// 正值表明鼠标轮向前转动，即远离用户的方向；负值表明鼠标轮向后转动，即朝向用户。一个轮击定义为WHEEL_DELTA，即120。
        /// 如果dwFlags不是MOUSEEVENTF_WHEEL，则dWData应为零</param>
        /// <param name="dwExtraInfo">指定与鼠标事件相关的附加32位值。应用程序调用函数GetMessageExtraInfo来获得此附加信息</param>
        /// <returns></returns>
        [DllImport("user32", CharSet = CharSet.Unicode)]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        //移动鼠标
        const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;
    }
}
