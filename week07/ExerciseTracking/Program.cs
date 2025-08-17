using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Exercise Tracking Project\n");

        // Create some activities
        Activity run = new Running(new DateTime(2022, 11, 3), 30, 3.0);  
        Activity cycle = new Cycling(new DateTime(2022, 11, 3), 30, 9.7); 
        Activity swim = new Swimming(new DateTime(2022, 11, 3), 30, 20);  

        List<Activity> activities = new List<Activity> { run, cycle, swim };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}

abstract class Activity
{
    private DateTime _date;
    private int _length; 

    public Activity(DateTime date, int length)
    {
        _date = date;
        _length = length;
    }

    public DateTime Date => _date;
    public int Length => _length;

    public abstract double GetDistance(); 
    public abstract double GetSpeed();    
    public abstract double GetPace();     

    public virtual string GetSummary()
    {
        return $"{Date.ToString("dd MMM yyyy")} {this.GetType().Name} ({Length} min) - " +
               $"Distance {GetDistance():F2} miles, " +
               $"Speed {GetSpeed():F2} mph, " +
               $"Pace: {GetPace():F2} min per mile";
    }
}

class Running : Activity
{
    private double _distance;

    public Running(DateTime date, int length, double distance) : base(date, length)
    {
        _distance = distance;
    }

    public override double GetDistance() => _distance;

    public override double GetSpeed() => (_distance / Length) * 60;

    public override double GetPace() => Length / _distance;
}

class Cycling : Activity
{
    private double _speed;

    public Cycling(DateTime date, int length, double speed) : base(date, length)
    {
        _speed = speed;
    }

    public override double GetDistance() => (_speed * Length) / 60;

    public override double GetSpeed() => _speed;

    public override double GetPace() => 60 / _speed;
}

class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int length, int laps) : base(date, length)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        double distanceMeters = _laps * 50;
        double distanceMiles = distanceMeters / 1000 * 0.62;
        return distanceMiles;
    }

    public override double GetSpeed() => (GetDistance() / Length) * 60;

    public override double GetPace() => Length / GetDistance();
}
