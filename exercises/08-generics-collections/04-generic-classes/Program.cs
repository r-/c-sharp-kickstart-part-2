using System;
using System.Collections.Generic;

// TODO: Part 1 - Create Stack<T> class
class Stack<T>
{
    // TODO: Add private List<T> field
    
    
    // TODO: Add Count property
    
    
    // TODO: Add constructor
    
    
    // TODO: Implement Push method
    
    
    // TODO: Implement Pop method
    // Hint: Check if empty, throw exception if so
    
    
    // TODO: Implement Peek method
    
    
    // TODO: Implement IsEmpty method
    
    
    // TODO: Implement Clear method
    
}

// TODO: Part 2 - Create Pair<T1, T2> class
class Pair<T1, T2>
{
    // TODO: Add First and Second properties
    
    
    // TODO: Add constructor
    
    
    // TODO: Implement ToString override
    
    
    // TODO: Implement Equals override (optional)
    
}

// TODO: Part 3 - Create Box<T> class with IComparable constraint
class Box<T> where T : IComparable<T>
{
    // TODO: Add Item property
    
    
    // TODO: Add constructor
    
    
    // TODO: Implement IsGreaterThan method
    // Hint: Use Item.CompareTo(other.Item)
    
    
    // TODO: Implement IsLessThan method
    
    
    // TODO: Implement Max method
    
    
    // TODO: Implement ToString override
    
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Testing Generic Classes ===\n");
        
        // TODO: Test Stack<int>
        Console.WriteLine("--- Stack<int> Test ---");
        
        
        // TODO: Test Stack<string>
        Console.WriteLine("\n--- Stack<string> Test ---");
        
        
        // TODO: Test Pair<string, int>
        Console.WriteLine("\n--- Pair<string, int> Test ---");
        
        
        // TODO: Test Pair<int, double>
        Console.WriteLine("\n--- Pair<int, double> Test ---");
        
        
        // TODO: Test Box<int>
        Console.WriteLine("\n--- Box<int> Test ---");
        
        
        // TODO: Test Box<string>
        Console.WriteLine("\n--- Box<string> Test ---");
        
        
        // TODO: Test Box<double>
        Console.WriteLine("\n--- Box<double> Test ---");
        
    }
}