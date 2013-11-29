using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxManager;

namespace Tax.Test
{
    [TestClass]
    public class CompanyTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_For_Null_EmployeeList()
        {
            //Arrange


        }

        [TestMethod]
        public void Test_For_Empty_List()
        {

            //what should happend? 

            //output should be 0

        }

        [TestMethod]
        public void Test_For_Aggregation()
        {
            //Arrange
            var mock = new Mock<ITaxDbProvider>();
            List<Person> employees = new List<Person>()
            {
                new Person(),
                new Person(),
                new Person()
            };
            mock.Setup(foo => foo.GetEmployees()).Returns(employees);


            var mockCalc = new Mock<ITaxCalculator>();
            mockCalc.Setup(foo => foo.CalculateTax(It.IsAny<Person>()))
                .Returns(new TaxReturn() { Tax = 1000.0M });

            //Act 
            CompanyTaxCalculator calculator = new CompanyTaxCalculator(mock.Object, mockCalc.Object);
            var totalTax = calculator.GetTotalEmployeeTax();

            //Assert
            Assert.AreEqual(3000, totalTax);
        }

        [TestMethod]
        public void Test_For_Aggregation2()
        {
            //Arrange
            var mock = new Mock<ITaxDbProvider>();
            List<Person> employees = new List<Person>()
            {
                new Person() { Name = "X"},
                new Person(){ Name = "Y"},
                new Person(){ Name = "Z"}
            };
            mock.Setup(foo => foo.GetEmployees()).Returns(employees);


            var mockCalc = new Mock<ITaxCalculator>();
            mockCalc.Setup(foo => foo.CalculateTax(It.IsAny<Person>() ))
                .Returns(new TaxReturn() { Tax = 1000.0M });

            //Act 
            CompanyTaxCalculator calculator = new CompanyTaxCalculator(mock.Object, mockCalc.Object);
            var totalTax = calculator.GetTotalEmployeeTax();

            //Assert
            Assert.AreEqual(3000, totalTax);
        }

        [TestMethod]
        public void Test_For_Auditing()
        {
            //Arrange
            var mock = new Mock<ITaxDbProvider>();
            List<Person> employees = new List<Person>()
            {
                new Person() { Name = "X", PAN = "XXXTTT"},
                new Person(){ Name = "Y"},
                new Person(){ Name = "Z"}
            };
            mock.Setup(foo => foo.GetEmployees()).Returns(employees);


            var mockCalc = new Mock<ITaxCalculator>();
            mockCalc.Setup(foo => foo.CalculateTax(It.IsAny<Person>()))
                .Returns(new TaxReturn() { Tax = 1000.0M });

            var mockAuditor = new Mock<IGovtAudit>();


            //Act 
            CompanyTaxCalculator calculator = new CompanyTaxCalculator(
                mock.Object, mockCalc.Object, mockAuditor.Object);

            var totalTax = calculator.GetTotalEmployeeTax();

            //Assert
            //check that the audit function was invoked

            mockAuditor.Verify(foo => foo.AuditTaxCalculation(
                It.IsAny<String>(), It.IsAny<decimal>()), Times.AtLeastOnce);

            mockAuditor.Verify(foo => foo.AuditTaxCalculation(
                "XXXTTT", It.IsAny<decimal>()), Times.AtLeastOnce);


        }
        [TestMethod]
        public void Test_For_Performance()
        {
            //Arrange
            var mock = new Mock<ITaxDbProvider>();
            List<Person> employees = new List<Person>()
            {
                new Person() { Name = "X", PAN = "XXXTTT"},
                new Person(){ Name = "Y"},
                new Person(){ Name = "Z"}
            };
            mock.Setup(foo => foo.GetEmployees()).Returns(employees);


            var mockCalc = new Mock<ITaxCalculator>();
            mockCalc.Setup(foo => foo.CalculateTax(It.IsAny<Person>()))
                .Returns(new TaxReturn() { Tax = 1000.0M });

            var mockAuditor = new Mock<IGovtAudit>();


            //Act 
            CompanyTaxCalculator calculator = new CompanyTaxCalculator(
                mock.Object, mockCalc.Object, mockAuditor.Object);

            var totalTax = calculator.GetTotalEmployeeTax();

            //Assert
            //check that the audit function was invoked

            mock.Verify(foo => foo.GetEducationCess(), Times.AtMostOnce);


        }

        public object List { get; set; }
    }
}
