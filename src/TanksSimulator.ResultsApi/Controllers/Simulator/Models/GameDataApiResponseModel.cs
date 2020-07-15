using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.ResultsApi.Controllers.Simulator.Models
{
    public class GameDataApiResponseModel
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Tank1Id { get; set; }
        public string Tank2Id { get; set; }
        public string MapId { get; set; }
        public string WinnerId { get; set; }
        public ICollection<string> Logs { get; set; }

        public GameDataApiResponseModel()
        {
        }

        public GameDataApiResponseModel(GameDataModel model)
        {
            Id = model.Id;
            Status = model.Status.ToString();
            Tank1Id = model.Tank1Id;
            Tank2Id = model.Tank2Id;
            MapId = model.MapId;
            WinnerId = model.WinnerId;
            Logs = model.Logs;
        }
    }
}
