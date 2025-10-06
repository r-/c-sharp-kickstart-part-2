using System;
using System.Collections.Generic;

class GenericUtility
{
    // Part 1: Basic Generic Methods
    
    public static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }
    
    public static T GetFirst<T>(List<T> items)
    {
        if (items.Count == 0)
            throw new InvalidOperationException("List is empty");
        return items[0];
    }
    
    public static T GetLast<T>(List<T> items)
    {
        if (items.Count == 0)
            throw new InvalidOperationException("List is empty");
        return items[items.Count - 1];
    }
    
    // Part 2: Generic Methods with Constraints
    
    public static T Max<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) > 0 ? a : b;
    }
    
    public static T Min<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) < 0 ? a : b;
    }
    
    // Part 3: Advanced Generic Methods
    
    public static void PrintAll<T>(List<T> items)
    {
        Console.WriteLine($"Printing {items.Count} items:");
        foreach (T item in items)
        {
            Console.WriteLine(item);
        }
    }
    
    public static (T1, T2) CreatePair<T1, T2>(T1 first, T2 second)
    {
        return (first, second);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Testing Generic Methods ===\n");
        
        // Test Swap with int
        Console.WriteLine("--- Swap Test ---");
        int x = 5, y = 10;
        Console.WriteLine($"Before swap: x = {x}, y = {y}");
        GenericUtility.Swap(ref x, ref y);
        Console.WriteLine($"After swap: x = {x}, y = {y}");
        
        // Test Swap with string
        string s1 = "hello", s2 = "world";
        Console.WriteLine($"\nBefore swap: s1 = {s1}, s2 = {s2}");
        GenericUtility.Swap(ref s1, ref s2);
        Console.WriteLine($"After swap: s1 = {s1}, s2 = {s2}");
        
        // Test GetFirst/GetLast with List<int>
        Console.WriteLine("\n--- GetFirst/GetLast Test ---");
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        Console.WriteLine($"Numbers: {string.Join(", ", numbers)}");
        Console.WriteLine($"First: {GenericUtility.GetFirst(numbers)}");
        Console.WriteLine($"Last: {GenericUtility.GetLast(numbers)}");
        
        // Test GetFirst/GetLast with List<string>
        List<string> names = new List<string> { "Alice", "Bob", "Carol" };
        Console.WriteLine($"\nNames: {string.Join(", ", names)}");
        Console.WriteLine($"First: {GenericUtility.GetFirst(names)}");
        Console.WriteLine($"Last: {GenericUtility.GetLast(names)}");
        
        // Test Max/Min with int
        Console.WriteLine("\n--- Max/Min Test ---");
        Console.WriteLine($"Max(15, 7) = {GenericUtility.Max(15, 7)}");
        Console.WriteLine($"Min(15, 7) = {GenericUtility.Min(15, 7)}");
        
        // Test Max/Min with double
        Console.WriteLine($"\nMax(3.14, 2.71) = {GenericUtility.Max(3.14, 2.71)}");
        Console.WriteLine($"Min(3.14, 2.71) = {GenericUtility.Min(3.14, 2.71)}");
        
        // Test Max/Min with string
        Console.WriteLine($"\nMax(\"apple\", \"zebra\") = {GenericUtility.Max("apple", "zebra")}");
        Console.WriteLine($"Min(\"apple\", \"zebra\") = {GenericUtility.Min("apple", "zebra")}");
        
        // Test PrintAll with List<int>
        Console.WriteLine("\n--- PrintAll Test ---");
        List<int> integers = new List<int> { 42, 17, 99 };
        GenericUtility.PrintAll(integers);
        
        // Test PrintAll with List<string>
        List<string> words = new List<string> { "Hello", "World" };
        Console.WriteLine();
        GenericUtility.PrintAll(words);
        
        // Test CreatePair with different type combinations
        Console.WriteLine("\n--- CreatePair Test ---");
        var pair1 = GenericUtility.CreatePair("Alice", 25);
        Console.WriteLine($"Pair: {pair1}");
        
        var pair2 = GenericUtility.CreatePair(100, "Active");
        Console.WriteLine($"Pair: {pair2}");
    }
}