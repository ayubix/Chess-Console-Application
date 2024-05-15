using System;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Tasks;

namespace ChessApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my Chess Console Game\n");
          
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Chess Game Menu:");
                Console.WriteLine("1. Start a new game");
                Console.WriteLine("2. View game instructions");
                Console.WriteLine("3. Exit\n");
                Console.WriteLine("Enter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.\n");
                    continue;
                }


                switch (choice)
                {
                    case 1:

                        Game g = new Game();
                        g.gamePlay();
                        break;

                    case 2:
                        Console.WriteLine("");
                        Console.WriteLine("Hey new chess player in this game moves are written as the starting square of the piece,");
                        Console.WriteLine("followed directly by the destination square and no spaces.");
                        Console.WriteLine("For example: To move the piece from square e2 to e4, you would enter: e2e4");
                        Console.WriteLine("To move the piece from square a7 to a5, you would enter: a7a5 and so on... enjoy");
                        break;

                    case 3:
                        Console.WriteLine("");
                        Console.WriteLine("Thank you for playing! I hope you enjoyed the game. Goodbye!");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number from 1 to 3.\n");
                        break;
                }
                Console.WriteLine();
            }
              
        }
    }
}


