using System;

namespace Part2.Ch06.Ex04
{
    // TODO: Create Shape base class here
    // Should have: Name, Color properties
    // Should have: Virtual CalculateArea(), CalculatePerimeter(), Draw()
    // Should have: GetInfo() method that uses virtual methods
    
    
    // TODO: Create Rectangle derived class
    // Should add: Width, Height properties
    // Should override: All calculation and draw methods
    
    
    // TODO: Create Circle derived class
    // Should add: Radius property
    // Should override: All calculation and draw methods
    // Hint: Use Math.PI for calculations
    
    
    // TODO: Create Triangle derived class
    // Should add: Base, Height properties
    // Should override: CalculateArea() and Draw()
    
    
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Shape Drawing System ===");
            Console.WriteLine();
            Console.WriteLine("Creating shapes...");
            
            // TODO: Create shapes of different types
            
            
            Console.WriteLine();
            Console.WriteLine("Drawing all shapes:");
            Console.WriteLine();
            
            // TODO: Use polymorphism to call GetInfo() on all shapes
            
            
            Console.WriteLine("Shape Statistics:");
            
            // TODO: Calculate and display total/average area
        }
    }
}