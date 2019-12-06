using System;

namespace BitmexbotWPF.Objects
{
    public class Candle
    {
            public DateTime? Timestamp { get; set; }
            public string Symbol { get; set; }
            public decimal Open { get; set; }
            public decimal High { get; set; }
            public decimal Low { get; set; }
            public decimal close { get; set; }
            public double trades { get; set; }
            public double volume { get; set; }
            public decimal? vwap { get; set; }
            public double? lastSize { get; set; }
            public double? turnover { get; set; }
            public decimal? homeNotional { get; set; }
            public decimal? foreignNotional { get; set; }


    }
}
