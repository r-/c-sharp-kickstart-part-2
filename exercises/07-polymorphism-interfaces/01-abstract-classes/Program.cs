using System;
using System.Collections.Generic;

namespace Part2.Ch07.Ex01
{
    // TODO: Define the abstract Employee base class
    // - Properties: Name (string), EmployeeId (int)
    // - Abstract method: decimal CalculatePay()
    // - Concrete method: void DisplayInfo() that shows employee details and pay
    
    
    // TODO: Create HourlyEmployee class that inherits from Employee
    // - Properties: HoursWorked (double), HourlyRate (decimal)
    // - Override CalculatePay(): return HoursWorked * HourlyRate
    
    
    // TODO: Create SalariedEmployee class that inherits from Employee
    // - Property: AnnualSalary (decimal)
    // - Override CalculatePay(): return AnnualSalary / 12
    
    
    // TODO: Create CommissionEmployee class that inherits from Employee
    // - Properties: BaseSalary (decimal), SalesAmount (decimal), CommissionRate (decimal)
    // - Override CalculatePay(): return BaseSalary + (SalesAmount * CommissionRate)
    
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Employee Payroll Report");
            Console.WriteLine("=======================");
            
            // TODO: Create a List<Employee> and add one of each employee type
            
            
            // TODO: Loop through employees calling DisplayInfo() on each
            
            
            // TODO: Calculate and display total payroll
            
        }
    }
}