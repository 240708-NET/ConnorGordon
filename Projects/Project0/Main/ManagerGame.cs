using Project0.Actor;
using Project0.World;
namespace Project0.Main;

class ManagerGame {
    //  ~Reference Variables
    public Random rand;

    //  _Game Variables
    public bool GameActive;

    //  Enemy Variables
    private Character enemy;
    private List<Character> enemyList;

    //  Player Variables
    private Character player;

    //  World Variables
    private ManagerWorld world;

    //  Constructor
    public ManagerGame() {
        //  Part - Setup ~Reference
        rand = new Random();

        //  Part - Setup Enemy
        enemyList = new List<Character>() {
            new Character("Goblin", 8, 6, 6),
            new Character("Orc", 12, 8, 12),
            new Character("Spider", 6, 14, 8),
        };

        //  Part - Setup Player
        player = new Character("Player", 10, 10, 30);
        enemy = new Character("Enemy", 10, 10, 10);

        //  Part - Setup World
        world = new ManagerWorld();
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
        SubManagerCombat smCombat = new SubManagerCombat(player, new Character(enemyList[rand.Next(0, enemyList.Count)]));
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