using System.Collections.Generic;
using System.Linq;
using TanksSimulator.Game.Events;
using TanksSimulator.Game.Map;
using TanksSimulator.Game.Utils;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.Game.Entities.Tank
{
    public class Tank : Entity
    {
        public TankMainBody MainBody { get; set; }
        public TankBarrel Barrel { get; private set; }
        public TankRoadWheels RoadWheel { get; set; }

        public bool CanMove { get { return !RoadWheel.IsDestroyed; } }
        public bool CanShoot { get { return !Barrel.IsDestroyed; } }
        public bool IsDestroyed { get { return MainBody.IsDestroyed; } }

        private int _remainingLandmines;

        public string TankId { get; private set; }

        public Tank Enemy { get; set; }

        public Tank(
            string id,
            TankModel model,
            Vector2i position,
            GameMap gameMap)
            : base(gameMap)
        {
            TankId = id;

            Name = model.Name;
            Position = position;

            Barrel = new TankBarrel(model.Barrel);
            MainBody = new TankMainBody();
            RoadWheel = new TankRoadWheels();

            _remainingLandmines = 2; // initialize number of landmines
        }

        public override Event Act()
        {
            if (!CanShoot && Position.DistanceTo(Enemy.Position) < 2 && _remainingLandmines > 0)
            {
                _remainingLandmines--;
                return new PlantLandmineEvent(this);
            }

            if (MainBody.Armor < 15 || !CanShoot) // go into defensive mode
            {
                var destination = GetNeighourVisitableTiles()
                    .Where(p => !HasShootingLine(p))
                    .OrderByDescending(p => p.DistanceTo(Enemy.Position))
                    .FirstOrDefault();

                if (destination != null)
                {
                    return new TankMoveEvent(this, destination - Position);
                }
            }

            if (CanShoot && HasShootingLine(Position)) // if it can shoot, shoot
            {
                return new TankHitEvent(this, Enemy);
            }
            else // if there is nothing to do, move towards the enemy
            {
                var moveTo = DecideMovePosition();
                return new TankMoveEvent(this, moveTo);
            }
        }

        private Vector2i DecideMovePosition()
        {
            var path = GameMap.FindPath(Position, Enemy.Position);
            var nextNode = path[path.Count - 1];

            return nextNode.Position - Position;
        }

        private bool HasShootingLine(Vector2i startingPosition)
        {
            var weather = GameMap.CurrentWeather;
            var range = Barrel.Range;

            if (weather == "raining")
            {
                range /= 2;
            }

            if (startingPosition.DistanceTo(Enemy.Position) > Barrel.Range)
            {
                return false;
            }

            var shootingLine = new Line(startingPosition, Enemy.Position);
            var collidingTiles = shootingLine.GetIntersectingPoints();

            foreach (var coords in collidingTiles)
            {
                var tile = GameMap.GetTile(coords);
                if (tile != null && tile.Solid)
                {
                    return false;
                }
            }
            return true;
        }

        private IEnumerable<Vector2i> GetNeighourVisitableTiles()
        {
            var result = new List<Vector2i>();

            var deltas = new List<Vector2i>
            {
                new Vector2i {X= 0, Y= 1},
                new Vector2i {X= 1, Y= 1},
                new Vector2i {X= 1, Y= 0},
                new Vector2i {X= 1, Y= -1},
                new Vector2i {X= 0, Y= -1},
                new Vector2i {X= -1,Y= -1},
                new Vector2i {X= -1,Y= 0},
                new Vector2i {X= -1,Y= 1}
            };

            foreach (var delta in deltas)
            {
                var pos = Position + delta;
                var tile = GameMap.GetTile(pos);
                if (!tile.Solid)
                {
                    result.Add(pos);
                }
            }
            return result;
        }
    }
}
