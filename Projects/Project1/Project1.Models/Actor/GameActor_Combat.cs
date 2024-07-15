namespace Project1.Models.Actor {
    public class GameActor_Combat {
        //  ~Reference Variables
        public GameActor RefGActor { get; private set; }
        private GameActor_Admin refAAdmin => RefGActor.Actor_Admin;
        private Dictionary<string, int> refAAttrMod => refAAdmin.D_AttrMod;

        //  Attack Variables
        public GameAttack Atk_Unarmed { get; private set; }
        public List<GameAttack> Atk_List { get; private set; }

        //  Defense Variables
        public int Def_Unarmored { get; private set; }

        //  Health Variables
        public string Actor_HealthDice { get; private set; }
        public int Actor_HealthBase { get; private set; }
        public int Actor_HealthCurr { get; private set; }
        public string ActorHealth_Str => $"{Actor_HealthCurr}/{Actor_HealthBase}";
        
        //  Constructor
        /// <summary>
        /// Combat handler for the actor
        /// </summary>
        /// <param name="pRef">Reference to the actor</param>
        /// <param name="pUnarmed">Unarmed/Base attack</param>
        public GameActor_Combat(GameActor pRef, GameAttack pUnarmed) {   
            //  Setup ~Reference
            RefGActor = pRef;

            //  Setup Attack
            Atk_Unarmed = new GameAttack(pUnarmed);
            Atk_List = new List<GameAttack>();

            //  Setup Defense
            Def_Unarmored = 0;

            //  Setup Health
            Actor_HealthDice = "";
            Actor_HealthBase = 0;
            Actor_HealthCurr = 0;
        }

        //  Copy Constructor
        public GameActor_Combat(GameActor_Combat pCombat) {
            //  Setup ~Reference
            RefGActor = pCombat.RefGActor;
            
            //  Setup Attack
            Atk_Unarmed = new GameAttack(pCombat.Atk_Unarmed);

            Atk_List = new List<GameAttack>();
            foreach(GameAttack attack in pCombat.Atk_List) {
                Atk_List.Add(new GameAttack(attack));
            }

            //  Setup Defense
            Def_Unarmored = 0 + pCombat.Def_Unarmored;

            //  Setup Health
            Actor_HealthDice = "" + pCombat.Actor_HealthDice;
            Actor_HealthBase = 0 + pCombat.Actor_HealthBase;
            Actor_HealthCurr = 0 + Actor_HealthBase;
        }

        //  MainMethod - Add Attack
        /// <summary>
        /// Add attack to the actor
        /// </summary>
        /// /// <param name="pName">Name of the attack</param>
        /// <param name="pAction">Action used for the attack</param>
        /// <param name="pType">Attack type [Melee, Ranged, Spell, Saving Throw]</param>
        /// <param name="pMod">Attack modification [Attack Mod, Saving Throw DC]</param>
        /// <param name="pDamages">Damage information (Dice/[Mod]/Type)</param>
        public void AddAttack(string pName, string pAction, string pType, int pMod, string pDamages) {
            Atk_List.Add(new GameAttack(pName, pAction, pType, pMod, pDamages));
        }

        //  MainMethod - Set Defence
        /// <summary>
        /// Calculates and sets new unarmored defense
        /// </summary>
        public void SetDefense() {
            Def_Unarmored = 10 + RefGActor.Actor_Admin.D_AttrMod["DEX"];
        }

        //  SubMethod of Constructor - Setup Health
        /// <summary>
        /// Setup health and dice for the actor
        /// </summary>
        /// <param name="pHealth">Total health of the actor</param>
        /// <param name="pDice">Health dice in XdY format</param>
        public void SetupHealth(int pHealth, string pDice) {
            Actor_HealthDice = "" + pDice;

            Actor_HealthBase = 0 + pHealth;
            Actor_HealthCurr = 0 + Actor_HealthBase;
        }

        //  SubMethod of Constructor - Roll Health
        /// <summary>
        /// Rolls health dice and adds number of (health dice * constitution modifier)
        /// </summary>
        /// <param name="pRand">Reference to Random</param>
        /// <param name="pDice">Health dice in XdY format</param>
        public void RollHealth(Random pRand, string pDice) {
            Actor_HealthDice = "" + pDice;

            //  Split dice into type and num (pDice should be in XdY format)
            string[] diceArr = Actor_HealthDice.Split("d");
            int diceNum = int.Parse(diceArr[0]);
            int diceType = int.Parse(diceArr[1]);
            
            //  Roll health dice
            int health = 0;            
            for(int i = 0; i < diceNum; i++) {
                health += pRand.Next(1, diceType+1);
            }

            //  Add health dice and (Dice number * Constitution modifier)
            Actor_HealthBase = 0 + health + (diceNum * RefGActor.Actor_Admin.D_AttrMod["CON"]);
            Actor_HealthCurr = 0 + Actor_HealthBase;
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

        private int AttackCalc(Random pRand, GameActor pTarget, GameAttack pAtk) {
            string[] attackArr = pAtk.Attack_Str.Split("/");
            //string[] attackTags = attackArr[2].Split(", ");
            int attackMod = int.Parse(attackArr[1]);

            //  Part - Calculate if attack lands
            int dice = pRand.Next(1, 21);
            int attrMod = 0;
            switch(attackArr[0]) {
                //  Melee Attack (If Finesse, can use Dex mod instead of Str mod)
                case "Melee":
                    bool finesse = false;
                    /*
                    foreach(string str in attackTags) {
                        if (str == "Finesse") {
                            finesse = true;
                        }
                    }
                    */
                    
                    attrMod = ((finesse == true && refAAttrMod["DEX"] > refAAttrMod["STR"]) ? refAAttrMod["DEX"] : refAAttrMod["STR"]);
                    Console.Write($"{RefGActor.Actor_Admin.Actor_Name} {pAtk.Attack_Action} {pAtk.Attack_Name}, ");
                    Console.Write($"rolls {dice}{(((attackMod + attrMod) >= 0) ? "+" : "")}{(attackMod + attrMod)} ({(dice + attackMod + attrMod)})");

                    //  Part - Check vs Target AC
                    if ((dice + attackMod + attrMod) >= pTarget.Actor_Combat.Def_Unarmored) {
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
        /// <summary>
        /// Character-level method for attacking
        /// </summary>
        /// <param name="pRand">Reference to global random</param>
        /// <param name="pTarget">Targeted Character for the attack</param>
        /// <param name="pMod">Damage modifier for the attack</param>
        private void DealDamage(Random pRand, GameActor pTarget, GameAttack pAtk, int pMod) {
            List<GameDamage> attackDamages = pAtk.Attack_Damages;
            List<string> attackDmgStrs = new List<string>();
            List<int> attackDmgVals = new List<int>();

            //  Part - Get Damage Actual (Gets the total values for the damages)
            for (int i = 0; i < attackDamages.Count; i++) {
                attackDmgVals.Add(attackDamages[i].GetDamage(pRand));
                attackDmgStrs.Add(attackDmgVals[i] + ((i == 0 && pMod != 0) ? ((pMod > 0) ? "+" : "") + pMod : "") + " " + attackDamages[i].Dmg_Type);

                if (i == 0) {
                    attackDmgVals[0] += ((i == 0) ? pMod : 0);
                }
            }

            // Part - Get Attack String (Gets the printable version of values for the damages)
            string damageStr = "";
            if (attackDmgStrs.Count == 1) {
                damageStr = $"{RefGActor.Actor_Admin.Actor_Name} attacks for {attackDmgStrs[0]} damage";
            }
            else if (attackDmgStrs.Count == 2) {
                damageStr = $"{RefGActor.Actor_Admin.Actor_Name} attacks for {attackDmgStrs[0]} and {attackDmgStrs[1]} damage";
            }
            else if (attackDmgStrs.Count >= 3) {
                damageStr = "{RefGActor.Actor_Admin.Actor_Name} attacks for ";
                for (int i = 0; i < attackDmgStrs.Count-1; i++) {
                    damageStr += attackDmgStrs[i] + ", ";
                }
                damageStr += "and " + attackDmgStrs[attackDmgStrs.Count-1] + " damage";
            }

            Console.WriteLine(damageStr);

            //  Part - Apply Damage (Applies damage if > 0)
            for (int i = 0; i < attackDamages.Count; i++) {
                if (attackDmgVals[i] > 0) {
                    pTarget.Actor_Combat.TakeDamage(attackDmgVals[i], attackDamages[i].Dmg_Type);
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
            if (Actor_HealthCurr > 0) {
                Actor_HealthCurr -= pAmt;
                Console.WriteLine($"{RefGActor.Actor_Admin.Actor_Name} takes {pAmt} {pType} damage");

                //  Character has died
                if (Actor_HealthCurr <= 0) {
                    Console.WriteLine($"- {RefGActor.Actor_Admin.Actor_Name} has died");
                }
            }
        }

        //  MainMethod - Restore Health
        /// <summary>
        /// Character-level method for restoring current health to base health
        /// </summary>
        public void RestoreHealth() {
            Actor_HealthCurr = 0 + Actor_HealthBase;
        }
    }
}