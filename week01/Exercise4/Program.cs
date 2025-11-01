using System;

class Program
{
    static void Main(string[] args)
    {
       List<int> numbers = new List<int>();
        int input;
        Console.WriteLine("Enter numbers (0 to stop):");
        do
        {
            input = int.Parse(Console.ReadLine());
            if (input != 0)
            {
                numbers.Add(input);
            }
        } while (input != 0);

        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }
        Console.WriteLine($"The sum of the entered numbers is: {sum}"); 

        double average = (double)sum / numbers.Count;
        Console.WriteLine($"The average is: {average}");

        int max = numbers[0];
        foreach (int number in numbers)
        {
            if (number > max)
            {
                max = number;
            }
        }
        Console.WriteLine($"The maximum number is: {max}");

        
    
    }
}