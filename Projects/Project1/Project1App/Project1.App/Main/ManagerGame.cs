using Project1.Models.Actor;

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
            while(Force_Quit == false) {
                M_Combat = new ManagerCombat(this);
                M_Combat.CombatLoop();
            }
        }

        //  MainMethod - Save Game
        private void SaveGame() {
            
        }

        //  MainMethod - Write Text
        public void WriteText(string pText, int pSleep) {
            for(int i = 0; i < pText.Length; i++) {
                Console.Write(pText.Substring(i, 1));
                Thread.Sleep(pSleep);
            }
        }

        //  MainMethod - Write Line
        public void WriteLine(string pText, int pSleep) {
            WriteText(pText, pSleep);
            Console.WriteLine("");
        }
    }
}