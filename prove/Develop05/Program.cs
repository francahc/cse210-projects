using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Base class for all types of goals
public abstract class Goal
{
    public string Name { get; set; }
    public int Value { get; set; }

    public abstract void MarkComplete();
}

// Simple goal that can be marked complete once
public class SimpleGoal : Goal
{
    public bool IsCompleted { get; private set; }

    public override void MarkComplete()
    {
        if (!IsCompleted)
        {
            IsCompleted = true;
            Console.WriteLine($"Goal '{Name}' completed! You gained {Value} points.");
        }
        else
        {
            Console.WriteLine($"Goal '{Name}' has already been completed.");
        }
    }
}

// Eternal goal that can be marked complete multiple times
public class EternalGoal : Goal
{
    public int TimesCompleted { get; private set; }

    public override void MarkComplete()
    {
        TimesCompleted++;
        Console.WriteLine($"You completed the eternal goal '{Name}'! You gained {Value} points.");
    }
}

// Checklist goal that must be completed a certain number of times
public class ChecklistGoal : Goal
{
    public int TargetCount { get; private set; }
    public int CompletedCount { get; private set; }

    public ChecklistGoal(int targetCount)
    {
        TargetCount = targetCount;
    }

    public override void MarkComplete()
    {
        CompletedCount++;
        Console.WriteLine($"You completed a checklist item for goal '{Name}'. You gained {Value} points.");

        if (CompletedCount == TargetCount)
        {
            Console.WriteLine($"Congratulations! Goal '{Name}' completed. You gained an extra bonus of {Value * 10} points!");
        }
    }
}

// Class to manage goals and score
public class GoalManager
{
    private List<Goal> goals = new List<Goal>();
    private int totalScore = 0;

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void RecordEvent(string goalName)
    {
        var goal = goals.FirstOrDefault(g => g.Name == goalName);
        if (goal != null)
        {
            goal.MarkComplete();
            totalScore += goal.Value;
        }
        else
        {
            Console.WriteLine($"Goal '{goalName}' not found.");
        }
    }

    public void DisplayGoals()
    {
        Console.WriteLine("Current Goals:");
        foreach (var goal in goals)
        {
            if (goal is ChecklistGoal checklistGoal)
            {
                Console.WriteLine($"- {goal.Name} [{checklistGoal.CompletedCount}/{checklistGoal.TargetCount}]");
            }
            else
            {
                Console.WriteLine($"- {goal.Name} [{(goal is SimpleGoal simpleGoal && simpleGoal.IsCompleted ? "X" : " ")}]");
            }
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Total Score: {totalScore}");
    }

    public void SaveGoals(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var goal in goals)
            {
                writer.WriteLine($"{goal.Name},{goal.Value}");
            }
        }
    }

    public void LoadGoals(string fileName)
    {
        goals.Clear();
        totalScore = 0;
        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(',');
                string name = parts[0];
                int value = int.Parse(parts[1]);

                if (name != null)
                {
                    if (name.EndsWith("(Checklist)"))
                    {
                        var goal = new ChecklistGoal(int.Parse(name.Split(' ')[0]));
                        goal.Name = name;
                        goal.Value = value;
                        AddGoal(goal);
                    }
                    else if (name.EndsWith("(Eternal)"))
                    {
                        var goal = new EternalGoal();
                        goal.Name = name;
                        goal.Value = value;
                        AddGoal(goal);
                    }
                    else
                    {
                        var goal = new SimpleGoal();
                        goal.Name = name;
                        goal.Value = value;
                        AddGoal(goal);
                    }
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();

        // Load existing goals if available
        manager.LoadGoals("goals.txt");

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n1. Create new goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. List Goals");
            Console.WriteLine("4. Display Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddGoal(manager);
                    break;
                case 2:
                    RecordEvent(manager);
                    break;
                case 3:
                    manager.DisplayGoals();
                    break;
                case 4:
                    manager.DisplayScore();
                    break;
                case 5:
                    manager.SaveGoals("goals.txt");
                    Console.WriteLine("Goals saved successfully.");
                    break;
                case 6:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void AddGoal(GoalManager manager)
    {
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter goal value: ");
        int value = int.Parse(Console.ReadLine());

        if (name.EndsWith("(Checklist)"))
        {
            Console.Write("Enter target count: ");
            int targetCount = int.Parse(Console.ReadLine());
            var goal = new ChecklistGoal(targetCount);
            goal.Name = name;
            goal.Value = value;
            manager.AddGoal(goal);
        }
        else if (name.EndsWith("(Eternal)"))
        {
            var goal = new EternalGoal();
            goal.Name = name;
            goal.Value = value;
            manager.AddGoal(goal);
        }
        else
        {
            var goal = new SimpleGoal();
            goal.Name = name;
            goal.Value = value;
            manager.AddGoal(goal);
        }

        Console.WriteLine("Goal added successfully.");
    }

    static void RecordEvent(GoalManager manager)
    {
        Console.Write("Enter goal name to record event: ");
        string goalName = Console.ReadLine();
        manager.RecordEvent(goalName);
    }
}
