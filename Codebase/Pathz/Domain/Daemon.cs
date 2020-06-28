using System;

namespace Domain
{
    public class Daemon
    {
        private string[] names;

        private int health;
        private int strength;
        private int wisdom;
        private string name;

        public int Health { 
            get { return health; } 
            set
            {
                health = value;

                if (health < 0)
                    health = 0;
            }
        }
        internal int Strength { get { return strength; } }
        internal int Wisdom { get { return wisdom; } }
        internal string Name { get { return name; } }

        public Daemon()
        {
            /* hard coded for now */
            names = new string[] { "Ghoul", "Troll", "Ghost" };
            Random random = new Random();
            name = names[random.Next(3)];
            switch (name)
            {
                case "Ghoul":
                    health = 100;
                    strength = 15;
                    wisdom = 10;
                    break;
                case "Troll":
                    health = 100;
                    strength = 10;
                    wisdom = 5;
                    break;
                case "Ghost":
                    health = 100;
                    strength = 5;
                    wisdom = 15;
                    break;
            }
        }

        /* this will make it too difficult for players with the current set of operations available to them. */
        /* try to add more resources for players before messing with this. Limit the resources according to the figures below. */
        /* The following are the figures I'd like to see for novice to skilled users... */
        /* 10% success to 90% failure */
        /* 50% success to 50% */
        /* 60-70% success to 30-40% failure */
        /* 90-99% success to 1-10% failure */
        /*
        private void AutomateDaemonInit()
        {
            //Randomized health - assign 100+
            //Randomized strength - assign 1+
            //Randomized wisdom - assign  1+
            //1. Generate Names dictionary
            //2. Generate Linguistics engine for more suitable name generation. (APIs are against standards for this project. Everything from scratch.)
            return;
        }
        */
    }
}
