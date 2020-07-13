using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TanksSimulator.WebApi.Controllers.Simulator.Models
{
    public class SimulateApiRequestModel
    {
        public string[] Tanks { get; set; }
        public string MapId { get; set; }
    }
}
