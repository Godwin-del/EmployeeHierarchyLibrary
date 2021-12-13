using Employees.Models;

namespace Employees.Functions
{
    public static class LibFunctions
    {
        static int Row = 0;
        public static bool ValidInteger(string StringValue)
        {
            return (int.TryParse(StringValue, out _));
        }
        public static List<CsvInfo> CsvValidIntegerToList(string CsvFilePath)
        {
            try
            {
                List<CsvInfo> DataList = new();
                foreach (string line in File.ReadLines(CsvFilePath))
                {
                    Row++;
                    string[] DataArray = line.Split(",");
                    if (ValidInteger(DataArray[2]))
                    {
                        CsvInfo info = new()
                        {
                            EmployeeId = DataArray[0],
                            ManagerId = DataArray[1],
                            EmployeeSalary = Convert.ToInt32(DataArray[2])
                        };
                        DataList.Add(info);
                    }
                    if (!ValidInteger(DataArray[2]))
                    {
                        throw new Exception("Row Number " + Row + " has invalid integer. " + DataArray[2]);
                    }
                }
                return DataList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<CsvInfo> EmployeeToOneManager(List<CsvInfo> DataList)
        {
            var Employees = DataList.GroupBy(employee => employee.EmployeeId);

            foreach (var EmployeeCount in Employees)
            {
                if (EmployeeCount.Count() > 1)
                {
                    throw new Exception("Employee " + EmployeeCount.Key + " reports to more than one manager");
                }
            }
            return DataList;
        }
        public static List<CsvInfo> CeoCheck(List<CsvInfo> DataList)
        {
            List<CsvInfo> lstceos = new();
            foreach (CsvInfo csvInfo in DataList)
            {
                int occurrence = 0;
                //for (int i = 0; i < DataList.Count; i++)
                //{
                if (DataList.Find(obj => obj.EmployeeId == csvInfo.EmployeeId && obj.ManagerId == String.Empty) != null)
                {
                    occurrence++;
                    lstceos.Add(csvInfo);
                }
                //}
                if (lstceos.Count > 1)
                {
                    throw new Exception("CEO repeated. Only one CEO is allowed.");
                }

            }
            return DataList;
        }
        public static List<CsvInfo> CircularReference(List<CsvInfo> DataList)
        {
            //List<CsvInfo> lstcircularoccurence = new();
            IDictionary<string, string> lstcircularoccurence = new Dictionary<string, string>();

            foreach (CsvInfo csvInfo in DataList)
            {
                lstcircularoccurence.Add(csvInfo.EmployeeId, csvInfo.ManagerId);
            }
            foreach (var csvInfo in DataList)
            {
                string i = csvInfo.EmployeeId + csvInfo.ManagerId;
                string inverse = csvInfo.ManagerId + csvInfo.EmployeeId;
                var search = DataList.Find(obj => obj.ManagerId == csvInfo.EmployeeId && obj.EmployeeId == csvInfo.ManagerId);
                if (DataList.Find(obj => obj.ManagerId == csvInfo.EmployeeId && obj.EmployeeId == csvInfo.ManagerId) != null)
                {
                    throw new Exception("Circular Reference detected " + csvInfo.EmployeeId + " " + csvInfo.ManagerId + " and " + csvInfo.ManagerId + " " + csvInfo.EmployeeId);
                }
            }
            return DataList;
        }
        public static List<CsvInfo> ManagerThatIsNotEmployee(List<CsvInfo> DataList)
        {
            //get managers
            List<string> employees = new();
            foreach (var item in DataList)
            {
                employees.Add(item.EmployeeId);
            }
            foreach (CsvInfo csvInfo in DataList)
            {
                if (!employees.Contains(csvInfo.ManagerId) && !string.IsNullOrEmpty(csvInfo.ManagerId))
                {
                    throw new Exception("Manager " + csvInfo.ManagerId + " is not listed as an employee.");
                }
            }
            return DataList;
        }
        public static int ManagerSalaryBudget(string Manager)
        {
            List<CsvInfo> DataList = new();
            List<CsvInfo> DirectEmployeesList = new();
            List<string> ManagersList = new();
            List<CsvInfo> Employeelisttosumm = new();
            List<string> managers = new();
            List<string> salaries = new();
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
                DirectEmployeesList = null;
                DirectEmployeesList.AddRange(EmployeesManagersList);
            }
            int sum = DirectEmployeesList.Sum(salary => salary.EmployeeSalary);
            return sum;
        }
    }
}
