using TanksSimulator.Game.Events;
using TanksSimulator.Game.Map;
using TanksSimulator.Game.Utils;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.Game.Entities.Tank
{
    public class Tank : Entity
    {
        public TankMainBody MainBody { get; set; }
        public TankTurret Turret { get; private set; }
        public TankRoadWheels RoadWheel { get; set; }

        public bool CanMove { get { return !RoadWheel.IsDestroyed; } }
        public bool CanShoot { get { return !Turret.IsDestroyed; } }
        public bool IsDestroyed { get { return MainBody.IsDestroyed; } }

        private Tank _enemy;

        public Tank(
            TankModel model,
            Vector2i position,
            GameMap gameMap)
            : base(gameMap)
        {
            Name = model.Name;
            Position = position;

            Turret = new TankTurret(model.Turret);
        }

        public void SetEnemy(Tank tank)
        {
            _enemy = tank;
        }

        public override Event DecideAction()
        {
            if (CanShoot && HasShootingLine())
            {
                return new TankShootEvent(this, _enemy);
            }

            return new TankMoveEvent(this, new Vector2i { X = 1, Y = 1 });
        }

        private bool HasShootingLine()
        {
            var shootingLine = new Line(Position, _enemy.Position);
            var collidingTiles = shootingLine.GetIntersectingPoints();

            foreach (var coords in collidingTiles)
            {
                var tile = GameMap.GetTile(coords);
                if (tile.Solid)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
