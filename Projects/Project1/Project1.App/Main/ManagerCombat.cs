using Project1.Models.Actor;

namespace Project1.Main {
    public class ManagerCombat {
        //  ~Reference Variables
        public ManagerGame RefMGame { get; private set; }
        private Random RefRand => RefMGame.Rand;

        //  Combat Variables
        private bool combatActive;

        //  Enemy Variables
        private GameActor? enemy;
        private GameActor_Admin? enemyAdmin => enemy.Actor_Admin;
        private GameActor_Combat? enemyCombat => enemy.Actor_Combat;
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

            enemy = new GameActor(RefMGame.M_Actor.GetEnemy());
            string enemyName = (enemy.Actor_Admin.Actor_NameProper == true) ? enemy.Actor_Admin.Actor_Name : enemy.Actor_Admin.Actor_Name.ToLower();
            string enemyArticle = enemy.Actor_Admin.Actor_Article;

            enemy_ACLow = -999;
            enemy_ACHigh = 999;
            enemy_ACRange = "???";

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

            //  While Enemy is still active, loop through combat
            while (combatActive == true) {
                DisplayCombat_Status();

                if (playerAdmin.Actor_State == E_ActorState.Active) {
                    PlayerAction_Combat();
                }

                else {
                    combatActive = false;
                    //Player_Dead = true;
                }
            }
        }

        //  SubMethod of Combat Loop - Display Combat Status
        private void DisplayCombat_Status() {
            Console.WriteLine($"+ {new string('-', 27)} +");
            Console.WriteLine(enemy_Name);
            Console.WriteLine(enemy_Health);
            Console.WriteLine(enemy_AC);
            Console.WriteLine($"+ {new string(' ', 27)} +");
            Console.WriteLine(player_Name);
            Console.WriteLine(player_Health);
            Console.WriteLine(player_AC);
            Console.WriteLine($"+ {new string('-', 27)} +");
            DisplayCombat_Attack();
        }

        //  SubMethod of CombatLoop - Display Attack Options
        private void DisplayCombat_Attack() {
            Console.WriteLine($"+ (1) Attack                  +");
            Console.WriteLine($"+ {new string('-', 27)} +");
        }

        //  SubMethod of CombatLoop - Player Action Combat
        private void PlayerAction_Combat() {
            Console.WriteLine("Check 1");
            bool actionValid = false;
            int actionCount = 0;

            while(actionValid == false) {
                string? action = Console.ReadLine();
                Console.WriteLine("");

                switch(action) {
                    //  Part - Force Quit
                    case "fquit":
                        actionValid = true;
                        combatActive = false; 
                        RefMGame.Force_Quit = true;
                        break;

                    //  Part - Attack
                    case "1":
                        Console.WriteLine("Check 2");
                        actionValid = true;
                        int attack = playerCombat.Attack(RefRand, enemy);

                        if (attack != -999) {
                            //  Attack missed
                            if (attack < enemyCombat.Def_Unarmored) {
                                if (attack > enemy_ACLow) {
                                    enemy_ACLow = (attack + 1);
                                }
                            }

                            //  Attack hit
                            else {
                                if (attack < enemy_ACHigh) {
                                    enemy_ACHigh = attack;
                                }
                            }

                            //  Update AC Range
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
                        }
                        break;

                    //  Part - Default
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
    }
}