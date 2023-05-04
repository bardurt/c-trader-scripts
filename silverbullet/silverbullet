using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cAlgo.API;
using cAlgo.API.Collections;
using cAlgo.API.Indicators;
using cAlgo.API.Internals;

namespace cAlgo
{
    [Indicator(IsOverlay = true,  TimeZone = TimeZones.EasternStandardTime, AccessRights = AccessRights.None)]
    public class SilverBullet : Indicator
    {
        private Frame frame;


        private String silverBulletAmStart = "10";
        private String silverBulletAmEnd = "11";
        
        private String silverBulletPmStart = "14";
        private String silverBulletPmEnd = "15";
        
        protected override void Initialize()
        {
            frame = new Frame(TimeFrame);
        }

        public override void Calculate(int index)
        {
        
            if(!frame.IsValid())
            {
                return;
            }
            
            DateTime current = MarketSeries.OpenTime[index];
            double currentOpen = MarketSeries.Open[index];
            
            if (current.Hour == Int32.Parse(silverBulletAmStart) && current.Minute == Int32.Parse("00"))
            {
                DrawSessionLine("SILVER_BULLET_AM_START_"+index, index, currentOpen, Colors.Red);
            }
              
            if (current.Hour == Int32.Parse(silverBulletAmEnd) && current.Minute == Int32.Parse("00"))
            {
                DrawSessionLine("SILVER_BULLET_AM_END_"+index, index, currentOpen, Colors.Red);
            }
            
            if (current.Hour == Int32.Parse(silverBulletPmStart) && current.Minute == Int32.Parse("00"))
            {
                DrawSessionLine("SILVER_BULLET_PM_START_"+index, index, currentOpen, Colors.Red);
            }
              
            if (current.Hour == Int32.Parse(silverBulletPmEnd) && current.Minute == Int32.Parse("00"))
            {
                DrawSessionLine("SILVER_BULLET_PM_END_"+index, index, currentOpen, Colors.Red);
            }
         
            
        }
        
        private void DrawSessionLine(String label, int index, double origin, Colors color)
        {
           ChartObjects.DrawLine(label, index,origin+100, index, origin-100, color, 1, LineStyle.LinesDots);
        }
        
        
        class Frame{
        
            private readonly TimeFrame TimeFrame;
            
            public Frame(TimeFrame timeFrame){
                this.TimeFrame = timeFrame;
            }
            
            public bool IsValid(){
            
                if(TimeFrame == TimeFrame.Minute)
                {
                    return true;
                }
                
                if(TimeFrame == TimeFrame.Minute2)
                {
                    return true;
                }
                
                if(TimeFrame == TimeFrame.Minute3)
                {
                    return true;
                }
                
                if(TimeFrame == TimeFrame.Minute5)
                {
                    return true;
                }
                
                if(TimeFrame == TimeFrame.Minute15)
                {
                    return true;
                }
                
                return false;
            }
            
            public int GetScalar() {
                if(TimeFrame == TimeFrame.Minute)
                {
                    return 60;
                }
                
                if(TimeFrame == TimeFrame.Minute2)
                {
                    return 30;
                }
                
                if(TimeFrame == TimeFrame.Minute3)
                {
                    return 20;
                }
                
                if(TimeFrame == TimeFrame.Minute5)
                {
                    return 12;
                }
                
                if(TimeFrame == TimeFrame.Minute15)
                {
                    return 4;
                }
            
                return 0;
            }
        }
    }
}
