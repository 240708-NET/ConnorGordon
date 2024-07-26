using Microsoft.AspNetCore.Mvc;
using Project1.Data;
using Project1.Models.Actor;

namespace Project1.API.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ActorController : ControllerBase {
        private readonly ILogger<ActorController> actorLogger;
        private readonly IData actorData;

        public ActorController(ILogger<ActorController> pLogger, IData pData) {
            actorLogger = pLogger;
            actorData = pData;
        }

        [HttpGet("/getAllEnemies")]
        public Dictionary<string, GameActor> GetAllEnemies() {
            return actorData.GetAllEnemies();
        }

        [HttpPost("/createAllEnemies")]
        public Dictionary<string, GameActor> CreateAllEnemies([FromBody] Dictionary<string, GameActor> pEnemies) {
            return actorData.CreateAllEnemies(pEnemies);
        }
    }
}