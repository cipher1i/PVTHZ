using System;

namespace Domain
{
    public class Player
    {
        private bool in_test = false;

        /// <summary>
        /// For testing methods/functions that require input.
        /// </summary>
        /// <remarks>
        /// Private key is to ensure only the testing team has permission to handle these types of functions.
        /// </remarks>
        /// <param name="private_key"></param>
        /// <returns><see cref="bool"/></returns>
        public bool SetToTesting(string private_key)
        {
            if (private_key == "1k9I28N!83(#827#(=2938(382-1fqj9KKD92938*(346^*73&639)19")
            {
                in_test = in_test == false ? true : false;
                return true;
            }

            return false;
        }

        private string[] zen_mode;
        private bool in_zen;

        private bool daemon_mode;
        private int health;
        private int strength;
        private int trust;

        public int Health 
        { 
            get { return health; }
            set
            {
                health = value;

                if (health < 0)
                    health = 0;
            }
        }
        public int Strength 
        {
            get { return strength; }
            set
            {
                strength = value;

                if (strength < 1)
                    strength = 1;
            }
        }
        public int Trust 
        { 
            get { return trust; }
            set
            {
                trust = value;

                if (trust < 0)
                    trust = 0;
            }
        }
        public bool Daemon_Mode { get { return daemon_mode; } }

        public Player()
        {
            health = 100;
            strength = 1;
            trust = 100;
            daemon_mode = false;
            zen_mode = new string[]{ "ZEN",
                                  "p: port",
                                  "g: guess",
                                  "s: switch",
                                  "d: die",
                                  "-------------" };
        }

        /// <summary>
        /// Player commits an action in the game.
        /// </summary>
        public bool Input(char test_value = '\0')
        {
            Meditate();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ m: meditate ]");
            Console.ResetColor();
            Console.Write("ACTION - ");
            char action = new char();

            if (in_test)
                action = test_value;
            else
                action = Console.ReadKey().KeyChar;

            switch (action)
            {
                case 'm':
                    in_zen = true;
                    return true;
                case 'p':
                    in_zen = false;
                    Port();
                    return true;
                case 'g':
                    in_zen = false;
                    Guess();
                    return true;
                case 's':
                    in_zen = false;
                    Switch();
                    return true;
                case 'd':
                    in_zen = false;
                    health = 0;
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Player enters meditative state and enters Thoth, a help wizard.
        /// </summary>
        private void Meditate()
        {
            if (in_zen)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                foreach (string s in zen_mode)
                    Console.WriteLine(s);

                Console.ResetColor();
            }
        }

        /// <summary>
        /// Player ports to a random location in the dimension.
        /// </summary>
        public int Port()
        {
            Random random = new Random();
            int location = random.Next(0, Game.Dimension);
            Game.Place(false, location);

            Console.WriteLine();
            return location;
        }

        /// <summary>
        /// Player guesses exit number
        /// </summary>
        public int Guess(string test_value = "")
        {
            Console.WriteLine();
            Console.Write("GUESS - ");
            int guess;

            if(in_test)
            {
                if (!Int32.TryParse(test_value, out guess))
                    guess = Game.Player_Region;
            }
            else
            {
                if (!Int32.TryParse(Console.ReadLine(), out guess))
                    guess = Game.Player_Region;
            }
            
            Game.Place(false, guess);
            return guess;
        }

        /// <summary>
        /// Player switches sides into and out of daemon mode. Trust decreases every time.
        /// </summary>
        public bool Switch()
        {
            daemon_mode = daemon_mode == false ? true : false;
            trust = trust != 0 ? trust - 1 : 0;

            return daemon_mode;
        }
    }
}
