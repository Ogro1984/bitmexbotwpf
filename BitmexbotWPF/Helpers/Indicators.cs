using BitmexbotWPF.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BitmexbotWPF.Helpers
{
    class Indicators
    {
        //cci calculator

        public List<CCI> CCICalculator(List<Candle> candles, int period) {
            int counter = 0;
        
            
            List<CCI> listCCI = new List<CCI>();
            foreach (Candle candle in candles) {

                if (counter < (period-1))
                {
                    CCI cci = new CCI();
                    listCCI.Add(cci);
                    cci.Timestamp = candle.Timestamp;
                    cci.Open = candle.Open;
                    cci.High = candle.High;
                    cci.Low = candle.Low;
                    cci.Close = candle.close;
                    cci.TP = Math.Round(TPCalculator(candle).Value);
                   
                    counter++;
                }
            else   if (counter >= (period-1))
                {
                    CCI cci = new CCI();
                    listCCI.Add(cci);
                    cci.Timestamp = candle.Timestamp;
                    cci.Open = candle.Open;
                    cci.High = candle.High;
                    cci.Low = candle.Low;
                    cci.Close = candle.close;
                    cci.TP = TPCalculator(candle).Value;
                    cci.SMA = Math.Round(SMATPCalculator(listCCI, period).Value);
                    cci.MD = Math.Round(MDCalculator(listCCI, period,cci.SMA).Value);
                    
                    cci.Value = Math.Round((decimal)((cci.TP - cci.SMA) / ((decimal)0.015 * cci.MD)));

                    
                    counter++;
                }


            }

            return listCCI;

        }

        //SMA calculator

        public decimal? SMATPCalculator(List<CCI> listCCI, int period)
        {
            decimal? accum = 0;
           
            var longitude = (int)listCCI.LongCount();



                for (int i = 0; i < period; i++) {
                accum = accum + listCCI[longitude - i-1].TP;
                    
                }
            accum = accum / period;


            return accum;
            

        }

        public decimal? MDCalculator(List<CCI> listCCI, int period,decimal? sma1)
        {
            decimal? md = 0;
            int longitude = (int)listCCI.LongCount();
            decimal sma = (decimal) sma1;
           
            
            for (int i = 0; i < period; i++)
            {
               
                
                md = md +Math.Abs( sma - (decimal)listCCI[longitude - i-1].TP);
                
            }
            md = md / period;
            return md;
        }

        //tipical price calculator

        public TP TPCalculator(Candle candle)
        {
            TP tp = new TP();

            tp.Value = (candle.High + candle.Low + candle.close) / 3;

            tp.Timestamp = candle.Timestamp;
            return tp;
        }



    }
}
