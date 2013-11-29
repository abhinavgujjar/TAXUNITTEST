using System;
namespace TaxManager
{
    public interface ITaxCalculator
    {
        TaxReturn CalculateTax(Person person);
    }
}
