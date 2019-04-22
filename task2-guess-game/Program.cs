using System;
using System.Collections.Generic;

namespace task2_guess_game {
    internal class Game {
        private static readonly Random Rand = new Random();

        private readonly string _name;

        private readonly string[] _phrases = new[] {
            "Go on, ", "A journey of a thousand miles begins with a single step, ",
            "Step by step you will reach your goal, ", "Next time you will surely guess, "
        };


        public Game(string name) {
            this._name = name;
        }

        public void Start() {
            var answer = Rand.Next(51);
            var log = new List<string>();
            var attempts = 0;

            Console.WriteLine(
                "Hi, " + _name + "! I want to play a game with you. Try to guess the number from 0 to 50?");
            var startTime = DateTime.Now;

            var guess = 0;
            string str;
            do {
                attempts++;
                str = Console.ReadLine();
                if ("q".Equals(str)) {
                    Console.WriteLine("Good bye");
                    break;
                }

                try {
                    guess = int.Parse(str);
                }
                catch (FormatException) {
                    Console.WriteLine("This is not number");
                    continue;
                }

                if (answer > guess) {
                    Console.WriteLine("Too few");
                    log.Add($"{guess,-2}" + " <");
                } else if (answer < guess) {
                    Console.WriteLine("Too big");
                    log.Add($"{guess,-2}" + " >");
                }

                if (attempts % 4 == 0) {
                    int nextPhrase = Rand.Next(_phrases.Length);
                    Console.WriteLine(_phrases[nextPhrase] + _name);
                }
            } while (answer != guess);

            var interval = DateTime.Now - startTime;
            log.Add(str + " =");

            Console.WriteLine("Good job!");
            Console.WriteLine("It takes " + attempts + " attempts and " + interval.ToString("g"));
            foreach (String entry in log) {
                Console.WriteLine(entry);
            }
        }
    }

    internal class Program {
        public static void Main(string[] args) {
            Console.WriteLine("Please, introduce yourself:");
            var name = Console.ReadLine();

            new Game(name).Start();

            Console.ReadKey();
        }
    }
}