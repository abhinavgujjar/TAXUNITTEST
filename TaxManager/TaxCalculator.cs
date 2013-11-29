using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxManager;


namespace TaxManager
{
    public class TaxCalculator : TaxManager.ITaxCalculator
    {
        ITaxDbProvider _rateProvider;
       
        IGovtAudit _auditor;

        public TaxCalculator(ITaxDbProvider provider)
        {
            _rateProvider = provider;
        }

        public TaxCalculator(ITaxDbProvider provider, IGovtAudit auditor)
        {
            _rateProvider = provider;
            _auditor = auditor;
        }
        
        public TaxReturn CalculateTax(Person person)
        {
            decimal taxRate = _rateProvider.GetBaseTaxRate();

            TaxReturn taxReturn = new TaxReturn();

            if (person == null)
            {
                throw new ArgumentNullException();
            }

            if (person.Salary < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (person.IsMale)
            {
                CalculateTaxForMales(person, taxReturn, taxRate);
            }
            else
            {
                CalculateTaxForFemales(person, taxReturn, taxRate);
            }

            if (_auditor != null)
            {
                _auditor.AuditTaxCalculation(person.PAN, taxReturn.Tax);
            }

            return taxReturn;

        }

        private static void CalculateTaxForFemales(Person person, TaxReturn taxReturn, decimal taxRate)
        {
            if (person.Salary <= 300000)
            {
                taxReturn.Tax = 0;
            }
            else if (person.Salary > 300000 && person.Salary <= 1000000)
            {
                taxReturn.Tax = (person.Salary - 300000) * taxRate / 100;
            }
            else
            {
                taxReturn.Tax = (700000 * taxRate / 100) + ((person.Salary - 1000000) * (taxRate*2) / 100);
            }
        }

        private static void CalculateTaxForMales(Person person, TaxReturn taxReturn, decimal taxRate)
        {
            if (person.Salary <= 200000)
            {
                taxReturn.Tax = 0;
            }
            else if (person.Salary > 200000 && person.Salary <= 1000000)
            {
                taxReturn.Tax = (person.Salary - 200000) * taxRate / 100;
            }
            else
            {
                taxReturn.Tax = (800000 * taxRate / 100) + ((person.Salary - 1000000) * (taxRate*2) / 100);
            }
        }
    }
}