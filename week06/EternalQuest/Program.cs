using System;
using System.Collections.Generic;
using System.IO;

namespace GoalTracker
{
    // ============================
    // Base Class: Goal (Abstraction + Encapsulation)
    // ============================
    public abstract class Goal
    {
        protected string _name;
        protected string _description;
        protected int _points;

        public Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
        }

        public abstract void RecordEvent();
        public abstract string DisplayStatus();
        public abstract bool IsComplete();

        public int GetPoints() => _points;
        public string GetName() => _name;
        public string GetDescription() => _description;
    }

    // ============================
    // Derived Class: SimpleGoal
    // ============================
    public class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string name, string description, int points)
            : base(name, description, points)
        {
            _isComplete = false;
        }

        public override void RecordEvent()
        {
            _isComplete = true;
        }

        public override string DisplayStatus()
        {
            return $"{_name} - {(_isComplete ? "Completed ✅" : "Not Completed ❌")}";
        }

        public override bool IsComplete() => _isComplete;
    }

    // ============================
    // Derived Class: EternalGoal
    // ============================
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points) { }

        public override void RecordEvent()
        {
            // Eternal goals never complete, but points are awarded each time
        }

        public override string DisplayStatus()
        {
            return $"{_name} - Eternal Goal ♾️ (Points earned each time)";
        }

        public override bool IsComplete() => false;
    }

    // ============================
    // Derived Class: ChecklistGoal
    // ============================
    public class ChecklistGoal : Goal
    {
        private int _targetCount;
        private int _currentCount;
        private int _bonusPoints;

        public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
            : base(name, description, points)
        {
            _targetCount = targetCount;
            _bonusPoints = bonusPoints;
            _currentCount = 0;
        }

        public override void RecordEvent()
        {
            _currentCount++;
        }

        public override string DisplayStatus()
        {
            return $"{_name} - Progress: {_currentCount}/{_targetCount}";
        }

        public override bool IsComplete() => _currentCount >= _targetCount;

        public int GetBonusPoints() => IsComplete() ? _bonusPoints : 0;
    }

    // ============================
    // Main Program
    // ============================
    class Program
    {
        private static List<Goal> goals = new List<Goal>();
        private static int totalPoints = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("🏆 Welcome to the Goal Tracker Program!");
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Create Goal");
                Console.WriteLine("2. Record Event");
                Console.WriteLine("3. Display Goals");
                Console.WriteLine("4. Save Goals");
                Console.WriteLine("5. Load Goals");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1": CreateGoal(); break;
                    case "2": RecordEvent(); break;
                    case "3": DisplayGoals(); break;
                    case "4": SaveGoals(); break;
                    case "5": LoadGoals(); break;
                    case "6": running = false; break;
                    default: Console.WriteLine("Invalid option. Try again."); break;
                }
            }
        }

        // ============================
        // Create Goal (User Created Goals)
        // ============================
        static void CreateGoal()
        {
            Console.WriteLine("Choose Goal Type: 1=Simple, 2=Eternal, 3=Checklist");
            string type = Console.ReadLine();

            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Description: ");
            string description = Console.ReadLine();
            Console.Write("Points: ");
            int points = int.Parse(Console.ReadLine());

            if (type == "1")
                goals.Add(new SimpleGoal(name, description, points));
            else if (type == "2")
                goals.Add(new EternalGoal(name, description, points));
            else if (type == "3")
            {
                Console.Write("Target Count: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Bonus Points: ");
                int bonus = int.Parse(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, description, points, target, bonus));
            }
        }

        // ============================
        // Record Event (Functionality)
        // ============================
        static void RecordEvent()
        {
            DisplayGoals();
            Console.Write("Select goal number: ");
            int index = int.Parse(Console.ReadLine()) - 1;

            goals[index].RecordEvent();
            totalPoints += goals[index].GetPoints();

            if (goals[index] is ChecklistGoal checklist)
                totalPoints += checklist.GetBonusPoints();

            Console.WriteLine($"Points earned! Total: {totalPoints}");
        }

        // ============================
        // Display Goals
        // ============================
        static void DisplayGoals()
        {
            Console.WriteLine("\nGoals:");
            for (int i = 0; i < goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {goals[i].DisplayStatus()}");
            }
            Console.WriteLine($"Total Points: {totalPoints}");
        }

        // ============================
        // Save Goals (Persistence)
        // ============================
        static void SaveGoals()
        {
            using (StreamWriter writer = new StreamWriter("goals.txt"))
            {
                writer.WriteLine(totalPoints);
                foreach (var goal in goals)
                {
                    if (goal is ChecklistGoal checklist)
                    {
                        writer.WriteLine($"{goal.GetType().Name}|{goal.GetName()}|{goal.GetDescription()}|{goal.GetPoints()}|{checklist.IsComplete()}");
                    }
                    else
                    {
                        writer.WriteLine($"{goal.GetType().Name}|{goal.GetName()}|{goal.GetDescription()}|{goal.GetPoints()}");
                    }
                }
            }
            Console.WriteLine("Goals saved successfully!");
        }

        // ============================
        // Load Goals (Persistence)
        // ============================
        static void LoadGoals()
        {
            if (File.Exists("goals.txt"))
            {
                string[] lines = File.ReadAllLines("goals.txt");
                totalPoints = int.Parse(lines[0]);
                goals.Clear();

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split('|');
                    string type = parts[0];
                    string name = parts[1];
                    string description = parts[2];
                    int points = int.Parse(parts[3]);

                    if (type == "SimpleGoal")
                        goals.Add(new SimpleGoal(name, description, points));
                    else if (type == "EternalGoal")
                        goals.Add(new EternalGoal(name, description, points));
                    else if (type == "ChecklistGoal")
                        goals.Add(new ChecklistGoal(name, description, points, 3, 10)); // default values
                }
                Console.WriteLine("Goals loaded successfully!");
            }
        }
    }
}