using Project0.Actor;
namespace Project0.Main;

class SubManagerCombat {
    //  _Game Variables
    public bool Force_Quit;
    public bool Player_Dead;

    //  Character Variables
    private Character player;
    private string player_Name => $"+ {player.Char_Name}{new string(' ', (27 - player.Char_Name.Length))} +";
    private string player_Health => $"+ HP: {player.Health_Str}{new string(' ', (23 - player.Health_Str.Length))} +";
    private string player_AC => $"+ AC: {player.Def_Unarmored}{new string(' ', (23 - player.Def_Unarmored.ToString().Length))} +";

    private Character enemy;
    private string enemy_Name => $"+ {new string(' ', (27 - enemy.Char_Name.Length))}{enemy.Char_Name} +";
    private string enemy_Health => $"+ {new string(' ', (23 - enemy.Health_Str.Length))}HP: {enemy.Health_Str} +";

    private int enemy_ACLow;
    private int enemy_ACHigh;
    private string enemy_ACRange;
    private string enemy_AC => $"+ {new string(' ', (23 - enemy_ACRange.Length))}AC: {enemy_ACRange} +";

    //  Combat Variables
    private bool combatActive;

    //  Constructor (param Player, Enemy)
    public SubManagerCombat(Character pPlayer, Character pEnemy) {
        //  Part - Setup Character
        player = pPlayer;
        enemy = pEnemy;

        enemy_ACLow = -999;
        enemy_ACHigh = 999;
        enemy_ACRange = "???";

        //  Part - Setup Combat
        combatActive = true;
    }

    //  MainMethod - Combat Loop (param Random)
    public void CombatLoop(Random pRand) {
        Console.WriteLine($"Player has encountered {enemy.Char_Article} {enemy.Char_Name}! Combat initiated!");
        string? action = Console.ReadLine();

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
            else {
                combatActive = false;
                Player_Dead = true;
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
        Console.WriteLine(enemy_AC);
        Console.WriteLine(player_Name);
        Console.WriteLine(player_Health);
        Console.WriteLine(player_AC);
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
            string? action = Console.ReadLine();
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
                    int attack = player.Attack(pRand, enemy);

                    if (attack != -999) {
                        //  Attack missed
                        if (attack < enemy.Def_Unarmored) {
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
            string? action = Console.ReadLine();
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