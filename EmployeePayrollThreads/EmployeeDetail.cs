using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayrollThreads
{
    public class EmployeeDetail
    {
        public EmployeeDetail(int employeeID, string employeeName, string gender, int salaryID)
        {
            this.employeeID = employeeID;
            this.employeeName = employeeName;
            this.gender = gender;
            this.salaryID = salaryID;
        }

        public int employeeID { get; set; }
        public string employeeName { get; set; }
        public string gender { get; set; }
        public int salaryID { get; set; }
    }
}
