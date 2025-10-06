# Chapter 9 – Exception Handling and Robust Code

In Chapter 8, we learned to work with generics and collections safely. Now we'll discover **exception handling**—the ability to write code that anticipates problems and handles errors gracefully. You'll master try/catch/finally, create custom exceptions, and build applications that never crash unexpectedly.

## Learning Objectives

By the end of this chapter, you will be able to:

- Explain what exceptions are and when they occur
- Use try/catch blocks to handle runtime errors
- Catch specific exception types with multiple catch blocks
- Use finally blocks for cleanup code
- Create and throw custom exception types
- Write defensive code that prevents errors
- Choose between throwing exceptions and returning error codes

## Prerequisites

This chapter assumes you've completed **Chapter 8** and can:

- Work with `List<T>` and `Dictionary<K,V>`
- Create generic methods and classes
- Use inheritance and interfaces
- Implement classes with proper encapsulation
- Validate input in constructors and methods

---

## Mission Brief

In this chapter you will:

- Discover why programs crash and how to prevent it
- Learn to catch and handle runtime errors gracefully
- Master the try/catch/finally pattern for robust code
- Create custom exception types for domain-specific errors
- Build applications that recover from failures
- Write code that users trust because it never crashes

This chapter teaches you to write professional-grade, production-ready code.

---

## Concept Power-Up – The Problem with Crashes

Imagine you're building a calculator app. Without exception handling:

```csharp
// ❌ Brittle code - crashes on bad input
static void Main()
{
    Console.Write("Enter first number: ");
    int num1 = int.Parse(Console.ReadLine());  // 💥 Crash if not a number!
    
    Console.Write("Enter second number: ");
    int num2 = int.Parse(Console.ReadLine());  // 💥 Crash if not a number!
    
    Console.WriteLine($"Result: {num1 / num2}");  // 💥 Crash if num2 is zero!
}
```

**The problems:**
- Crashes if user types "hello" instead of a number
- Crashes if user enters 0 for division
- No way to recover—program just dies
- Terrible user experience

With exception handling:

```csharp
// ✅ Robust code - handles errors gracefully
static void Main()
{
    try
    {
        Console.Write("Enter first number: ");
        int num1 = int.Parse(Console.ReadLine());
        
        Console.Write("Enter second number: ");
        int num2 = int.Parse(Console.ReadLine());
        
        int result = num1 / num2;
        Console.WriteLine($"Result: {result}");
    }
    catch (FormatException)
    {
        Console.WriteLine("Error: Please enter valid numbers.");
    }
    catch (DivideByZeroException)
    {
        Console.WriteLine("Error: Cannot divide by zero.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected error: {ex.Message}");
    }
}
```

> **Key Insight**: Exception handling lets you anticipate problems, provide helpful error messages, and keep your program running instead of crashing.

---

## Core Concepts

### Concept 1 – What Are Exceptions?

An **exception** is a runtime error that disrupts normal program flow. Unlike compile-time errors (syntax mistakes), exceptions occur when your program is running.

**Common exceptions:**

```csharp
// DivideByZeroException
int result = 10 / 0;  // 💥 Cannot divide by zero

// IndexOutOfRangeException
int[] numbers = { 1, 2, 3 };
int value = numbers[5];  // 💥 Index 5 doesn't exist

// NullReferenceException
string text = null;
int length = text.Length;  // 💥 Cannot access property of null

// FormatException
int number = int.Parse("hello");  // 💥 "hello" is not a number

// ArgumentException
List<int> scores = new List<int>();
scores.Insert(-1, 100);  // 💥 Index cannot be negative

// KeyNotFoundException
Dictionary<string, int> ages = new Dictionary<string, int>();
int age = ages["Bob"];  // 💥 Key "Bob" doesn't exist
```

**Understanding the stack trace:**

```csharp
void MethodA()
{
    MethodB();
}

void MethodB()
{
    MethodC();
}

void MethodC()
{
    int result = 10 / 0;  // Exception occurs here
}

// Stack trace shows the call path:
// at MethodC() line 15
// at MethodB() line 10
// at MethodA() line 5
```

The stack trace tells you exactly where the error occurred and how execution got there.

### Concept 2 – Try-Catch Blocks: Catching Errors

Use `try/catch` to handle errors without crashing:

```csharp
try
{
    // Code that might throw an exception
    int result = 10 / 0;
}
catch (DivideByZeroException ex)
{
    // Handle the error
    Console.WriteLine("Cannot divide by zero!");
}
```

**How it works:**
1. Code in `try` block executes normally
2. If an exception occurs, execution jumps to `catch` block
3. Catch block handles the error
4. Program continues after the catch block

**Accessing exception details:**

```csharp
try
{
    int[] numbers = { 1, 2, 3 };
    Console.WriteLine(numbers[10]);
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    Console.WriteLine($"Type: {ex.GetType().Name}");
    // Can also log: ex.StackTrace
}
```

**Safe parsing example:**

```csharp
try
{
    Console.Write("Enter your age: ");
    string input = Console.ReadLine();
    int age = int.Parse(input);
    
    if (age < 0 || age > 150)
    {
        Console.WriteLine("Age seems unrealistic!");
    }
    else
    {
        Console.WriteLine($"You are {age} years old.");
    }
}
catch (FormatException)
{
    Console.WriteLine("Please enter a valid number.");
}
catch (OverflowException)
{
    Console.WriteLine("Number is too large.");
}
```

**Better approach with TryParse:**

```csharp
// TryParse is often better than Parse + try/catch
Console.Write("Enter your age: ");
string input = Console.ReadLine();

if (int.TryParse(input, out int age))
{
    Console.WriteLine($"You are {age} years old.");
}
else
{
    Console.WriteLine("Please enter a valid number.");
}
```

### Concept 3 – Multiple Catch Blocks: Handling Different Errors

Catch different exception types with specific handlers:

```csharp
try
{
    Console.Write("Enter array index: ");
    int index = int.Parse(Console.ReadLine());
    
    int[] numbers = { 10, 20, 30, 40, 50 };
    Console.WriteLine($"Value: {numbers[index]}");
}
catch (FormatException)
{
    Console.WriteLine("Error: Please enter a valid number.");
}
catch (IndexOutOfRangeException)
{
    Console.WriteLine("Error: Index is out of range.");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}
```

**Order matters—most specific first:**

```csharp
try
{
    // Some risky code
}
catch (FileNotFoundException ex)  // Specific
{
    Console.WriteLine("File not found!");
}
catch (IOException ex)  // More general (includes FileNotFound)
{
    Console.WriteLine("I/O error occurred!");
}
catch (Exception ex)  // Most general - catches everything
{
    Console.WriteLine($"Something went wrong: {ex.Message}");
}
```

**Why order matters:**

```csharp
// ❌ Wrong - will never reach FileNotFoundException
try { }
catch (Exception ex) { }  // Catches everything
catch (FileNotFoundException ex) { }  // Never reached!

// ✅ Correct - specific first
try { }
catch (FileNotFoundException ex) { }  // Checked first
catch (Exception ex) { }  // Fallback for others
```

**Dictionary access with multiple catches:**

```csharp
Dictionary<string, int> scores = new Dictionary<string, int>
{
    { "Alice", 95 },
    { "Bob", 87 }
};

try
{
    Console.Write("Enter player name: ");
    string name = Console.ReadLine();
    
    int score = scores[name];
    Console.WriteLine($"{name}'s score: {score}");
}
catch (KeyNotFoundException)
{
    Console.WriteLine("Player not found in database.");
}
catch (ArgumentNullException)
{
    Console.WriteLine("Name cannot be empty.");
}
```

> **🏋️ Practice Now**: Complete [Exercise 09-01: Try-Catch Basics](../../exercises/09-exception-handling/01-try-catch-basics/) to practice handling common exceptions.

---

## Try It – Safe Calculator

Let's build a complete calculator with proper error handling:

```csharp
class Calculator
{
    public double Add(double a, double b) => a + b;
    public double Subtract(double a, double b) => a - b;
    public double Multiply(double a, double b) => a * b;
    
    public double Divide(double a, double b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero");
        }
        return a / b;
    }
}

class Program
{
    static void Main()
    {
        Calculator calc = new Calculator();
        bool running = true;
        
        while (running)
        {
            try
            {
                Console.WriteLine("\n=== Calculator ===");
                Console.WriteLine("1. Add");
                Console.WriteLine("2. Subtract");
                Console.WriteLine("3. Multiply");
                Console.WriteLine("4. Divide");
                Console.WriteLine("5. Exit");
                Console.Write("Choose operation: ");
                
                int choice = int.Parse(Console.ReadLine());
                
                if (choice == 5)
                {
                    running = false;
                    Console.WriteLine("Goodbye!");
                    continue;
                }
                
                Console.Write("Enter first number: ");
                double num1 = double.Parse(Console.ReadLine());
                
                Console.Write("Enter second number: ");
                double num2 = double.Parse(Console.ReadLine());
                
                double result = choice switch
                {
                    1 => calc.Add(num1, num2),
                    2 => calc.Subtract(num1, num2),
                    3 => calc.Multiply(num1, num2),
                    4 => calc.Divide(num1, num2),
                    _ => throw new InvalidOperationException("Invalid choice")
                };
                
                Console.WriteLine($"Result: {result}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter valid numbers.");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}
```

**Output example:**
```
=== Calculator ===
1. Add
2. Subtract
3. Multiply
4. Divide
5. Exit
Choose operation: 4
Enter first number: 10
Enter second number: 0
Error: Cannot divide by zero

=== Calculator ===
Choose operation: 1
Enter first number: 15
Enter second number: 25
Result: 40
```

> **🏋️ Practice Now**: Complete [Exercise 09-02: Multiple Catches and Custom Exceptions](../../exercises/09-exception-handling/02-custom-exceptions/) to create domain-specific error types.

---

## Challenge – File Reader with Error Handling

Create a safe file reader that handles multiple error scenarios:

1. Create `SafeFileReader` class with:
   - Method `string ReadFile(string filename)`
   - Handles file not found
   - Handles access denied
   - Handles general I/O errors

2. Test with different scenarios:
   - Reading existing file
   - Reading non-existent file
   - Reading protected file

**Hints:**

```csharp
class SafeFileReader
{
    public string ReadFile(string filename)
    {
        try
        {
            return File.ReadAllText(filename);
        }
        catch (FileNotFoundException)
        {
            return "Error: File not found.";
        }
        catch (UnauthorizedAccessException)
        {
            return "Error: Access denied.";
        }
        catch (IOException ex)
        {
            return $"Error reading file: {ex.Message}";
        }
    }
}
```

---

## Core Concepts (Continued)

### Concept 4 – Finally Block: Code That Always Runs

The `finally` block executes whether an exception occurs or not:

```csharp
try
{
    Console.WriteLine("Opening file...");
    // File operations
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
finally
{
    Console.WriteLine("Closing file...");
    // Cleanup code always runs
}
```

**Resource cleanup pattern:**

```csharp
FileStream file = null;
try
{
    file = File.OpenRead("data.txt");
    // Read from file
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
finally
{
    if (file != null)
    {
        file.Close();  // Always close file
    }
}
```

**Better: Using statement (automatic cleanup):**

```csharp
// Using statement calls Dispose() automatically
using (FileStream file = File.OpenRead("data.txt"))
{
    // Read from file
}  // file.Dispose() called here, even if exception occurs

// Or with C# 8+ syntax:
using FileStream file = File.OpenRead("data.txt");
// Read from file
// file.Dispose() called at end of method/block
```

**When to use finally:**

```csharp
// Logging example
try
{
    Console.WriteLine("Starting process...");
    // Risky operation
    Console.WriteLine("Process completed.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
finally
{
    Console.WriteLine("Process ended.");  // Always logs
}
```

### Concept 5 – Custom Exceptions: Creating Your Own Error Types

Create custom exceptions for domain-specific errors:

```csharp
class InsufficientFundsException : Exception
{
    public decimal Balance { get; }
    public decimal AttemptedAmount { get; }
    
    public InsufficientFundsException(decimal balance, decimal attempted)
        : base($"Insufficient funds. Balance: ${balance}, Attempted: ${attempted}")
    {
        Balance = balance;
        AttemptedAmount = attempted;
    }
}

class BankAccount
{
    public string Owner { get; set; }
    public decimal Balance { get; private set; }
    
    public BankAccount(string owner, decimal initialBalance)
    {
        if (string.IsNullOrWhiteSpace(owner))
            throw new ArgumentException("Owner name is required");
        
        if (initialBalance < 0)
            throw new ArgumentException("Initial balance cannot be negative");
        
        Owner = owner;
        Balance = initialBalance;
    }
    
    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdrawal amount must be positive");
        
        if (amount > Balance)
            throw new InsufficientFundsException(Balance, amount);
        
        Balance -= amount;
    }
    
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be positive");
        
        Balance += amount;
    }
}
```

**Using custom exceptions:**

```csharp
BankAccount account = new BankAccount("Alice", 100);

try
{
    account.Withdraw(150);
}
catch (InsufficientFundsException ex)
{
    Console.WriteLine($"Cannot withdraw ${ex.AttemptedAmount}");
    Console.WriteLine($"Current balance: ${ex.Balance}");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Invalid operation: {ex.Message}");
}
```

**Another example: Custom validation exception:**

```csharp
class InvalidAgeException : Exception
{
    public int Age { get; }
    
    public InvalidAgeException(int age)
        : base($"Age {age} is invalid. Must be between 0 and 150.")
    {
        Age = age;
    }
}

class Person
{
    private int age;
    
    public int Age
    {
        get => age;
        set
        {
            if (value < 0 || value > 150)
                throw new InvalidAgeException(value);
            age = value;
        }
    }
}
```

**When to create custom exceptions:**
- Domain-specific errors (bank operations, game rules, business logic)
- Need to include additional data (like Balance and AttemptedAmount)
- Want to catch specific error types separately
- Building a library or framework for others to use

> **🏋️ Practice Now**: Complete [Exercise 09-03: Finally and Resource Cleanup](../../exercises/09-exception-handling/03-finally-cleanup/) to master resource management patterns.

---

## Mini Project – Build YOUR Robust Data Validator

**Goal:** Create YOUR own data validation system that handles errors gracefully and provides helpful feedback!

This is YOUR project to design and implement. You'll build a system that validates different types of data (emails, phone numbers, dates, etc.) with comprehensive error handling.

### Why This Project?

Professional applications validate user input constantly. This project teaches you to build robust validation systems that provide clear error messages and never crash, no matter what users enter.

### Learning Goals

- Handle multiple exception types appropriately
- Create custom exception classes for validation errors
- Use finally blocks for cleanup
- Write defensive code with validation
- Provide user-friendly error messages
- Build systems that recover from failures

### Minimum Requirements

YOUR data validator MUST include:

1. **Validator class with at least 3 validation methods:**
   - `ValidateEmail(string email)` - Check email format
   - `ValidatePhoneNumber(string phone)` - Check phone format
   - `ValidateAge(int age)` - Check age range
   - Each throws custom exception on invalid data

2. **Custom exception classes:**
   - `InvalidEmailException`
   - `InvalidPhoneException`
   - `InvalidAgeException`
   - Each includes the invalid value and helpful message

3. **Interactive validation program:**
   - Prompt user for multiple pieces of data
   - Validate each input
   - Handle all exceptions gracefully
   - Allow retry on validation failure
   - Display summary of valid data

4. **Error logging:**
   - Log all validation errors to console
   - Use try/catch/finally pattern
   - Ensure cleanup happens even on errors

5. **Batch processing:**
   - Process multiple records from a list/file
   - Continue processing even if some fail
   - Report success and failure counts
   - Display which records failed and why

### Expected Output

```
=== Data Validation System ===

Enter email: alice@example.com
✓ Valid email

Enter phone: 555-1234
✗ Invalid phone: Phone must be in format XXX-XXX-XXXX

Enter phone: 555-123-4567
✓ Valid phone

Enter age: 200
✗ Invalid age: Age 200 is out of range (0-150)

Enter age: 25
✓ Valid age

=== Validation Summary ===
Successful validations: 2
Failed validations: 2

Processing batch of 5 records...
Record 1: ✓ Valid
Record 2: ✗ Invalid email format
Record 3: ✓ Valid
Record 4: ✗ Age out of range
Record 5: ✓ Valid

Batch complete: 3 successful, 2 failed
```

### Implementation Steps

1. Create custom exception classes
2. Build validator with validation methods
3. Implement interactive validation loop
4. Add error handling and retry logic
5. Create batch processing
6. Add logging and reporting
7. Test edge cases and error scenarios
8. Polish user experience

### Hints

**Custom exception:**

```csharp
class InvalidEmailException : Exception
{
    public string Email { get; }
    
    public InvalidEmailException(string email)
        : base($"Invalid email format: {email}")
    {
        Email = email;
    }
}
```

**Validator class:**

```csharp
class DataValidator
{
    public void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");
        
        if (!email.Contains("@") || !email.Contains("."))
            throw new InvalidEmailException(email);
    }
    
    public void ValidateAge(int age)
    {
        if (age < 0 || age > 150)
            throw new InvalidAgeException(age);
    }
}
```

**Interactive validation:**

```csharp
DataValidator validator = new DataValidator();
bool retry = true;

while (retry)
{
    try
    {
        Console.Write("Enter email: ");
        string email = Console.ReadLine();
        validator.ValidateEmail(email);
        Console.WriteLine("✓ Valid email");
        retry = false;
    }
    catch (InvalidEmailException ex)
    {
        Console.WriteLine($"✗ {ex.Message}");
        Console.Write("Try again? (y/n): ");
        retry = Console.ReadLine().ToLower() == "y";
    }
}
```

### Make It YOUR Own!

Add YOUR creative features:

- **Credit card validation** - Luhn algorithm, expiry dates
- **Password strength checker** - Length, complexity rules
- **URL validation** - Protocol, domain, path checking
- **Date/time validation** - Format checking, range validation
- **File validation** - Extension, size, existence checks
- **Username validation** - Length, allowed characters
- **ZIP code validation** - Country-specific formats
- **Social security number** - Format and checksum
- **ISBN validation** - Book identifier validation
- **Currency validation** - Amount ranges, format
- **Regex-based validation** - Custom pattern matching
- **Validation rules engine** - Configurable validation
- **Multi-field validation** - Cross-field rules
- **Async validation** - Check against external services
- **Localization** - Multi-language error messages

### Self-Assessment Checklist

- [ ] Custom exception classes created
- [ ] At least 3 validation methods work correctly
- [ ] Try/catch blocks handle errors appropriately
- [ ] Finally blocks used for cleanup when needed
- [ ] User-friendly error messages displayed
- [ ] Retry mechanism implemented
- [ ] Batch processing handles partial failures
- [ ] Validation summary shows statistics
- [ ] Code is well-organized and readable
- [ ] All edge cases handled

### What Makes a Great Project?

**Error Handling (40%)**
- Comprehensive exception handling
- Custom exceptions with useful data
- Appropriate use of try/catch/finally
- No unhandled exceptions

**Functionality (30%)**
- All validators work correctly
- Batch processing implemented
- Retry logic functional
- Clear reporting

**Code Quality (20%)**
- Clean exception class design
- Good separation of concerns
- Meaningful error messages
- Defensive programming

**User Experience (10%)**
- Clear prompts and feedback
- Helpful error messages
- Professional output formatting

> **🚀 Mini Project**: Ready to build production-ready code? Complete the [Mini Project: Data Validator](../../projects/09-exception-handling/) to create YOUR robust validation system.

---

## Common Mistakes

### Swallowing Exceptions

```csharp
// ❌ Wrong - hides errors, impossible to debug
try
{
    // Risky code
}
catch (Exception)
{
    // Do nothing - error is hidden!
}

// ✅ Correct - at minimum, log the error
try
{
    // Risky code
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    // Or log to file, reporting system, etc.
}
```

### Catching Too Broadly

```csharp
// ❌ Wrong - catches everything, even bugs
try
{
    // Complex code
}
catch (Exception)
{
    Console.WriteLine("Something went wrong");
    // Which exception? Where did it happen?
}

// ✅ Correct - catch specific exceptions
try
{
    // Complex code
}
catch (FileNotFoundException)
{
    Console.WriteLine("File not found");
}
catch (UnauthorizedAccessException)
{
    Console.WriteLine("Access denied");
}
catch (Exception ex)  // Fallback for unexpected errors
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}
```

### Using Exceptions for Control Flow

```csharp
// ❌ Wrong - exceptions are expensive
try
{
    int index = 0;
    while (true)
    {
        Console.WriteLine(array[index]);
        index++;
    }
}
catch (IndexOutOfRangeException)
{
    // Used exception to end loop
}

// ✅ Correct - use proper loop bounds
for (int i = 0; i < array.Length; i++)
{
    Console.WriteLine(array[i]);
}
```

### Not Including Error Details

```csharp
// ❌ Wrong - vague error message
throw new Exception("Error");

// ✅ Correct - specific, helpful message
throw new ArgumentException(
    $"Age must be between 0 and 150. Received: {age}",
    nameof(age)
);
```

---

## Hands-On Practice

Practice exception handling with these exercises:

### Exercise Progression

1. **[Exercise 09-01: Try-Catch Basics](../../exercises/09-exception-handling/01-try-catch-basics/)** - Handle common exceptions safely
2. **[Exercise 09-02: Multiple Catches and Custom Exceptions](../../exercises/09-exception-handling/02-custom-exceptions/)** - Create domain-specific error types
3. **[Exercise 09-03: Finally and Resource Cleanup](../../exercises/09-exception-handling/03-finally-cleanup/)** - Master resource management patterns

Complete these in order to build comprehensive error handling skills.

> **💡 Tip**: Exceptions should be exceptional—use them for errors, not normal control flow!

---

## Why It Matters

Exception handling is fundamental to professional software development because it:

1. **Prevents crashes** - Applications stay running even when problems occur
2. **Improves debugging** - Clear error messages speed up problem resolution
3. **Enhances UX** - Users get helpful feedback instead of cryptic errors
4. **Enables recovery** - Programs can retry, rollback, or gracefully degrade
5. **Supports maintenance** - Future developers understand error conditions
6. **Meets requirements** - Production code MUST handle errors robustly

Every professional C# application uses exception handling. Master it now and you'll write code that's reliable, debuggable, and production-ready.

---

## Checkpoint

You should now be able to:

- Explain what exceptions are and when they occur
- Use try/catch blocks to handle runtime errors
- Catch specific exceptions with multiple catch blocks
- Use finally blocks for cleanup code
- Create custom exception classes
- Write defensive code with proper validation
- Choose appropriate error handling strategies

---

## Reflection Questions

1. How does exception handling improve user experience compared to crashes?
2. When should you create a custom exception instead of using built-in types?
3. Why does the order of catch blocks matter?
4. What's the difference between using `finally` and the `using` statement?
5. Can you think of a scenario where throwing an exception is better than returning an error code?

---

## Next Chapter Preview

In **Chapter 10 – Object-Oriented Design & Architecture**, you'll learn professional design principles like SOLID, composition over inheritance, and how to architect maintainable systems. You'll discover how to design class structures that are flexible, testable, and easy to extend.

---

## Key Terms

- **Exception**: Runtime error that disrupts normal program flow
- **Try Block**: Code section that might throw an exception
- **Catch Block**: Handles specific exception types
- **Finally Block**: Code that runs whether exception occurs or not
- **Stack Trace**: Shows the call path where exception occurred
- **Custom Exception**: User-defined exception class for domain errors
- **Throwing**: Signaling that an error occurred with `throw`
- **Handling**: Catching and processing an exception
- **Resource Cleanup**: Releasing resources (files, connections) in finally or using
- **Using Statement**: Automatic resource cleanup for IDisposable objects
- **Exception Propagation**: How exceptions travel up the call stack
- **Graceful Degradation**: Handling errors while maintaining functionality

---

**Teacher Notes:**

**Pedagogical Goals:**
- Transition from "hope it works" to "handle what can go wrong"
- Emphasize user experience and robustness over just "making it work"
- Show professional error handling patterns used in production
- Prepare for defensive programming and design by contract

**Common Student Questions:**
- "When should I use exceptions vs if statements?" → Exceptions for exceptional cases, if for expected conditions
- "Should I catch all exceptions?" → No, catch specific types and let unexpected ones propagate
- "How do I know what exceptions to catch?" → Check documentation, use debugger, learn common types
- "Are exceptions slow?" → Yes, but correctness matters more. Don't use for control flow.

**Assessment Hints:**
- Check proper try/catch/finally structure
- Verify specific exception types are caught
- Ensure custom exceptions inherit from Exception
- Look for helpful error messages
- Check that resources are cleaned up

**Connection to Curriculum (PRRPRR02):**
- Professional code quality standards
- Defensive programming practices
- Error handling patterns
- Resource management

**Classroom Activities:**
- Live coding: Fix crashing code with exception handling
- Group exercise: Design custom exceptions for a domain
- Discussion: When to validate vs when to catch
- Code review: Find and fix exception handling mistakes

**Rubric Suggestions (Mini Project):**
- Exception handling: 35%
- Custom exceptions: 25%
- Code organization: 20%
- User experience: 15%
- Resource cleanup: 5%

---

*© C# Kickstart Part 2 Contributors*