namespace Project1.Models.Actor {
    public enum E_ActorState {
        Null,
        Active,
        Unconscious,
        Dead
    }

    public class GameActor {
        //  Manager Variables
        public GameActor_Admin Actor_Admin { get; private set; }
        public GameActor_Combat Actor_Combat { get; private set; }

        //  Constructor
        /// <summary>
        /// Basic Actor Structure
        /// </summary>
        /// <param name="pUnarmed">Unarmed/Base attack of the actor</param>
        public GameActor(GameAttack pUnarmed) {
            //  Setup Manager
            Actor_Admin = new GameActor_Admin();
            Actor_Combat = new GameActor_Combat(this, pUnarmed);
        }

        //  Copy Constructor
        public GameActor(GameActor pActor) {
            //  Setup Manager
            Actor_Admin = new GameActor_Admin(pActor.Actor_Admin);
            Actor_Combat = new GameActor_Combat(pActor.Actor_Combat);
        }
    }
}