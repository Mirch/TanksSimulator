using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.WebApi.Controllers.Simulator.Models
{
    public class GameDataApiRequestModel
    {
        public string Id { get; set; }
        public GameStatus Status { get; set; }
        public string Tank1Id { get; set; }
        public string Tank2Id { get; set; }
        public string MapId { get; set; }
        public string WinnerId { get; set; }
        public ICollection<string> Logs { get; set; }

        public GameDataApiRequestModel()
        {
        }

        public GameDataApiRequestModel(GameDataModel model)
        {
            Id = model.Id;
            Status = model.Status;
            Tank1Id = model.Tank1Id;
            Tank2Id = model.Tank2Id;
            MapId = model.MapId;
            WinnerId = model.WinnerId;
            Logs = model.Logs;
        }
    }
}
