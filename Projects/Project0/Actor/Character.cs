namespace Project0.Actor;

class Character {
    //  _Attribute Variables
    private int attr_Str;
    private int attr_Con;

    //  Attack Variables
    private Attack atk_Unarmed;

    //  Life Variables
    private int health_Base;
    private int health_Curr;

    //  Constructor (param Strength, Constitution, Health)
    public Character(int pStr, int pCon, int pHP) {
        //  Part - Setup _Attributes
        attr_Str = 0 + pStr;
        attr_Con = 0 + pCon;

        //  Part - Setup Attack
        atk_Unarmed = new Attack($"1/{attr_Str}/bludgeoning, 3d4/fire");

        //  Part - Setup Life
        health_Base = 0 + pHP;
        health_Curr = 0 + pHP;
    }

    //  MainMethod - Attack (param Target)
    /// <summary>
    /// Character-level method for attacking
    /// </summary>
    /// <param name="pTarget">Targeted Character for the attack</param>
    public void Attack(Random pRand, Character pTarget) {
        List<AttackDamage> attackDamages = atk_Unarmed.Attack_Damages;
        List<string> attackDmgStrs = new List<string>();
        List<int> attackDmgVals = new List<int>();

        //  Part - Get Damage Actual
        for (int i = 0; i < attackDamages.Count; i++) {
            attackDmgVals.Add(attackDamages[i].GetDamage(pRand));
            attackDmgStrs.Add(attackDmgVals[i] + " " + attackDamages[i].DmgType);
        }

        // Part - Get Attack String
        string damageStr = "";
        if (attackDmgStrs.Count == 1) {
            damageStr = $"Character attacks for {attackDmgStrs[0]} damage";
        }
        else if (attackDmgStrs.Count == 2) {
            damageStr = $"Character attacks for {attackDmgStrs[0]} and {attackDmgStrs[1]} damage";
        }
        else if (attackDmgStrs.Count >= 3) {
            damageStr = "Character attacks for ";
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
        if (health_Curr > 0 && (health_Curr - pAmt) <= 0) {
            health_Curr -= pAmt;
            Console.WriteLine($"Character takes {pAmt} {pType} damage");

            if (health_Curr <= 0) {
                Console.WriteLine("- Character has died");
            }
        }
    }
}