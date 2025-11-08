using System;
using System.Collections.Generic;
using System.IO;

public class Entry
{
    public DateTime Date { get; set; }
    public string Prompt { get; private set; }
    public string Response { get; private set; }

    public Entry(string prompt, string response)
    {
        Date = DateTime.Now;
        Prompt = prompt;
        Response = response;
    }

    public override string ToString()
    {
        return $"Date: {Date.ToShortDateString()}\nPrompt: {Prompt}\nResponse: {Response}";
    }
}

public class Journal
{
    private List<Entry> _entries;

    public Journal()
    {
        _entries = new List<Entry>();
    }

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
        Console.WriteLine("Entry added successfully!");
    }

    public void DisplayEntries()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("\nNo entries found in journal.");
            return;
        }

        foreach (Entry entry in _entries)
        {
            Console.WriteLine($"\n{entry}");
        }
    }

    public void SaveToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Entry entry in _entries)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
    }

    public void LoadFromFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("File not found.");
            return;
        }

        _entries.Clear();
        string[] lines = File.ReadAllLines(fileName);

        foreach (string line in lines)
        {
            string[] parts = line.Split("|");
            if (parts.Length == 3)
            {
                Entry entry = new Entry(parts[1], parts[2])
                {
                    Date = DateTime.Parse(parts[0])
                };
                _entries.Add(entry);
            }
        }
    }
}

public class PromptGenerator
{
    private static readonly string[] _prompts = {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What am I most grateful for today?",
        "What did I learn today that I didn't know before?",
        "What meaningful goal did I accomplish today?"
    };

    public static string GetRandomPrompt()
    {
        Random random = new Random();
        return _prompts[random.Next(_prompts.Length)];
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== Journal Program ===");
            Console.WriteLine("1. Write new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal");
            Console.WriteLine("4. Load journal");
            Console.WriteLine("5. Exit");
            Console.Write("\nSelect an option (1-5): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string prompt = PromptGenerator.GetRandomPrompt();
                    Console.WriteLine($"\nPrompt: {prompt}");
                    Console.Write("Your response: ");
                    string response = Console.ReadLine();
                    journal.AddEntry(new Entry(prompt, response));
                    break;

                case "2":
                    journal.DisplayEntries();
                    break;

                case "3":
                    Console.Write("\nEnter filename to save: ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    Console.WriteLine("Journal saved successfully!");
                    break;

                case "4":
                    Console.Write("\nEnter filename to load: ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    Console.WriteLine("Journal loaded successfully!");
                    break;

                case "5":
                    running = false;
                    Console.WriteLine("\nThank you for using the Journal Program!");
                    break;

                default:
                    Console.WriteLine("\nInvalid option. Please try again.");
                    break;
            }
        }
    }
}