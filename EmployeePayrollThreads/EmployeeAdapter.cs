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
        private object locker = new object();
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
        /// Method to add salary and employee to two separate tables
        /// using transaction, thread and locker
        public void AddEmployeeAndSalary(List<EmployeeDetail> employeeList,List<SalaryModel> salaryList)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB; Initial Catalog =payroll_ado; User ID = AkSharma; Password=abhishek123";
         SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlTransaction sqlTran = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;

                try
                {
                    //Locking a thread with the help of lockers 
                    //So one thread executes at one time
                    lock (locker) {
                        Thread thread = new Thread(() =>
                        {
                            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " starting");
                            salaryList.ForEach(salary =>
                            {

                                Console.WriteLine("Salary adding " + salary.SalaryId);
                                this.AddSalaryTransaction(salary);
                                Console.WriteLine(" Salary added: " + salary.SalaryId);

                            }
                        );
                            employeeList.ForEach(employeeData =>

                            {

                                Console.WriteLine("Employee adding: " + employeeData.employeeName);
                                this.AddEmployeeTransaction(employeeData);
                                Console.WriteLine(" Employee added: " + employeeData.employeeName);


                            }
                        );
                            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " ended");

                        });
                        thread.Start();
                        thread.Join();
                        sqlTran.Commit();
                    }
                    
                    
                    Console.WriteLine("Both records were written to database."); 
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        sqlTran.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                    
                }
            }
        }
        /// Method to add employee for UC5
        public void AddEmployeeTransaction(EmployeeDetail employee) {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB; Initial Catalog =payroll_ado; User ID = AkSharma; Password=abhishek123";
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection) {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                "insert into Employee_Payroll(name,gender,salaryid) values('" + employee.employeeName + "','" + employee.gender + "'," + employee.salaryID + ")";
                command.ExecuteNonQuery();
            }
        }
        /// Method to add salaries for UC5
        public void AddSalaryTransaction(SalaryModel salary) {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB; Initial Catalog =payroll_ado; User ID = AkSharma; Password=abhishek123";
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "insert into Payroll_Details(salaryid,salary) values(" + salary.SalaryId + "," + salary.basicPay + ")";
                command.ExecuteNonQuery();
            }
        }
    }
}
