// Exercise 01-03: Loops Practice - SOLUTION
// This is a reference solution for teachers

using System;

class Program
{
    static void Main()
    {
        // Ask for starting number
        Console.Write("Enter starting number: ");
        int start = int.Parse(Console.ReadLine());
        
        // Countdown with FOR loop
        Console.WriteLine("Counting down with FOR loop:");
        for (int i = start; i > 0; i--)
        {
            Console.WriteLine(i);
        }
        Console.WriteLine("Blast off!");
        
        // Count up with WHILE loop
        Console.WriteLine("\nCounting up with WHILE loop:");
        int count = 1;
        while (count <= start)
        {
            Console.WriteLine(count);
            count++;
        }
        Console.WriteLine("Done!");
        
        // Bonus: Add delay for realistic countdown
        // System.Threading.Thread.Sleep(1000);
    }
}