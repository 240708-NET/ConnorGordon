using System.IO;
using Project1.Models.Actor;

namespace Project1.Main {
    public class ManagerActor_CC {
        //  ~Reference Variables
        public ManagerActor RefMActor { get; private set; }
        private ManagerGame refMGame => RefMActor.RefMGame;
        private Random refRand => refMGame.Rand;

        //  Player Variables
        private GameActor player;

        //  Constructor
        public ManagerActor_CC(ManagerActor pRef) {
            //  Setup ~Reference
            RefMActor = pRef;

            // Setup Player
            player = new GameActor();
            //player.SetupUnarmed("fists", "punches with their", "Melee", 0, "0_1_bludgeoning");
        }
        /*

        //  MainMethod - Character Creation
        public GameActor CharacterCreation() {
            player.SetupName(CC_Name(), true);
            //player.Actor_Admin.SetupAttributes(CC_Attribute());
            player.SetupClass(CC_Class());

            return player;
        }

        //  SubMethod of CharacterCreation - Character Creation Name
        private string CC_Name() {
            string name = "";

            bool nameValid = false;
            while(nameValid == false) {
                refMGame.WriteText("Please enter your name: ", 25);
                name = Console.ReadLine() ?? "";

                int actionCount = 0;
                while(actionCount < 5) {
                    refMGame.WriteLine($"{name} is your name? (Y/N)", 25);
                    if ((Console.ReadLine() ?? "").ToLower() == "y") {
                        Console.WriteLine("");
                        nameValid = true;
                        actionCount = 5;
                    }
                    
                    actionCount++;
                }
            }

            return name;
        }

        //  SubMethod of CharacterCreation - Character Creation Attribute
        private List<int> CC_Attribute() {
            refMGame.WriteText("Rolling Attributes", 25);
            refMGame.WriteLine("...", 400);
            Thread.Sleep(500);

            //  Roll attributes
            List<int> attributePool = CCAttr_Roll();
            List<int> attributeNum = CCAttr_Assign(attributePool);

            return attributeNum;
        }

        private List<int> CCAttr_Roll() {
            int actionCount = 0;

            //  Roll method
            string rollMethod = "";
            bool methodValid = false;
            while (methodValid == false) {
                refMGame.WriteLine("Please choose a rolling method: ", 25);
                refMGame.WriteLine("(1) 4d6 drop lowest", 25);
                refMGame.WriteLine("(2) 3d6", 25);
                refMGame.WriteLine("(3) 2d6+6", 25);

                actionCount = 0;
                while (actionCount < 5) {
                    rollMethod = Console.ReadLine() ?? "";

                    switch(rollMethod) {
                    case "1":
                        rollMethod = "4d6d1";
                        refMGame.WriteLine("Rolling using 4d6 drop lowest", 25);
                        Console.WriteLine("");

                        methodValid = true;
                        actionCount = 5;
                        break;
                    case "2":
                        rollMethod = "3d6";
                        refMGame.WriteLine("Rolling using 3d6", 25);
                        Console.WriteLine("");

                        methodValid = true;
                        actionCount = 5;
                        break;
                    case "3":
                        rollMethod = "2d6+6";
                        refMGame.WriteLine("Rolling using 2d6+6", 25);
                        Console.WriteLine("");

                        methodValid = true;
                        actionCount = 5;
                        break;
                    }
                }
            }

            //  Rolling attribute pool
            List<int> attributePool = new List<int>();
            List<int> rolls = new List<int>();
            int total = 0;

            for(int i = 0; i < 6; i++) {
                refMGame.WriteText($"Rolling {(i+1)}: ", 75);
                rolls = new List<int>();

                for(int x = 0; x < 4; x++) {
                    int roll = refRand.Next(0, 6)+1;
                    rolls.Add(roll);

                    refMGame.WriteText((roll + ((x < 3) ? ", " : "")), 75);
                }
                Console.WriteLine("");

                rolls.Sort();
                total = (rolls[1] + rolls[2] + rolls[3]);
                attributePool.Add(total);

                refMGame.WriteText($"Dropping {rolls[0]}", 75);
                refMGame.WriteLine($", Total {total}{CCAttr_DisplayMod(total)}", 75);

                Console.WriteLine("");
                Thread.Sleep(1000);
            }

            attributePool.Sort();
            attributePool.Reverse();

            return attributePool;
        }

        private void CCAttr_DisplayPool(List<int> pAttr) {
            refMGame.WriteText("Attribute Pool: ", 25);
            for(int i = 0; i < pAttr.Count; i++) {
                refMGame.WriteText((pAttr[i] + CCAttr_DisplayMod(pAttr[i]) + ((i < pAttr.Count-1) ? ", " : "")), 25);
            }
            Console.WriteLine("");
        }

        private string CCAttr_DisplayMod(int pAttr) {
            int mod = (pAttr / 2) - 5;
            return $"({((mod > 0) ? "+" : "")}{mod})";
        }

        private List<int> CCAttr_Assign(List<int> pPool) {
            List<int> attributes = new List<int>();
            int actionCount = 0;
            int attrNum = -1;

            while(pPool.Count > 0) {
                CCAttr_DisplayPool(pPool);
                actionCount = 0;

                while(actionCount < 5) {
                    attrNum = -1;
                    switch(pPool.Count) {
                        //  Assign strength
                        case 6:
                            refMGame.WriteText("Enter strength score from pool: ", 25);
                            break;
                            //  Assign strength
                        case 5:
                            refMGame.WriteText("Enter dexterity score from pool: ", 25);
                            break;
                            //  Assign strength
                        case 4:
                            refMGame.WriteText("Enter constitution score from pool: ", 25);
                            break;
                            //  Assign strength
                        case 3:
                            refMGame.WriteText("Enter intelligence score from pool: ", 25);
                            break;
                            //  Assign strength
                        case 2:
                            refMGame.WriteText("Enter wisdom score from pool: ", 25);
                            break;
                            //  Assign strength
                        case 1:
                            refMGame.WriteText($"Assigning last value of {pPool[0]} to charisma", 25);
                            attributes.Add(pPool[0]);
                            pPool.Clear();
                            actionCount = 6;
                            Console.WriteLine("");
                            break;
                    }

                    if (pPool.Count > 0 && int.TryParse(Console.ReadLine(), out attrNum) == true && pPool.Contains(attrNum)) {
                        attributes.Add(attrNum);
                        pPool.Remove(attrNum);
                        actionCount = 6;
                    }
                    else if (pPool.Count > 0) {
                        actionCount++;
                    }

                    Console.WriteLine("");
                }
            }

            //  Final check for attributes
            refMGame.WriteLine("Attributes are:", 25);
            refMGame.WriteLine($" - Strength     : {((attributes[0] < 10 && attributes[0] > 0) ? " " : "") + attributes[0]} {CCAttr_DisplayMod(attributes[0])}", 25);
            refMGame.WriteLine($" - Dexterity    : {((attributes[1] < 10 && attributes[0] > 0) ? " " : "") + attributes[1]} {CCAttr_DisplayMod(attributes[1])}", 25);
            refMGame.WriteLine($" - Constitution : {((attributes[2] < 10 && attributes[0] > 0) ? " " : "") + attributes[2]} {CCAttr_DisplayMod(attributes[2])}", 25);
            refMGame.WriteLine($" - Intelligence : {((attributes[3] < 10 && attributes[0] > 0) ? " " : "") + attributes[3]} {CCAttr_DisplayMod(attributes[3])}", 25);
            refMGame.WriteLine($" - Wisdom       : {((attributes[4] < 10 && attributes[0] > 0) ? " " : "") + attributes[4]} {CCAttr_DisplayMod(attributes[4])}", 25);
            refMGame.WriteLine($" - Charisma     : {((attributes[5] < 10 && attributes[0] > 0) ? " " : "") + attributes[5]} {CCAttr_DisplayMod(attributes[5])}", 25);
            
            actionCount = 0;
            string action = "";

            while(actionCount >= 0) {
                refMGame.WriteLine("Do you wish to continue? (Y/N)", 25);
                action = (Console.ReadLine() ?? "").ToLower();
                Console.WriteLine("");

                if (action == "n") {
                    return CCAttr_Assign(attributes);
                }
                else if (action == "y") {
                    actionCount = -1;
                }
            }

            return attributes;
        }
    
        //  SubMethod of CharacterCreation - Character Creation Class
        private string CC_Class() {
            string classType = "";

            bool nameValid = false;
            while(nameValid == false) {
                refMGame.WriteLine("Available classes: ", 25);
                refMGame.WriteLine("(1) Fighter", 25);
                Console.WriteLine("");

                refMGame.WriteText("Select enter your class: ", 25);
                classType = Console.ReadLine() ?? "";

                switch(classType) {
                    case "1":
                        classType = "Fighter";
                        break;
                }

                int actionCount = 0;
                while(actionCount < 5) {
                    refMGame.WriteLine($"You are a {classType}? (Y/N)", 25);
                    if ((Console.ReadLine() ?? "").ToLower() == "y") {
                        nameValid = true;
                        actionCount = 5;
                    }
                    
                    actionCount++;
                }
            }

            return classType;
        }

        */
    }
}