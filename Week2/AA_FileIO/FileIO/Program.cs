﻿using System;
using System.IO;
using AA_FileIO.Repo;
using AA_FileIO.Models;

namespace FileIO {
    class Program {
        public static void Main(string[] args) {
            Console.WriteLine("Hello Again!");
            Console.WriteLine("");

            //IRepository file = new FileReadWrite();
            IRepository file = new Serialization();
            string path = @"./TestText.txt";

            //file.ReadAndWriteWithFile(path);
            //Console.WriteLine("");

            //file.StreamReaderReadLine(path);
            //Console.WriteLine("");

            //file.StreamReadToEnd(path);
            //Console.WriteLine("");

            path = @"./Ducks.txt";

            List<Duck> duckList = new List<Duck>();

            Duck myDuck = new Duck("red", 20);
            //file.SaveDuck(path, myDuck);

            duckList.Add(myDuck);
            duckList.Add(new Duck("green", 50));
            duckList.Add(new Duck("black", 120));

            file.SaveAllDucks(path, duckList);

            //Duck myDuck = new Duck("purple", 100);
            //myDuck.Quack();
            //Console.WriteLine(myDuck.ToString());

            //File.WriteAllText(path, myDuck.ToString());
            //string ducks = File.ReadAllText(path);
            //Console.WriteLine(ducks);

            //string[] duckVals = ducks.Split(' ');
            //Duck mySavedDuck = new Duck(duckVals[0], int.Parse(duckVals[1]));
            //mySavedDuck.Quack();

            //duckList = file.ReadDucksFromFile(path);
            duckList = file.LoadAllDucks(path);

            foreach(Duck d in duckList) {
                d.Quack();
            }
        }
    }
}