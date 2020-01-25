using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmexbotWPF.Objects
{
    class CCI
    {
        public DateTime Timestamp { get; set; }
        public decimal? Value { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal? SMA { get; set; }
        public decimal? MD { get; set; }
        public decimal? TP { get; set; }

    }

    class SMA
    {
        public DateTime Timestamp { get; set; }
        public decimal Value { get; set; }
    }

    class TP
    {
        public DateTime Timestamp { get; set; }
        public decimal Value { get; set; }
    }
    
    class MD
    {
        public DateTime Timestamp { get; set; }
        public decimal Value { get; set; }
       


    }




}
