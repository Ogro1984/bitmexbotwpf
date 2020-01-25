﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trady.Core.Infrastructure;

namespace BitmexbotWPF.Objects
{
    public class Specialcandles : IOhlcv
    {
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Volume { get; set; }
        public DateTimeOffset DateTime { get; set; }

    }
}
