using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessManagement
{
    public class Task
    {
        public string Title { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public Employee ResponsibleEmployee { get; set; }

        public enum TaskPriority { Low, Medium, High, Urgent }
        public enum TaskStatus { ToDo, InProgress, PendingApproval, Completed, Rejected }

        public Task(string title, DateTime assignedDate, DateTime dueDate, TaskPriority priority, Employee responsibleEmployee)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            if (assignedDate > dueDate) throw new ArgumentException("Assigned date cannot be later than due date.", nameof(assignedDate));
            if (responsibleEmployee == null) throw new ArgumentNullException(nameof(responsibleEmployee), "Responsible employee cannot be null.");
            
            this.Title = title;
            this.AssignedDate = assignedDate;
            this.DueDate = dueDate;
            this.Priority = priority;
            this.Status = TaskStatus.ToDo;
            this.ResponsibleEmployee = responsibleEmployee;

        }

        public void ChangeStatus(TaskStatus newStatus)
        {
            this.Status = newStatus;
            Console.WriteLine($"Updated status of task '{Title}' to {Status}.");
        }
        public void AssignToEmployee(Employee employee)
        {
            if (employee != null)
            {
                this.ResponsibleEmployee = employee;
                Console.WriteLine($"Assigned task '{Title}' to employee {employee.Name}.");
            }
            else
            {
                Console.WriteLine("Employee cannot be null.");
            }
        }
        public void isOverdue()
        {
            if (Status != TaskStatus.Completed && DateTime.Now > DueDate)
            {
                Console.WriteLine($"Task '{Title}' is overdue.");
            }
            else
            {
                Console.WriteLine($"Task '{Title}' is not overdue.");
            }
        }
    }
}
