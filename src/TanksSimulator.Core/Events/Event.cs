using TanksSimulator.Shared.Utils;

namespace TanksSimulator.Game.Events
{
    public abstract class Event
    {
        public abstract EventResult Process(Logger logger);
    }
}
