# Mini Project: Robust Data Validator

## Project Overview

Build YOUR own comprehensive data validation system that handles errors gracefully! This is YOUR project to design—create a validator that processes user input, validates multiple data types, and provides clear error messages without ever crashing.

You'll combine everything from Chapter 9: try/catch blocks, custom exceptions, multiple catch handlers, finally blocks, and resource cleanup to create a production-quality validation system.

## Why This Project?

Every professional application validates user input. Payment processors, registration forms, file uploads, API endpoints—they all need robust error handling. This project teaches you to build validation systems that:
- Never crash regardless of input
- Provide helpful error messages
- Recover gracefully from failures
- Log errors for debugging
- Clean up resources properly

These are the same patterns used in enterprise applications handling millions of users.

## Learning Goals

- Combine multiple exception handling techniques
- Create custom exception hierarchy
- Handle errors at appropriate levels
- Provide excellent user experience through error handling
- Write defensive code with proper validation
- Use finally blocks and using statements correctly
- Build systems that are robust and maintainable

## Minimum Requirements

YOUR data validator MUST include:

### 1. Custom Exception Classes (At least 3)

Create a hierarchy of validation exceptions:
- `ValidationException` (base class for all validation errors)
- `InvalidEmailException` (inherits from ValidationException)
- `InvalidPhoneException` (inherits from ValidationException)
- `InvalidAgeException` (inherits from ValidationException)

Each exception should:
- Include the invalid value
- Provide a helpful message
- Inherit properly from Exception or ValidationException

### 2. Validator Class

Create a `DataValidator` class with methods:
- `ValidateEmail(string email)` - Throws `InvalidEmailException` if invalid
- `ValidatePhone(string phone)` - Throws `InvalidPhoneException` if invalid
- `ValidateAge(int age)` - Throws `InvalidAgeException` if invalid
- `ValidateNotEmpty(string value, string fieldName)` - Generic validation
- Each method uses defensive programming

### 3. Interactive Validation Program

Build a console program that:
- Prompts for multiple pieces of data (email, phone, age, etc.)
- Validates each input using try/catch
- Handles specific exceptions with appropriate messages
- Allows retry on validation failure
- Displays summary of successful validations
- Never crashes regardless of input

### 4. Batch Processing

Implement batch validation that:
- Processes multiple records from a list or collection
- Continues even if some records fail
- Collects all validation errors
- Reports success/failure statistics
- Logs all errors to console or file

### 5. Error Logging with Resource Cleanup

Create error logging that:
- Uses try/catch/finally or using statements
- Logs validation errors to a file
- Ensures file is always closed
- Handles file I/O errors gracefully

## Expected Output

```
=== Data Validation System ===

--- Single Record Validation ---

Enter email: alice@example.com
✓ Valid email

Enter phone number: 555-1234
✗ Invalid phone: Must be in format XXX-XXX-XXXX
Enter phone number: 555-123-4567
✓ Valid phone

Enter age: 200
✗ Invalid age: Age must be between 0 and 150 (got 200)
Enter age: 25
✓ Valid age

Enter name: Alice Smith
✓ Valid name

=== Validation Summary ===
Total fields validated: 4
Successful: 4
Failed attempts: 2

--- Batch Processing ---

Processing 5 records...

Record 1: alice@example.com, 555-123-4567, 25
  ✓ All fields valid

Record 2: invalid-email, 555-123-4567, 30
  ✗ Email validation failed: Invalid email format
  
Record 3: bob@test.com, 123, 40
  ✗ Phone validation failed: Must be in format XXX-XXX-XXXX
  
Record 4: carol@example.com, 555-987-6543, -5
  ✗ Age validation failed: Age cannot be negative
  
Record 5: dave@test.com, 555-111-2222, 45
  ✓ All fields valid

=== Batch Results ===
Total records: 5
Valid records: 2
Invalid records: 3
Success rate: 40%

=== Error Log ===
Errors written to: validation_errors.log
Log file closed successfully
```

## Implementation Steps

1. **Design exception hierarchy**
   - Create ValidationException base class
   - Create specific exception types
   - Add properties for invalid values

2. **Build validator methods**
   - Implement email validation logic
   - Implement phone validation logic
   - Implement age range validation
   - Add helper validation methods

3. **Create interactive validation**
   - Prompt for each field
   - Use try/catch for each validation
   - Handle specific exception types
   - Allow retry on failure

4. **Implement batch processing**
   - Create data structure for records
   - Loop through all records
   - Collect errors without stopping
   - Generate statistics report

5. **Add error logging**
   - Create logging method with try/finally
   - Write errors to file
   - Ensure cleanup in finally block
   - Handle file I/O exceptions

6. **Polish and test**
   - Test all validation rules
   - Test error recovery
   - Test batch processing
   - Verify resource cleanup

## Hints

**Exception hierarchy:**
```csharp
class ValidationException : Exception
{
    public string FieldName { get; }
    public string InvalidValue { get; }
    
    public ValidationException(string fieldName, string invalidValue, string message)
        : base(message)
    {
        FieldName = fieldName;
        InvalidValue = invalidValue;
    }
}

class InvalidEmailException : ValidationException
{
    public InvalidEmailException(string email)
        : base("Email", email, $"Invalid email format: {email}")
    {
    }
}
```

**Validator class structure:**
```csharp
class DataValidator
{
    public void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");
        
        if (!email.Contains("@") || !email.Contains("."))
            throw new InvalidEmailException(email);
        
        // Additional validation rules...
    }
    
    public void ValidatePhone(string phone)
    {
        // Remove common formatting
        string digits = phone.Replace("-", "").Replace(" ", "");
        
        if (digits.Length != 10)
            throw new InvalidPhoneException(phone);
        
        if (!long.TryParse(digits, out _))
            throw new InvalidPhoneException(phone);
    }
    
    public void ValidateAge(int age)
    {
        if (age < 0)
            throw new InvalidAgeException(age, "Age cannot be negative");
        
        if (age > 150)
            throw new InvalidAgeException(age, "Age must be 150 or less");
    }
}
```

**Interactive validation with retry:**
```csharp
DataValidator validator = new DataValidator();
string email = null;
bool validEmail = false;

while (!validEmail)
{
    try
    {
        Console.Write("Enter email: ");
        email = Console.ReadLine();
        validator.ValidateEmail(email);
        Console.WriteLine("✓ Valid email");
        validEmail = true;
    }
    catch (InvalidEmailException ex)
    {
        Console.WriteLine($"✗ {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"✗ {ex.Message}");
    }
}
```

**Batch processing:**
```csharp
class PersonRecord
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public int Age { get; set; }
}

void ProcessBatch(List<PersonRecord> records)
{
    int successCount = 0;
    int failCount = 0;
    List<string> errors = new List<string>();
    
    foreach (var record in records)
    {
        try
        {
            validator.ValidateEmail(record.Email);
            validator.ValidatePhone(record.Phone);
            validator.ValidateAge(record.Age);
            
            Console.WriteLine($"✓ Record valid: {record.Email}");
            successCount++;
        }
        catch (ValidationException ex)
        {
            Console.WriteLine($"✗ Record invalid: {ex.Message}");
            errors.Add($"{record.Email}: {ex.Message}");
            failCount++;
        }
    }
    
    Console.WriteLine($"\nResults: {successCount} valid, {failCount} invalid");
}
```

**Error logging with cleanup:**
```csharp
void LogErrors(List<string> errors)
{
    StreamWriter writer = null;
    try
    {
        writer = new StreamWriter("validation_errors.log", append: true);
        writer.WriteLine($"\n=== Validation Run: {DateTime.Now} ===");
        
        foreach (string error in errors)
        {
            writer.WriteLine(error);
        }
        
        Console.WriteLine($"✓ Logged {errors.Count} errors");
    }
    catch (IOException ex)
    {
        Console.WriteLine($"✗ Failed to log errors: {ex.Message}");
    }
    finally
    {
        if (writer != null)
        {
            writer.Close();
            Console.WriteLine("✓ Log file closed");
        }
    }
}
```

## Make It YOUR Own!

Add YOUR creative features:

### Data Validation Types
- **Credit card validation** - Luhn algorithm, expiry date
- **Password strength** - Length, complexity, common passwords
- **URL validation** - Protocol, domain, path checking
- **Date validation** - Format, range, future/past
- **Postal code** - Country-specific formats
- **Username rules** - Length, allowed characters
- **Currency amounts** - Range, decimal places
- **File paths** - Existence, permissions, extensions
- **Social security numbers** - Format, checksum
- **IP addresses** - IPv4, IPv6 validation

### Advanced Features
- **Validation rules engine** - Define rules in configuration
- **Async validation** - Check against external services
- **Batch file processing** - Read from CSV/JSON
- **Multiple languages** - Localized error messages
- **Severity levels** - Warnings vs errors
- **Auto-correction** - Suggest fixes for common mistakes
- **Validation reports** - HTML/PDF error reports
- **Database logging** - Store errors in database
- **Real-time validation** - As-you-type feedback
- **Composite validation** - Cross-field rules
- **Conditional validation** - Rules based on other fields
- **Custom validators** - Plugin architecture for new types

### User Experience
- **Progress indicators** - Show batch progress
- **Colored output** - Green for success, red for errors
- **Detailed help** - Examples of valid formats
- **Import/Export** - Load test data from files
- **Statistics dashboard** - Validation metrics
- **Interactive mode** - Menu-driven interface

## Self-Assessment Checklist

- [ ] Custom exception hierarchy created
- [ ] At least 3 validation methods implemented
- [ ] Try/catch blocks handle all error scenarios
- [ ] Multiple catch blocks for specific exceptions
- [ ] Custom exceptions include helpful data
- [ ] Interactive mode allows retry on errors
- [ ] Batch processing handles partial failures
- [ ] Error logging with proper cleanup
- [ ] Finally blocks or using statements for resources
- [ ] No unhandled exceptions
- [ ] User-friendly error messages
- [ ] Validation summary statistics
- [ ] Code is well-organized
- [ ] Resource leaks prevented

## What Makes a Great Project?

**Exception Handling (40%)**
- Comprehensive try/catch coverage
- Specific exception types caught appropriately
- Custom exceptions well-designed
- Proper resource cleanup

**Validation Logic (30%)**
- Validation rules work correctly
- Edge cases handled
- Clear success/failure reporting
- Helpful error messages

**Code Quality (20%)**
- Well-organized class structure
- Good separation of concerns
- Readable and maintainable
- Follows C# conventions

**User Experience (10%)**
- Clear prompts and feedback
- Professional output formatting
- Recovery from errors
- Helpful guidance

## Testing Scenarios

Test YOUR validator with these cases:

**Email Validation:**
- Valid: alice@example.com, bob@test.co.uk
- Invalid: notanemail, @example.com, alice@, alice.example.com

**Phone Validation:**
- Valid: 555-123-4567, 5551234567, 555 123 4567
- Invalid: 123, 555-12-3456, abc-def-ghij

**Age Validation:**
- Valid: 0, 25, 150
- Invalid: -5, 200, -100

**Batch Processing:**
- Mix of valid and invalid records
- All valid records
- All invalid records
- Empty batch

**Error Recovery:**
- Invalid input followed by valid input
- Multiple validation errors
- File write errors
- Resource cleanup on errors

---

**Time Estimate:** 2-3 hours  
**Difficulty:** Medium-Hard  
**Type:** Comprehensive Error Handling System

This project demonstrates professional-level error handling—the kind you'll write in every production application. Build it well, and you'll have a solid foundation for robust C# development!