using System;
using System.Collections.Generic;
using System.Linq;
using BitmexbotWPF.Objects;
using Newtonsoft.Json;
using Trady.Analysis.Extension;
using Trady.Core;
using Trady.Importer.Yahoo;
using Trady.Analysis;

namespace BitmexbotWPF.Helpers
{
    public class OrderHandler
    {
        BitMexApi bitmexapi = new BitMexApi();

        public List<Orderbook> getorderbook(string symbol, int depth)
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = symbol;
            param["depth"] = depth.ToString();   
            string res = bitmexapi.Query("get", "/orderbook", param);
            return JsonConvert.DeserializeObject<List<Orderbook>>(res);
            


        }

        public string GetOrders()
        {
            
            var param = new Dictionary<string, string>();
            param["symbol"] = "XBTUSD";
            //param["filter"] = "{\"open\":true}";
            //param["columns"] = "";
            //param["count"] = 100.ToString();
            //param["start"] = 0.ToString();
            //param["reverse"] = false.ToString();
            //param["startTime"] = "";
            //param["endTime"] = "";
            var response =  bitmexapi.Query("GET", "/order", param, true);
            return response;
        }

        public List<Objects.Candle> GetCandelstickData()
        {
            var today= DateTime.Now.AddHours(3);
            //var todayyymmdd = today.ToString("yyyy-MM-dd");
            var minutesparsed=0;
            var hourparsed = 0;
            if (today.Minute <= 30) { minutesparsed = 30; hourparsed = today.Hour - 1; } else { minutesparsed = 0; hourparsed = today.Hour; }
            //string todayformatted =todayyymmdd + "T" + hourparsed.ToString().PadLeft(2, '0') + ":" + minutesparsed.ToString().PadLeft(2, '0') + ":" + "00.000Z";
            DateTime exacthafhour= new DateTime(today.Year, today.Month, today.Day, hourparsed, minutesparsed, 0);
            var param = new Dictionary<string, string>();
            param["binSize"] = "1m";
            param["symbol"] = "XBTUSD";
            //param["filter"] = "{\"open\":true}";
            //param["columns"] = "" ;
            param["count"] = 480.ToString();
            //param["start"] = 0.ToString();
            param["reverse"] = true.ToString();
            
            //param["startTime"] = todayformatted;
            //param["endTime"] = "";

            var response = bitmexapi.Query("GET", "/trade/bucketed", param, true);

            var rawresult= JsonConvert.DeserializeObject<List<Objects.Candle>>(response);
            List<Objects.Candle> netresult = new List<Objects.Candle>();

            foreach (var candle in rawresult) {
                if (candle.Timestamp <= exacthafhour) { netresult.Add(candle); }

            }

            //var resultList = inverted.FindAll(x => x.Timestamp < exacthafhour).ToList();

            netresult =Unifycandels(netresult);
            //inverted.Reverse();
            var sma = Calcindicatorcci(netresult);
            return netresult;

        }

        public List<Objects.Candle> Unifycandels(List<Objects.Candle> candels)
        {
            List<Objects.Candle> candle30min = new List<Objects.Candle>();
            int counter = 0;
            int j = 0;
            foreach (var candle in candels)
            {

                if ((counter > 0) && (counter < 29))
                {

                    if (candle30min[j].High < candle.High) { candle30min[j].High = candle.High; }
                    if (candle30min[j].Low > candle.Low) { candle30min[j].Low = candle.Low; }
                    counter++;
                }
                else if (counter == 0)
                {
                    candle30min.Add(candle); 
                   
                    counter++;

                }
                else if (counter == 29)
                {


                    if (candle30min[j].High < candle.High) { candle30min[j].High = candle.High; }
                    if (candle30min[j].Low > candle.Low) { candle30min[j].Low = candle.Low; }
                    candle30min[j].close = candle.close;
                    j++;
                    counter = 0;

                }



            }
            return candle30min;
        }

        public List<decimal?> Calcindicatorcci(List<Objects.Candle> candels)
        {

            var closes = new List<decimal>();
            foreach (var candle in candels) { closes.Add(candle.close); }

            var smaTs = closes.Sma(3);
            var cci = closes.Cci(3);
            OhlcvExtension.Cci()
            var sma = closes.Sma(30)[0];
            List<decimal?> smareturned = new List<decimal?>();
            foreach (var item in smaTs) { smareturned.Add(item); }
            return smareturned;
        }


        public string PostOrders()
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = "XBTUSD";
            param["side"] = "Buy";
            param["orderQty"] = "1";
            param["ordType"] = "Market";
            return bitmexapi.Query("POST", "/order", param, true);
        }

    }
}
