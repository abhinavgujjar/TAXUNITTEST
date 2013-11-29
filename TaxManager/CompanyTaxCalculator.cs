using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxManager
{
    public class CompanyTaxCalculator
    {
        ITaxDbProvider _dbProvider;
        ITaxCalculator _calculator;

        public CompanyTaxCalculator(ITaxDbProvider dbProvider, ITaxCalculator calculator)
        {
            _dbProvider = dbProvider;
            _calculator = calculator;
        }

        public decimal GetTotalEmployeeTax()
        {
            var employees = _dbProvider.GetEmployees();

            var totalTax = 0.0M;
            foreach (var employee in employees)
            {
                var employeeTax = _calculator.CalculateTax(employee).Tax;
                totalTax = totalTax + employeeTax;
            }

            return totalTax;
        }

    }
}
