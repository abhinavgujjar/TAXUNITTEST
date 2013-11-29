using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxManager;
using System.Collections.Generic;
using Moq;

namespace Tax.Test
{
    [TestClass]
    public class TaxCalculationTest
    {
        TaxCalculator _calculator;

        public TaxCalculationTest()
        {
            _calculator = new TaxCalculator(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_For_Null_Employee()
        {
            //Arrange 
            var mock = new Mock<ITaxDbProvider>();
            mock.Setup(foo => foo.GetEmployees()).Returns((List<Person>)null);

            var _calculator = new TaxCalculator(mock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_For_Negative_Salary()
        {
            //Arrange 

            Person person = new Person()
            {

                Salary = -100000.0M
            };

            //ACT
            var taxReturn = _calculator.CalculateTax(person);
        }

        [TestMethod]
        public void Test_for_Male_10000()
        {
            //Arrange 

            Person person = new Person()
            {
                IsMale = true,
                Age = 44,
                Salary = 100000.0M

            };

            //ACT
            var taxReturn = _calculator.CalculateTax(person);

            //ASSERT
            Assert.AreEqual(0, taxReturn.Tax);
        }

        [TestMethod]
        public void Test_for_Female_20000()
        {
            //Arrange 

            Person person = new Person()
            {
                IsMale = false,
                Age = 44,
                Salary = 200000.0M

            };

            //ACT
            var taxReturn = _calculator.CalculateTax(person);

            //ASSERT
            Assert.AreEqual(0, taxReturn.Tax);
        }

        [TestMethod]
        public void Test_for_Male_60000()
        {
            //Arrange 

            Person person = new Person()
            {
                IsMale = true,
                Age = 44,
                Salary = 600000.0M

            };

            //ACT
            var taxReturn = _calculator.CalculateTax(person);

            //ASSERT
            Assert.AreEqual(40000, taxReturn.Tax);
        }

        [TestMethod]
        public void Test_for_female_60000()
        {
            //Arrange 

            Person person = new Person()
            {
                IsMale = false,
                Age = 44,
                Salary = 600000.0M

            };

            //ACT
            var taxReturn = _calculator.CalculateTax(person);

            //ASSERT
            Assert.AreEqual(30000, taxReturn.Tax);
        }

        [TestMethod]
        public void Test_for_male_120000()
        {
            //Arrange 

            Person person = new Person()
            {
                IsMale = true,
                Age = 44,
                Salary = 1200000.0M

            };

            //ACT
            var taxReturn = _calculator.CalculateTax(person);

            //ASSERT
            Assert.AreEqual(120000, taxReturn.Tax);
        }

        [TestMethod]
        public void Test_for_female_120000()
        {
            //Arrange 

            Person person = new Person()
            {
                IsMale = false,
                Age = 44,
                Salary = 1200000.0M

            };

            //ACT
            var taxReturn = _calculator.CalculateTax(person);

            //ASSERT
            Assert.AreEqual(110000, taxReturn.Tax);
        }

        [TestMethod]
        public void Test_for_Auditing()
        {
            //Arrange 

            Person person = new Person()
            {
                IsMale = false,
                Age = 44,
                Salary = 1200000.0M,
                PAN = "AA66779JJK",
            };

            FakeAuditor auditor = new FakeAuditor();
            FakeDbProvider provider = new FakeDbProvider(
                new List<Person>(){
                person});

            TaxCalculator calculator = new TaxCalculator(provider, auditor);

            //ACT
            var taxReturn = calculator.CalculateTax(person);

            //ASSERT
            Assert.AreEqual("AA66779JJK", auditor.loggedPAN);
            Assert.AreEqual(110000, auditor.loggedTax);

        }
    }
}
