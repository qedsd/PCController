using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.MsgParameter
{
    public class BatParameter : MsgParameter
    {
        public string BatPath { get; set; }
        public bool UseShellExecute { get; set; }
        public bool CreateNoWindow { get; set; }
        public bool Admin { get; set; }
    }
}
