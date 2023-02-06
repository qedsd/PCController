using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.Models
{
    public class ShowWindowParameter
    {
        public IntPtr HWnd { get; set; }
        public int NCmdShow { get; set; }
    }
}
