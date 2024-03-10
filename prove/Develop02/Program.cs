using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        journal.Run();
    }
}

class Journal
{
    private List<Entry> _journal = new List<Entry>();
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    private void Menu()
    {
        Console.WriteLine("Please select one of the following choices:");
        Console.WriteLine("1. Write");
        Console.WriteLine("2. Display");
        Console.WriteLine("3. Save");
        Console.WriteLine("4. Load");
        Console.WriteLine("5. Exit");
    }

    public void Run()
    {
        bool running = true;
        while (running)
        {
            Menu();
            int choice = GetChoice();
            switch (choice)
            {
                case 1:
                    Write();
                    break;
                case 2:
                    Display();
                    break;
                case 3:
                    Save();
                    break;
                case 4:
                    Load();
                    break;
                case 5:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private int GetChoice()
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
        {
            Console.WriteLine("Invalid option. Please try again.");
            Console.Write("Please select one of the following choices: ");
        }
        return choice;
    }

    private void Write()
    {
        string prompt = GetRandomPrompt();
        Console.WriteLine("Prompt: " + prompt);
        Console.Write("Your response: ");
        string response = Console.ReadLine();
        Console.Write("What food comes to your mind?: ");  // In this part, other information is saved in the journal entry.
        string eat = Console.ReadLine(); 
        _journal.Add(new Entry(prompt, response, DateTime.Now, eat));
        Console.WriteLine("Entry added successfully.");
    }

    private string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(_prompts.Count);
        return _prompts[index];
    }

    private void Display()
    {
        if (_journal.Count == 0)
        {
            Console.WriteLine("Nothing was found in the journal.");
        }
        else
        {
            foreach (var entry in _journal)
            {
                Console.WriteLine($"Date: {entry.Date}, Prompt: {entry.Prompt}, Response: {entry.Response}, Eat: {entry.Eat}");
            }
        }
    }

    private void Save()
    {
        Console.Write("Enter a filename to save the journal: ");
        string filename = Console.ReadLine();
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in _journal)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}|{entry.Eat}");
            }
        }
        Console.WriteLine("Journal saved successfully.");
    }

    private void Load()
    {
        _journal.Clear();
        Console.Write("Enter the filename to load the journal: ");
        string filename = Console.ReadLine();
        if (File.Exists(filename))
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    DateTime date = DateTime.Parse(parts[0]);
                    string prompt = parts[1];
                    string response = parts[2];
                    string eat = parts[3]; 
                    _journal.Add(new Entry(prompt, response, date, eat));
                }
            }
            Console.WriteLine("Journal loaded successfully.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}

class Entry
{
    public string Prompt { get; }
    public string Response { get; }
    public DateTime Date { get; }
    public string Eat { get; } 

    public Entry(string prompt, string response, DateTime date, string eat)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
        Eat = eat;
    }
}
