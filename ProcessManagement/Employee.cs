using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessManagement
{
    public class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Salary { get; set; }
        public int YearOfWorkExperience { get; set; }
        public decimal PerformanceRating { get; set; }

        public Employee(string name, string position, DateTime birthDate, decimal salary, int yearOfWorkExperience, decimal performanceRating)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (salary < 0) throw new ArgumentOutOfRangeException(nameof(salary), "Salary cannot be negative.");           
            if (yearOfWorkExperience < 0) throw new ArgumentOutOfRangeException(nameof(yearOfWorkExperience), "Year of work experience cannot be negative.");
           
            
            this.Name = name;
            this.Position = position;
            this.BirthDate = birthDate;
            this.Salary = salary;
            this.YearOfWorkExperience = yearOfWorkExperience;
            this.PerformanceRating = performanceRating;
        }

        public void UpdatePerformanceRating(decimal newRating)
            {
                if (newRating < 0 || newRating > 5) throw new ArgumentOutOfRangeException(nameof(newRating), "Performance rating must be between 0 and 5.");
                this.PerformanceRating = newRating;
                Console.WriteLine($"Updated performance rating for {Name} to {PerformanceRating}.");
        }
        public void UpdateSalary(decimal newSalary)
        {
            if (newSalary < 0) throw new ArgumentOutOfRangeException(nameof(newSalary), "Salary cannot be negative.");
            this.Salary = newSalary;
            Console.WriteLine($"Updated salary for {Name} to {Salary}.");
        }
        public void GetEmployeeInfo()
        {
            Console.WriteLine($"Name: {Name}, Position: {Position}, Birth Date: {BirthDate.ToShortDateString()}, Salary: {Salary}, Years of Experience: {YearOfWorkExperience}, Performance Rating: {PerformanceRating}");
        }

    }
}
