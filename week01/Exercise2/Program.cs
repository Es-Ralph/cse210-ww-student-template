using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        Console.WriteLine($"Hello, {name}!");

        Console.Write("Enter your score (0-100): ");
        string score = Console.ReadLine();
        int number = int.Parse(score);

        if (number >= 90)
        {
            Console.WriteLine("Grade: A");
        }
        else if (number >= 80)
        {
            Console.WriteLine("Grade: B");
        }
        else if (number >= 70)
        {
            Console.WriteLine("Grade: C");
        }
        else if (number<= 60)
        {
            Console.WriteLine("Grade: F");
        }
        else
        {
            Console.WriteLine("Invalid score");
        }

        if (number>= 90 || number >= 80)
        {
            Console.WriteLine($"Excellent work!{name}.");
        }

        else if (number >= 70 )
        {
            Console.WriteLine($"Needs improvement.{name}.");
        }
        
        else if (number <= 60)
        {
            Console.WriteLine($"You are failing so you need to sit up.{name}.");
        }
    }
}