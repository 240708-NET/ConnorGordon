using Project1.Models.Actor;

namespace Project1.Main {
    public class ManagerActor {
        //  ~Reference Variables
        public ManagerGame RefMGame { get; private set; }
        private Random refRand => RefMGame.Rand;

        //  Enemy Variables
        private Dictionary<string, GameActor> d_Enemies;
        private List<string> enemyKeys;

        //  Player Variables
        public GameActor Player { get; private set; }

        //  Constructor
        /// <summary>
        /// Manager that handles all actors in memory
        /// </summary>
        /// <param name="pRef">Reference to Game Manager</param>
        public ManagerActor(ManagerGame pRef) {
            //  Setup ~Reference
            RefMGame = pRef;

            //  Setup Enemy
            d_Enemies = new Dictionary<string, GameActor>();
            enemyKeys = new List<string>();
            AddEnemies();

            //  Setup Player
            Player = new GameActor(new GameAttack("fists", "punches with their", "Melee", 0, "0/1/bludgeoning"));
            Player.Actor_Admin.SetupName("Player", false);
            Player.Actor_Admin.SetupAttributes(10, 10, 10, 10, 10, 10);
            Player.Actor_Combat.SetupHealth(10, "1d10");
            Player.Actor_Combat.SetDefense();
        }

        //  SubMethod of Constructor - Add Enemies
        /// <summary>
        /// Adds enemies to the d_Enemies dictionary and the key to enemyKeys
        /// </summary>
        private void AddEnemies() {
            enemyKeys.Add("Goblin");
            d_Enemies.Add("Goblin", new GameActor(new GameAttack("fists", "punches with their", "Melee", 0, "0/1/bludgeoning")));
            d_Enemies["Goblin"].Actor_Admin.SetupName("Goblin", false);
            d_Enemies["Goblin"].Actor_Admin.SetupAttributes(10, 14, 10, 10, 8, 8);
            d_Enemies["Goblin"].Actor_Combat.SetupHealth(7, "2d6");
            d_Enemies["Goblin"].Actor_Combat.SetDefense();

            enemyKeys.Add("Orc");
            d_Enemies.Add("Orc", new GameActor(new GameAttack("fists", "punches with their", "Melee", 0, "0/1/bludgeoning")));
            d_Enemies["Orc"].Actor_Admin.SetupName("Orc", false);
            d_Enemies["Orc"].Actor_Admin.SetupAttributes(16, 12, 16, 7, 11, 10);
            d_Enemies["Orc"].Actor_Combat.SetupHealth(15, "2d8");
            d_Enemies["Orc"].Actor_Combat.SetDefense();

            enemyKeys.Add("Giant Spider");
            d_Enemies.Add("Giant Spider", new GameActor(new GameAttack("fists", "punches with their", "Melee", 0, "0/1/bludgeoning")));
            d_Enemies["Giant Spider"].Actor_Admin.SetupName("Giant Spider", false);
            d_Enemies["Giant Spider"].Actor_Admin.SetupAttributes(14, 16, 12, 2, 11, -3);
            d_Enemies["Giant Spider"].Actor_Combat.SetupHealth(26, "4d10");
            d_Enemies["Giant Spider"].Actor_Combat.SetDefense();
        }

        //  MainMethod - Get Enemy
        /// <summary>
        /// Returns a random enemy from d_Enemies
        /// </summary>
        /// <returns></returns>
        public GameActor GetEnemy() {
            return d_Enemies[enemyKeys[refRand.Next(0, enemyKeys.Count)]];
        }
    }
}