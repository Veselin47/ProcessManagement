using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessManagement
{
    public class TextSaveStrategy : ISaveStrategy
    {
        public void Save(Company company, string fileName)
        {
            System.IO.File.WriteAllText(fileName + ".txt", $"Компания: {company.Name}\nКапитал: {company.Capital}");
            Console.WriteLine("Записано в текстов формат (опростено).");
        }
    }
}
