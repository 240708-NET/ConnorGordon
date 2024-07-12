using Project0.Actor;
using Project0.World;
namespace Project0.Main;

class ManagerGame {
    //  ~Reference Variables
    public Random rand;

    //  _Game Variables
    public bool GameActive;

    //  Enemy Variables
    private List<string> enemyKeys;
    private Dictionary<string, Character> d_Enemies;

    //  Player Variables
    private Character player;

    //  World Variables
    private ManagerWorld world;

    //  Constructor
    public ManagerGame() {
        //  Part - Setup ~Reference
        rand = new Random();

        //  Part - Setup Enemy
        enemyKeys = new List<string>();
        d_Enemies = new Dictionary<string, Character>();

        AddEnemies();

        //  Part - Setup Player
        player = new Character("Player", 10, 10, 30, new Attack("fists", "punches with their", "Melee", 0, "1/bludgeoning", ""));
        player.AddAttack("longsword", "swings with their", "Melee", 0, "1d8/slashing", "");

        //  Part - Setup World
        world = new ManagerWorld();
    }

    //  SubMethod of Constructor - Add Enemies
    private void AddEnemies() {
        enemyKeys.Add("Goblin");
        d_Enemies.Add("Goblin", new Character("Goblin", 8, 6, 6, new Attack("fists", "punches with their", "Melee", 0, "1/bludgeoning", "")));

        enemyKeys.Add("Orc");
        d_Enemies.Add("Orc", new Character("Orc", 12, 8, 12, new Attack("fists", "punches with their", "Melee", 0, "1/bludgeoning", "")));

        enemyKeys.Add("Spider");
        d_Enemies.Add("Spider", new Character("Spider", 6, 14, 8, new Attack("fangs", "bites with their", "Melee", 0, "1/piercing", "")));
    }

    //  MainMethod - Play Game
    public void PlayGame() {
        GameActive = true;

        while (GameActive == true) {
            CombatEncounter();
        }
    }

    //  SubMethod of PlayGame - Combat Encounter
    private void CombatEncounter() {
        SubManagerCombat smCombat = new SubManagerCombat(player, new Character(d_Enemies[enemyKeys[rand.Next(0, enemyKeys.Count)]]));
        smCombat.CombatLoop(rand);

        //  Part - Force Quit
        if (smCombat.Force_Quit == true || smCombat.Player_Dead == true) {
            GameActive = false;

            if (smCombat.Player_Dead == true) {
                Console.WriteLine("You have died! Better luck next time!");
            }
        }
    }
}