using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.Models.Actor {
    public class GameActor {
        //  _Server Variables
        [Key]
        public int Id { get; set; }

        //  Attribute Variables
        public int Proficiency { get; set; }
        [NotMapped]
        public Dictionary<string, int> D_AttrScr { get; set; }
        [NotMapped]
        public Dictionary<string, int> D_AttrMod { get; set; }
        public string Attributes { get; set; }

        //  Class Variables
        public string Class { get; set; }

        //  Combat Variables
        [NotMapped]
        public GameAttack Atk_Unarmed { get; private set; }
        public string AttackUnarmed { get; set; }
        [NotMapped]
        public List<GameAttack> Atk_List { get; private set; }
        public string AttackList { get; set; }

        //  Defense Variables
        [NotMapped]
        public int Def_Unarmored { get; private set; }

        //  Health Variables
        public string HealthDice { get; set; }
        [NotMapped]
        public int HealthBase { get; private set; }
        [NotMapped]
        public int HealthCurr { get; private set; }
        [NotMapped]
        public string HealthStr => $"{HealthCurr}/{HealthBase}";

        //  Name Variables
        public string Name { get; set; }
        [NotMapped]
        public bool Proper { get; set; }
        [NotMapped]
        public string Article { get; set; }

        //  Default Constructor
        public GameActor() {
            //  Setup Attribute
            D_AttrScr = new Dictionary<string, int>();
            D_AttrMod = new Dictionary<string, int>();
            Attributes = "";

            //  Setup Class
            Class = "";

            //  Setup Combat
            Atk_Unarmed = new GameAttack();
            AttackUnarmed = "";

            Atk_List = new List<GameAttack>();
            AttackList = "";

            //  Setup Health
            HealthDice = "";

            //  Setup Name
            Name = "";
            Article = "";
        }

        //  Copy Constructor
        /// <summary>
        /// Copy constructor for an actor
        /// </summary>
        /// <param name="pActor">Actor to be copied</param>
        /// <param name="pServer">Is actor pulled from server or local</param>
        public GameActor(GameActor pActor, bool pServer) {
            //  Setup Attribute
            D_AttrScr = new Dictionary<string, int>();
            D_AttrMod = new Dictionary<string, int>();
            Attributes = "";

            //  Setup Class
            Class = "";

            //  Setup Combat
            Atk_Unarmed = new GameAttack();
            AttackUnarmed = "";

            Atk_List = new List<GameAttack>();
            AttackList = "";

            //  Setup Health
            HealthDice = "";

            //  Setup Name
            Name = "";
            Article = "";
            
            if (pServer == false) {
                GameActor_Local(pActor);
            }

            else {
                GameActor_Server(pActor);
            }
        }

        //  SubMethod of CopyConstructor - Game Actor Local
        /// <summary>
        /// Copy local actor template to this
        /// </summary>
        /// <param name="pActor">Actor to be copied</param>
        private void GameActor_Local(GameActor pActor) {
            //  Setup _Server
            Id = 0 + pActor.Id;

            //  Setup Attribute
            Proficiency = 0 + pActor.Proficiency;

            D_AttrScr = new Dictionary<string, int>();
            foreach(var attr in pActor.D_AttrScr) {
                D_AttrScr.Add(attr.Key, attr.Value);
            }

            D_AttrMod = new Dictionary<string, int>();
            foreach(var attr in pActor.D_AttrMod) {
                D_AttrMod.Add(attr.Key, attr.Value);
            }

            Attributes = "" + pActor.Attributes;

            //  Setup Class
            Class = "" + pActor.Class;

            //  Setup Combat
            Atk_Unarmed = new GameAttack(pActor.Atk_Unarmed);
            AttackUnarmed = "" + pActor.AttackUnarmed;

            Atk_List = new List<GameAttack>();
            foreach(GameAttack attack in pActor.Atk_List) {
                Atk_List.Add(new GameAttack(attack));
            }
            AttackList = "" + pActor.AttackList;

            //  Setup Defense
            Def_Unarmored = 0 + pActor.Def_Unarmored;

            //  Setup Health
            HealthDice = "" + pActor.HealthDice;
            HealthBase = 0 + pActor.HealthBase;
            HealthCurr = 0 + pActor.HealthCurr;

            //  Setup Name
            Name = "" + pActor.Name;
            Proper = pActor.Proper;
            Article = "" + pActor.Article;
        }

        //  SubMethod of CopyConstructor - Game Actor Server
        /// <summary>
        /// Translate server template into local actor
        /// </summary>
        /// <param name="pActor">Actor to be copied</param>
        private void GameActor_Server(GameActor pActor) {
            //  Setup _Server
            Id = 0 + pActor.Id;

            //  Setup Attribute
            Proficiency = 0 + pActor.Proficiency;

            Attributes = "" + pActor.Attributes;
            string[] attrArr = Attributes.Split(",");

            D_AttrScr = new Dictionary<string, int>() {
                ["STR"] = int.Parse(attrArr[0]),
                ["DEX"] = int.Parse(attrArr[1]),
                ["CON"] = int.Parse(attrArr[2]),
                ["INT"] = int.Parse(attrArr[3]),
                ["WIS"] = int.Parse(attrArr[4]),
                ["CHA"] = int.Parse(attrArr[5]),
            };

            D_AttrMod = new Dictionary<string, int>() {
                ["STR"] = (D_AttrScr["STR"] / 2) - 5,
                ["DEX"] = (D_AttrScr["DEX"] / 2) - 5,
                ["CON"] = (D_AttrScr["CON"] / 2) - 5,
                ["INT"] = (D_AttrScr["INT"] / 2) - 5,
                ["WIS"] = (D_AttrScr["WIS"] / 2) - 5,
                ["CHA"] = (D_AttrScr["CHA"] / 2) - 5,
            };

            //  Setup Class
            Class = "" + pActor.Class;

            //  Setup Combat
            AttackUnarmed = "" + pActor.AttackUnarmed;
            //fists_strikes with_Melee_0/0_1_bludgeoning
            string[] unarmedArr1 = AttackUnarmed.Split("/");
            string[] unarmedArr2 = unarmedArr1[0].Split("_");

            Atk_Unarmed = new GameAttack(unarmedArr2[0], unarmedArr2[1], unarmedArr2[2], int.Parse(unarmedArr2[3]), unarmedArr1[1]);

            AttackList = "" + pActor.AttackList;
            Atk_List = new List<GameAttack>();
            if (AttackList != "") {
                string[] atklistArr1 = AttackList.Split(",");
                string[] atklistArr2;
                string[] atklistArr3;

                foreach(string attack in atklistArr1) {
                    atklistArr2 = attack.Split("/");
                    atklistArr3 = atklistArr2[0].Split("_");
                    Atk_List.Add(new GameAttack(atklistArr3[0], atklistArr3[1], atklistArr3[2], int.Parse(atklistArr3[3]), atklistArr2[1]));
                }
            }

            //  Setup Defense
            Def_Unarmored = 10 + D_AttrMod["CON"];

            //  Setup Health
            HealthDice = "" + pActor.HealthDice;
            string[] healthArr = HealthDice.Split("d");
            int diceNum = int.Parse(healthArr[0]);
            int diceType = int.Parse(healthArr[1]);

            HealthBase = 0 + (diceNum * diceType) + (diceNum * D_AttrMod["CON"]);
            HealthCurr = 0 + HealthBase;

            //  Setup Name
            string[] nameArr = pActor.Name.Split("_");
            Name = "" + nameArr[0];
            Proper = nameArr[1] == "True";

            string first = Name.Substring(0, 1);
            Article = "" + ((nameArr[1] == "True") ? "" : (((first == "a") || (first == "e") || (first == "i") || (first == "o") || (first == "u")) ? "an" : "a"));
        }

        //  MainMethod - Attack
        /// <summary>
        /// Character-level method for attacking
        /// </summary>
        /// <param name="pRand">Reference to global random</param>
        /// <param name="pTarget">Targeted actor for the attack</param>
        public int Attack(Random pRand, GameActor pTarget) {
            int rand = (Atk_List.Count > 0) ? pRand.Next(0, Atk_List.Count) : -1;
            return AttackCalc(pRand, pTarget, (rand == -1) ? Atk_Unarmed : Atk_List[rand]);
        }

        //  MainMethod - Attack
        /// <summary>
        /// Character-level method for attacking
        /// </summary>
        /// <param name="pRand">Reference to global random</param>
        /// <param name="pTarget">Targeted actor for the attack</param>
        /// <param name="pAtk">Index of chosen attack</param>
        /// <returns></returns>
        public int Attack(Random pRand, GameActor pTarget, int pAtk) {
            return AttackCalc(pRand, pTarget, (pAtk > 0 && pAtk < Atk_List.Count-1) ? Atk_List[pAtk] : Atk_Unarmed);
        }

        private int AttackCalc(Random pRand, GameActor pTarget, GameAttack pAtk) {
            int attackMod = int.Parse(pAtk.Attack_Mod);

            //  Part - Calculate if attack lands
            int dice = pRand.Next(1, 21);
            int attrMod = 0;

            switch(pAtk.Attack_Type) {
                //  Melee Attack
                case "Melee":
                    bool finesse = false;
                    attrMod = ((finesse == true && D_AttrMod["DEX"] > D_AttrMod["STR"]) ? D_AttrMod["DEX"] : D_AttrMod["STR"]);
                    int modNum = attackMod + attrMod;
                    string modStr = $"{((modNum != 0) ? ((modNum > 0) ? "+" : "") + modNum : "")}";

                    Console.Write($"{Name} {pAtk.Attack_Action} {pAtk.Attack_Name}, ");
                    Console.Write($"rolls {dice}{modStr} ({(dice + attackMod + attrMod)})");

                    //  Part - Check vs Target AC
                    if ((dice + attackMod + attrMod) >= 0) {//pTarget.Def_Unarmored) {
                        Console.WriteLine(", and hits!");
                        DealDamage(pRand, pTarget, pAtk, attrMod);
                        return (dice + attackMod + attrMod);
                    }
                    else {
                        Console.WriteLine(", and misses!");
                        Console.WriteLine("");
                        return (dice + attackMod + attrMod);
                    }
            }

            return -999;
        }

        //  SubMethod of Attack - Deal Damage (param Random, Target, Attack, Mod)
        private void DealDamage(Random pRand, GameActor pTarget, GameAttack pAtk, int pMod) {
            List<string> attackDamages = pAtk.Attack_Damages;
            List<string> attackDmgStrs = new List<string>();
            List<int> attackDmgVals = new List<int>();

            //  Get Damage Actual (Gets the total values for the damages)
            for (int i = 0; i < attackDamages.Count; i++) {
                attackDmgVals.Add(pAtk.GetDamage(pRand, attackDamages[i]));
                attackDmgStrs.Add(attackDmgVals[i] + ((i == 0 && pMod != 0) ? ((pMod > 0) ? "+" : "") + pMod : "") + " " + attackDamages[i].Split("_")[2]);
                attackDmgVals[0] += ((i == 0) ? pMod : 0);
            }

            // Get Attack String (Gets the printable version of values for the damages)
            string damageStr = $"{Name} attacks for ";
            if (attackDmgStrs.Count == 1) {
                damageStr += $"{attackDmgStrs[0]} damage";
            }
            else if (attackDmgStrs.Count == 2) {
                damageStr += $"{attackDmgStrs[0]} and {attackDmgStrs[1]} damage";
            }
            else if (attackDmgStrs.Count >= 3) {
                for (int i = 0; i < attackDmgStrs.Count-1; i++) {
                    damageStr += attackDmgStrs[i] + ", ";
                }
                damageStr += "and " + attackDmgStrs[attackDmgStrs.Count-1] + " damage";
            }
            Console.WriteLine(damageStr);

            //  Apply Damage (Applies damage if > 0)
            for (int i = 0; i < attackDamages.Count; i++) {
                if (attackDmgVals[i] > 0) {
                    pTarget.TakeDamage(attackDmgVals[i], attackDamages[i].Split("_")[2]);
                }
            }
            Console.WriteLine("");
        }
      
        //  MainMethod - Take Damage (param Amount, Type)
        /// <summary>
        /// Character-level method for taking damage
        /// </summary>
        /// <param name="pAmt">Amount of damage taken</param>
        /// <param name="pType">Type of damage taken</param>
        public void TakeDamage(int pAmt, string pType) {
            if (HealthCurr > 0) {
                HealthCurr -= pAmt;

                Console.WriteLine($"{Name} takes {pAmt} {pType} damage");
            }
        }

        //  MainMethod - Restore Health
        /// <summary>
        /// Character-level method for restoring current health to base health
        /// </summary>
        public void RestoreHealth() {
            HealthCurr = 0 + HealthBase;
        }
    }
}