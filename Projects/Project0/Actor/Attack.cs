namespace Project0.Actor;

class Attack {
    // Attack Variables
    private string attack_Name;
    public string Attack_Name => attack_Name;

    private string attack_Action;
    public string Attack_Action => attack_Action;

    private string attack_Type; // Melee, Ranged, Spell, Saving Throw
    public string Attack_Type => attack_Type;

    private string attack_Mod; // Melee/Ranged/Spell Mod, Saving Throw DC
    public string Attack_Mod => attack_Mod;

    private string attack_Tags; //  Finesse
    public string Attack_Tags => attack_Tags;
    public string Attack_Str => attack_Type + "/" + attack_Mod + "/" + attack_Tags;

    //  Damage Variables
    private List<AttackDamage> attack_Damages;
    public List<AttackDamage> Attack_Damages => attack_Damages;

    //  Constructor (param Attack, Damages)
    /// <summary>
    /// The Attack data structure
    /// </summary>
    /// <param name="pName">Name of the attack</param>
    /// <param name="pAction">Action used for the attack</param>
    /// <param name="pType">Attack type [Melee, Ranged, Spell, Saving Throw]</param>
    /// <param name="pMod">Attack modification [Attack Mod, Saving Throw DC]</param>
    /// <param name="pDamages">Damage information (Dice/[Mod]/Type)</param>
    /// /// <param name="pDamages">Attack tags [Finesse]</param>
    public Attack(string pName, string pAction, string pType, int pMod, string pDamages, string pTags) {
        //  Part - Setup Attack
        attack_Name = "" + pName;
        attack_Type = "" + pType;
        attack_Action = "" + pAction;
        attack_Mod = "" + pMod;
        attack_Tags = "" + pTags;
        
        //  Part - Setup Damage
        attack_Damages = new List<AttackDamage>();

        //  SubPart - Split pDamages into individual damages
        string[] attackArr = pDamages.Split(", ");
        for (int i = 0; i < attackArr.Length; i++) {
            string[] damageArr = attackArr[i].Split("/");
            
            //  MinorPart - Damage is only Dice and Type
            if (damageArr.Length == 2) {
                attack_Damages.Add(new AttackDamage(damageArr[0], damageArr[1]));
            }
            
            //  MinorPart - Damage also includes a modifier
            else {
                attack_Damages.Add(new AttackDamage(damageArr[0], int.Parse(damageArr[1]), damageArr[2]));
            }
        }
    }

    //  Copy Constructor (param Attack)
    public Attack(Attack pAtk) {
        //  Part - Setup Attack
        attack_Name = "" + pAtk.Attack_Name;
        attack_Type = "" + pAtk.Attack_Type;
        attack_Action = "" + pAtk.Attack_Action;
        attack_Mod = "" + pAtk.attack_Mod;
        attack_Tags = "" + pAtk.Attack_Tags;
        
        //  Part - Setup Damage
        attack_Damages = new List<AttackDamage>();

        foreach(AttackDamage damage in pAtk.Attack_Damages) {
            attack_Damages.Add(new AttackDamage(damage));
        }
    }
}