using Project0.Actor;
using Project0.World;
namespace Project0.Main;

class ManagerGame {
    //  ~Reference Variables
    public Random rand;

    //  Player Variables
    private Player player;
    private Character enemy;

    //  World Variables
    private ManagerWorld world;

    //  Constructor
    public ManagerGame() {
        //  Part - Setup ~Reference
        rand = new Random();

        //  Part - Setup Player
        player = new Player("Player", 20, 10, 10, 1);
        enemy = new Character("Enemy", 10, 10, 10, 1);

        //  Part - Setup World
        world = new ManagerWorld();
    }

    //  MainMethod - Play Game
    public void PlayGame() {
        player.Attack(rand, enemy);

        SubManagerCombat smCombat = new SubManagerCombat(player, enemy);
        smCombat.CombatLoop();
    }
}