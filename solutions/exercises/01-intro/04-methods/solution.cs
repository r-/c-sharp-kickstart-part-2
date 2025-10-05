// Exercise 01-04: Methods - SOLUTION
// This is a reference solution for teachers

using System;

class Program
{
    // Method that displays a greeting
    static void Greet(string name)
    {
        Console.WriteLine($"Hello, {name}!");
    }
    
    // Method that returns the sum of two numbers
    static int Add(int a, int b)
    {
        return a + b;
    }
    
    // Method that checks if a number is even
    static bool IsEven(int number)
    {
        return number % 2 == 0;
    }
    
    // Method that prints a square pattern
    static void PrintSquare(int size)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }
    }
    
    // Bonus: Factorial method
    static int Factorial(int n)
    {
        int result = 1;
        for (int i = 1; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }
    
    static void Main()
    {
        // Test Greet method
        Greet("Alex");
        
        // Test Add method
        int sum = Add(5, 3);
        Console.WriteLine($"5 + 3 = {sum}");
        
        // Test IsEven method
        Console.WriteLine($"Is 10 even? {IsEven(10)}");
        Console.WriteLine($"Is 7 even? {IsEven(7)}");
        
        // Test PrintSquare method
        Console.WriteLine();
        PrintSquare(3);
        
        // Bonus: Test Factorial
        // Console.WriteLine($"\n5! = {Factorial(5)}");
    }
}