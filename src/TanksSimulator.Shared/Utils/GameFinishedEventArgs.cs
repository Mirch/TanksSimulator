using System;
using System.Collections.Generic;
using System.Text;

namespace TanksSimulator.Shared.Utils
{
    public class GameFinishedEventArgs : EventArgs
    {
        public string GameId { get; set; }
        public string WinnerTankId { get; set; }
        public int NumberOfTurns { get; set; }
    }
}
