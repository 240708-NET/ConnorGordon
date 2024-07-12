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

    //  Defense Variables
    private int def_Unarmored;
    public int Def_Unarmored => def_Unarmored;

    //  Life Variables
    private int health_Base;
    private int health_Curr;
    public bool Health_Alive => health_Curr > 0;
    public string Health_Str => $"{health_Curr}/{health_Base}";

    //  Constructor (param Name, Strength, Dexterity, Constitution)
    public Character(string pName, int pStr, int pDex, int pCon) {
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
        atk_Unarmed = new Attack("Melee", 0, $"1/bludgeoning", "");

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
        atk_Unarmed = new Attack("Melee", 0, $"1/bludgeoning", "");

        //  Part - Setup Defense
        def_Unarmored = 10 + attr_DexMod;

        //  Part - Setup Life
        health_Base = 10 + attr_ConMod;
        health_Curr = 0 + health_Base;
    }

    //  MainMethod - Attack (param Target)
    /// <summary>
    /// Character-level method for attacking
    /// </summary>
    /// <param name="pRand">Reference to global random</param>
    /// <param name="pTarget">Targeted Character for the attack</param>
    public void Attack(Random pRand, Character pTarget) {
        AttackCalc(pRand, pTarget, atk_Unarmed);
    }

    private void AttackCalc(Random pRand, Character pTarget, Attack pAtk) {
        string[] attackArr = pAtk.Attack_Type.Split("/");
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

                Console.WriteLine($"{Char_Name} rolls {dice}{(((attackMod + attrMod) > 0) ? "+" : "")}{(attackMod + attrMod)} ({(dice + attackMod + attrMod)})");

                //  Part - Check vs Target AC
                if ((dice + attackMod + attrMod) >= pTarget.Def_Unarmored) {
                    DealDamage(pRand, pTarget, attrMod);
                }
                break;
        }
    }

    //  SubMethod of Attack - Deal Damage (param Random, Target)
    /// <summary>
    /// Character-level method for attacking
    /// </summary>
    /// <param name="pRand">Reference to global random</param>
    /// <param name="pTarget">Targeted Character for the attack</param>
    /// <param name="pMod">Damage modifier for the attack</param>
    private void DealDamage(Random pRand, Character pTarget, int pMod) {
        List<AttackDamage> attackDamages = atk_Unarmed.Attack_Damages;
        List<string> attackDmgStrs = new List<string>();
        List<int> attackDmgVals = new List<int>();

        //  Part - Get Damage Actual
        for (int i = 0; i < attackDamages.Count; i++) {
            attackDmgVals.Add(attackDamages[i].GetDamage(pRand));
            attackDmgStrs.Add(attackDmgVals[i] + ((i == 0 && pMod != 0) ? ((pMod > 0) ? "+" : "") + pMod : "") + " " + attackDamages[i].DmgType);

            if (i == 0) {
                attackDmgVals[0] += ((i == 0) ? pMod : 0);
            }
        }

        // Part - Get Attack String
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

        //  Part - Apply Damage
        for (int i = 0; i < attackDamages.Count; i++) {
            pTarget.TakeDamage(attackDmgVals[i], attackDamages[i].DmgType);
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

            if (health_Curr <= 0) {
                Console.WriteLine($"- {Char_Name} has died");
            }
        }
    }

    public void RestoreHealth() {
        health_Curr = 0 + health_Base;
    }
}