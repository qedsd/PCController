using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.MsgParameter
{
    public class CMDParameter : MsgParameter
    {
        public string FileName { get; set; }
        public bool UseShellExecute { get; set; }
        public bool RedirectStandardInput { get; set; }
        public bool RedirectStandardOutput { get; set; }
        public bool RedirectStandardError { get; set; }
        public bool CreateNoWindow { get; set; }
        public bool Admin { get; set; }
        public string CMD { get; set; }
    }
}
