using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TanksSimulator.ResultsApi.Data
{
    public class TanksSimulatorDbSettings : ITanksSimulatorDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string TanksCollection { get; set; }
        public string MapsCollection { get; set; }
        public string GameDataCollection { get; set; }
    }

    public interface ITanksSimulatorDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string TanksCollection { get; set; }
        string MapsCollection { get; set; }
        string GameDataCollection { get; set; }
    }
}
