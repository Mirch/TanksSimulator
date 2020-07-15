using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Game.Entities.Tank;
using TanksSimulator.Shared.Utils;

namespace TanksSimulator.Game.Events
{
    public class LandmineWaitingEvent : Event
    {
        public Landmine Landmine { get; private set; }

        public LandmineWaitingEvent(Landmine landmine)
        {
            Landmine = landmine;
        }

        public override EventResult Process(Logger logger)
        {
            Landmine.Act();
            var result = new EventResult();
            result.ChainEvent = new LandmineWaitingEvent(Landmine);

            return result;
        }
    }
}
