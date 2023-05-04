using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;

namespace cAlgo
{

    [Indicator(IsOverlay = true, AccessRights = AccessRights.FullAccess)]
    public class FairValueGap : Indicator
    {
    
        // Set the color for this indicator
        [Parameter("FVG Color", DefaultValue = Colors.Blue)]
        public Colors FvgColor { get; set; }
        
        // Width of this indicator
        [Parameter("Indicator Width", DefaultValue = 3, MinValue = 1, MaxValue = 5)]
        public int IndicatorWidth { get; set; }

        // The size of a Fair Value Gap
        [Parameter("Min Gap (Pips)", DefaultValue = 1, MinValue = 0, MaxValue = 5)]
        public int MinGap { get; set; }

        // Size of the pip 
        [Parameter("Pips per point", DefaultValue = 1, MinValue = 0.1, MaxValue = 10000)]
        public double PipSize { get; set; }
        
        [Parameter("Lookback Days", DefaultValue = 5, MinValue = 2, MaxValue = 60)]
        public int LookbackDays { get; set; }
       
        
        private DateTime studyDate;
        private bool Prepared = false;
        private double PipScalar = 1;
        
        protected override void Initialize()
        {
            studyDate = DateTime.Now.AddDays(-(LookbackDays+1));
            Prepared = false;
        }


        public override void Calculate(int index)
        {
        
            if(!Prepared){
                double high = MarketSeries.High[index];
                double low = MarketSeries.Low[index];
                double diff = high - low;
                PipScalar = diff < 0.01 ? 10000 : 1;
                Prepared = true;
            }
           
            DateTime current = MarketSeries.OpenTime[index];

            if (current < studyDate)
            {
                return;
            }
            
            
            var nextBar = index - 1;
            
            if(index < 2){
                return;
            }

            var prevCandleHigh = MarketSeries.High[index-2];
            var prevCandleLow = MarketSeries.Low[index-2];
            
            var studyCandleOpen = MarketSeries.Open[index-1];
            var studyCandleClose = MarketSeries.Close[index-1];

            var nextCandleHigh = MarketSeries.High[index];
            var nextCandleLow = MarketSeries.Low[index];
            
        
            if (IsFvg(prevCandleLow, nextCandleHigh))
            {
                  
                double top = prevCandleLow;
                double bottom = nextCandleHigh;
                        
                if(IsBullish(studyCandleOpen, studyCandleClose))
                {
                    if(prevCandleLow > studyCandleClose)
                    {
                        top = studyCandleClose;
                    }
                            
                    if(nextCandleHigh > studyCandleClose)
                    {
                        bottom = studyCandleClose;
                    }
                } 
                else 
                {
                    if(prevCandleLow > studyCandleOpen)
                    {
                        top = studyCandleOpen;
                    }
                           
                    if(nextCandleHigh < studyCandleClose)
                    {
                        bottom = studyCandleClose;
                    }
                }
                DrawFvg("FVG_BEARISH_BAR_"+index, nextBar, top, bottom, FvgColor);
            }


      
            if (IsFvg(nextCandleLow, prevCandleHigh))
            {
                double top = nextCandleLow;
                double bottom = prevCandleHigh;
                       
                if(IsBullish(studyCandleOpen, studyCandleClose))
                {
                    if(nextCandleLow > studyCandleClose)
                    {
                        top = studyCandleClose;
                    }
                            
                    if(prevCandleHigh < studyCandleOpen)
                    {
                        bottom = studyCandleOpen;
                    }
                } 
                else 
                {
                    if(prevCandleLow > studyCandleOpen)
                    {
                        top = studyCandleOpen;
                    }
                        
                    if(nextCandleHigh < studyCandleClose)
                    {
                        bottom = studyCandleClose;
                    }
                }
                DrawFvg("FVG_BULLISH_BAR_"+index, nextBar, top, bottom, FvgColor);
            }
        }

        
        private bool IsFvg(double high, double low)
        {
            double gapInPips = MinGap / PipScalar;

            if ((high - low) >= gapInPips)
            {
                return true;
            }

            return false;
        }
        
   
        private void DrawFvg(String label, int index, double high, double low, Colors color)
        {
            ChartObjects.DrawLine(label, index, high, index, low, color, IndicatorWidth, LineStyle.Solid);
        }
        
        
        private bool IsBullish(double open, double close){
        
            if(close > open)
            {
                return true;
            }
           
            return false;
        }
    }
}
