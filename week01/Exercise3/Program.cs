using System;
using System.Formats.Asn1;
using System.Net;

class Program
{
    static void Main(string[] args)
    {
        int magic_number = 17;
        string response;
        do
        {
            Console.Write("What is your guess? ");
            string guess = Console.ReadLine();
            int number = int.Parse(guess);

            if (number < magic_number)
            {
                Console.WriteLine("Too low!");
            }
            else if (number > magic_number)
            {
                Console.WriteLine("Too high!");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }

            Console.Write("Do you want to continue?");
            response = Console.ReadLine();
        } while (response == "yes" || response == "y" || response == "Y" || response == "YES");
        






        }
}

