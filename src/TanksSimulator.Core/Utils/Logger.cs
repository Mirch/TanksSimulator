using System;
using System.Collections.Generic;
using System.Text;

namespace TanksSimulator.Game.Utils
{
    public class Logger
    {
        private List<string> _logs;

        public Logger()
        {
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
