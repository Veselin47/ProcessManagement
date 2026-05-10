using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessManagement
{
    public class Investor
    {
        public string Name { get; set; }
        public decimal InvestedAmount { get; set; }
        public DateTime InvestmentDate { get; set; }

        public Investor(string name, decimal investedAmount, DateTime investmentDate)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (investedAmount <= 0) throw new ArgumentOutOfRangeException(nameof(investedAmount), "Invested amount must be greater than zero.");
            
            this.Name = name;
            this.InvestedAmount = investedAmount;
            this.InvestmentDate = investmentDate;
        }
    }
}
