using System;
using System.Collections.Generic;
using System.Threading;

namespace VotingSystem
{
    // Voter class to represent individual voters
    public class Voter
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string PassportNumber { get; set; }
        public string Address { get; set; }
        public Candidate ChosenCandidate { get; set; } 

        
        public Voter(string name, int age, string passportNumber, string address)
        {
            Name = name;
            Age = age;
            PassportNumber = passportNumber;
            Address = address;
        }
    }

    
    public class Candidate
    {
        public string Name { get; set; }
        public int Votes { get; set; }
        public bool IsVoted { get; set; } 

       
        public Candidate(string name)
        {
            Name = name;
            Votes = 0;
            IsVoted = false; 
        }
    }

   
    public class VotingSystem
    {
        private List<Candidate> candidates;

       
        public VotingSystem()
        {
            
            candidates = new List<Candidate>
            {
                new Candidate("Candidate A"),
                new Candidate("Candidate B"),
                new Candidate("Candidate C"),
                new Candidate("Candidate D"),
                new Candidate("Candidate E")
            };
        }

       
        public void RegisterVoter()
        {
            Console.WriteLine("Data registration form");
            Console.Write("Enter your full name: ");
            string name = Console.ReadLine();

            Console.Write("Enter your age: ");
            int age;
            while (!int.TryParse(Console.ReadLine(), out age))
            {
                Console.WriteLine("Invalid age! Please enter a valid integer.");
                Console.Write("Enter your age: ");
            }

            Console.Write("Enter your passport number: ");
            string passportNumber = Console.ReadLine();

            Console.Write("Enter your address: ");
            string address = Console.ReadLine();

            Voter newVoter = new Voter(name, age, passportNumber, address);

            

            ChooseCandidate(newVoter); 
            ProcessRegistrationInfo(); 
        }

        
        public void DisplayCandidates()
        {
            Console.WriteLine("\nCandidates:");
            for (int i = 0; i < candidates.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {candidates[i].Name}");
            }
        }

       
        public void ChooseCandidate(Voter voter)
        {
            while (true)
            {
                DisplayCandidates();

                Console.Write("\nPlease enter the number corresponding to your chosen candidate: ");
                if (!int.TryParse(Console.ReadLine(), out int chosenIndex) || chosenIndex < 1 || chosenIndex > candidates.Count)
                {
                    Console.WriteLine("Invalid input! Please enter a valid candidate number.");
                    continue;
                }

                chosenIndex--;

                if (candidates[chosenIndex].IsVoted)
                {
                    Console.WriteLine($"You have already voted for {candidates[chosenIndex].Name}. Please choose another candidate.");
                }
                else
                {
                    candidates[chosenIndex].IsVoted = true;
                    voter.ChosenCandidate = candidates[chosenIndex];
                    Console.WriteLine($"\nThank you, {voter.Name}, for voting for {voter.ChosenCandidate.Name}!");
                    Console.WriteLine();
                    break;
                }
            }
        }

        
        private void ProcessRegistrationInfo()
        {
            Console.WriteLine("Please wait while we process your registration information...");
            Thread.Sleep(30000); 
            Console.Write("\b \b");
        }

        
        public void DisplayElectionResults()
        {
            Console.WriteLine("Election Results:");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Candidate        Votes");
            Console.WriteLine("----------------------------");
            foreach (var candidate in candidates)
            {
                Console.WriteLine($"{candidate.Name,-16} {candidate.Votes}");
            }
            Console.WriteLine("----------------------------");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            VotingSystem votingSystem = new VotingSystem();

            Console.WriteLine("Welcome to the electronic voting system!");
            Console.WriteLine();

            votingSystem.RegisterVoter();

            
            for (int i = 0; i < 3; i++)
            {
                votingSystem.RegisterVoter();
            }

            votingSystem.DisplayElectionResults();
        }
    }
}
