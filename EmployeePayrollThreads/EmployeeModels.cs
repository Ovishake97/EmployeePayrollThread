using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayrollThreads
{
   public class EmployeeModels
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public char Gender { get; set; }
    public string Address { get; set; }

        public EmployeeModels(int employeeID, string employeeName, char gender, string address)
        {
            EmployeeID = employeeID;
            EmployeeName = employeeName;
            Gender = gender;
            Address = address;
        }
        public EmployeeModels() { 
            //default constructor
        }
    }
}
