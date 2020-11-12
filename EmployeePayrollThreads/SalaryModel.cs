using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayrollThreads
{
   public class SalaryModel
    {
        public int SalaryId { get; set; }
        public decimal basicPay { get; set; }
        public decimal deduction { get; set; }
        public decimal taxablePay { get; set; }
        public decimal incomeTax { get; set; }
        public decimal netPay { get; set; }

        public SalaryModel(int salaryId, decimal basicPay)
        {
            SalaryId = salaryId;
            this.basicPay = basicPay;
        }
    }
}
