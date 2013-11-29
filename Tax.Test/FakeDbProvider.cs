using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxManager;

namespace Tax.Test
{
    class FakeDbProvider : ITaxDbProvider
    {
        private List<Person> _employees;

        public FakeDbProvider(List<Person> employees)
        {
            _employees = employees;
        }

        public decimal GetBaseTaxRate()
        {
            return 10.0M;
        }

        public List<Person> GetEmployees()
        {
            return _employees;
        }


        public decimal GetEducationCess()
        {
            throw new NotImplementedException();
        }
    }
}
