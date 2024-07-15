namespace Project1.Models.Actor {
    public class GameDamage {
        public string Dmg_Dice { get; private set; }
        public int Dmg_Mod { get; private set; }
        public string Dmg_Type { get; private set; }

        // Constructor
        /// <summary>
        /// Basic Damage Structure
        /// </summary>
        /// <param name="pDice">Dice of the damage</param>
        /// <param name="pType">Type of the damage</param>
        public GameDamage(string pDice, string pType) {
            Dmg_Dice = "" + pDice;
            Dmg_Mod = 0;
            Dmg_Type = "" + pType;
        }

        // Constructor
        /// <summary>
        /// Basic Damage Structure
        /// </summary>
        /// <param name="pMod">Modifier of the damage</param>
        /// <param name="pType">Type of the damage</param>
        public GameDamage(int pMod, string pType) {
            Dmg_Dice = "";
            Dmg_Mod = 0 + pMod;
            Dmg_Type = "" + pType;
        }

        // Constructor
        /// <summary>
        /// Basic Damage Structure
        /// </summary>
        /// <param name="pDice">Dice of the damage</param>
        /// <param name="pMod">Modifier of the damage</param>
        /// <param name="pType">Type of the damage</param>
        public GameDamage(string pDice, int pMod, string pType) {
            Dmg_Dice = "" + pDice;
            Dmg_Mod = 0 + pMod;
            Dmg_Type = "" + pType;
        }

        //  Copy Constructor
        public GameDamage(GameDamage pDmg) {
            Dmg_Dice = "" + pDmg.Dmg_Dice;
            Dmg_Mod = 0 + pDmg.Dmg_Mod;
            Dmg_Type = "" + pDmg.Dmg_Type;
        }

        //  MainMethod - To String
        public override string ToString() {
            return Dmg_Dice + "+" + ((Dmg_Mod != 0) ? (" + " + Dmg_Mod) : "") + " " + Dmg_Type;
        }

        //  MainMethod - Get Damage
        public int GetDamage(Random pRand) {
            int damage = 0;

            //  Part - Add Dice Damage
            if (Dmg_Dice != "0") {
                string[] diceArr = Dmg_Dice.Split("d");

                for (int i = 0; i < int.Parse(diceArr[0]); i++) {
                    damage += pRand.Next(1, int.Parse(diceArr[1])+1);
                }
            }

            //  Part - Add Mod Damage
            damage += Dmg_Mod;

            return damage;
        }
    }
}