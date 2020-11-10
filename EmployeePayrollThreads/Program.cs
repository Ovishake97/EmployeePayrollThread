using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EmployeePayrollThreads
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeAdapter adapter = new EmployeeAdapter();
            List<EmployeeModels> employeeModels = new List<EmployeeModels>();
            employeeModels.Add(new EmployeeModels(7, "Amrit", 'M', "Kolkata"));
            employeeModels.Add(new EmployeeModels(8, "Boris", 'M', "Morocco"));
            employeeModels.Add(new EmployeeModels(9, "Lalita", 'F', "Kochi"));
            employeeModels.Add(new EmployeeModels(10, "Sultan", 'M', "London"));
            employeeModels.Add(new EmployeeModels(11, "Hannah", 'F', "Prague"));
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            adapter.AddToEmployeePayroll_UsingThreads(employeeModels);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
