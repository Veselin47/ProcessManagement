using System;
using System.Collections.Generic;
using System.Text;
namespace ProcessManagement
{
    public class Company
    {
        private static Company _instance;

        public string Name { get; set; }
        public decimal Capital { get; set; }
        public int FoundingYear { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();
        public List<Investor> Investors { get; set; } = new List<Investor>();

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

        public static void Initialize(string name, decimal capital, int foundingYear, string address, string email, string phoneNumber)
        {
            if (_instance == null)
            {
                _instance = new Company(name, capital, foundingYear, address, email, phoneNumber);
            }
        }

        public static Company Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new Exception("Company not initialized! Call Initialize() first.");
                }
                return _instance;
            }
        }

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

        public void Save(ISaveStrategy strategy, string fileName)
        {
            strategy.Save(this, fileName);
        }

        public void DisplayHierarchy()
        {
            Console.WriteLine($"Company: {Name} | Capital: {Capital:C2}");


            foreach (var department in Departments)
            {
                department.Display(1); // Стартираме с отстъп 1
            }
        }
    }
}