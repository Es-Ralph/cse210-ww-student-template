using System;
using System.Collections.Generic;

// Base Class
public abstract class Activity
{
    private DateTime _date;
    private int _lengthMinutes;

    protected Activity(DateTime date, int lengthMinutes)
    {
        _date = date;
        _lengthMinutes = lengthMinutes;
    }

    public DateTime Date => _date;
    public int LengthMinutes => _lengthMinutes;

    // Abstract methods for polymorphism
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Virtual method using abstract methods
    public virtual string GetSummary()
    {
        return $"{Date.ToShortDateString()} ({GetType().Name}) - " +
               $"Length: {LengthMinutes} min, " +
               $"Distance: {GetDistance():0.0} km, " +
               $"Speed: {GetSpeed():0.0} kph, " +
               $"Pace: {GetPace():0.0} min/km";
    }
}

// Derived Class: Running
public class Running : Activity
{
    private double _distanceKm;

    public Running(DateTime date, int lengthMinutes, double distanceKm)
        : base(date, lengthMinutes)
    {
        _distanceKm = distanceKm;
    }

    public override double GetDistance() => _distanceKm;
    public override double GetSpeed() => (_distanceKm / LengthMinutes) * 60;
    public override double GetPace() => LengthMinutes / _distanceKm;
}

//  Derived Class: Cycling
public class Cycling : Activity
{
    private double _speedKph;

    public Cycling(DateTime date, int lengthMinutes, double speedKph)
        : base(date, lengthMinutes)
    {
        _speedKph = speedKph;
    }

    public override double GetDistance() => (_speedKph * LengthMinutes) / 60;
    public override double GetSpeed() => _speedKph;
    public override double GetPace() => 60 / _speedKph;
}

// Derived Class: Swimming
public class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int lengthMinutes, int laps)
        : base(date, lengthMinutes)
    {
        _laps = laps;
    }

    public override double GetDistance() => _laps * 0.05; // 50m per lap
    public override double GetSpeed() => (GetDistance() / LengthMinutes) * 60;
    public override double GetPace() => LengthMinutes / GetDistance();
}

// Program Runner
class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2025, 12, 1), 30, 5.0),
            new Cycling(new DateTime(2025, 12, 2), 45, 20.0),
            new Swimming(new DateTime(2025, 12, 3), 25, 40)
        };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}