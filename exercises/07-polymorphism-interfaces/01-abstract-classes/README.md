# Exercise 07-01: Abstract Classes

## Goal

Learn to create abstract base classes with abstract and concrete methods. You'll build a hierarchy of employee types that share common behavior but implement specific payroll calculations differently.

## Background

Abstract classes are powerful when you have a family of related types that share some implementation but need to customize specific behaviors. The `abstract` keyword prevents direct instantiation while allowing you to define common functionality all derived classes will share.

## Instructions

You will create an employee payroll system using abstract classes.

### Part 1: Define the Abstract Base Class

Create an abstract class `Employee` with:
- Properties: `Name` (string), `EmployeeId` (int)
- Abstract method: `decimal CalculatePay()` (no implementation)
- Concrete method: `void DisplayInfo()` that shows employee details and pay

### Part 2: Create Concrete Implementations

Implement three employee types:

1. **HourlyEmployee**
   - Additional properties: `HoursWorked` (double), `HourlyRate` (decimal)
   - Override `CalculatePay()`: returns `HoursWorked * HourlyRate`

2. **SalariedEmployee**
   - Additional property: `AnnualSalary` (decimal)
   - Override `CalculatePay()`: returns `AnnualSalary / 12` (monthly pay)

3. **CommissionEmployee**
   - Additional properties: `BaseSalary` (decimal), `SalesAmount` (decimal), `CommissionRate` (decimal)
   - Override `CalculatePay()`: returns `BaseSalary + (SalesAmount * CommissionRate)`

### Part 3: Test Polymorphism

In `Main()`:
1. Create one employee of each type
2. Store them in a `List<Employee>`
3. Loop through the list calling `DisplayInfo()` on each
4. Calculate and display total payroll

## Requirements

Your solution must:
1. Define `Employee` as an abstract class
2. Mark `CalculatePay()` as abstract
3. Implement all three employee types
4. Override `CalculatePay()` in each derived class
5. Use `List<Employee>` to demonstrate polymorphism
6. Calculate total payroll for all employees

## Expected Output

```
Employee Payroll Report
=======================

Name: Alice Johnson
ID: 101
Pay: $3,200.00

Name: Bob Smith
ID: 102
Pay: $4,166.67

Name: Carol White
ID: 103
Pay: $3,500.00

=======================
Total Payroll: $10,866.67
```

## Hints

**Abstract base class:**
```csharp
abstract class Employee
{
    public string Name { get; set; }
    public int EmployeeId { get; set; }
    
    // Abstract - must be implemented by derived classes
    public abstract decimal CalculatePay();
    
    // Concrete - shared by all employees
    public void DisplayInfo()
    {
        Console.WriteLine($"\nName: {Name}");
        Console.WriteLine($"ID: {EmployeeId}");
        Console.WriteLine($"Pay: ${CalculatePay():F2}");
    }
}
```

**Derived class example:**
```csharp
class HourlyEmployee : Employee
{
    public double HoursWorked { get; set; }
    public decimal HourlyRate { get; set; }
    
    public override decimal CalculatePay()
    {
        return (decimal)HoursWorked * HourlyRate;
    }
}
```

**Using polymorphism:**
```csharp
List<Employee> employees = new List<Employee>
{
    new HourlyEmployee 
    { 
        Name = "Alice", 
        EmployeeId = 101,
        HoursWorked = 160,
        HourlyRate = 20
    },
    // Add more employees...
};

decimal total = 0;
foreach (Employee emp in employees)
{
    emp.DisplayInfo();
    total += emp.CalculatePay();
}

Console.WriteLine($"\nTotal Payroll: ${total:F2}");
```

## Common Mistakes to Avoid

- ❌ Forgetting to mark the class as `abstract`
- ❌ Not using `override` keyword in derived classes
- ❌ Trying to instantiate the abstract `Employee` class directly
- ❌ Forgetting to implement `CalculatePay()` in a derived class

## Bonus Challenge

Add a fourth employee type:
- **ContractorEmployee** with `HourlyRate` and `HoursWorked`
- Add a `decimal ContractFee` property
- Override `CalculatePay()`: `(HoursWorked * HourlyRate) + ContractFee`
- Include overtime pay at 1.5x rate for hours over 40

## What You're Learning

- How abstract classes enforce implementation contracts
- When to use abstract methods vs concrete methods
- Polymorphism through abstract base classes
- The power of List<BaseType> for treating different types uniformly

---

**Next:** After completing this exercise, move to [Exercise 07-02: Interfaces](../02-interfaces/) to learn pure contracts without implementation.