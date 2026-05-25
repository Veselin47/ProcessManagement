using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessManagement
{
    public class Department : IOrganizationComponent
    {
        public string Name { get; set; }
        public int FoundingYear { get; set; }

        public Employee Manager { get; set; }

        public List<Team> Teams { get; set; } = new List<Team>();

        public Department(string name, int foundingYear, Employee manager)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (foundingYear < 0) throw new ArgumentOutOfRangeException(nameof(foundingYear), "Founding year cannot be negative.");
            if (manager == null) throw new ArgumentNullException(nameof(manager), "Manager cannot be null.");

            this.Name = name;
            this.FoundingYear = foundingYear;
            this.Manager = manager;
        }

        public void AddTeam(Team team)
        {
            if (team != null)
            {
                Teams.Add(team);
                Console.WriteLine($"Added team {team.Name} to department {Name}.");
            }
        }
        public void SetManager(Employee manager)
        {
            if (manager != null)
            {
                this.Manager = manager;
                Console.WriteLine($"Set manager {manager.Name} for department {Name}.");
            }
        }
        public void GetDepartmentInfo()
        {
            Console.WriteLine($"Department: {Name}, Founding Year: {FoundingYear}, Manager: {Manager.Name}");
            foreach (var team in Teams)
            {
                Console.WriteLine($"\tTeam: {team.Name}, Leader: {team.Leader.Name}");
                foreach (var member in team.Members)
                {
                    Console.WriteLine($"\t\tMember: {member.Name}, Position: {member.Position}");
                }
            }
        }
        public void Display(int depth)
        {
            string indent = new string('\t', depth);
            Console.WriteLine($"{indent}Department: {Name}, Manager: {Manager.Name}");

            foreach (var team in Teams)
            {
                team.Display(depth + 1);
            }
        }
    }
}
