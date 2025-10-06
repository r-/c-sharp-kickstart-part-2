using System;
using System.Collections.Generic;

namespace Part2.Ch07.Ex01.Solution
{
    abstract class Employee
    {
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        
        public abstract decimal CalculatePay();
        
        public void DisplayInfo()
        {
            Console.WriteLine($"\nName: {Name}");
            Console.WriteLine($"ID: {EmployeeId}");
            Console.WriteLine($"Pay: ${CalculatePay():F2}");
        }
    }
    
    class HourlyEmployee : Employee
    {
        public double HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        
        public override decimal CalculatePay()
        {
            return (decimal)HoursWorked * HourlyRate;
        }
    }
    
    class SalariedEmployee : Employee
    {
        public decimal AnnualSalary { get; set; }
        
        public override decimal CalculatePay()
        {
            return AnnualSalary / 12;
        }
    }
    
    class CommissionEmployee : Employee
    {
        public decimal BaseSalary { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal CommissionRate { get; set; }
        
        public override decimal CalculatePay()
        {
            return BaseSalary + (SalesAmount * CommissionRate);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Employee Payroll Report");
            Console.WriteLine("=======================");
            
            List<Employee> employees = new List<Employee>
            {
                new HourlyEmployee 
                { 
                    Name = "Alice Johnson", 
                    EmployeeId = 101,
                    HoursWorked = 160,
                    HourlyRate = 20
                },
                new SalariedEmployee
                {
                    Name = "Bob Smith",
                    EmployeeId = 102,
                    AnnualSalary = 50000
                },
                new CommissionEmployee
                {
                    Name = "Carol White",
                    EmployeeId = 103,
                    BaseSalary = 2000,
                    SalesAmount = 10000,
                    CommissionRate = 0.15m
                }
            };
            
            decimal totalPayroll = 0;
            
            foreach (Employee emp in employees)
            {
                emp.DisplayInfo();
                totalPayroll += emp.CalculatePay();
            }
            
            Console.WriteLine("\n=======================");
            Console.WriteLine($"Total Payroll: ${totalPayroll:F2}");
        }
    }
}