using System;
using System.Linq;
using cAlgo.API;
using cAlgo.API.Indicators;
using cAlgo.API.Internals;
using cAlgo.Indicators;

namespace cAlgo
{
    [Robot(TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class ArronExample : Robot
    {
        protected override void OnStart()
        {
            //Activate Timer
            Timer.Start(1);
        }

        protected override void OnTimer()
        {
            //Prints data each Second
            Print("Time {0} | Bid {1} | Ask {2} | Difference {3} | Difference in pips {4}", Server.Time, Symbol.Bid, Symbol.Ask, Math.Abs(Symbol.Ask - Symbol.Bid), Math.Abs(Symbol.Ask - Symbol.Bid) / Symbol.PipSize);
        }

        protected override void OnTick()
        {
            //Prints data each Tick
            Print("Time {0} | Bid {1} | Ask {2} | Difference {3} | Difference in pips {4}", Server.Time, Symbol.Bid, Symbol.Ask, Math.Abs(Symbol.Ask - Symbol.Bid), Math.Abs(Symbol.Ask - Symbol.Bid) / Symbol.PipSize);
        }
    }
}
