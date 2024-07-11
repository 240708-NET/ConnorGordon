namespace Project0.Actor;

class Attack {
    private List<AttackDamage> attack_Damages;
    public List<AttackDamage> Attack_Damages => attack_Damages;

    //  Constructor (param Damages)
    public Attack(string pDamages) {
        attack_Damages = new List<AttackDamage>();

        //  Part - Split pDamages into individual damages
        string[] attackArr = pDamages.Split(", ");
        for (int i = 0; i < attackArr.Length; i++) {
            string[] damageArr = attackArr[i].Split("/");

            if (damageArr.Length == 2) {
                attack_Damages.Add(new AttackDamage(damageArr[0], damageArr[1]));
            }
            
            else {
                attack_Damages.Add(new AttackDamage(damageArr[0], int.Parse(damageArr[1]), damageArr[2]));
            }
        }
    }
}