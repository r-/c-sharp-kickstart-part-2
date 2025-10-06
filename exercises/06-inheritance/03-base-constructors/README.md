# Exercise 06-03: Base Class Constructors

## Goal

Master constructor chaining in inheritance hierarchies. You'll learn how derived classes must call base class constructors and how to pass parameters up the inheritance chain.

## Background

When you create an object of a derived class, the base class constructor must run first to initialize the base class portion. The `:base()` syntax allows you to pass values from the derived constructor to the base constructor.

## Instructions

You're building a vehicle rental system. All vehicles have common properties (make, model, year, daily rate), but different vehicle types need different initialization and have type-specific properties.

1. Create a `Vehicle` base class with comprehensive validation
2. Create derived classes that properly call base constructors
3. Add multiple constructor overloads to practice constructor chaining
4. Demonstrate that base validation runs for all derived classes

## Requirements

1. **Vehicle base class must have:**
   - Make property (string)
   - Model property (string)
   - Year property (int)
   - DailyRate property (decimal, protected set)
   - Constructor that validates all parameters
   - Method `GetRentalInfo()` that returns formatted string

2. **Car derived class must:**
   - Add NumberOfDoors property (int)
   - Have constructor accepting all parameters including doors
   - Call base constructor with validation
   - Validate that doors is 2, 4, or 5
   - Override GetRentalInfo() to include door count

3. **Truck derived class must:**
   - Add PayloadCapacity property (int, in kg)
   - Have constructor accepting all parameters including capacity
   - Call base constructor with validation
   - Validate that capacity is positive
   - Override GetRentalInfo() to include capacity

4. **Motorcycle derived class must:**
   - Add EngineSize property (int, in cc)
   - Have TWO constructors:
     - Full constructor with all parameters
     - Simplified constructor that uses defaults
   - Both must call appropriate base constructor
   - Validate engine size is between 50 and 2000 cc

5. **In Main() demonstrate:**
   - Creating vehicles with valid data
   - Attempting to create vehicles with invalid data (catch exceptions)
   - Using both motorcycle constructors
   - Showing that base class validation protects all derived classes

## Expected Output

```
=== Vehicle Rental System ===

Creating valid vehicles...
✓ Car created: 2023 Volvo V90 - 4 doors - $85.00/day
✓ Truck created: 2022 Ford F-150 - 1000 kg capacity - $120.00/day
✓ Motorcycle created: 2024 Harley Davidson Street - 883 cc - $65.00/day

Testing invalid data...
❌ Error creating car: Year must be between 1900 and 2025
❌ Error creating truck: Payload capacity must be positive
❌ Error creating motorcycle: Engine size must be between 50 and 2000 cc

Using simplified constructor...
✓ Motorcycle created with defaults: 2024 Honda CB - 500 cc - $50.00/day

All vehicles:
- 2023 Volvo V90 (4-door car) - $85.00/day
- 2022 Ford F-150 (1000 kg truck) - $120.00/day
- 2024 Harley Davidson Street (883 cc motorcycle) - $65.00/day
- 2024 Honda CB (500 cc motorcycle) - $50.00/day
```

## Hints

**Vehicle base class:**
```csharp
class Vehicle
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal DailyRate { get; protected set; }
    
    public Vehicle(string make, string model, int year, decimal dailyRate)
    {
        if (string.IsNullOrWhiteSpace(make))
            throw new ArgumentException("Make is required");
        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model is required");
        if (year < 1900 || year > DateTime.Now.Year)
            throw new ArgumentException($"Year must be between 1900 and {DateTime.Now.Year}");
        if (dailyRate <= 0)
            throw new ArgumentException("Daily rate must be positive");
        
        Make = make;
        Model = model;
        Year = year;
        DailyRate = dailyRate;
    }
    
    public virtual string GetRentalInfo()
    {
        return $"{Year} {Make} {Model} - ${DailyRate:F2}/day";
    }
}
```

**Car derived class:**
```csharp
class Car : Vehicle
{
    public int NumberOfDoors { get; set; }
    
    public Car(string make, string model, int year, decimal dailyRate, int doors)
        : base(make, model, year, dailyRate)
    {
        if (doors != 2 && doors != 4 && doors != 5)
            throw new ArgumentException("Car must have 2, 4, or 5 doors");
        
        NumberOfDoors = doors;
    }
    
    public override string GetRentalInfo()
    {
        return $"{base.GetRentalInfo()} - {NumberOfDoors} doors";
    }
}
```

**Motorcycle with multiple constructors:**
```csharp
class Motorcycle : Vehicle
{
    public int EngineSize { get; set; }
    
    // Full constructor
    public Motorcycle(string make, string model, int year, decimal dailyRate, int engineSize)
        : base(make, model, year, dailyRate)
    {
        if (engineSize < 50 || engineSize > 2000)
            throw new ArgumentException("Engine size must be between 50 and 2000 cc");
        
        EngineSize = engineSize;
    }
    
    // Simplified constructor with defaults
    public Motorcycle(string make, string model, int year)
        : this(make, model, year, 50.00m, 500)  // Calls the other constructor
    {
    }
}
```

**Handling exceptions:**
```csharp
try
{
    Car badCar = new Car("Toyota", "Camry", 2050, 75, 4);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"❌ Error creating car: {ex.Message}");
}
```

## Bonus Challenge

Extend your learning with these additions:

1. **Add validation summary** - Create method that returns all validation rules for a vehicle type
2. **Add constructor overloads** - Add more constructors with different parameter combinations
3. **Add ElectricCar** - Derived from Car, adds BatteryCapacity, validates battery size
4. **Add rental calculation** - Method to calculate total cost for a number of days
5. **Add discount system** - Base class tracks rental days, applies discounts for long rentals
6. **Chain three levels** - Create Sedan derived from Car, showing multi-level inheritance

## What You'll Learn

- How to call base class constructors with `:base()`
- Why derived classes must initialize their base class
- How validation in base constructors protects all derived classes
- How to create constructor overloads in derived classes
- How to chain constructors within the same class using `:this()`
- The initialization order (base → derived)

Remember: Base class constructors run before derived class constructors. This ensures the base class portion is fully initialized before the derived class adds its features.