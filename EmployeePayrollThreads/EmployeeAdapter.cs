using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollThreads
{
    public class EmployeeAdapter
    {
        
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
        public void AddToEmployeePayroll(List<EmployeeModels> employeeList) {
            employeeList.ForEach(employee =>
            {
                this.AddEmployee(employee);
                Console.WriteLine(employee.EmployeeName + " added");
            }
            );
        }
    }
}
