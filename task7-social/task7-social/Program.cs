using System;

namespace task7_social {
    class Program {
        private static string _serviceToken;
        private static string _accessToken;

        static void Main(string[] args) {
            var vk = new VkApiWrapper();

            AskCredentials();

            PrintHelp();

            while (true) {
                var line = Console.ReadLine();
                line = line?.Trim();
                switch (line) {
                    case "": {
                        break;
                    }
                    case "/f": {
                        vk.Authorize(_serviceToken);
                        try {
                            var friendList = vk.GetFriendList();
                            Console.WriteLine("dem1tris' friend list:");
                            foreach (var friend in friendList) {
                                Console.WriteLine(friend);
                            }
                        }
                        catch (VkNet.Exception.UserAuthorizationFailException) {
                            Console.WriteLine("Bad credentials (service token). Try again");
                            AskCredentials();
                            PrintHelp();
                        }

                        Console.WriteLine("");
                        break;
                    }
                    case "/h": {
                        PrintHelp();
                        break;
                    }
                    case "/q": {
                        return;
                    }
                    default: {
                        try {
                            vk.Authorize(_accessToken);
                            vk.SendMessage(line);
                        }
                        catch (VkNet.Exception.UserAuthorizationFailException) {
                            Console.WriteLine("Bad credentials (access token). Try again");
                            AskCredentials();
                            PrintHelp();
                        }

                        break;
                    }
                }
            }
        }

        private static void AskCredentials() {
            var repeat = false;
            do {
                try {
                    Console.WriteLine("VK service token:");
                    _serviceToken = ReadPassword();
                    Console.WriteLine("VK access token:");
                    _accessToken = ReadPassword();
                    repeat = false;
                }
                catch (ArgumentException) {
                    Console.WriteLine("Id should be integer");
                    repeat = true;
                }
            } while (repeat);
        }

        private static string ReadPassword() {
            var pass = "";
            do {
                var key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter) {
                    pass += key.KeyChar;
                } else {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0) {
                        pass = pass.Substring(0, (pass.Length - 1));
                    } else if (key.Key == ConsoleKey.Enter) {
                        break;
                    }
                }
            } while (true);

            return pass;
        }

        private static void PrintHelp() {
            Console.WriteLine("Available commands:");
            Console.WriteLine("/f - Print list of dem1tris' friends");
            Console.WriteLine("/q - Exit");
            Console.WriteLine("/h - Show this text again");
            Console.WriteLine("<some text> - Send message to dem1tris");
        }
    }
}