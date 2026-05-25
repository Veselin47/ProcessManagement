using ProcessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using static ProcessManagement.Task;

namespace ProcessManagement
{

    class Program
    {
        static Company myStartup;

        static void Main(string[] args)
        {
            InitializeSampleData();

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine($"=== STARTUP MANAGEMENT: {myStartup.Name} ===");
                Console.WriteLine("1. Display full hierarchy and projects");
                Console.WriteLine("2. Employee management (Salary/Rating)");
                Console.WriteLine("3. Task management (Change status)");
                Console.WriteLine("4. Invest capital");
                Console.WriteLine("5. Project report (Progress %)");
                Console.WriteLine("6. Save data to file (JSON(1) / Text(2))");
                Console.WriteLine("0. Exit");
                Console.Write("\nChoose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": DisplayHierarchyUI(); break;
                    case "2": EmployeeManagementUI(); break;
                    case "3": TaskManagementUI(); break;
                    case "4": InvestmentUI(); break;
                    case "5": ProjectStatusUI(); break;
                    case "6": SaveDataUI(); break;
                    case "0": exit = true; break;
                    default: Console.WriteLine("Invalid choice. Press any key..."); Console.ReadKey(); break;
                }
            }
        }

        #region UI Logic Methods

        static void DisplayHierarchyUI()
        {
            Console.Clear();
            myStartup.DisplayHierarchy();
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        static void EmployeeManagementUI()
        {
            Console.Write("Enter employee name: ");
            string name = Console.ReadLine();
            Employee emp = FindEmployee(name);

            if (emp != null)
            {
                Console.WriteLine($"Found: {emp.Position} {emp.Name}");
                Console.WriteLine("1. Change salary | 2. Change rating");
                string op = Console.ReadLine();
                if (op == "1")
                {
                    Console.Write("New salary: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal sal))
                        emp.UpdateSalary(sal);
                }
                else if (op == "2")
                {
                    Console.Write("New rating (0.0 - 5.0): ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal rat))
                        emp.UpdatePerformanceRating(rat);
                }
            }
            else Console.WriteLine("Employee not found.");
            Console.ReadKey();
        }

        static void TaskManagementUI()
        {
            Console.Write("Enter task title: ");
            string title = Console.ReadLine();
            Task task = FindTask(title);

            if (task != null)
            {
                Console.WriteLine("Choose new status: 1. InProgress 2. Completed 3. Rejected");
                string st = Console.ReadLine();
                if (st == "1") task.ChangeStatus(Task.TaskStatus.InProgress);
                else if (st == "2") task.ChangeStatus(Task.TaskStatus.Completed);
                else if (st == "3") task.ChangeStatus(Task.TaskStatus.Rejected);
            }
            else Console.WriteLine("Task not found.");
            Console.ReadKey();
        }

        static void ProjectStatusUI()
        {
            foreach (var dept in myStartup.Departments)
                foreach (var team in dept.Teams)
                    foreach (var proj in team.Projects)
                    {
                        Console.WriteLine($"Project: {proj.Name} | Progress: {proj.GetProjectProgress()}%");
                    }
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        static void InvestmentUI()
        {
            Console.Write("Investor name: ");
            string invName = Console.ReadLine();
            Console.Write("Amount to invest: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Investor inv = new Investor(invName, amount, DateTime.Now);
                myStartup.AddInvestor(amount, inv);
                Console.WriteLine("Investment accepted successfully!");
            }
            Console.ReadKey();
        }

        static void SaveDataUI()
        {

            Console.WriteLine("Choose save strategy: JSON(1) Text(2)");
            string choice = Console.ReadLine();
            ISaveStrategy strategy;
            if (choice == "1")
                strategy = new JsonSaveStrategy();
            else if (choice == "2")
                strategy = new TextSaveStrategy();
            else
            {
                Console.WriteLine("Invalid choice. Defaulting to JSON strategy.");
                strategy = new JsonSaveStrategy();
            }

            myStartup.Save(strategy, "startup_backup");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        #endregion

        #region Helper Methods

        static Employee FindEmployee(string name)
        {
            foreach (var d in myStartup.Departments)
                foreach (var t in d.Teams)
                    foreach (var m in t.Members)
                        if (m.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) return m;
            return null;
        }

        static Task FindTask(string title)
        {
            foreach (var d in myStartup.Departments)
                foreach (var t in d.Teams)
                    foreach (var p in t.Projects)
                        foreach (var task in p.Tasks)
                            if (task.Title.Equals(title, StringComparison.OrdinalIgnoreCase)) return task;
            return null;
        }

        static void InitializeSampleData()
        {
            Company.Initialize("Alpha Tech", 100000, 2024, "ul. Vitosha 1", "contact@alpha.com", "0888112233");

            myStartup = Company.Instance;

            Employee boss = new Employee("Georgi Georgiev", "Meninger", new DateTime(1980, 1, 1), 5000, 10, 4.8m);
            Employee boss2 = new Employee("Daniel Georgiev", "Meninger", new DateTime(1990, 1, 1), 5200, 10, 4.8m);

            Employee dev1 = new Employee("Ivan Petrov", "Developer", new DateTime(1995, 5, 20), 3000, 3, 4.2m);
            Employee dev2 = new Employee("Petar Popov", "Developer", new DateTime(1999, 7, 22), 3000, 2, 4.5m);

            Department itDept = new Department("IT", 2024, boss, new List<Team>());
            Team devTeam = new Team("Web Team", boss);
            Team netTeam = new Team("Net Team", boss2);

            devTeam.AddMember(dev1);
            netTeam.AddMember(dev2);
            devTeam.AddMember(boss);
            netTeam.AddMember(boss2);
            itDept.AddTeam(devTeam);
            itDept.AddTeam(netTeam);

            Project p1 = new Project("Mobile App", DateTime.Now, DateTime.Now.AddMonths(2));
            Task t1 = new Task("UI Design", DateTime.Now, DateTime.Now.AddDays(10), TaskPriority.High, dev1);
            Task t2 = new Task("Backend API", DateTime.Now, DateTime.Now.AddDays(20), TaskPriority.Medium, dev2);
            p1.AddTask(t1);
            p1.AddTask(t2);
            devTeam.AddProject(p1);
            myStartup.AddDepartment(itDept);
        }

        #endregion
    }
}
