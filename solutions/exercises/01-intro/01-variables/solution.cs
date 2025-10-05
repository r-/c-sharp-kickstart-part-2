// Exercise 01-01: Variables Practice - SOLUTION
// This is a reference solution for teachers

using System;

class Program
{
    static void Main()
    {
        // Declare variables with different data types
        string name = "Alex";
        int age = 17;
        double height = 1.75;
        bool isStudent = true;
        
        // Print all information
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Age: {age}");
        Console.WriteLine($"Height: {height} meters");
        Console.WriteLine($"Student: {isStudent}");
        
        // Bonus: Calculate birth year
        int birthYear = 2025 - age;
        Console.WriteLine($"Birth year: {birthYear}");
    }
}