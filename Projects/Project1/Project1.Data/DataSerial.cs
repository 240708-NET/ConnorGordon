using Project1.Models.Actor;
using System.Text.Json;

namespace Project1.Data {
    public class DataSerial : IData {
        public Dictionary<string, GameActor> LoadAllEnemies(string pPath) {
            Dictionary<string, GameActor> d_Enemies = new Dictionary<string, GameActor>();

            StreamReader? sr = new StreamReader(pPath);
            string? line = "";
            while ((line = sr.ReadLine()) != "{C}") {
                //  Read Admin Lines
                string[]? actorAdmin = line.Split("_");
                Dictionary<string, int>? actorAttr = JsonSerializer.Deserialize<Dictionary<string, int>>(sr?.ReadLine());
                string[]? actorHealth = sr.ReadLine().Split("_");

                //  Read Unarmed Lines
                string[]? actorAttack = sr.ReadLine().Split("_");
                string? actorDamage = "";
                while((line = sr.ReadLine()) != "{A}") {
                    actorDamage += line + ", ";
                }
                actorDamage = actorDamage.Substring(0, actorDamage.Length-2);

                GameActor? enemy = new GameActor(new GameAttack(actorAttack[0], actorAttack[1], actorAttack[2], int.Parse(actorAttack[3]), actorDamage));
                enemy.Actor_Admin.SetupName(actorAdmin[0], Convert.ToBoolean(actorAdmin[1]));
                enemy.Actor_Admin.SetupAttributes(actorAttr["STR"], actorAttr["DEX"], actorAttr["CON"], actorAttr["INT"], actorAttr["WIS"], actorAttr["CHA"]);
                enemy.Actor_Combat.SetupHealth(int.Parse(actorHealth[0]), actorHealth[1]);
                enemy.Actor_Combat.SetDefense();

                while((line = sr.ReadLine()) != "{E}") {
                    actorAttack = line.Split("_");

                    actorDamage = "";
                    while((line = sr.ReadLine()) != "{A}") {
                        actorDamage += line + ", ";
                    }
                    actorDamage = actorDamage.Substring(0, actorDamage.Length-2);

                    enemy.Actor_Combat.AddAttack(new GameAttack(actorAttack[0], actorAttack[1], actorAttack[2], int.Parse(actorAttack[3]), actorDamage));
                }

                d_Enemies.Add(enemy.Actor_Admin.Actor_Name, enemy);
            }

            return d_Enemies;
        }

        public void SaveAllEnemies(string pPath, List<GameActor> pEnemies) {
            StreamWriter sw = new StreamWriter(pPath);

            foreach(GameActor enemy in pEnemies) {
                Console.WriteLine("Saving enemy");
                //  Actor Base
                sw.WriteLine(enemy.Actor_Admin.Actor_Str);
                sw.WriteLine(JsonSerializer.Serialize(enemy.Actor_Admin.D_AttrScr));
                sw.WriteLine(enemy.Actor_Combat.Actor_Str);

                //  Unarmed Attack
                sw.WriteLine(enemy.Actor_Combat.Atk_Unarmed.Attack_Str);
                foreach(GameDamage damage in enemy.Actor_Combat.Atk_Unarmed.Attack_Damages) {
                    sw.WriteLine(damage.Dmg_Str);
                }
                sw.WriteLine("{A}");

                // Other Attacks
                foreach(GameAttack attack in enemy.Actor_Combat.Atk_List) {
                    sw.WriteLine(attack.Attack_Str);

                    foreach(GameDamage damage in attack.Attack_Damages) {
                        sw.WriteLine(damage.Dmg_Str);
                    }
                    sw.WriteLine("{A}");
                }
                sw.WriteLine("{E}");
                sw.WriteLine("{C}");
            }

            sw.Close();
        }
    }
}