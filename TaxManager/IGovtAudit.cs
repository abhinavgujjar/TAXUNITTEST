using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxManager
{
    public interface IGovtAudit
    {
        void AuditTaxCalculation(string PAN, decimal tax);
    }
}
