using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.Models
{
    public class KeyboardParameter
    {
        public byte BVk { get; set; }
        public byte BScan { get; set; }
        public uint DwFlags { get; set; }
        public uint DwExtraInfo { get; set; }
    }
}
