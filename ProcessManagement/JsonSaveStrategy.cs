using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessManagement
{
    public class JsonSaveStrategy : ISaveStrategy
    {
        public void Save(Company company, string fileName)
        {
            var options = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };
            string json = System.Text.Json.JsonSerializer.Serialize(company, options);
            System.IO.File.WriteAllText(fileName + ".json", json);
            Console.WriteLine($"Data successfully saved in {fileName}.json"); 
        }
    }
}
