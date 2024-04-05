using System;
using System.Threading;
using System.Collections.Generic;

class BaseAct
{
    protected string name;
    protected string description;
    protected int duration;

    public BaseAct(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public virtual void StartActivity()
    {
        Console.WriteLine($"\nStarting {name} Activity:");
        Console.WriteLine(description);
        Console.Write("How long, in seconds, would you like for your session? ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("get ready...");
        Thread.Sleep(1000);
    }

    public virtual void EndActivity()
    {
        Console.WriteLine($"\nGreat job! You've completed the {name} activity.");
        Console.WriteLine($"You spent {duration} seconds on this activity.");
        Thread.Sleep(1000);
    }
}

class BreathingActivity : BaseAct
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public override void StartActivity()
    {
        base.StartActivity();
        Console.WriteLine("\nStarting breathing exercise...");
        Breathe();
    }

    private void Breathe()
    {
        for (int i = 0; i < duration; i++)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
            
        }
        Console.WriteLine("done");
    }
}

class ReflectionActivity : BaseAct
{
    private static readonly string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private static readonly string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    public override void StartActivity()
    {
        base.StartActivity();
        Console.WriteLine("\nStarting reflection exercise...");
        Reflect();
    }

    private void Reflect()
    {
        string prompt = prompts[new Random().Next(prompts.Length)];
        Console.WriteLine(prompt);
        Console.WriteLine("Beginning countdown...");
        Countdown();
        Console.WriteLine("\nBegin reflection:");
        foreach (string question in questions)
        {
            Console.WriteLine(question);
            Thread.Sleep(1000);
        }
    }

    private void Countdown()
    {
        for (int i = 3; i > 0; i--)
        {
            Console.WriteLine(i);
            Thread.Sleep(1000);
        }
    }
}

class ListingActivity : BaseAct
{
    private static readonly string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    public override void StartActivity()
    {
        base.StartActivity();
        Console.WriteLine("\nStarting listing exercise...");
        ListItems();
    }

    private void ListItems()
    {
        string prompt = prompts[new Random().Next(prompts.Length)];
        Console.WriteLine(prompt);
        Console.WriteLine("Begin listing items...");
        Thread.Sleep(duration * 1000);
        Console.WriteLine($"You listed {duration} items.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nMenu options:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.StartActivity();
                    breathingActivity.EndActivity();
                    break;
                case "2":
                    ReflectionActivity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.StartActivity();
                    reflectionActivity.EndActivity();
                    break;
                case "3":
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.StartActivity();
                    listingActivity.EndActivity();
                    break;
                case "4":
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 1 to 4.");
                    break;
            }
        }
    }
}
