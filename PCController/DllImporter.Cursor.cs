using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PCController
{
    public static partial class DllImporter
    {
        [DllImport("User32.dll")]  
        public extern static bool GetCursorPos(ref Point pot);

        [DllImport("User32.dll")] 
        public extern static void SetCursorPos(int x, int y);
    }
}
