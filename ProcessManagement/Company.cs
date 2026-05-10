using System;
using System.Collections.Generic;
using System.Text;
namespace ProcessManagement
{
    public class Company
    {
        // 1. Статично поле за единствената инстанция
        private static Company _instance;

        // Свойствата остават същите
        public string Name { get; set; }
        public decimal Capital { get; set; }
        public int FoundingYear { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();
        public List<Investor> Investors { get; set; } = new List<Investor>();

        // 2. ПРОМЯНА: Конструкторът става PRIVATE
        private Company(string name, decimal capital, int foundingYear, string address, string email, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (capital <= 0) throw new ArgumentOutOfRangeException(nameof(capital), "Capital cannot be negative.");
            if (foundingYear < 0) throw new ArgumentOutOfRangeException(nameof(foundingYear), "Founding year cannot be negative.");

            this.Name = name;
            this.Capital = capital;
            this.FoundingYear = foundingYear;
            this.Address = address;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }

        // 3. НОВ МЕТОД: Първоначално създаване на компанията
        public static void Initialize(string name, decimal capital, int foundingYear, string address, string email, string phoneNumber)
        {
            if (_instance == null)
            {
                _instance = new Company(name, capital, foundingYear, address, email, phoneNumber);
            }
        }

        // 4. НОВО СВОЙСТВО: Глобален достъп до обекта
        public static Company Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Ако забравиш да заредиш данни, това ще те подсети
                    throw new Exception("Company not initialized! Call Initialize() first.");
                }
                return _instance;
            }
        }

        // Твоите методи за управление (AddDepartment, RemoveDepartment и т.н. си остават същите)
        public void AddDepartment(Department department)
        {
            if (department != null) Departments.Add(department);
            else Console.WriteLine("Department cannot be null.");
        }

        public void RemoveDepartment(Department department)
        {
            if (department != null)
            {
                Departments.Remove(department);
                Console.WriteLine($"Removed department {department.Name}.");
            }
        }

        public void AddInvestor(decimal amount, Investor investor)
        {
            if (investor == null) return;
            Investors.Add(investor);
            Capital += amount;
            Console.WriteLine($"Added investor {investor.Name}. New capital: {Capital:C2}");
        }

        // Метод за Стратегията (за да работи с ISaveStrategy)
        public void Save(ISaveStrategy strategy, string fileName)
        {
            strategy.Save(this, fileName);
        }

        public void DisplayHierarchy()
        {
            Console.WriteLine($"Company: {Name} | Capital: {Capital:C2}");
            foreach (var department in Departments)
            {
                Console.WriteLine($"\tDepartment: {department.Name}, Manager: {department.Manager.Name}");
                foreach (var team in department.Teams)
                {
                    Console.WriteLine($"\t\tTeam: {team.Name}, Leader: {team.Leader.Name}");
                    foreach (var member in team.Members)
                    {
                        Console.Write($"\t\t\tMember: {member.Name}, Position: {member.Position}");
                        var personalTasks = team.Projects
                            .SelectMany(p => p.Tasks)
                            .Where(t => t.ResponsibleEmployee.Name == member.Name);

                        Console.WriteLine($" | Tasks: {personalTasks.Count()}");
                    }
                }
            }
        }
    }
}