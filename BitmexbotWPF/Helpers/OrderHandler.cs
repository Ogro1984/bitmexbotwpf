using System;
using System.Collections.Generic;
using System.Linq;
using BitmexbotWPF.Objects;
using Newtonsoft.Json;
using Trady.Analysis.Extension;
using Trady.Core;
using Trady.Importer.Yahoo;
using Trady.Analysis;
using Trady.Analysis.Indicator;
using Trady.Core.Infrastructure;

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
            var minutesparsed=0;
            var hourparsed = 0;
            if (today.Minute >= 30) { minutesparsed = 30; hourparsed = today.Hour; } else { minutesparsed = 0; hourparsed = today.Hour;  }
            DateTime exacthafhour= new DateTime(today.Year, today.Month, today.Day, hourparsed, minutesparsed, 0);
           // DateTime exacthafhour = new DateTime(today.Year, today.Month, today.Day, 22, 0, 0);

            var param = new Dictionary<string, string>();
            param["binSize"] = "5m";
            param["symbol"] = "XBTUSD";
            //param["filter"] = "{\"open\":true}";
            //param["columns"] = "" ;
            param["count"] = 607.ToString();
            //param["start"] = 0.ToString();
            param["reverse"] = true.ToString();
            
            //param["startTime"] = todayformatted;
            //param["endTime"] = "";

            var response = bitmexapi.Query("GET", "/trade/bucketed", param, true);

            var rawresult= JsonConvert.DeserializeObject<List<Objects.Candle>>(response);
            List<Objects.Candle> netresult = new List<Objects.Candle>();
            var counter = 0;

            foreach (var candle in rawresult)
            {
                Objects.Candle tempcandle = new Objects.Candle();
                tempcandle = candle;
                tempcandle.Timestamp=   tempcandle.Timestamp.AddMinutes(-5);
         
                if ((tempcandle.Timestamp < exacthafhour)&&(counter<=599)) {
                   
                                      
                    netresult.Add(tempcandle); }
                counter++;
            }
            
            

              netresult = Unifycandels(netresult);

             var sma = Calcindicatorcci(netresult);
      
            netresult.Reverse();
            Indicators indicators = new Indicators();
            List<CCI> ccii = indicators.CCICalculator(netresult, 20);
            var cc222 = Calcindicatorcci(netresult);


            return netresult;
        }

        public List<Objects.Candle> Indicatortesting() {

            List<Objects.Candle> candles = new List<Objects.Candle>();


            Objects.Candle c1 = new Objects.Candle();
            Objects.Candle c2 = new Objects.Candle();
            Objects.Candle c3 = new Objects.Candle();
            Objects.Candle c4 = new Objects.Candle();
            Objects.Candle c5 = new Objects.Candle();
            Objects.Candle c6 = new Objects.Candle();
            Objects.Candle c7 = new Objects.Candle();
            Objects.Candle c8 = new Objects.Candle();
            Objects.Candle c9 = new Objects.Candle();
            Objects.Candle c10 = new Objects.Candle();
            Objects.Candle c11 = new Objects.Candle();
            Objects.Candle c12 = new Objects.Candle();
            Objects.Candle c13 = new Objects.Candle();
            Objects.Candle c14 = new Objects.Candle();
            Objects.Candle c15 = new Objects.Candle();
            Objects.Candle c16 = new Objects.Candle();
            Objects.Candle c17 = new Objects.Candle();
            Objects.Candle c18 = new Objects.Candle();
            Objects.Candle c19 = new Objects.Candle();
            Objects.Candle c20 = new Objects.Candle();
            Objects.Candle c21 = new Objects.Candle();
            Objects.Candle c22 = new Objects.Candle();
            Objects.Candle c23 = new Objects.Candle();
            Objects.Candle c24 = new Objects.Candle();
            Objects.Candle c25 = new Objects.Candle();
            Objects.Candle c26 = new Objects.Candle();
            Objects.Candle c27 = new Objects.Candle();
            Objects.Candle c28 = new Objects.Candle();
            Objects.Candle c29 = new Objects.Candle();
            Objects.Candle c30 = new Objects.Candle();



            c1.Open = (decimal)23.9429;
            c1.High = (decimal)24.2013;
            c1.Low = (decimal)23.8534;
            c1.close = (decimal)23.8932;


            c2.Open = (decimal)23.8534;
            c2.High = (decimal)24.0721;
            c2.Low = (decimal)23.7242;
            c2.close = (decimal)23.9528;


            c3.Open = (decimal)23.9429;
            c3.High = (decimal)24.0423;
            c3.Low = (decimal)23.6447;
            c3.close = (decimal)23.6745;



            c4.Open = (decimal)23.7342;
            c4.High = (decimal)23.8733;
            c4.Low = (decimal)23.3664;
            c4.close = (decimal)23.7839;


            c5.Open = (decimal)23.5950;
            c5.High = (decimal)23.6745;
            c5.Low = (decimal)23.4559;
            c5.close = (decimal)23.4956;


            c6.High = (decimal)23.4559;
            c6.Low = (decimal)23.5851;
            c6.Open = (decimal)23.1776;
            c6.close = (decimal)23.3217;




            c7.Open = (decimal)23.5255;
            c7.High = (decimal)23.8037;
            c7.Low = (decimal)23.3962;
            c7.close = (decimal)23.7540;




            c8.Open = (decimal)23.7342;
            c8.High = (decimal)23.8036;
            c8.Low = (decimal)23.5652;
            c8.close = (decimal)23.7938;




            c9.Open = (decimal)24.0920;
            c9.High = (decimal)24.3007;
            c9.Low = (decimal)24.0522;
            c9.close = (decimal)24.1417;



            c10.Open = (decimal)23.9528;
            c10.High = (decimal)24.1516;
            c10.Low = (decimal)23.7739;
            c10.close = (decimal)23.8137;

            c11.High = (decimal)23.9230;
            c11.Low = (decimal)24.0522;
            c11.close = (decimal)23.5950;
            c11.Open = (decimal)23.7839;




            c12.Open = (decimal)24.0423;
            c12.High = (decimal)24.0622;
            c12.Low = (decimal)23.835;
            c12.close = (decimal)23.8634;




            c13.Open = (decimal)23.8336;
            c13.High = (decimal)23.8833;
            c13.Low = (decimal)23.6447;
            c13.close = (decimal)23.7044;




            c14.Open = (decimal)24.0522;
            c14.High = (decimal)25.1356;
            c14.Low = (decimal)23.9429;
            c14.close = (decimal)24.9567;




            c15.Open = (decimal)24.8871;
            c15.High = (decimal)25.1952;
            c15.Low = (decimal)24.7380;
            c15.close = (decimal)24.8771;




            c16.Open = (decimal)24.9467;
            c16.High = (decimal)25.0660;
            c16.Low = (decimal)24.7678;
            c16.close = (decimal)24.9616;




            c17.Open = (decimal)24.9070;
            c17.High = (decimal)25.2151;
            c17.Low = (decimal)24.8970;
            c17.close = (decimal)25.1753;




            c18.Open = (decimal)25.2449;
            c18.High = (decimal)25.3741;
            c18.Low = (decimal)24.9268;
            c18.close = (decimal)25.0660;




            c19.Open = (decimal)25.1256;
            c19.High = (decimal)25.3642;
            c19.Low = (decimal)24.9567;
            c19.close = (decimal)25.2747;




            c20.Open = (decimal)25.2648;
            c20.High = (decimal)25.2648;
            c20.Low = (decimal)24.9268;
            c20.close = (decimal)24.9964;




            c21.Open = (decimal)24.7380;
            c21.High = (decimal)24.8175;
            c21.Low = (decimal)24.2112;
            c21.close = (decimal)24.4597;


            c22.Open = (decimal)24.3603;
            c22.High = (decimal)24.4398;
            c22.Low = (decimal)24.2112;
            c22.close = (decimal)24.2808;



            c23.Open = (decimal)24.4895;
            c23.High = (decimal)24.6485;
            c23.Low = (decimal)24.4299;
            c23.close = (decimal)24.6237;



            c24.Open = (decimal)24.6982;
            c24.High = (decimal)24.8374;
            c24.Low = (decimal)24.4398;
            c24.close = (decimal)24.5815;





            c25.Open = (decimal)24.6485;
            c25.High = (decimal)24.7479;
            c25.Low = (decimal)24.2013;
            c25.close = (decimal)24.5268;




            c26.Open = (decimal)24.4796;
            c26.High = (decimal)24.5094;
            c26.Low = (decimal)24.2510;
            c26.close = (decimal)24.3504;





            c27.Open = (decimal)24.4597;
            c27.High = (decimal)24.6784;
            c27.Low = (decimal)24.2112;
            c27.close = (decimal)24.3404;



            c28.Open = (decimal)24.6187;
            c28.High = (decimal)24.6684;
            c28.Low = (decimal)24.1516;
            c28.close = (decimal)24.2311;




            c29.Open = (decimal)23.8137;
            c29.High = (decimal)23.8435;
            c29.Low = (decimal)23.6348;
            c29.close = (decimal)23.7640;





            c30.Open = (decimal)23.9131;
            c30.High = (decimal)24.3007;
            c30.Low = (decimal)23.7640;
            c30.close = (decimal)24.2013;


            candles.Add(c1);
            candles.Add(c2);
            candles.Add(c3);
            candles.Add(c4);
            candles.Add(c5);
            candles.Add(c6);
            candles.Add(c7);
            candles.Add(c8);
            candles.Add(c9);
            candles.Add(c10);
            candles.Add(c11);
            candles.Add(c12);
            candles.Add(c13);
            candles.Add(c14);
            candles.Add(c15);
            candles.Add(c16);
            candles.Add(c17);
            candles.Add(c18);
            candles.Add(c19);
            candles.Add(c20);
            candles.Add(c21);
            candles.Add(c22);
            candles.Add(c23);
            candles.Add(c24);
            candles.Add(c25);
            candles.Add(c26);
            candles.Add(c27);
            candles.Add(c28);
            candles.Add(c29);
            candles.Add(c30);

            foreach (Objects.Candle c in candles)
            {
                c.Timestamp = DateTime.Now;
            }

            
            return candles;
        }

        public List<Objects.Candle> Unifycandels(List<Objects.Candle> candels)
        {
            List<Objects.Candle> candle30min = new List<Objects.Candle>();
            int counter = 0;
            int j = 0;
            foreach (var candle in candels)
            {

                if ((counter > 0) && (counter < 5))
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
                else if (counter == 5)
                {


                    if (candle30min[j].High < candle.High) { candle30min[j].High = candle.High; }
                    if (candle30min[j].Low > candle.Low) { candle30min[j].Low = candle.Low; }
                    candle30min[j].Open = candle.Open;
                    candle30min[j].Timestamp = candle.Timestamp;
                    
                    j++;
                    counter = 0;

                }



            }
            return candle30min;
        }



        public List<decimal?> Calcindicatorcci(List<Objects.Candle> candels)
        {

            var closes = new List<decimal>();
            var ccicloses = new List<decimal?>();
            List<Specialcandles> candelslist = new List<Specialcandles>();
            List<Specialcandles> spccibbcandelist = new List<Specialcandles>();

            foreach (var candle in candels) { closes.Add(candle.close); }





            foreach (var candle in candels) {
                Specialcandles supercandle = new Specialcandles();
                supercandle.Open = candle.Open;
                supercandle.High = candle.High;
                supercandle.Low = candle.Low;
                supercandle.Close = candle.close;
                DateTime utcTime1 = candle.Timestamp;
                utcTime1 = DateTime.SpecifyKind(utcTime1, DateTimeKind.Utc);
                DateTimeOffset utcTime2 = utcTime1;


                supercandle.DateTime = utcTime2;

                candelslist.Add(supercandle);





            }
            candelslist.Reverse();



            var cci = candelslist.Cci(20);
            foreach (var ccitick in cci) { ccicloses.Add(ccitick.Tick); }
            var smacci = ccicloses.Sma(20);
    
            foreach (var tick in smacci)
            {
                Specialcandles supercandle = new Specialcandles();
                //  if (tick == null) { supercandle.Open = 0; supercandle.High = 0; supercandle.Low = 0; supercandle.Close = 0; }
                if (tick == null) { spccibbcandelist.Add(supercandle); }
                else
                {
                    supercandle.Open = (decimal)tick;
                    supercandle.High = (decimal)tick;
                    supercandle.Low = (decimal)tick;
                    supercandle.Close = (decimal)tick;
                    spccibbcandelist.Add(supercandle);
                }



                

            }

            var bb344 = candelslist.Bb(20, 2);
            var bb355 = spccibbcandelist.Bb(20, 2);



            var smaTs = closes.Sma(3);

            foreach (var candle in candels)
            {
                Specialcandles supercandle = new Specialcandles();
                supercandle.Open = candle.Open;
                supercandle.High = candle.High;
                supercandle.Low = candle.Low;
                supercandle.Close = candle.close;
                DateTime utcTime1 = candle.Timestamp;
                utcTime1 = DateTime.SpecifyKind(utcTime1, DateTimeKind.Utc);
                DateTimeOffset utcTime2 = utcTime1;


                supercandle.DateTime = utcTime2;

                candelslist.Add(supercandle);
                                                            
            }


            CommodityChannelIndex ccii = new CommodityChannelIndex(candelslist,20);
            BollingerBands bb1 = new BollingerBands(candelslist, 20, 2);

           // OhlcvExtension.Cci()
            var sma = closes.Sma(30)[0];
            
            List<decimal?> smareturned = new List<decimal?>();
            foreach (var item in smaTs)     { smareturned.Add(item); }
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
