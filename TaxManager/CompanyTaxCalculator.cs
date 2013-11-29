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
        IGovtAudit _auditor;

        public CompanyTaxCalculator(ITaxDbProvider dbProvider, ITaxCalculator calculator)
        {
            _dbProvider = dbProvider;
            _calculator = calculator;
        }

        public CompanyTaxCalculator(ITaxDbProvider dbProvider, ITaxCalculator calculator, IGovtAudit auditor)
        {
            _dbProvider = dbProvider;
            _calculator = calculator;
            _auditor = auditor;
        }

        public decimal GetTotalEmployeeTax()
        {
            var employees = _dbProvider.GetEmployees();
            var cess = _dbProvider.GetEducationCess();

            var totalTax = 0.0M;
            foreach (var employee in employees)
            {
                var employeeTax = _calculator.CalculateTax(employee).Tax;
                
                totalTax = totalTax + employeeTax + cess;

                _auditor.AuditTaxCalculation(employee.PAN, employeeTax);
            }

            return totalTax;
        }

    }
}
