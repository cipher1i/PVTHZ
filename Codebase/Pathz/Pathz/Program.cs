using System;
using Domain;
using BigText;

namespace Pathz
{
    /* Implementations will always be 1 full version behind actual gameplay starting Alpha. */
    /* This codebase was built for testability and is thereby less efficient than the original */
    public class Program
    {
        private static void Start()
        {
            Console.CursorVisible = false;
            Prompt prompt = new Prompt(null, "Press enter to start", ConsoleColor.Black, ConsoleColor.White);
            prompt.SetPosition(1, 0, 0, 0);
            prompt.Runner();
            Console.Clear();
        }
        private static void Main()
        {
            Start();
            Game.InitializationENG();
            Game.GameplayENG();
        }
    }
}
