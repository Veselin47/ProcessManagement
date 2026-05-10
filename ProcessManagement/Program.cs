using ProcessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using static ProcessManagement.Task;

namespace ProcessManagement
{
   
    class Program
    {
        // Статична променлива за достъп до Singleton инстанцията
        static Company myStartup;

        static void Main(string[] args)
        {
            // 1. Инициализиране на Singleton данните
            InitializeSampleData();

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine($"=== УПРАВЛЕНИЕ НА СТАРТЪП: {myStartup.Name} ===");
                Console.WriteLine("1. Показване на пълна йерархия и проекти");
                Console.WriteLine("2. Управление на служител (Заплата/Рейтинг)");
                Console.WriteLine("3. Управление на задачи (Промяна на статус)");
                Console.WriteLine("4. Инвестиране на капитал");
                Console.WriteLine("5. Справка за проект (Прогрес %)");
                Console.WriteLine("6. Запис на данни във файл (JSON Strategy)");
                Console.WriteLine("0. Изход");
                Console.Write("\nИзберете опция: ");

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
                    default: Console.WriteLine("Грешен избор. Натиснете клавиш..."); Console.ReadKey(); break;
                }
            }
        }

        #region UI Logic Methods

        static void DisplayHierarchyUI()
        {
            Console.Clear();
            myStartup.DisplayHierarchy();
            Console.WriteLine("\nНатиснете клавиш за връщане...");
            Console.ReadKey();
        }

        static void EmployeeManagementUI()
        {
            Console.Write("Въведете име на служител: ");
            string name = Console.ReadLine();
            Employee emp = FindEmployee(name);

            if (emp != null)
            {
                Console.WriteLine($"Намерен: {emp.Position} {emp.Name}");
                Console.WriteLine("1. Промяна на заплата | 2. Промяна на рейтинг");
                string op = Console.ReadLine();
                if (op == "1")
                {
                    Console.Write("Нова заплата: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal sal))
                        emp.UpdateSalary(sal);
                }
                else if (op == "2")
                {
                    Console.Write("Нов рейтинг (0.0 - 5.0): ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal rat))
                        emp.UpdatePerformanceRating(rat);
                }
            }
            else Console.WriteLine("Служителят не е намерен.");
            Console.ReadKey();
        }

        static void TaskManagementUI()
        {
            Console.Write("Въведете заглавие на задача: ");
            string title = Console.ReadLine();
            Task task = FindTask(title);

            if (task != null)
            {
                Console.WriteLine("Изберете нов статус: 1. InProgress 2. Completed 3. Rejected");
                string st = Console.ReadLine();
                // Извикваме метода ChangeStatus от твоя клас Task
                if (st == "1") task.ChangeStatus(Task.TaskStatus.InProgress);
                else if (st == "2") task.ChangeStatus(Task.TaskStatus.Completed);
                else if (st == "3") task.ChangeStatus(Task.TaskStatus.Rejected);
            }
            else Console.WriteLine("Задачата не е намерена.");
            Console.ReadKey();
        }

        static void ProjectStatusUI()
        {
            foreach (var dept in myStartup.Departments)
                foreach (var team in dept.Teams)
                    foreach (var proj in team.Projects)
                    {
                        // Използваме твоето име на метод: GetProjectProgress
                        Console.WriteLine($"Проект: {proj.Name} | Прогрес: {proj.GetProjectProgress()}%");
                    }
            Console.WriteLine("\nНатиснете клавиш...");
            Console.ReadKey();
        }

        static void InvestmentUI()
        {
            Console.Write("Име на инвеститор: ");
            string invName = Console.ReadLine();
            Console.Write("Сума за инвестиране: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Investor inv = new Investor(invName, amount, DateTime.Now);
                myStartup.AddInvestor(amount, inv);
                Console.WriteLine("Инвестицията е приета успешно!");
            }
            Console.ReadKey();
        }

        static void SaveDataUI()
        {
            Console.WriteLine("Избор на стратегия за запис: JSON");
            // Използваме шаблона Strategy
            ISaveStrategy strategy = new JsonSaveStrategy();
            myStartup.Save(strategy, "startup_backup");

            Console.WriteLine("Натиснете клавиш...");
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
            // ПРИЛАГАНЕ НА SINGLETON: Инициализираме чрез статичния метод
            Company.Initialize("Alpha Tech", 100000, 2024, "ul. Vitosha 1", "contact@alpha.com", "0888112233");

            // Вземаме инстанцията
            myStartup = Company.Instance;

            // Създаваме примерни обекти
            Employee boss = new Employee("Георги Георгиев", "Мениджър", new DateTime(1980, 1, 1), 5000, 10, 4.8m);
            Employee dev = new Employee("Иван Петров", "Програмист", new DateTime(1995, 5, 20), 3000, 3, 4.2m);

            Department itDept = new Department("IT", 2024, boss);
            Team devTeam = new Team("Web Team", boss);

            devTeam.AddMember(dev);
            devTeam.AddMember(boss);

            Project p1 = new Project("Mobile App", DateTime.Now, DateTime.Now.AddMonths(2));
            Task t1 = new Task("UI Design", DateTime.Now, DateTime.Now.AddDays(10), TaskPriority.High, dev);

            p1.AddTask(t1);
            devTeam.AddProject(p1);
            itDept.AddTeam(devTeam);
            myStartup.AddDepartment(itDept);
        }

        #endregion
    }
}