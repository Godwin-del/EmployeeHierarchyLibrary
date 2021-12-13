class Program
{
    static void Main(string[] args)
    {
        try
        {
            string Employee1_SalaryTest = "Employee4,Employee2,500"
                                          + Environment.NewLine + "Employee3,Employee1,500"
                                          + Environment.NewLine + "Employee1,,1000 "
                                          + Environment.NewLine + "Employee5,Employee1,500"
                                          + Environment.NewLine + "Employee2,Employee1,800"
                                          + Environment.NewLine + "Employee6,Employee2,500";
            var dr = AppDomain.CurrentDomain.BaseDirectory + "\\TestFiles";
            var dir = @"C:\Users\Support\Downloads\CsvFile.csv";
            Employees.Emloyeelib employeelib = new(dir);
            var salary = employeelib.ManagerSalaryBudget("Employee1");
            Console.WriteLine("Employee3 salary is : Ksh. " + salary);
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }
}