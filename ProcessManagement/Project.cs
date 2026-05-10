using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessManagement
{
    public class Project
    {

        public string Name { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime Deadline { get; set; }
        public List<Task> Tasks { get; set; }

        public Project(string name, DateTime assignedDate, DateTime deadline)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (assignedDate > deadline) throw new ArgumentException("Assigned date cannot be later than deadline.", nameof(assignedDate));
            
            this.Name = name;
            this.AssignedDate = assignedDate;
            this.Deadline = deadline;
            this.Tasks = new List<Task>();
        }
        public void AddTask(Task task)
        {
            if (task != null)
            {
                Tasks.Add(task);
                Console.WriteLine($"Added task {task.Title} to project {Name}.");
            }
            else
            {
                Console.WriteLine("Task cannot be null.");
            }
        }
        public double GetProjectProgress()
        {
            if (Tasks.Count == 0) return 0;
            int completedTasks = 0;
            foreach (var task in Tasks)
            {
                if (task.Status == Task.TaskStatus.Completed)
                {
                    completedTasks++;
                }
            }
            return (double)completedTasks / Tasks.Count * 100;
        }
        public List<Task> GetTasksByEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
            List<Task> tasksByEmployee = new List<Task>();
            foreach (var task in Tasks)
            {
                if (task.ResponsibleEmployee == employee)
                {
                    tasksByEmployee.Add(task);
                }
            }
            return tasksByEmployee;
        }

    }
}
