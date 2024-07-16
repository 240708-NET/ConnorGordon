using Project1.Models.Actor;

namespace Project1.Main {
    public class ManagerCombat {
        //  ~Reference Variables
        public ManagerGame RefMGame { get; private set; }
        private Random RefRand => RefMGame.Rand;

        //  Combat Variables
        private bool combatActive;
        private string combatUI_Solid => $"+{new string('-', 29)}+";
        private string combatUI_Empty => $"+{new string(' ', 29)}+";

        //  Enemy Variables
        private GameActor enemy;
        public GameActor Enemy {
            get { return enemy; }
            set {
                enemy = value;

                enemy_ACLow = -999;
                enemy_ACHigh = 999;
                enemy_ACRange = "???";
            }
        }
        private GameActor_Admin enemyAdmin => enemy.Actor_Admin;
        private GameActor_Combat enemyCombat => enemy.Actor_Combat;
        private string enemy_Name => $"+ {new string(' ', (27 - enemyAdmin.Actor_Name.Length))}{enemyAdmin.Actor_Name} +";
        private string enemy_Health => $"+ {new string(' ', (23 - enemyCombat.ActorHealth_Str.Length))}HP: {enemyCombat.ActorHealth_Str} +";

        private int enemy_ACLow;
        private int enemy_ACHigh;
        private string enemy_ACRange;
        private string enemy_AC => $"+ {new string(' ', (23 - enemy_ACRange.Length))}AC: {enemy_ACRange} +";

        //  Player Variables
        private GameActor player => RefMGame.M_Actor.Player;
        private GameActor_Admin playerAdmin => player.Actor_Admin;
        private GameActor_Combat playerCombat => player.Actor_Combat;

        private string player_Name => $"+ {playerAdmin.Actor_Name}{new string(' ', (27 - playerAdmin.Actor_Name.Length))} +";
        private string player_Health => $"+ HP: {playerCombat.ActorHealth_Str}{new string(' ', (23 - playerCombat.ActorHealth_Str.Length))} +";
        private string player_AC => $"+ AC: {playerCombat.Def_Unarmored}{new string(' ', (23 - playerCombat.Def_Unarmored.ToString().Length))} +";

        //  Constructor
        /// <summary>
        /// Manager that handles all combat encounters
        /// </summary>
        /// <param name="pRef">Reference to Game Manager</param>
        public ManagerCombat(ManagerGame pRef) {
            //  Setup ~Reference
            RefMGame = pRef;

            //  Setup Enemy
            enemy = new GameActor(RefMGame.M_Actor.GetEnemy());

            enemy_ACLow = -999;
            enemy_ACHigh = 999;
            enemy_ACRange = "???";
        }

        //  MainMethod - Combat Loop
        /// <summary>
        /// Initiates a combat loop that lasts until one side retreats or dies
        /// </summary>
        public void CombatLoop() {
            combatActive = true;

            Enemy = new GameActor(RefMGame.M_Actor.GetEnemy());
            string enemyName = (enemyAdmin.Actor_NameProper == true) ? enemyAdmin.Actor_Name : enemyAdmin.Actor_Name.ToLower();
            string enemyArticle = enemyAdmin.Actor_Article;

            //  Encounter initiated
            Console.WriteLine($"Player has encountered {enemyArticle} {enemyName}! Combat initiated!");
            string? action = Console.ReadLine();

            //  Initial action check
            switch(action) {
                //  Force Quit action
                case "fquit":
                    combatActive = false;
                    RefMGame.Force_Quit = true;
                    break;
            }

            //  While Enemies and Player are still active, loop through combat
            while (combatActive == true) {
                DisplayCombat_Status();

                switch(playerAdmin.Actor_State) {
                    //  If player is still active, handle their input
                    case E_ActorState.Active:
                        PlayerAction_Combat();
                        break;
                    
                    //  If player is dead/unconscious, end combat and exit
                    default:
                        combatActive = false;
                        break;
                }

                //  If player hasn't exited the program and player still active, enemy action
                if (RefMGame.Force_Quit == false && playerAdmin.Actor_State == E_ActorState.Active) {
                    switch(enemyAdmin.Actor_State) {
                        //  If enemy is still active, handle their action
                        case E_ActorState.Active:
                            EnemyAction_Combat();
                            break;

                        //  If enemy is dead/unconscious, end combat and exit
                        default:
                            DisplayCombat_Ending();
                            PlayerAction_Ending();
                            break;
                    }
                }
            }
        }

        //  SubMethod of Combat Loop - Display Combat Status
        private void DisplayCombat_Status() {
            Console.WriteLine(combatUI_Solid);

            Console.WriteLine(enemy_Name);
            Console.WriteLine(enemy_Health);
            Console.WriteLine(enemy_AC);

            Console.WriteLine(combatUI_Empty);

            Console.WriteLine(player_Name);
            Console.WriteLine(player_Health);
            Console.WriteLine(player_AC);

            Console.WriteLine(combatUI_Solid);
            DisplayCombat_Options();
        }

        //  SubMethod of CombatLoop - Display Combat Options
        private void DisplayCombat_Options() {
            Console.WriteLine($"+  (1) Attack                 +");
            Console.WriteLine(combatUI_Solid);
        }

        //  SubMethod of CombatLoop - Display Combat Ending
        private void DisplayCombat_Ending() {
            Console.WriteLine($"You've defeated the {enemyAdmin.Actor_Name}. Do you wish to press on or rest awhile?");
            Console.WriteLine("(1) Press on  (2) Rest");
        }

        //  SubMethod of CombatLoop - Player Action Combat
        private void PlayerAction_Combat() {
            string? action = "";
            int actionCount = 0;

            int playerToHit = 0;

            while(actionCount >= 0) {
                action = Console.ReadLine();
                Console.WriteLine("");

                switch(action) {
                    //  Force Quit action
                    case "fquit":
                        actionCount = -1;
                        combatActive = false;
                        RefMGame.Force_Quit = true;
                        break;

                    //  Player attack
                    case "1":
                        actionCount = -1;
                        playerToHit = playerCombat.Attack(RefRand, enemy);

                        //  Update enemy AC assumption range
                        if (playerToHit != -999) {
                            if (playerToHit >= enemyCombat.Def_Unarmored) {
                                //  Player hasn't hit before and has missed, increment enemy_ACLow to switch to range
                                if (enemy_ACHigh == 999 && enemy_ACLow != -999) {
                                    enemy_ACLow++;
                                }
                                enemy_ACHigh = (playerToHit < enemy_ACHigh) ? playerToHit : enemy_ACHigh;
                            }

                            else {
                                enemy_ACLow = (playerToHit > enemy_ACLow) ? playerToHit : enemy_ACLow;
                            }
                        }

                        //  Actual AC is known
                        if (enemy_ACHigh == enemy_ACLow) {
                            enemy_ACRange = "" + enemy_ACHigh;
                        }

                        //  AC range is known
                        else if (enemy_ACHigh != 999 && enemy_ACLow != -999) {
                            enemy_ACRange = enemy_ACLow + "-" + enemy_ACHigh;
                        }

                        //  AC low is known
                        else if (enemy_ACLow != -999) {
                            enemy_ACRange = ">" + enemy_ACLow;
                        }

                        //  AC high is known
                        else if (enemy_ACHigh != 999) {
                            enemy_ACRange = "<" + enemy_ACHigh;
                        }
                        break;

                    //  Invalid input
                    default:
                        actionCount++;

                        if (actionCount > 5) {
                            actionCount = 0;

                            Console.WriteLine("Reset Display");
                            Console.WriteLine("");

                            DisplayCombat_Status();
                        }
                        break;
                }
            }
        }

        //  SubMethod of CombatLoop - Player Action Ending
        private void PlayerAction_Ending() {
            string? action = "";
            int actionCount = 0;

            while(actionCount >= 0) {
                action = Console.ReadLine();
                Console.WriteLine("");

                switch(action) {
                    //  Force Quit action
                    case "fquit":
                        actionCount = -1;
                        combatActive = false;
                        RefMGame.Force_Quit = true;
                        break;

                    //  Player presses on
                    case "1":
                        actionCount = -1;
                        combatActive = false;
                        break;

                    //  Player rests
                    case "2":
                        actionCount = -1;
                        combatActive = false;
                        playerCombat.RestoreHealth();
                        break;

                    //  Invalid input
                    default:
                        actionCount++;

                        if (actionCount > 5) {
                            actionCount = 0;

                            Console.WriteLine("Reset Display");
                            Console.WriteLine("");

                            DisplayCombat_Status();
                        }
                        break;
                }
            }
        }

        //  SubMethod of CombatLoop - Enemy Action Combat
        private void EnemyAction_Combat() {
            enemyCombat.Attack(RefRand, player);
        }
    }
}