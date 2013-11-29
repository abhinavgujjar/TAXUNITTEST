using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxManager;

namespace Tax.Test
{
    public class FakeAuditor: IGovtAudit
    {
        public string loggedPAN { get; set; }
        public decimal loggedTax { get; set; }

        public void AuditTaxCalculation(string PAN, decimal totalTax)
        {
            loggedPAN = PAN;
            loggedTax = totalTax;
        }
    }
}
