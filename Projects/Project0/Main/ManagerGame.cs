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

    //  Player Variables
    private Character player;

    //  World Variables
    private ManagerWorld world;

    //  Constructor
    public ManagerGame() {
        //  Part - Setup ~Reference
        rand = new Random();

        //  Part - Setup Player
        player = new Character("Player", 20, 10, 10);
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
        SubManagerCombat smCombat = new SubManagerCombat(player, enemy);
        smCombat.CombatLoop(rand);

        //  Part - Force Quit
        if (smCombat.Force_Quit == true) {
            GameActive = false;
        }
    }
}