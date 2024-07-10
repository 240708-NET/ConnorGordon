namespace Project0.Actor {
    class Character {
        //  Attribute Variables
        private int attr_Str;
        private int attr_Con;

        //  Life Variables
        private int health_Base;
        private int health_Curr;

        //  Constructor (param Strength, Constitution, Health)
        public Character(int pStr, int pCon, int pHP) {
            //  Part - Setup Attributes
            attr_Str = 0 + pStr;
            attr_Con = 0 + pCon;

            //  Part - Setup Life
            health_Base = 0 + pHP;
            health_Curr = 0 + pHP;
        }
    }
}