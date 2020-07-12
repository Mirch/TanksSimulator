using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Game.Entities.Tank;
using TanksSimulator.Game.Utils;

namespace TanksSimulator.Game.Events
{
    public class TankDestroyedEvent : Event
    {
        private Tank _winner;
        private Tank _loser;

        public TankDestroyedEvent(Tank winner, Tank loser)
        {
            _winner = winner;
            _loser = loser;
        }

        public override void Process(Logger logger)
        {
            logger.Log($"{_loser.Name} was destroyed by {_winner.Name}.");
            logger.Log($"{_winner.Name} WON THE GAME!");

        }
    }
}
