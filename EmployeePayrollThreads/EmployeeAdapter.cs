using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeePayrollThreads
{
    public class EmployeeAdapter
    {
        
        /// Defining a function to add an employee object to 
        /// the sql table
        public void AddEmployee(EmployeeModels employee)
        {
              string connectionString = "Server=(localdb)\\MSSQLLocalDB; Initial Catalog =payroll_service; User ID = AkSharma; Password=abhishek123";
         SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("dbo.AddEmployeeDetails",connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", employee.EmployeeID);
                    command.Parameters.AddWithValue("@name", employee.EmployeeName); ;
                    command.Parameters.AddWithValue("@gender", employee.Gender);
                    command.Parameters.AddWithValue("@address", employee.Address);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Added sucessfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {
                connection.Close();
            }
        }
        /// Method to add the employee details from the list
        /// to the table
        public void AddToEmployeePayroll(List<EmployeeModels> employeeList) {
            employeeList.ForEach(employee =>
            {
                this.AddEmployee(employee);
                Console.WriteLine(employee.EmployeeName + " added");
            }
            );
        }
        /// Method to add employee details from the list
        /// to the table with the help of separate threads
        public void AddToEmployeePayroll_UsingThreads(List<EmployeeModels> employeeList) {
            
                employeeList.ForEach(employeeData =>
                {
                    Thread thread = new Thread(() =>
                    {
                        Console.WriteLine(Thread.CurrentThread.ManagedThreadId+" starting");
                        Console.WriteLine("Employee adding: "+employeeData.EmployeeName);
                        this.AddEmployee(employeeData);
                        Console.WriteLine(" Employee added: " + employeeData.EmployeeName);
                        Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " ended");
                    });

                    thread.Start();
                    thread.Join();
                });
            }
    }
}
