using Project2.Models.Actor;

namespace Project2.Data {
    public interface IData {
        //  Enemy CREATE Methods
        public ActorEnemy? CreateEnemy(ActorEnemy pEnemy);
        public Dictionary<string, ActorEnemy> CreateAllEnemies(Dictionary<string, ActorEnemy> pEnemies);

        //  Enemy READ Methods
        public ActorEnemy? GetEnemy(ActorEnemy pEnemy);
        public Dictionary<string, ActorEnemy> GetAllEnemies();
    }
}