﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Host
{
    internal interface ICMD
    {
        void Execute(string parameter);
    }
}
