using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Game.Utils;

namespace TanksSimulator.Game.Events
{
    public abstract class Event
    {
        public abstract EventResult Process(Logger logger);
    }
}
