using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessManagement
{
    public class TextSaveStrategy : ISaveStrategy
    {
        public void Save(Company company, string fileName)
        {
            System.IO.File.WriteAllText(fileName + ".txt", $"Company: {company.Name}\nCapital: {company.Capital}");
            Console.WriteLine("Saved in text format (simplified).");
        }
    }
}
