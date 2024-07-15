namespace Project1.Models.Actor {
    public class GameActor_Admin {
        //  Active Variables
        public E_ActorState Actor_State;

        //  Attribute Variables
        public Dictionary<string, int> D_AttrScr { get; private set; }
        public Dictionary<string, int> D_AttrMod { get; private set; }

        //  Name Variables
        public string Actor_Name { get; private set; }
        public bool Actor_NameProper { get; private set; }
        public string Actor_Article { get; private set; }

        //  Proficiency Variables
        public int Actor_Proficiency { get; private set; }

        //  Constructor
        public GameActor_Admin() {
            //  Setup Active
            Actor_State = E_ActorState.Active;
            
            //  Setup Attributes
            D_AttrScr = new Dictionary<string, int>();
            D_AttrMod = new Dictionary<string, int>();

            //  Setup Name
            Actor_Name = "";
            Actor_Article = "";

            //  Setup Proficiency
            Actor_Proficiency = 2;
        }

        //  Copy Constructor
        public GameActor_Admin(GameActor_Admin pAdmin) {
            //  Setup Active
            Actor_State = pAdmin.Actor_State;
            
            //  Setup Attributes
            D_AttrScr = new Dictionary<string, int>();
            foreach(var attr in pAdmin.D_AttrScr) {
                D_AttrScr.Add(attr.Key, attr.Value);
            }

            D_AttrMod = new Dictionary<string, int>();
            foreach(var mod in pAdmin.D_AttrMod) {
                D_AttrMod.Add(mod.Key, mod.Value);
            }

            //  Setup Name
            Actor_Name = "" + pAdmin.Actor_Name;
            Actor_Article = "" + pAdmin.Actor_Article;

            //  Setup Proficiency
            Actor_Proficiency = 0 + pAdmin.Actor_Proficiency;
        }

        //  SubMethod of Constructor - Setup Name
        /// <summary>
        /// Setup name for the actor
        /// </summary>
        /// <param name="pName">Name of the actor</param>
        /// <param name="pProper">If name is proper or generic</param>
        public void SetupName(string pName, bool pProper) {
            Actor_Name = "" + pName;
            Actor_NameProper = pProper;

            string charFirst = Actor_Name.Substring(0, 1).ToLower();
            Actor_Article = (charFirst == "a" || charFirst == "e" || charFirst == "i" || charFirst == "o" || charFirst == "u") ? "an" : "a";
        }

        //  SubMethod of Constructor - Setup Attributes
        /// <summary>
        /// Setup the attributes and modifiers for the actor
        /// </summary>
        /// <param name="pStr">Strength of the actor</param>
        /// <param name="pDex">Dexterity of the actor</param>
        /// <param name="pCon">Constitution of the actor</param>
        /// <param name="pInt">Intelligence of the actor</param>
        /// <param name="pWis">Wisdom of the actor</param>
        /// <param name="pCha">Charisma of the actor</param>
        public void SetupAttributes(int pStr, int pDex, int pCon, int pInt, int pWis, int pCha) {
            //  Set Strength and Modifier
            D_AttrScr.Add("STR", pStr);
            D_AttrMod.Add("STR", (pStr / 2) - 5);

            //  Set Dexterity and Modifier
            D_AttrScr.Add("DEX", pDex);
            D_AttrMod.Add("DEX", (pDex / 2) - 5);

            //  Set Constitution and Modifier
            D_AttrScr.Add("CON", pCon);
            D_AttrMod.Add("CON", (pCon / 2) - 5);

            //  Set Intelligence and Modifier
            D_AttrScr.Add("INT", pInt);
            D_AttrMod.Add("INT", (pInt / 2) - 5);

            //  Set Wisdom and Modifier
            D_AttrScr.Add("WIS", pWis);
            D_AttrMod.Add("WIS", (pWis / 2) - 5);

            //  Set Charisma and Modifier
            D_AttrScr.Add("CHA", pCha);
            D_AttrMod.Add("CHA", (pCha / 2) - 5);
        }
    }
}