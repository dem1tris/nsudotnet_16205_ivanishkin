using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace task3_linecounter {
    class Program {
        static void Main(string[] args) {
            if (args.Length != 1) {
                Console.WriteLine("Search pattern should be specified as first parameter");
                Console.ReadKey();
                Environment.Exit(-1);
            }

            // how to combine it beautifully?
            IEnumerable<string> dirs = new string[] {Directory.GetCurrentDirectory()};
            dirs = dirs.Union(Directory.GetDirectories(dirs.ElementAt(0)));
            var inComment = false;
            var cnt = 0;
            foreach (var dir in dirs) {
                foreach (var path in Directory.GetFiles(dir, args[0])) {
                    foreach (var line in File.ReadLines(path)) {
                        var trimmed = line.Trim();
                        if (!inComment && trimmed.StartsWith("/*")) {
                            inComment = true;
                        } else if (inComment && trimmed.EndsWith("*/")) {
                            inComment = false;
                        } else if (!inComment && !trimmed.StartsWith("//") && trimmed.Length != 0) {
                            cnt++;
                        }
                    }
                }
            }

            Console.WriteLine($"{cnt} code lines");
            Console.ReadKey();
        }
    }
}