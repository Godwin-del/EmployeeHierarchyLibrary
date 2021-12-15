using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LibUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Employee_1_SalaryTest()
        {
            var Employee1_SalaryTest = AppDomain.CurrentDomain.BaseDirectory + "\\TestFile/CsvFile.txt";
            Employees.Emloyeelib emloyeelib = new(Employee1_SalaryTest);
            long Salary = emloyeelib.ManagerSalaryBudget("Employee1");
            Assert.AreEqual(Salary, 3800, Salary);
        }
        [TestMethod]
        public void Employee_2_SalaryTest()
        {
            var Employee2_SalaryTest = AppDomain.CurrentDomain.BaseDirectory + "\\TestFile/CsvFile.txt";
            Employees.Emloyeelib emloyeelib = new(Employee2_SalaryTest);
            long Salary = emloyeelib.ManagerSalaryBudget("Employee2");
            Assert.AreEqual(Salary, 1800, Salary);
        }
        [TestMethod]
        public void Employee_3_SalaryTest()
        {
            var Employee3_SalaryTest = AppDomain.CurrentDomain.BaseDirectory + "\\TestFile/CsvFile.txt";
            Employees.Emloyeelib emloyeelib = new(Employee3_SalaryTest);
            long Salary = emloyeelib.ManagerSalaryBudget("Employee3");
            Assert.AreEqual(Salary, 500, Salary);
        }
        //. One employee does not report to more than one manager
        [TestMethod]
        public void ConstructorValidations()
        {
            try
            {
                var Employee1_SalaryTest = AppDomain.CurrentDomain.BaseDirectory + "\\TestFile/CsvFile_Employee_To_Manager.txt";
                Employees.Emloyeelib emloyeelib = new(Employee1_SalaryTest);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
