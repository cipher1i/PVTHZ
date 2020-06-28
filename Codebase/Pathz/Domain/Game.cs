using System;
using BigText;
using System.Collections.Generic;
using System.Threading;

namespace Domain
{
    public static class Game
    {
        private static bool in_test = false;

        /// <summary>
        /// For testing methods/functions that require input.
        /// </summary>
        /// <remarks>
        /// Private key is to ensure only the testing team has permission to handle these types of functions.
        /// </remarks>
        /// <param name="private_key"></param>
        /// <returns><see cref="bool"/></returns>
        public static bool SetToTesting(string private_key)
        {
            if (private_key == "aofofjw98j(*FJ(*JF(WJF(*j8fifodknfoqhf98h298hff8298h98udfh98298hf9(*H(@*HF898f47")
            {
                in_test = in_test == false ? true : false;
                return true;
            }

            return false;
        }

        private static Text text;
        private static Prompt prompt;
        private static int level;
        private static int exit;
        private static Dictionary<int, Daemon> daemons;
        private static List<int> keys;

        private static int dimension = 1;
        private static int player_region;

        public static int Dimension 
        { 
            get { return dimension; } 
            set 
            {
                dimension = value;

                if (dimension < 1)
                    dimension = 10;
            } 
        }
        internal static int Player_Region { get { return player_region; } }

        /// <summary>
        /// Sets number of levels and the exit point.
        /// </summary>
        public static void Set(bool newgame)
        {
            if (newgame)
            {
                level = 0;
                dimension = 10;
            }

            player_region = -1;
            daemons = new Dictionary<int, Daemon>();
            keys = new List<int>();
            for (int l = 0; l < level + 1; l++)
            {
                daemons.Add(11 + l, new Daemon());
                keys.Add(11 + l);
            }

            Random random = new Random();
            exit = random.Next(dimension);
        }

        /// <summary>
        /// Sets location of either player or daemons. The cpu and daemons are synonymous.
        /// </summary>
        /// <param name="cpu"></param>
        /// <param name="location"></param>
        public static int Place(bool cpu, int location = 11)
        {
            /* mainly for testing purposes (intended to be a private method) */
            if(location < 0 || location > (dimension-1))
            {
                Random random = new Random();
                location = random.Next(dimension);
            }

            switch (cpu)
            {
                case true:
                    Random random = new Random();
                    int count = daemons.Count;
                    for (int d = 0; d < count; d++)
                    {
                        location = random.Next(dimension);

                        /* remove the last condition when the run feature becomes available for the player */
                        while (daemons.ContainsKey(location) || location == exit)
                            location = random.Next(dimension);

                        var value = daemons[keys[d]];
                        daemons.Remove(keys[d]);
                        keys[d] = location;
                        daemons[keys[d]] = value;
                    }
                    break;
                case false:
                    player_region = location;
                    break;
            }

            return location;
        }

        /// <summary>
        /// Invokes battle protocol.
        /// </summary>
        private static void Battle(Player player, Daemon daemon)
        {

            daemon = daemons[player_region];

            ApplyHitDamage(player, daemon);

            if (daemon.Health < 1)
            {
                Console.WriteLine("You killed the " + daemon.Name);
                ApplyCredits(player, daemon);
                daemons.Remove(player_region);
            }
            else
                ShowDaemonStats(daemon);

            Console.WriteLine("\n\n");
        }

        /// <summary>
        /// Shows player stats.
        /// </summary>
        /// <param name="player"></param>
        public static string ShowPlayerStats(Player player)
        {
            Console.WriteLine("@" + player_region);
            Console.WriteLine("❤️ " + player.Health);
            Console.WriteLine("☯   " + player.Trust);
            Console.WriteLine("☀   " + player.Strength);
            string output = "@" + player_region + "\n❤️ " + player.Health + "\n☯   " + player.Trust + "\n☀   " + player.Strength + "\n";
            if (player.Daemon_Mode)
            {
                Console.WriteLine("☣");
                output += "☣\n";
            }

            Console.WriteLine("\n\n");
            output += "\n\n";

            return output;
        }

        /// <summary>
        /// Shows daemon stats.
        /// </summary>
        /// <param name="daemon"></param>
        private static void ShowDaemonStats(Daemon daemon)
        {
            Console.WriteLine("You encountered a " + daemon.Name);
            Console.WriteLine("❤️ " + daemon.Health);
            Console.WriteLine("智  " + daemon.Wisdom);
            Console.WriteLine("☀   " + daemon.Strength);
        }

        /// <summary>
        /// Credits player based on gains from defeated daemon.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="daemon"></param>
        public static void ApplyCredits(Player player, Daemon daemon)
        {
            player.Health += daemon.Strength + daemon.Wisdom;
            player.Strength += daemon.Strength;
            player.Trust += daemon.Wisdom;
        }

        /// <summary>
        /// Applies hit damage to player and daemon based on stats.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="daemon"></param>
        public static void ApplyHitDamage(Player player, Daemon daemon)
        {
            /* remove when the hit feature becomes available (user chooses to hit or evade) */
            daemon.Health -= player.Strength;

            switch (player.Daemon_Mode)
            {
                case true:
                    if (player.Trust < daemon.Wisdom)
                        player.Health -= daemon.Strength;

                    player.Trust -= daemon.Wisdom;
                    break;
                case false:
                    player.Health -= daemon.Strength;
                    player.Strength += 2;
                    break;
            }
        }

        /// <summary>
        /// Narrates script to console
        /// </summary>
        /// <param name="script"></param>
        public static string Narrator(string script)
        {
            string output = "";
            foreach (char c in script)
            {
                if(!in_test)
                    Console.Write(c);

                output += c;
                Thread.Sleep(50);
            }

            if (!in_test)
            {
                Console.WriteLine("\n");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Proceed");
                Console.ResetColor();
                Console.ReadKey(true);
                Console.Clear();
            }

            return output;
        }

        /// <summary>
        /// This module provides an introduction to the Pathz game application.
        /// </summary>
        public static void InitializationENG()
        {
            string[] title = new string[] {  "        ***       ^                         ",
                                            "OoOOO    o       |x|   Oo    OO OOOOOOOOOOOO",
                                            "Oo oO  O O O     |x|   Oo    OO        oO0  ",
                                            "OoOOO O X X O  __|x|__ OOOOOOOO      oo0    ",
                                            "Oo    o+++++o |__VVV__|Oo    oO  ooo0       ",
                                            "O     O     O    |_|   Oo    OOOOOOOOOOOOOOO"};


            text = new Text(title);
            text.Animate('*', '+', 'X');

            prompt = new Prompt("死へようこそ", "Press [enter] to start", ConsoleColor.Red, ConsoleColor.Black);
            prompt.SetPosition(0, 1, 13, -5);
            prompt.SetPosition(1, 1, 15, 0);
            prompt.Runner();
            Console.Clear();
            Console.ResetColor();
        }

        /// <summary>
        /// This module invokes gameplay operations in the Pathz game application.
        /// </summary>
        public static void GameplayENG()
        {
            Player player = null;
            Daemon daemon = null;
            string script = "You're in complete darkness and you have amnesia. Find your way out.";
            bool newgame = true;

            while (true)
            {
                player = new Player();
                Game.Narrator(script);
                Game.Set(newgame);

                while (player_region != exit && player.Health > 0)
                {
                    Console.Clear();
                    Game.Place(true);

                    Game.ShowPlayerStats(player);

                    if (daemons.ContainsKey(player_region))
                        Battle(player, daemon);

                    player.Input();
                }

                Console.Clear();
                Game.ShowPlayerStats(player);

                if (player.Health < 1)
                {
                    string[] gameover = new string[]
                    {
                        "███████████████████████████",
                        "███████▀▀▀░░░░░░░▀▀▀███████",
                        "████▀░░░░░░░░░░░░░░░░░▀████",
                        "███│░░░░░░░░░░░░░░░░░░░│███",
                        "██▌│░░░░░░░░░░░░░░░░░░░│▐██",
                        "██░└┐░░░░░░░░░░░░░░░░░┌┘░██",
                        "██░░└┐░░░░░░░░░░░░░░░┌┘░░██",
                        "██░░┌┘▄▄▄▄▄░░░░░▄▄▄▄▄└┐░░██",
                        "██▌░│██████▌░░░▐██████│░▐██",
                        "███░│▐███▀▀░░▄░░▀▀███▌│░███",
                        "██▀─┘░░░░░░░▐█▌░░░░░░░└─▀██",
                        "██▄░░░▄▄▄▓░░▀█▀░░▓▄▄▄░░░▄██",
                        "████▄─┘██▌░░░░░░░▐██└─▄████",
                        "█████░░▐█─┬┬┬┬┬┬┬─█▌░░█████",
                        "████▌░░░▀┬┼┼┼┼┼┼┼┬▀░░░▐████",
                        "█████▄░░░└┴┴┴┴┴┴┴┘░░░▄█████",
                        "███████▄░░░░░░░░░░░▄███████",
                        "██████████▄▄▄▄▄▄▄██████████"
                    };
                    Console.WriteLine("\n\n");
                    text = new Text(gameover, ConsoleColor.Red, ConsoleColor.DarkRed);
                    text.Animate();
                    Console.ResetColor();
                    break;
                }
                else
                {
                    Console.WriteLine("Level " + level + " complete");
                    script = "You fell into a new dimension. Find your way out.";
                    newgame = false;
                    level++;
                    dimension += 10;
                }
            }
        }
    }
}
