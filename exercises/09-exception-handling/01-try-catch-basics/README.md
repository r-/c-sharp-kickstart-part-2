# Exercise 09-01: Try-Catch Basics

## Goal

Learn to handle common runtime exceptions using try-catch blocks. You'll build a safe calculator that handles user input errors, division by zero, and numeric overflow without crashing.

## Background

Unhandled exceptions cause programs to crash. The try-catch pattern lets you catch these errors, display user-friendly messages, and keep your program running. This is the foundation of robust error handling in professional applications.

## Instructions

You will create a safe calculator with comprehensive error handling.

### Part 1: Safe Number Parsing

Create a method `SafeParseInt(string input)` that:
- Attempts to parse a string to an integer
- Returns `null` if parsing fails
- Uses try-catch to handle `FormatException` and `OverflowException`

### Part 2: Safe Division

Create a method `SafeDivide(int a, int b)` that:
- Divides two integers safely
- Handles division by zero
- Returns the result or throws a helpful exception

### Part 3: Calculator with Error Handling

Create an interactive calculator that:
- Prompts for two numbers
- Asks which operation to perform (+, -, *, /)
- Handles all input errors gracefully
- Displays clear error messages
- Allows the user to try again after errors

## Requirements

Your solution must:
1. Use try-catch blocks for all risky operations
2. Handle `FormatException` for invalid number input
3. Handle `OverflowException` for numbers too large
4. Handle `DivideByZeroException` for division by zero
5. Provide user-friendly error messages
6. Allow user to retry after errors
7. Never crash regardless of input

## Expected Output

```
=== Safe Calculator ===

Enter first number: 10
Enter second number: 5
Choose operation (+, -, *, /): /
Result: 2

Continue? (y/n): y

Enter first number: 15
Enter second number: 0
Choose operation (+, -, *, /): /
Error: Cannot divide by zero. Please try again.

Continue? (y/n): y

Enter first number: hello
Error: Invalid number format. Please enter a valid integer.

Enter first number: 999999999999999999
Error: Number is too large. Please use a smaller number.

Enter first number: 20
Enter second number: 4
Choose operation (+, -, *, /): +
Result: 24

Continue? (y/n): n
Thank you for using Safe Calculator!
```

## Hints

**SafeParseInt method:**
```csharp
static int? SafeParseInt(string input)
{
    try
    {
        return int.Parse(input);
    }
    catch (FormatException)
    {
        Console.WriteLine("Error: Invalid number format.");
        return null;
    }
    catch (OverflowException)
    {
        Console.WriteLine("Error: Number is too large.");
        return null;
    }
}
```

**SafeDivide method:**
```csharp
static double SafeDivide(int a, int b)
{
    if (b == 0)
    {
        throw new DivideByZeroException("Cannot divide by zero");
    }
    return (double)a / b;
}
```

**Main calculator loop structure:**
```csharp
bool running = true;
while (running)
{
    try
    {
        Console.Write("Enter first number: ");
        int? num1 = SafeParseInt(Console.ReadLine());
        if (num1 == null) continue;
        
        // Get second number and operation
        // Perform calculation
        // Display result
    }
    catch (DivideByZeroException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    
    Console.Write("\nContinue? (y/n): ");
    running = Console.ReadLine()?.ToLower() == "y";
}
```

## Common Mistakes to Avoid

- ❌ Catching `Exception` instead of specific types
- ❌ Not providing helpful error messages
- ❌ Letting one error crash the entire program
- ❌ Not validating input before using it

## Bonus Challenge

Enhance your calculator with these features:

1. **Additional operations** - Add power (^), modulus (%), square root
2. **Expression parsing** - Accept "10 + 5" as single input
3. **History** - Keep list of all calculations
4. **Retry limit** - Max 3 attempts per operation
5. **Input validation** - Check operation is valid before trying
6. **Memory functions** - Store and recall previous results

Example bonus output:
```
=== Enhanced Calculator ===
Operations: +, -, *, /, ^, sqrt
Memory: 0

Enter expression (or 'help'): 10 + 5
Result: 15
Saved to memory

Enter expression: sqrt 25
Result: 5
Saved to memory

Enter expression: memory
Current memory: 5

History:
  10 + 5 = 15
  sqrt 25 = 5
```

## What You're Learning

- How to use try-catch blocks effectively
- Handling specific exception types
- Providing user-friendly error messages
- Building robust applications that don't crash
- Validating user input safely

---

**Time Estimate:** 30 minutes  
**Difficulty:** Easy-Medium  
**Type:** Error Handling Fundamentals

**Next:** After completing this exercise, move to [Exercise 09-02: Multiple Catches and Custom Exceptions](../02-custom-exceptions/) to create your own exception types.