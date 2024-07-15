namespace Project1.Models.Actor {
    public class GameAttack {
        // Attack Variables
        public string Attack_Name { get; private set; }
        public string Attack_Action { get; private set; }
        public string Attack_Type { get; private set; } // Melee, Ranged, Spell, Saving Throw
        public string Attack_Mod  { get; private set; } // Melee/Ranged/Spell Mod, Saving Throw DC
        public string Attack_Str => Attack_Type + "/" + Attack_Mod;

        //  Damage Variables
        public List<GameDamage> Attack_Damages { get; private set; }

        //  Constructor
        /// <summary>
        /// Basic Attack Structure
        /// </summary>
        /// <param name="pName">Name of the attack</param>
        /// <param name="pAction">Action used for the attack</param>
        /// <param name="pType">Attack type [Melee, Ranged, Spell, Saving Throw]</param>
        /// <param name="pMod">Attack modification [Attack Mod, Saving Throw DC]</param>
        /// <param name="pDamages">Damage information (Dice/[Mod]/Type)</param>
        public GameAttack(string pName, string pAction, string pType, int pMod, string pDamages) {
            //  Part - Setup Attack
            Attack_Name = "" + pName;
            Attack_Type = "" + pType;
            Attack_Action = "" + pAction;
            Attack_Mod = "" + pMod;
            
            //  Part - Setup Damage
            Attack_Damages = new List<GameDamage>();

            //  SubPart - Split pDamages into individual damages
            string[] attackArr = pDamages.Split(", ");
            for (int i = 0; i < attackArr.Length; i++) {
                string[] damageArr = attackArr[i].Split("/");
                
                Attack_Damages.Add(new GameDamage(damageArr[0], int.Parse(damageArr[1]), damageArr[2]));
            }
        }

        //  Copy Constructor
        public GameAttack(GameAttack pAtk) {
            //  Part - Setup Attack
            Attack_Name = "" + pAtk.Attack_Name;
            Attack_Type = "" + pAtk.Attack_Type;
            Attack_Action = "" + pAtk.Attack_Action;
            Attack_Mod = "" + pAtk.Attack_Mod;
            
            //  Part - Setup Damage
            Attack_Damages = new List<GameDamage>();

            foreach(GameDamage damage in pAtk.Attack_Damages) {
                Attack_Damages.Add(new GameDamage(damage));
            }
        }
    }
}