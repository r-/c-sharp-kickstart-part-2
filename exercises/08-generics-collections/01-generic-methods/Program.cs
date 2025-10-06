using System;
using System.Collections.Generic;

class GenericUtility
{
    // TODO: Part 1 - Implement Swap<T> method
    // Hint: Use ref keyword for parameters
    
    
    // TODO: Part 1 - Implement GetFirst<T> method
    // Hint: Check if list is empty, throw exception if so
    
    
    // TODO: Part 1 - Implement GetLast<T> method
    // Hint: Use items.Count - 1 for last index
    
    
    // TODO: Part 2 - Implement Max<T> method with IComparable constraint
    // Hint: Use where T : IComparable<T>
    // Hint: Use CompareTo method
    
    
    // TODO: Part 2 - Implement Min<T> method with IComparable constraint
    
    
    // TODO: Part 3 - Implement PrintAll<T> method
    // Hint: Show count, then iterate and print each item
    
    
    // TODO: Part 3 - Implement CreatePair<T1, T2> method
    // Hint: Return a tuple (T1, T2)
    
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Testing Generic Methods ===\n");
        
        // TODO: Test Swap with int
        Console.WriteLine("--- Swap Test ---");
        
        
        // TODO: Test Swap with string
        
        
        // TODO: Test GetFirst/GetLast with List<int>
        Console.WriteLine("\n--- GetFirst/GetLast Test ---");
        
        
        // TODO: Test GetFirst/GetLast with List<string>
        
        
        // TODO: Test Max/Min with int
        Console.WriteLine("\n--- Max/Min Test ---");
        
        
        // TODO: Test Max/Min with double
        
        
        // TODO: Test Max/Min with string
        
        
        // TODO: Test PrintAll with List<int>
        Console.WriteLine("\n--- PrintAll Test ---");
        
        
        // TODO: Test PrintAll with List<string>
        
        
        // TODO: Test CreatePair with different type combinations
        Console.WriteLine("\n--- CreatePair Test ---");
        
    }
}