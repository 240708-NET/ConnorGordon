using System.Runtime.CompilerServices;
using Project0.Actor;
namespace Project0.Main;

class SubManagerCombat {
    //  _Game Variables
    public bool Force_Quit;

    //  Character Variables
    private Character player;
    private string player_Name => $"+ {player.Char_Name}{new string(' ', (27 - player.Char_Name.Length))} +";
    private string player_Health => $"+ {player.Health_Str}{new string(' ', (27 - player.Health_Str.Length))} +";

    private Character enemy;
    private string enemy_Name => $"+ {new string(' ', (27 - enemy.Char_Name.Length))}{enemy.Char_Name} +";
    private string enemy_Health => $"+ {new string(' ', (27 - enemy.Health_Str.Length))}{enemy.Health_Str} +";

    //  Combat Variables
    private bool combatActive;

    //  Constructor (param Player, Enemy)
    public SubManagerCombat(Character pPlayer, Character pEnemy) {
        //  Part - Setup Character
        player = pPlayer;
        enemy = pEnemy;

        //  Part - Setup Combat
        combatActive = true;
    }

    //  MainMethod - Combat Loop (param Random)
    public void CombatLoop(Random pRand) {
        Console.WriteLine($"Player has encountered {enemy.Char_Article} {enemy.Char_Name}! Combat initiated!");
        string action = Console.ReadLine();

        //  Force quit option
        if (action == "fquit") {
            combatActive = false;
            Force_Quit = true;
        }

        while (combatActive == true) {
            DisplayCombatStatus();

            // Player Action
            if (player.Health_Alive == true) {
                PlayerAction_Combat(pRand);
            }

            if (Force_Quit == false) {
                //  Enemy Action
                if (enemy.Health_Alive == true) {
                    EnemyAction_Combat(pRand);
                }

                //  If Enemy is dead
                else if (enemy.Health_Alive == false) {
                    DisplayCombatEnding();
                    PlayerAction_Ending();
                }
            }
        }
    }

    //  SubMethod of CombatLoop - Display Combat Status
    private void DisplayCombatStatus() {
        Console.WriteLine($"+-----+-----+-----+-----+-----+");
        Console.WriteLine(enemy_Name);
        Console.WriteLine(enemy_Health);
        Console.WriteLine(player_Name);
        Console.WriteLine(player_Health);
        Console.WriteLine($"+-----+-----+-----+-----+-----+");
        DisplayAttackOptions();
    }

    //  SubMethod of CombatLoop - Display Attack Options
    private void DisplayAttackOptions() {
        Console.WriteLine($"+  (1) Attack                 +");
        Console.WriteLine($"+-----+-----+-----+-----+-----+");
    }

    //  SubMethod of CombatLoop - Diplay Combat Ending
    private void DisplayCombatEnding() {
        Console.WriteLine($"You've defeated the {enemy.Char_Name}. Do you wish to press on or rest awhile?");
        Console.WriteLine("(1) Press on  (2) Rest");
    }

    //  SubMethod of CombatLoop - Player Action Combat (param Random)
    private void PlayerAction_Combat(Random pRand) {
        bool actionValid = false;
        int actionCount = 0;

        while(actionValid == false) {
            string action = Console.ReadLine();
            Console.WriteLine("");

            switch(action) {
                //  Part - Force Quit
                case "fquit":
                    actionValid = true;
                    combatActive = false; 
                    Force_Quit = true;
                    break;

                //  Part - Attack
                case "1":
                    actionValid = true;
                    player.Attack(pRand, enemy);
                    break;

                //  Part - Default
                default:
                    actionCount++;

                    if (actionCount > 5) {
                        actionCount = 0;

                        Console.WriteLine("Reset Display");
                        Console.WriteLine("");

                        DisplayCombatStatus();
                    }
                    break;
            }
        }
    }

    //  SubMethod of CombatLoop - Player Action Ending
    private void PlayerAction_Ending() {
        bool actionValid = false;
        int actionCount = 0;

        while(actionValid == false) {
            string action = Console.ReadLine();
            Console.WriteLine("");

            switch(action) {
                //  Part - Force Quit
                case "fquit":
                    actionValid = true;
                    combatActive = false; 
                    Force_Quit = true;
                    break;

                //  Part - Press on
                case "1":
                    actionValid = true;
                    combatActive = false;
                    break;

                //  Part - Rest
                case "2":
                    actionValid = true;
                    combatActive = false;

                    player.RestoreHealth();
                    break;

                //  Part - Default
                default:
                    actionCount++;

                    if (actionCount > 5) {
                        actionCount = 0;

                        Console.WriteLine("Reset Display");
                        Console.WriteLine("");

                        DisplayCombatEnding();
                    }
                    break;
            }
        }
    }

    //  SubMethod of CombatLoop - Enemy Action Combat (param Random)
    private void EnemyAction_Combat(Random pRand) {
        enemy.Attack(pRand, player);
    }
}