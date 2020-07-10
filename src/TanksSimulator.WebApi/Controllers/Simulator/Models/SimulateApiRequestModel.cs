using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TanksSimulator.WebApi.Controllers.Simulator.Models
{
    public class SimulateApiRequestModel
    {
        public int[] Tanks { get; set; }
        public int MapId { get; set; }
    }
}
