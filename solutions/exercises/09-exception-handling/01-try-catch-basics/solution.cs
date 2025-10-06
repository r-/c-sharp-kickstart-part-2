using System;

namespace Part2.Ch09.Ex01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Safe Calculator ===\n");
            
            bool running = true;
            while (running)
            {
                try
                {
                    Console.Write("Enter first number: ");
                    int? num1 = SafeParseInt(Console.ReadLine());
                    if (num1 == null) continue;
                    
                    Console.Write("Enter second number: ");
                    int? num2 = SafeParseInt(Console.ReadLine());
                    if (num2 == null) continue;
                    
                    Console.Write("Choose operation (+, -, *, /): ");
                    string operation = Console.ReadLine();
                    
                    double result = operation switch
                    {
                        "+" => num1.Value + num2.Value,
                        "-" => num1.Value - num2.Value,
                        "*" => num1.Value * num2.Value,
                        "/" => SafeDivide(num1.Value, num2.Value),
                        _ => throw new InvalidOperationException($"Unknown operation: {operation}")
                    };
                    
                    Console.WriteLine($"Result: {result}\n");
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please try again.\n");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}\n");
                }
                
                Console.Write("Continue? (y/n): ");
                running = Console.ReadLine()?.ToLower() == "y";
                Console.WriteLine();
            }
            
            Console.WriteLine("Thank you for using Safe Calculator!");
        }
        
        static int? SafeParseInt(string input)
        {
            try
            {
                return int.Parse(input);
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Invalid number format. Please enter a valid integer.");
                return null;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: Number is too large. Please use a smaller number.");
                return null;
            }
        }
        
        static double SafeDivide(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }
            return (double)a / b;
        }
    }
}