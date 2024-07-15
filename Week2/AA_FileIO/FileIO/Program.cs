﻿using System;
using System.IO;

namespace FileIO {
    class Program {
        public static void Main(string[] args) {
            Console.WriteLine("Hello Again!");
            Console.WriteLine("");

            string path = @"./TestText.txt";
            ReadAndWriteWithFile(path);
            StreamReaderReadLine(path);
            StreamReadToEnd(path);

            //Duck myDuck = new Duck("purple", 100);
            //myDuck.Quack();
            //Console.WriteLine(myDuck.ToString());

            path = @"./Ducks.txt";
            //File.WriteAllText(path, myDuck.ToString());
            //string ducks = File.ReadAllText(path);
            //Console.WriteLine(ducks);

            //string[] duckVals = ducks.Split(' ');
            //Duck mySavedDuck = new Duck(duckVals[0], int.Parse(duckVals[1]));
            //mySavedDuck.Quack();

            List<Duck> duckList = ReadDucksFromFile(path);

            foreach(Duck d in duckList) {
                d.Quack();
            }
        }

        public static void ReadAndWriteWithFile(string pPath) {
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
                Console.WriteLine("");
            }
        }

        public static void StreamReaderReadLine(string pPath) {
            StreamReader sr = new StreamReader(pPath);
            string? text = "";

            Console.WriteLine("Reading from file with StreamReader (line by line): ");
            while((text = sr.ReadLine()) != null) {
                Console.WriteLine(text + " -- ");
            }

            Console.WriteLine();
            sr.Close();
        }

        public static void StreamReadToEnd(string pPath) {
            StreamReader sr = new StreamReader(pPath);
            string text = "";

            Console.WriteLine("Reading from file with StreamReader (EOF): ");
            while ((text = sr.ReadToEnd()) != "") {
                Console.WriteLine(text + " -- ");
            }

            Console.WriteLine();
            sr.Close();
        }

        public static List<Duck> ReadDucksFromFile(string pPath) {
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
    }
}