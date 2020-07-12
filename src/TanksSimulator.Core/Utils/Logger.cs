using System;
using System.Collections.Generic;
using System.Text;

namespace TanksSimulator.Game.Utils
{
    public class Logger
    {
        private StringBuilder _builder;

        public Logger()
        {
            _builder = new StringBuilder();
        }

        public void Log(string message)
        {
            _builder.Append(message).Append('\n');
        }

        public string Flush()
        {
            return _builder.ToString();
        }
    }
}
