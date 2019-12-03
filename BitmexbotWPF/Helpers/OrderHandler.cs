using System;
using System.Collections.Generic;
using BitmexbotWPF.Objects;
using Newtonsoft.Json;

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

        public List<Candle> GetCandelstickData()
        {
            var today= DateTime.UtcNow.AddHours(-8);
            var todayyymmdd = today.ToString("yyyy-MM-dd");
            string todayformatted =todayyymmdd + "T" + today.Hour.ToString().PadLeft(2, '0') + ":" + today.Minute.ToString().PadLeft(2, '0') + ":" + "00.000Z";
            var param = new Dictionary<string, string>();
            param["binSize"] = "5m";
            param["symbol"] = "XBTUSD";
            //param["filter"] = "{\"open\":true}";
            //param["columns"] = "" ;
            param["count"] = 96.ToString();
            //param["start"] = 0.ToString();
            param["reverse"] = false.ToString();
            
            param["startTime"] = todayformatted;
            //param["endTime"] = "";

            var response = bitmexapi.Query("GET", "/trade/bucketed", param, true);
            var inverted= JsonConvert.DeserializeObject<List<Candle>>(response);
            inverted.Reverse();
            inverted=Unifycandels(inverted);
            return inverted;

        }

        public List<Candle> Unifycandels(List<Candle> candels) {
            List<Candle> candle30min = new List<Candle>();
            int counter = 0;
            int j = 0;
            foreach (var candle in candels) {

                if (!(counter == 4)&&(counter == 0)) {
                    candle30min[j].Open = candle.Open;
                    counter++;
                }
                else if (!(counter == 4) && (!(counter == 0)))
                {

                } 

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
