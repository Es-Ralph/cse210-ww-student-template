// Activity.cs (Base Class)
public abstract class Activity
{
    protected string _activityName;
    protected string _activityDescription;
    protected int _activityDuration;

    public Activity(string name, string description, int duration)
    {
        _activityName = name;
        _activityDescription = description;
        _activityDuration = duration;
    }

    public void StartActivity()
    {
        Console.WriteLine($"Starting {_activityName}: {_activityDescription}");
        DisplayAnimation(3);
    }

    public void EndActivity()
    {
        Console.WriteLine($"You have completed {_activityName} for {_activityDuration} seconds.");
    }

    protected void DisplayAnimation(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write("|");
            System.Threading.Thread.Sleep(300);
            Console.Write("\b/");
            System.Threading.Thread.Sleep(300);
            Console.Write("\b-");
            System.Threading.Thread.Sleep(300);
            Console.Write("\b\\");
            System.Threading.Thread.Sleep(300);
            Console.Write("\b ");
        }
        Console.WriteLine();
    }
}

// BreathingActivity.cs
public class BreathingActivity : Activity
{
    public BreathingActivity(int duration) 
        : base("Breathing", "Relax with guided breathing.", duration) { }

    public void Run()
    {
        StartActivity();
        for (int i = 0; i < _activityDuration; i += 2)
        {
            Console.WriteLine("Inhale...");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Exhale...");
            System.Threading.Thread.Sleep(1000);
        }
        EndActivity();
    }
}

// ListingActivity.cs
public class ListingActivity : Activity
{
    public ListingActivity(int duration) 
        : base("Listing", "List as many items as you can.", duration) { }

    public void Run()
    {
        StartActivity();
        Console.WriteLine("Start listing things you are grateful for:");
        DateTime endTime = DateTime.Now.AddSeconds(_activityDuration);
        while (DateTime.Now < endTime)
        {
            string item = Console.ReadLine();
            Console.WriteLine($"You listed: {item}");
        }
        EndActivity();
    }
}

// ReflectingActivity.cs
public class ReflectingActivity : Activity
{
    private string[] _prompts = {
        "Think of a time you overcame a challenge.",
        "Recall a moment of kindness you experienced.",
        "Reflect on a goal you achieved recently."
    };

    public ReflectingActivity(int duration) 
        : base("Reflecting", "Reflect on meaningful experiences.", duration) { }

    public void Run()
    {
        StartActivity();
        Random rand = new Random();
        string prompt = _prompts[rand.Next(_prompts.Length)];
        Console.WriteLine(prompt);
        System.Threading.Thread.Sleep(_activityDuration * 1000);
        EndActivity();
    }
}

// Program.cs
class Program
{
    static void Main(string[] args)
    {
        BreathingActivity breathing = new BreathingActivity(6);
        breathing.Run();

        ListingActivity listing = new ListingActivity(10);
        listing.Run();

        ReflectingActivity reflecting = new ReflectingActivity(8);
        reflecting.Run();
    }
}