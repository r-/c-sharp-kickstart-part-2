// Exercise 01-02: If Statements - SOLUTION
// This is a reference solution for teachers

using System;

class Program
{
    static void Main()
    {
        // Prompt user for age
        Console.Write("Enter your age: ");
        
        // Read and convert input
        int age = int.Parse(Console.ReadLine());
        
        // Categorize age using if-else statements
        if (age < 13)
        {
            Console.WriteLine("You are a child");
        }
        else if (age >= 13 && age <= 17)
        {
            Console.WriteLine("You are a teenager");
        }
        else if (age >= 18 && age <= 64)
        {
            Console.WriteLine("You are an adult");
        }
        else
        {
            Console.WriteLine("You are a senior");
        }
        
        // Bonus: Input validation
        // if (age < 0 || age > 120)
        // {
        //     Console.WriteLine("Invalid age entered");
        // }
    }
}