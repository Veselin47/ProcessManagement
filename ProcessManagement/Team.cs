using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessManagement
{
    public class Team
    {
        public string Name { get; set; }
        public Employee Leader { get; set; }
        public List<Employee> Members { get; set; } = new List<Employee>();
        public List<Project> Projects { get; set; } = new List<Project>();
    
        public Team(string name, Employee leader)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (leader == null) throw new ArgumentNullException(nameof(leader), "Leader cannot be null.");
            
            this.Name = name;
            this.Leader = leader;
        }

        public void AddMember(Employee member)
        {
            if (member != null)
            {
                Members.Add(member);
                Console.WriteLine($"Added member {member.Name} to team {Name}.");
            }
            else
            {
                Console.WriteLine("Member cannot be null.");
            }
        }
        public void RemoveMember(Employee member) { 
           if (member != null && Members.Contains(member))
            {
                Members.Remove(member);
                Console.WriteLine($"Removed member {member.Name} from team {Name}.");
            }
            else
            {
                Console.WriteLine("Member not found in the team.");
            }
        }
        public void AddProject(Project project)
        {
            if (project != null)
            {
                Projects.Add(project);
                Console.WriteLine($"Added project {project.Name} to team {Name}.");
            }
            else
            {
                Console.WriteLine("Project cannot be null.");
            }
        }
        public void GetTeamWorkload()
        {
                Console.WriteLine($"Team: {Name}, Leader: {Leader.Name}");
                Console.WriteLine("Members:");
                foreach (var member in Members)
                {
                    Console.WriteLine($"\t{member.Name}, Position: {member.Position}");
                }
                Console.WriteLine("Projects:");
                foreach (var project in Projects)
                {
                    Console.WriteLine($"\t{project.Name}, Deadline: {project.Deadline.ToShortDateString()}");
                }
        }
    }
}
