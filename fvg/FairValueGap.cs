using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;

namespace cAlgo
{

    [Indicator(IsOverlay = true, AccessRights = AccessRights.None)]
    public class FairValueGap : Indicator
    {

        // Set the color for this indicator
        [Parameter("Indicator Color", DefaultValue = Colors.Yellow)]
        public Colors color { get; set; }

        // Width of this indicator
        [Parameter("Indicator Width", DefaultValue = 3, MinValue = 1, MaxValue = 5)]
        public int indicatorWidth { get; set; }

        // The size of a Fair Value Gap
        [Parameter("Min Gap (Pips)", DefaultValue = 1, MinValue = 1, MaxValue = 5)]
        public int minGap { get; set; }

        // Size of the pip 
        [Parameter("Pip Size", DefaultValue = 5E-05, MinValue = 1E-05, MaxValue = 1)]
        public double pipsize { get; set; }

        public override void Calculate(int index)
        {
            var nextBar = index - 1;
            var studyIndex = index - 2;
            var previousBar = index - 3;

            var prevCandleHigh = MarketSeries.High.Last(2);
            var prevCandleLow = MarketSeries.Low.Last(2);

            var nextCandleHigh = MarketSeries.High.Last(0);
            var nextCandleLow = MarketSeries.Low.Last(0);


            // bearish fair value gap
            if (prevCandleLow > nextCandleHigh)
            {
                if (isFvg(prevCandleLow, nextCandleHigh))
                {
                    drawFvg(nextBar, prevCandleLow, nextCandleHigh);
                }
            }

            // bullish fair value gap
            if (nextCandleLow > prevCandleHigh)
            {
                if (isFvg(nextCandleLow, prevCandleHigh))
                {
                    drawFvg(nextBar, nextCandleLow, prevCandleHigh);
                }
            }

        }

        private bool isFvg(double high, double low)
        {
            double gapInPips = minGap * pipsize;

            if ((high - low) >= gapInPips)
            {
                return true;
            }

            return false;
        }
        
   
        private void drawFvg(int index, double high, double low)
        {
            ChartObjects.DrawLine("FVG" + index, index, high, index, low, color, indicatorWidth, LineStyle.Solid);
        }
    }
}
