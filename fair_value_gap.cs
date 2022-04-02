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
        [Parameter("Pip Size", DefaultValue = 0.0001, MinValue = 0.0001, MaxValue = 1)]
        public double pipsize { get; set; }

        // Draw horizontal lines to highlight the Fair Value Gap
        [Parameter("Paint Horizontal Lines", DefaultValue = false)]
        public bool paintHorizontalLines { get; set; }

        // Lemgth of the lines for highlighting the Fair Value Gap
        [Parameter("Horizontal Lines Length", DefaultValue = 10, MinValue = 5, MaxValue = 50)]
        public int horizontalLinesLength { get; set; }

        public override void Calculate(int index)
        {
            var nextBar = index;
            var studyIndex = index - 1;
            var previousBar = index + 2;

            var prevoiousCandleHigh = MarketSeries.High.Last(2);
            var prevoiousCCandleLow = MarketSeries.Low.Last(2);

            var nextCandleHigh = MarketSeries.High.Last(0);
            var nextCandleLow = MarketSeries.Low.Last(0);


            // bearish fair value gap
            if (prevoiousCCandleLow > nextCandleHigh)
            {
                if (isGap(prevoiousCCandleLow, nextCandleHigh))
                {
                    drawFvg(studyIndex, prevoiousCCandleLow, nextCandleHigh);
                }
            }

            // bullish fair value gap
            if (nextCandleLow > prevoiousCandleHigh)
            {
                if (isGap(nextCandleLow, prevoiousCandleHigh))
                {
                    drawFvg(studyIndex, nextCandleLow, prevoiousCandleHigh);
                }
            }

        }

        private bool isGap(double high, double low)
        {
            double gapInPips = minGap * pipsize;

            if ((high - low) > gapInPips)
            {
                return true;
            }

            return false;
        }

        private void drawFvg(int index, double high, double low)
        {
            if (paintHorizontalLines)
            {
                ChartObjects.DrawLine("FVG TOP" + index, index, high, index + horizontalLinesLength, high, color, 1, LineStyle.Solid);
                ChartObjects.DrawLine("FVG BOTTOM" + index, index, low, index + horizontalLinesLength, low, color, 1, LineStyle.Solid);
            }
            ChartObjects.DrawLine("FVG" + index, index, high, index, low, color, indicatorWidth, LineStyle.Solid);
        }
    }
}
