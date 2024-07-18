using Project1.Models.Actor;
using System.Text.Json;

namespace Project1.Data {
    public interface IData {
        public Dictionary<string, GameActor> LoadAllEnemies(string pPath);
        public void SaveAllEnemies(string pPath, List<GameActor> pEnemies);
    }
}