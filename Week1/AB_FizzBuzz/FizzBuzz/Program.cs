using System;
using System.Collections;

/* Application should run "FizzBuzz"
User should be able to play "FizzBuzz" from numStart to numEnd
Counting from lower to upper numbers, it should print the result on it's own line
When the number is a multiple of a key in d_ModResults, it should add the correlating value to the result*/

class Program {
    static Dictionary<int, string> d_ModResults = new Dictionary<int, string>() {
        [2] = "Fizz",
        [3] = "Bug",
        [5] = "Buzz",
        [7] = "Bang",
        [9] = "Crack"
    };

    public static void Main(string[] args) {
        int numStart = 1;
        int numEnd = 630;
        int numPlace = (int)Math.Floor(Math.Log10(numEnd - numStart) + 1);

        for (int i = numStart; i <= numEnd; i++) {
            Console.WriteLine(FizzBuzzBuilder(i, numPlace));
        }
    }

    public static string FizzBuzzBuilder(int pNum, int pPlace) {
        string result = "";
        string resultNum = pNum.ToString("D" + ((pNum < 0) ? pPlace - 1 : pPlace)) + " ";
        string resultEnd = "";

        foreach(var mod in d_ModResults) {
            result += ((pNum % mod.Key == 0) ? mod.Value : "");
            resultEnd += ((pNum % mod.Key == 0) ? ((resultEnd.Length == 0) ? " (" : ", ") + mod.Key : "");
        }

        resultEnd += ((result != "") ? ")" : "");

        //Reverse commented return to switch return modes
        return resultNum + result + resultEnd;
        //return (result != "") ? result + resultEnd : resultNum;
    }
}