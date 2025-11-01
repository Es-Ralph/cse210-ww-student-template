using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your name? ");
        string name = Console.ReadLine();

        Console.Write("What is your middle name? ");
        string middle_name = Console.ReadLine();


        Console.Write("What is your last name? ");
        string last_name = Console.ReadLine();

        Console.WriteLine($"Your ful name is {last_name}, {name} {middle_name} {last_name}.");
    }
}