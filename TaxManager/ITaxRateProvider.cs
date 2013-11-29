using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxManager
{
    public interface ITaxRateProvider
    {
        decimal GetTaxRate(string state);

        decimal GetPersonTaxRateBase();
    }
}
