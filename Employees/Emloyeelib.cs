using Employees.Functions;
using Employees.Models;

namespace Employees
{
    public class Emloyeelib
    {
        readonly List<CsvInfo> DataList = new();
        public Emloyeelib(string Csv)
        {
            //validate intergers
            DataList = new List<CsvInfo>(LibFunctions.CsvValidIntegerToList(Csv));
            //One employee does not report to more than one manager check
            DataList = new List<CsvInfo>(LibFunctions.EmployeeToOneManager(DataList));
            //There is only one CEO, i.e. only one employee with no manager check
            DataList = new List<CsvInfo>(LibFunctions.CeoCheck(DataList));
            //There is no circular reference, i.e. a first employee reporting to a second employee that is also under the first employee.
            DataList = new List<CsvInfo>(LibFunctions.CircularReference(DataList));
            // There is no manager that is not an employee, i.e. all managers are also listed in the employee column.
            DataList = new List<CsvInfo>(LibFunctions.ManagerThatIsNotEmployee(DataList));
        }
        public int ManagerSalaryBudget(string Manager)
        {
            List<CsvInfo> DirectEmployeesList = new();
            List<string> ManagersList = new();
            List<CsvInfo> Employeelisttosumm = new();
            int managerssalary = 0;
            //get manager's salary
            var managerdata = DataList.Find(obj => obj.EmployeeId == Manager);
            managerssalary = managerdata.EmployeeSalary;

            //get managers list
            foreach (var item in DataList)
            {
                ManagersList.Add(item.ManagerId);
            }
            //get direct employees
            foreach (var item in DataList)
            {
                if (item.ManagerId == Manager)
                {
                    DirectEmployeesList.Add(item);
                }
                //managers.Add(item.ManagerId);
            }
            Employeelisttosumm.AddRange(DirectEmployeesList);
            //check if they are managers, if they appear in manager list it means they have employees reporting to them.
            List<CsvInfo> EmployeesManagersList = new();
            while (DirectEmployeesList.Count != 0)
            {
                foreach (var employee in DirectEmployeesList)
                {
                    if (ManagersList.Contains(employee.EmployeeId))
                    {
                        EmployeesManagersList.Add(employee);
                    }
                }

                DirectEmployeesList = new List<CsvInfo>();
                //get the employees reporting
                foreach (var item in DataList)
                {
                    for (int i = 0; i < EmployeesManagersList.Count; i++)
                    {
                        if (item.ManagerId == EmployeesManagersList[i].EmployeeId)
                        {
                            DirectEmployeesList.Add(item);
                        }
                    }

                }
                break;
            }

            Employeelisttosumm.AddRange(DirectEmployeesList);
            int sum = Employeelisttosumm.Sum(salary => salary.EmployeeSalary) + managerssalary;
            return sum;
        }
    }
}