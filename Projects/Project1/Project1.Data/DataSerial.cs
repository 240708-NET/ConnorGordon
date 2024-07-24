using Project1.Models.Actor;

namespace Project1.Data {
    public class DataSerial : IData {
        private DataContext context;

        public DataSerial() {
            context = new DataContext();
        }

        public GameActor? GetEnemy(GameActor pEnemy) {
            var found = from e in context.Enemies.ToList()
                where e.Id == pEnemy.Id
                select e;

            return found.FirstOrDefault();
        }

        public Dictionary<string, GameActor> GetAllEnemies() {
            Dictionary<string, GameActor> result = new Dictionary<string, GameActor>();

            List<GameActor> enemies = context.Enemies.ToList();
            foreach(GameActor enemy in enemies) {
                result.Add($"{enemy.Name.Split("_")[0]}", new GameActor(enemy, true));
            }

            return result;
        }

        public GameActor? CreateEnemy(GameActor pEnemy) {
            context.Add(pEnemy);
            context.SaveChanges();

            return GetEnemy(pEnemy);
        }

        public Dictionary<string, GameActor> CreateAllEnemies(Dictionary<string, GameActor> pEnemies) {
            foreach(var enemy in pEnemies) {
                context.Add(enemy.Value);
            }
            context.SaveChanges();

            return GetAllEnemies();
        }
    }
}