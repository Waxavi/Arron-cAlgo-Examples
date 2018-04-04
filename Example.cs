using System;
using cAlgo.API;
using cAlgo.API.Internals;
using System.Collections.Generic;

namespace cAlgo
{
    [Robot(TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class ArronExample : Robot
    {
        //in addition,
        //can we store the history of seconds,
        //so let's say it stores the last updates from the last 5 seconds,
        //so in the log it would print:
        //second 2: difference in pips 1.1
        //percentage change from previous second = 10%
        //previous seconds: 10% (most current), 20%, 20%, -5%, -20% (oldest)

        //Percentage Change Formula
        //((_secondvalue - _firstvalue) / _firstvalue)*100

        //Create a History (Queue) for 5 items
        Queue<double> _Items = new Queue<double>();
        //Store the previous difference in a field
        double _PreviousDiff = 0;

        /// <summary>
        /// Returns Percentage Change from 2 values
        /// </summary>
        /// <param name="_firstvalue"></param>
        /// <param name="_secondvalue"></param>
        /// <returns></returns>
        double PercentageChange(double _firstvalue, double _secondvalue)
        {
            return Math.Round(((_secondvalue - _firstvalue) / _firstvalue) * 100,2);
        } 

        protected override void OnStart()
        {
            //Activate Timer
            Timer.Start(1);
        }

        protected override void OnTimer()
        {
            //Prints data each Second
            if (_PreviousDiff == 0)
            {
                //Initial value of the previous difference, but will not be saved or written just yet, but on the next timer.
                _PreviousDiff = Math.Abs(Symbol.Ask - Symbol.Bid) / Symbol.PipSize;
            }
            else
            {
                //If queue is at 5 or more values dequeue last item
                if (_Items.Count >= 5)
                    _Items.Dequeue();

                //Enqueue current Percentage Change between current bid-ask diff and _PreviousDiff
                _Items.Enqueue(PercentageChange(_PreviousDiff, Math.Abs(Symbol.Ask - Symbol.Bid) / Symbol.PipSize));
                //Update Previous diff value for next timer
                _PreviousDiff = Math.Abs(Symbol.Ask - Symbol.Bid) / Symbol.PipSize;
            }

            Print("Time {0} | Bid {1} | Ask {2} | Difference {3} | Difference in pips {4}", Server.Time, Symbol.Bid, Symbol.Ask, _PreviousDiff*Symbol.PipSize, _PreviousDiff);

            //If there are no items in queue do not print anything just yet.
            if (_Items.Count == 0)
                return;

            string _differences = "";

            foreach (var diff in _Items)
            {
                _differences += diff.ToString() + "%" + " | ";
            }

            Print("List of Percentage Changes: {0}",_differences);

        }

        protected override void OnTick()
        {
            //Prints data each Tick
            //Print("Time {0} | Bid {1} | Ask {2} | Difference {3} | Difference in pips {4}", Server.Time, Symbol.Bid, Symbol.Ask, Math.Abs(Symbol.Ask - Symbol.Bid), Math.Abs(Symbol.Ask - Symbol.Bid) / Symbol.PipSize);
        }
    }
}
