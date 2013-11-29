using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxManager
{
    public class TaxDbProvider 
    {
        public decimal GetBaseTaxRate()
        {
            //Dbcommand db = new DbCommand
            //
            return 10.0M;
        }

        public List<Person> GetEmployees()
        {
            throw new NotImplementedException();
        }
    }
}
