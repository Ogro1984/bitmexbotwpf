using System;

namespace BitmexbotWPF.Objects
{
    public class Orderbook
    {
            public string Symbol { get; set; }
            public int Level { get; set; }
            public int BidSize { get; set; }
            public decimal BidPrice { get; set; }
            public int AskSize { get; set; }
            public decimal AskPrice { get; set; }
            public DateTime Timestamp { get; set; }
            

    }
}
