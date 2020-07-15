using System;
using System.Collections.Generic;
using System.Text;

namespace TanksSimulator.Game.Events
{
    public class EventResult
    {
        public static EventResult Succeeded = new EventResult();

        public Event ChainEvent { get; set; }

        public EventResult()
        {
        }
    }
}
