class Game {
    private Random rand;

    private int numGuess;
    private int numTarget;
    private int roundCurr;
    
    public string StrGuess { get; set; } = "";
    
    //  Constructor
    public Game() {
        rand = new Random();
        numTarget = rand.Next(11);
        roundCurr = 0;
    }

    //  MainMethod - Play Game
    public void PlayGame() {
        //  Part - Start Text
        Console.WriteLine("Running Higher or Lower...");
        Console.WriteLine("");

        // Part - Round Text (Get User Input)
        do {
            roundCurr++;
            Console.Write("> Round {0}: Please enter a guess between -1 and 11: ", roundCurr);
            StrGuess = Console.ReadLine();
            numGuess = Int32.Parse(StrGuess);

            Console.WriteLine((numGuess > numTarget) ? "Oops, too high!" : ((numGuess < numTarget) ? "Oops, too low!" : "Hey, nice job!"));
        } while (numGuess != numTarget);

        Console.WriteLine("Thanks for playing! You took {0} rounds!", roundCurr);
    }
}