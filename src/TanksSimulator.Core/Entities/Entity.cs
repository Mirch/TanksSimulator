using TanksSimulator.Game.Events;
using TanksSimulator.Game.Map;
using TanksSimulator.Game.Utils;

namespace TanksSimulator.Game.Entities
{
    public abstract class Entity
    {
        public string Name { get; set; }
        public Vector2i Position { get; set; }
        public GameMap GameMap { get; private set; }

        public Entity(GameMap map)
        {
            GameMap = map;
        }

        public abstract Event DecideAction();
    }
}
