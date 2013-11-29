using System;
namespace TaxManager
{
    public interface ITaxDbProvider
    {
        decimal GetBaseTaxRate();
        System.Collections.Generic.List<TaxManager.Person> GetEmployees();

        decimal GetEducationCess();
    }
}
