namespace Project0.Actor;

class Character {
    //  _Character Variables
    private string char_Name;
    public string Char_Name => char_Name;
    public string Char_Article { get; private set; }

    //  _Attribute Variables
    private int attr_Str;
    private int attr_StrMod;
    public int Attr_Str => attr_Str;

    private int attr_Dex;
    private int attr_DexMod;
    public int Attr_Dex => attr_Dex;

    private int attr_Con;
    private int attr_ConMod;
    public int Attr_Con => attr_Con;

    //  Attack Variables
    private Attack atk_Unarmed;
    public Attack Atk_Unarmed => atk_Unarmed;

    private List<Attack> atk_List;
    public List<Attack> Atk_List => atk_List;

    //  Defense Variables
    private int def_Unarmored;
    public int Def_Unarmored => def_Unarmored;

    //  Life Variables
    private int health_Base;
    private int health_Curr;
    public bool Health_Alive => health_Curr > 0;
    public string Health_Str => $"{health_Curr}/{health_Base}";

    //  Constructor (param Name, Strength, Dexterity, Constitution, Unarmed)
    /// <summary>
    /// Basic character object
    /// </summary>
    /// <param name="pName">Name of the character</param>
    /// <param name="pStr">Character strength</param>
    /// <param name="pDex">Character dexterity</param>
    /// <param name="pCon">Character constitution</param>
    public Character(string pName, int pStr, int pDex, int pCon, Attack pUnarmed) {
        //  Part - Setup _Character
        char_Name = "" + pName;
        string charFirst = char_Name.Substring(0, 1);
        if (charFirst == "a" || charFirst == "e" || charFirst == "i" || charFirst == "o" || charFirst == "u") {
            Char_Article = "an";
        }
        else {
            Char_Article = "a";
        }

        //  Part - Setup _Attributes
        attr_Str = 0 + pStr;
        attr_StrMod = (attr_Str / 2) - 5;

        attr_Dex = 0 + pDex;
        attr_DexMod = (attr_Dex / 2) - 5;

        attr_Con = 0 + pCon;
        attr_ConMod = (attr_Con / 2) - 5;

        //  Part - Setup Attack
        atk_Unarmed = new Attack(pUnarmed);
        atk_List = new List<Attack>();

        //  Part - Setup Defense
        def_Unarmored = 10 + attr_DexMod;

        //  Part - Setup Life
        health_Base = 10 + attr_ConMod;
        health_Curr = 0 + health_Base;
    }

    //  Copy Constructor (param Character)
    public Character(Character pChar) {
        //  Part - Setup _Character
        char_Name = "" + pChar.Char_Name;
        Char_Article = "" + pChar.Char_Article;

        //  Part - Setup _Attributes
        attr_Str = 0 + pChar.Attr_Str;
        attr_StrMod = (attr_Str / 2) - 5;

        attr_Dex = 0 + pChar.Attr_Dex;
        attr_DexMod = (attr_Dex / 2) - 5;

        attr_Con = 0 + pChar.Attr_Con;
        attr_ConMod = (attr_Con / 2) - 5;

        //  Part - Setup Attack
        atk_Unarmed = new Attack(pChar.Atk_Unarmed);

        atk_List = new List<Attack>();
        foreach(Attack atk in pChar.Atk_List) {
            atk_List.Add(new Attack(atk));
        }

        //  Part - Setup Defense
        def_Unarmored = 10 + attr_DexMod;

        //  Part - Setup Life
        health_Base = 10 + attr_ConMod;
        health_Curr = 0 + health_Base;
    }

    //  MainMethod - Add Attack (param Type, Mod, Damages, Tags)
    /// <summary>
    /// Add attack to the character
    /// </summary>
    /// /// <param name="pName">Name of the attack</param>
    /// <param name="pAction">Action used for the attack</param>
    /// <param name="pType">Attack type [Melee, Ranged, Spell, Saving Throw]</param>
    /// <param name="pMod">Attack modification [Attack Mod, Saving Throw DC]</param>
    /// <param name="pDamages">Damage information (Dice/[Mod]/Type)</param>
    /// /// <param name="pDamages">Attack tags [Finesse]</param>
    public void AddAttack(string pName, string pAction, string pType, int pMod, string pDamages, string pTags) {
        atk_List.Add(new Attack(pName, pAction, pType, pMod, pDamages, pTags));
    }

    //  MainMethod - Attack (param Target)
    /// <summary>
    /// Character-level method for attacking
    /// </summary>
    /// <param name="pRand">Reference to global random</param>
    /// <param name="pTarget">Targeted Character for the attack</param>
    public int Attack(Random pRand, Character pTarget) {
        int rand = (atk_List.Count > 0) ? pRand.Next(0, atk_List.Count) : -1;
        return AttackCalc(pRand, pTarget, (rand == -1) ? atk_Unarmed : atk_List[rand]);
    }

    private int AttackCalc(Random pRand, Character pTarget, Attack pAtk) {
        string[] attackArr = pAtk.Attack_Str.Split("/");
        string[] attackTags = attackArr[2].Split(", ");
        int attackMod = int.Parse(attackArr[1]);

        //  Part - Calculate if attack lands
        int dice = pRand.Next(1, 21);
        int attrMod = 0;
        switch(attackArr[0]) {
            //  Melee Attack (If Finesse, can use Dex mod instead of Str mod)
            case "Melee":
                bool finesse = false;
                foreach(string str in attackTags) {
                    if (str == "Finesse") {
                        finesse = true;
                    }
                }
                
                attrMod = ((finesse == true && attr_DexMod > attr_StrMod) ? attr_DexMod : attr_StrMod);
                Console.Write($"{Char_Name} {pAtk.Attack_Action} {pAtk.Attack_Name}, ");
                Console.Write($"rolls {dice}{(((attackMod + attrMod) >= 0) ? "+" : "")}{(attackMod + attrMod)} ({(dice + attackMod + attrMod)})");

                //  Part - Check vs Target AC
                if ((dice + attackMod + attrMod) >= pTarget.Def_Unarmored) {
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
    private void DealDamage(Random pRand, Character pTarget, Attack pAtk, int pMod) {
        List<AttackDamage> attackDamages = pAtk.Attack_Damages;
        List<string> attackDmgStrs = new List<string>();
        List<int> attackDmgVals = new List<int>();

        //  Part - Get Damage Actual (Gets the total values for the damages)
        for (int i = 0; i < attackDamages.Count; i++) {
            attackDmgVals.Add(attackDamages[i].GetDamage(pRand));
            attackDmgStrs.Add(attackDmgVals[i] + ((i == 0 && pMod != 0) ? ((pMod > 0) ? "+" : "") + pMod : "") + " " + attackDamages[i].DmgType);

            if (i == 0) {
                attackDmgVals[0] += ((i == 0) ? pMod : 0);
            }
        }

        // Part - Get Attack String (Gets the printable version of values for the damages)
        string damageStr = "";
        if (attackDmgStrs.Count == 1) {
            damageStr = $"{Char_Name} attacks for {attackDmgStrs[0]} damage";
        }
        else if (attackDmgStrs.Count == 2) {
            damageStr = $"{Char_Name} attacks for {attackDmgStrs[0]} and {attackDmgStrs[1]} damage";
        }
        else if (attackDmgStrs.Count >= 3) {
            damageStr = "{Char_Name} attacks for ";
            for (int i = 0; i < attackDmgStrs.Count-1; i++) {
                damageStr += attackDmgStrs[i] + ", ";
            }
            damageStr += "and " + attackDmgStrs[attackDmgStrs.Count-1] + " damage";
        }

        Console.WriteLine(damageStr);

        //  Part - Apply Damage (Applies damage if > 0)
        for (int i = 0; i < attackDamages.Count; i++) {
            if (attackDmgVals[i] > 0) {
                pTarget.TakeDamage(attackDmgVals[i], attackDamages[i].DmgType);
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
        if (health_Curr > 0) {
            health_Curr -= pAmt;
            Console.WriteLine($"{Char_Name} takes {pAmt} {pType} damage");

            //  Character has died
            if (health_Curr <= 0) {
                Console.WriteLine($"- {Char_Name} has died");
            }
        }
    }

    //  MainMethod - Restore Health
    /// <summary>
    /// Character-level method for restoring current health to base health
    /// </summary>
    public void RestoreHealth() {
        health_Curr = 0 + health_Base;
    }
}