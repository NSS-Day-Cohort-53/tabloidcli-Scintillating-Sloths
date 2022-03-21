using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class ColorManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;

        public ColorManager(IUserInterfaceManager parentUI)
        {
            _parentUI = parentUI;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Color Menu");
            Console.WriteLine("1. Change menu color");
            Console.WriteLine("2. Change text color");
 
            Console.WriteLine("0. Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    BackgroundColorSet();
                    return this;
                case "2":
                    TextColorSet();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        public void BackgroundColorSet()
        {
            Console.WriteLine("1. Red");
            Console.WriteLine("2. Blue");
            Console.WriteLine("3. Dark Magenta");
            Console.WriteLine("4. Dark Yellow");
            Console.WriteLine("Choose a color");
            Console.Write("> ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case "2":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case "3":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    break;
                case "4":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }
        }
        public void TextColorSet()
        {
            Console.WriteLine("1. Cyan");
            Console.WriteLine("2. Dark Red");
            Console.WriteLine("3. Dark Magenta");
            Console.WriteLine("4. Green");
            Console.WriteLine("Choose a color");
            Console.Write("> ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "2":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "3":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case "4":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }
        }
    }
}