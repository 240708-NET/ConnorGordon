namespace Project1.Main {
    public class ManagerGame {
        //  ~Reference Variables
        public Random Rand;

        //  End Variables
        public bool Force_Quit;

        //  Manager Variables
        public ManagerActor M_Actor { get; private set; }
        public ManagerCombat M_Combat { get; private set; }

        //  Constructor
        /// <summary>
        /// Manager that connects all parts of game logic
        /// </summary>
        public ManagerGame() {
            //  Setup ~Reference
            Rand = new Random();

            //  Setup Managers
            M_Actor = new ManagerActor(this);
            M_Combat = new ManagerCombat(this);
        }

        //  MainMethod - Play Game
        /// <summary>
        /// Main Game Method
        /// </summary>
        public void PlayGame() {
            M_Combat.CombatLoop();
        }
    }
}