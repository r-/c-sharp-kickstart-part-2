using System;

namespace Part2.Ch06.Ex03
{
    // TODO: Create Vehicle base class here
    // Should have: Make, Model, Year, DailyRate
    // Should have: Constructor with comprehensive validation
    // Should have: Virtual GetRentalInfo() method
    
    
    // TODO: Create Car derived class
    // Should add: NumberOfDoors
    // Should validate: doors is 2, 4, or 5
    // Should call: base constructor
    
    
    // TODO: Create Truck derived class
    // Should add: PayloadCapacity
    // Should validate: capacity is positive
    // Should call: base constructor
    
    
    // TODO: Create Motorcycle derived class
    // Should add: EngineSize
    // Should have: TWO constructors (full and simplified)
    // Should validate: engine size 50-2000 cc
    // Should call: base constructor
    
    
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Vehicle Rental System ===");
            Console.WriteLine();
            Console.WriteLine("Creating valid vehicles...");
            
            // TODO: Create valid vehicles and display success
            
            
            Console.WriteLine();
            Console.WriteLine("Testing invalid data...");
            
            // TODO: Try to create vehicles with invalid data
            // Catch and display exceptions
            
            
            Console.WriteLine();
            Console.WriteLine("Using simplified constructor...");
            
            // TODO: Create motorcycle using simplified constructor
            
            
            Console.WriteLine();
            Console.WriteLine("All vehicles:");
            
            // TODO: Display all valid vehicles created
        }
    }
}