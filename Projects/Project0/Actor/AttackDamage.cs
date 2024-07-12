namespace Project0.Actor;

class AttackDamage {
    private string dmgDice;
    public string DmgDice {
        get { return dmgDice; }
        set { dmgDice = "" + value; }
    }

    private int dmgMod;
    public int DmgMod {
        get { return dmgMod; }
        set { dmgMod = 0 + value; }
    }

    private string dmgType;
    public string DmgType {
        get { return dmgType; }
        set { dmgType = "" + value; }
    }

    //  Constructor (param Dice, Type, Modifier)
    /// <summary>
    /// Basic Damage Data Structure
    /// </summary>
    /// <param name="pDice">Dice of Damage</param>
    /// <param name="pType">Type of Damage</param>
    public AttackDamage(string pDice, string pType) {
        dmgDice = "" + pDice;
        dmgType = "" + pType;
        dmgMod = 0;
    }

    //  Constructor (param Dice, Type, Modifier)
    /// <summary>
    /// Advanced Damage Data Structure
    /// </summary>
    /// <param name="pDice">Dice of Damage</param>
    /// <param name="pType">Type of Damage</param>
    /// <param name="pMod">Extra Damage</param>
    public AttackDamage(string pDice, int pMod, string pType) {
        dmgDice = "" + pDice;
        dmgMod = 0 + pMod;
        dmgType = "" + pType;
    }

    // Copy Constructor (param Damage)
    public AttackDamage(AttackDamage pDmg) {
        dmgDice = "" + pDmg.DmgDice;
        dmgMod = 0 + pDmg.DmgMod;
        dmgType = "" + pDmg.DmgType;
    }

    //  MainMethod - To String
    public override string ToString() {
        return dmgDice + "+" + ((dmgMod != 0) ? (" + " + dmgMod) : "") + " " + dmgType;
    }

    //  MainMethod - Get Damage
    public int GetDamage(Random pRand) {
        int damage = 0;

        //  Part - Get Dice Damage
        if (DmgDice.Contains("d") == true) {
            string[] diceArr = DmgDice.Split("d");

            for (int i = 0; i < int.Parse(diceArr[0]); i++) {
                damage += pRand.Next(1, int.Parse(diceArr[1])+1);
            }
        }

        else {
            damage += int.Parse(DmgDice);
        }

        //  Part - Add Mod Damage
        damage += DmgMod;

        return damage;
    }
}