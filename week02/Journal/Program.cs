// Extra Requirements: Allows the user to skip a prompt and records a "Skipped Entry" to address the problem of feeling they have nothing to say. Saves and loads the journal as a JSON file.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        Journal myJournal = new Journal();
        PromptGenerator promptGen = new PromptGenerator();
        string choice = "";

        while (choice != "5")
        {
            Console.WriteLine("\nJournal Menu");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a JSON file");
            Console.WriteLine("4. Load the journal from a JSON file");
            Console.WriteLine("5. Exit");
            Console.Write("What would you like to do? ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string prompt = promptGen.GetRandomPrompt();
                    Console.WriteLine($"\nPrompt: {prompt}");
                    Console.WriteLine("Write your response below, or type 'skip' to skip this prompt.");
                    Console.Write("> ");
                    string response = Console.ReadLine();
                    string date = DateTime.Now.ToShortDateString();

                    if (response.Trim().ToLower() == "skip")
                    {
                        myJournal.AddEntry(new Entry(date, prompt, "(Skipped)"));
                        Console.WriteLine("You chose to skip this prompt. Entry recorded as 'Skipped'.");
                    }
                    else
                    {
                        myJournal.AddEntry(new Entry(date, prompt, response));
                    }
                    break;

                case "2":
                    myJournal.Display();
                    break;

                case "3":
                    Console.Write("Enter filename to save (e.g., journal.json): ");
                    string saveFile = Console.ReadLine();
                    myJournal.SaveToJson(saveFile);
                    Console.WriteLine("Journal saved as JSON.");
                    break;

                case "4":
                    Console.Write("Enter filename to load (e.g., journal.json): ");
                    string loadFile = Console.ReadLine();
                    myJournal.LoadFromJson(loadFile);
                    Console.WriteLine("Journal loaded from JSON.");
                    break;

                case "5":
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}

class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }

    public Entry() { }

    public void Display()
    {
        Console.WriteLine($"\nDate: {Date}");
        Console.WriteLine($"Prompt: {Prompt}");
        Console.WriteLine($"Response: {Response}");
    }
}

class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void Display()
    {
        Console.WriteLine($"\nJournal Entries ({_entries.Count}):");
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToJson(string filename)
    {
        string json = JsonSerializer.Serialize(_entries, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, json);
    }

    public void LoadFromJson(string filename)
    {
        if (File.Exists(filename))
        {
            string json = File.ReadAllText(filename);
            _entries = JsonSerializer.Deserialize<List<Entry>>(json);
        }
        else
        {
            Console.WriteLine("File not found. Starting with an empty journal.");
            _entries.Clear();
        }
    }
}

class PromptGenerator
{
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(_prompts.Count);
        return _prompts[index];
    }
}
