using EmployeePayrollThreads;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MSTest
{
    [TestClass]
    public class UnitTest1
    {
        private object locker = null;
        EmployeeAdapter adapter = null;
        List<EmployeeModels> employeeModels;
        [TestInitialize]
        public void SetUp() {
            adapter = new EmployeeAdapter();
            employeeModels = new List<EmployeeModels>();
            locker = new object();
        }
        /// Adding a list of employees
        /// and adding each employee to the table
        /// and printing out the duration of execution
        [TestMethod]
        public void EntersEmployeeAddsToTheTable()
        {
            employeeModels.Add(new EmployeeModels(1, "John", 'M', "Chicago"));
            employeeModels.Add(new EmployeeModels(2, "Tim", 'M', "Quebec"));
            employeeModels.Add(new EmployeeModels(3, "Antony", 'M', "Venice"));
            employeeModels.Add(new EmployeeModels(4, "Rebecca", 'F', "Prague"));
            employeeModels.Add(new EmployeeModels(5, "Ramesh", 'M', "Surat"));
            employeeModels.Add(new EmployeeModels(6, "Freda", 'F', "Oslo"));
            DateTime startTime = DateTime.Now;
            adapter.AddToEmployeePayroll(employeeModels);
            DateTime stopTime = DateTime.Now;
            Console.WriteLine("Duration "+(stopTime-startTime));
        }
        [TestMethod]
        public void EntersEmployeeObjectAddsToTheTable_Threading() {
            employeeModels.Add(new EmployeeModels(7,"Amrit",'M',"Kolkata"));
            employeeModels.Add(new EmployeeModels(8, "Boris", 'M', "Morocco"));
            employeeModels.Add(new EmployeeModels(9, "Lalita", 'F', "Kochi"));
            employeeModels.Add(new EmployeeModels(10, "Sultan", 'M', "London"));
            employeeModels.Add(new EmployeeModels(11, "Hannah", 'F', "Prague"));
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            adapter.AddToEmployeePayroll_UsingThreads(employeeModels);
            stopwatch.Stop();
            Console.WriteLine("Durarion "+stopwatch.ElapsedMilliseconds+" milliseconds");
        }
        [TestMethod]
        public void EntersEmployeeObjectAddsToTheTable_Synchronised() {
            employeeModels.Add(new EmployeeModels(12,"Prateek",'M',"Cuttack"));
            employeeModels.Add(new EmployeeModels(13, "Freddie", 'M', "Venice"));
            employeeModels.Add(new EmployeeModels(14, "Julia", 'F', "London"));
            employeeModels.Add(new EmployeeModels(15, "Omar", 'M', "Muscat"));
            //Using locker to lock a thread till its execution is over
            //In order to implement synchronisation
            lock (locker) {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                adapter.AddToEmployeePayroll_UsingThreads(employeeModels);
                stopwatch.Stop();
                Console.WriteLine("Duration "+stopwatch.ElapsedMilliseconds+" milliseconds");
            }

        }
    }
}
