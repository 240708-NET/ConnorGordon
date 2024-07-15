using AA_FileIO.Models;
using System.Text.Json;

namespace AA_FileIO.Repo {
    public class FileReadWrite : IRepository {
        public void ReadAndWriteWithFile(string pPath) {
            string text = "some content for the file";

            // Creating the file and writing text to it
            if(File.Exists(pPath) == false) {
                File.WriteAllText(pPath, text);
                //File.WriteAllLines(path, string[]);
            }

            //  Reading all text from file at pPath
            else {
                Console.WriteLine("Reading from file with File.ReadAllText: ");

                text = File.ReadAllText(pPath);
                Console.WriteLine(text);
            }
        }

        public void StreamReaderReadLine(string pPath) {
            StreamReader sr = new StreamReader(pPath);
            string? text = "";

            Console.WriteLine("Reading from file with StreamReader (line by line): ");
            while((text = sr.ReadLine()) != null) {
                Console.WriteLine(text + " -- ");
            }

            sr.Close();
        }

        public void StreamReadToEnd(string pPath) {
            StreamReader sr = new StreamReader(pPath);
            string text = "";

            Console.WriteLine("Reading from file with StreamReader (EOF): ");
            while ((text = sr.ReadToEnd()) != "") {
                Console.WriteLine(text + " -- ");
            }

            sr.Close();
        }

        public List<Duck> ReadDucksFromFile(string pPath) {
            StreamReader sr = new StreamReader(pPath);
            string? text = "";

            List<Duck> duckList = new List<Duck>();
            while((text = sr.ReadLine()) != null) {
                string[] duckVals = text.Split(' ');
                duckList.Add(new Duck(duckList.Count+1, duckVals[0], int.Parse(duckVals[1])));
            }
            sr.Close();
            
            return duckList;
        }
    
        public void SaveDuck(string path, Duck pDuck) {
            
        }

        public void SaveAllDucks(string pPath, List<Duck> pDucks) {
            
        }

        public List<Duck> LoadAllDucks(string pPath){
            return new List<Duck>();
        }
    }
}