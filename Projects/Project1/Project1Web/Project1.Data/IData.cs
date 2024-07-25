using Project1.Models.Actor;

namespace Project1.Data {
    public interface IData {
        public GameActor? GetEnemy(GameActor pEnemy);
        public Dictionary<string, GameActor> GetAllEnemies();
        public GameActor? CreateEnemy(GameActor pEnemy);
        public Dictionary<string, GameActor> CreateAllEnemies(Dictionary<string, GameActor> pEnemies);
    }
}