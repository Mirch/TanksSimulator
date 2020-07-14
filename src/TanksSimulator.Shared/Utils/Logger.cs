using System;
using System.Collections.Generic;
using System.Text;

namespace TanksSimulator.Shared.Utils
{
    public class Logger
    {
        private List<string> _logs;

        private string _gameId;

        public Logger(string gameId)
        {
            _gameId = gameId;
            _logs = new List<string>();
        }

        public void Log(string message)
        {
            _logs.Add(message);
        }

        public List<string> GetLogs()
        {
            return _logs;
        }
    }
}
