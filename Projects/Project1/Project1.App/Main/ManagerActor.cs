using Project1.Models.Actor;

namespace Project1.Main {
    public class ManagerActor {
        //  ~Reference Variables
        public ManagerGame RefMGame { get; private set; }
        private Random refRand => RefMGame.Rand;

        //  Enemy Variables
        public Dictionary<string, GameActor> D_Enemies { get; private set; }
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
            D_Enemies = new Dictionary<string, GameActor>();
            enemyKeys = new List<string>();
            AddEnemies();

            //  Setup Player
            //ManagerActor_CC creation = new ManagerActor_CC(this);
            //creation.CharacterCreation();

            Player = new GameActor { 
                Name = "Bob_True", 
                Proficiency = 2,
                Attributes = "10,10,10,10,10,10",
                Class = "Fighter",
                HealthDice = "1d10",
                AttackUnarmed = "fists_strikes with their_Melee_0/0_1_bludgeoning",
                AttackList = "longsword_swings with their_Melee_0/1d8_0_slashing"
            };
            Player = new GameActor(Player, true);
        }

        //  SubMethod of Constructor - Add Enemies
        /// <summary>
        /// Adds enemies to the d_Enemies dictionary and the key to enemyKeys
        /// </summary>
        private void AddEnemies() {
            Dictionary<string, GameActor> tempDict = RefMGame.DS.GetAllEnemies();

            if (tempDict.Count == 0) {
                CreateEnemies();
                RefMGame.DS.CreateAllEnemies(D_Enemies);
            }
            tempDict = RefMGame.DS.GetAllEnemies();

            D_Enemies = new Dictionary<string, GameActor>();
            enemyKeys = new List<string>();
            foreach(var enemy in tempDict) {
                D_Enemies.Add(enemy.Key, new GameActor(enemy.Value, false));
                enemyKeys.Add(enemy.Key);
            }
        }

        //  SubMethod of Add Enemies - Create Enemies
        private void CreateEnemies() {
            enemyKeys.Add("Goblin");
            D_Enemies.Add("Goblin", new GameActor { 
                Name = "Goblin_False", 
                Proficiency = 2,
                Attributes = "8,14,10,10,8,8",
                Class = "Enemy",
                HealthDice = "2d6",
                AttackUnarmed = "fists_strikes with their_Melee_0/0_1_bludgeoning",
                AttackList = "scimitar_swings with their_Melee_0/1d6_0_slashing"
            });

            enemyKeys.Add("Orc");
            D_Enemies.Add("Orc", new GameActor { 
                Name = "Orc_False", 
                Proficiency = 2,
                Attributes = "16,12,16,7,11,10",
                Class = "Enemy",
                HealthDice = "2d8",
                AttackUnarmed = "fists_strikes with their_Melee_0/0_1_bludgeoning",
                AttackList = "greataxe_swings with their_Melee_0/1d12_0_slashing"
            });

            enemyKeys.Add("Giant Spider");
            D_Enemies.Add("Giant Spider", new GameActor { 
                Name = "Giant Spider_False", 
                Proficiency = 2,
                Attributes = "14,16,12,2,11,4",
                Class = "Enemy",
                HealthDice = "4d10",
                AttackUnarmed = "fangs_bites wit theirh_Melee_0/0_1_bludgeoning",
                AttackList = ""
            });
        }

        //  MainMethod - Character Creation
        public void CharacterCreation() {
            ManagerActor_CC creation = new ManagerActor_CC(this);
            //creation.CharacterCreation();
        }

        //  MainMethod - Get Enemy
        /// <summary>
        /// Returns a random enemy from d_Enemies
        /// </summary>
        /// <returns></returns>
        public GameActor GetEnemy() {
            return D_Enemies[enemyKeys[refRand.Next(0, enemyKeys.Count)]];
        }
    }
}