using System;
using System.Collections.Generic;
using System.Threading;

class Activity
{
    private string _name;
    private string _description;
    protected int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _duration = 0;

    }

    public string GetName()
    {
        return _name;
    }

    public string GetDescription()
    {
        return _description;
    }

    public int GetDuration()
    {
        return _duration;
    }

    public void DisplayStartingMessage()
    {
        Console.WriteLine($"Welcome to the: {_name}.");
        Console.WriteLine($"{_description}");
        Console.Write($"How long, in seconds, would you like for your session: ");
        _duration = int.Parse(Console.ReadLine());

        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);
        Console.WriteLine();
        
    }
    public void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        ShowSpinner(3);
        Console.WriteLine();
        Console.WriteLine($"You have completed another {_duration} seconds of the {_name}.");
        ShowSpinner(3);
    }
    public void ShowSpinner(int seconds)
    {
        List<string> animationStrings = new List<string>();
    animationStrings.Add("|");
    animationStrings.Add("/");
    animationStrings.Add("-");
    animationStrings.Add("\\");

    int totalIterations = (seconds * 1000) / 200; 
    
    for (int i = 0; i < totalIterations; i++)
    {
        string currentChar = animationStrings[i % animationStrings.Count];
        Console.Write(currentChar);
        Thread.Sleep(200); 
        Console.Write("\b \b");
    }
    }
    public void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
    {
        Console.Write($"\r{i} ");
        Thread.Sleep(2000);
    }
        Console.Write("\r¡Finished time!");
    }

}