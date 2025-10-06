using System;
using System.IO;

namespace Part2.Ch09.Ex03
{
    // TODO: Create FileLogger class with manual try-finally cleanup
    // Method: void LogMessage(string filename, string message)
    
    // TODO: Create SafeFileLogger class with using statement
    // Method: void LogMessage(string filename, string message)
    
    // TODO: Create LogFile class implementing IDisposable
    // Constructor opens file, WriteLine writes to it, Dispose closes it
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== File Logger Test ===\n");
            
            // TODO: Test FileLogger with manual cleanup
            // TODO: Test error handling with finally
            // TODO: Test SafeFileLogger with using statement
            // TODO: Test LogFile wrapper class
            // TODO: Demonstrate cleanup happens even with errors
        }
    }
}