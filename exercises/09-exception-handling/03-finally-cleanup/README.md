# Exercise 09-03: Finally and Resource Cleanup

## Goal

Master resource cleanup using `finally` blocks and `using` statements. You'll build a simple file logger that ensures files are always closed properly, even when errors occur during writing.

## Background

Resources like files, database connections, and network streams must be cleaned up properly to avoid resource leaks. The `finally` block runs whether an exception occurs or not, making it perfect for cleanup. The `using` statement provides a cleaner syntax for the same pattern.

## Instructions

You will create a logging system that demonstrates proper resource management.

### Part 1: Manual Cleanup with Finally

Create a `FileLogger` class with:
- Method `LogMessage(string filename, string message)` using try-catch-finally
- Opens file for writing
- Writes message
- Always closes file in finally block
- Handles file-related exceptions

### Part 2: Automatic Cleanup with Using

Create a `SafeFileLogger` class with:
- Method `LogMessage(string filename, string message)` using the `using` statement
- Demonstrates automatic resource disposal
- Cleaner syntax than manual finally

### Part 3: Resource Wrapper

Create a `LogFile` class that implements `IDisposable`:
- Constructor opens the file
- Method `WriteLine(string message)` writes to file
- `Dispose()` method closes the file
- Used with `using` statement

### Part 4: Testing

Create a test program that:
- Logs several messages successfully
- Triggers an error during logging
- Verifies files are closed even when errors occur
- Compares manual vs automatic cleanup approaches

## Requirements

Your solution must:
1. Implement FileLogger with try-catch-finally
2. Implement SafeFileLogger with using statement
3. Create LogFile class implementing IDisposable
4. Handle FileNotFoundException
5. Handle UnauthorizedAccessException
6. Handle IOException
7. Ensure files are closed in all scenarios
8. Never leak file handles

## Expected Output

```
=== File Logger Test ===

Testing manual cleanup (try-finally)...
Opening file: log1.txt
Writing: Application started
Writing: User logged in
Writing: Data processed
Closing file (finally block executed)
✓ Log completed successfully

Testing with error during write...
Opening file: log2.txt
Writing: Starting operation...
Error occurred: Simulated write error
Closing file (finally block executed)
✓ File closed despite error

Testing automatic cleanup (using statement)...
✓ Logged: System initialized
✓ Logged: Task completed
✓ File automatically closed

Testing LogFile wrapper...
Creating LogFile: log3.txt
Writing line 1: Event A
Writing line 2: Event B
Writing line 3: Event C
Disposing LogFile
✓ All lines written and file closed

Testing protected file (access denied)...
✗ Error: Access to protected.txt denied
✓ Cleanup completed

=== All Tests Completed ===
Files closed: 4
Errors handled: 2
No resource leaks detected
```

## Hints

**Manual cleanup with finally:**
```csharp
class FileLogger
{
    public void LogMessage(string filename, string message)
    {
        StreamWriter writer = null;
        try
        {
            Console.WriteLine($"Opening file: {filename}");
            writer = new StreamWriter(filename, append: true);
            
            Console.WriteLine($"Writing: {message}");
            writer.WriteLine($"[{DateTime.Now}] {message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            if (writer != null)
            {
                Console.WriteLine("Closing file (finally block executed)");
                writer.Close();
            }
        }
    }
}
```

**Automatic cleanup with using:**
```csharp
class SafeFileLogger
{
    public void LogMessage(string filename, string message)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filename, append: true))
            {
                writer.WriteLine($"[{DateTime.Now}] {message}");
                Console.WriteLine($"✓ Logged: {message}");
            }  // writer.Dispose() automatically called here
        }
        catch (IOException ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }
}
```

**IDisposable implementation:**
```csharp
class LogFile : IDisposable
{
    private StreamWriter writer;
    private bool disposed = false;
    
    public LogFile(string filename)
    {
        Console.WriteLine($"Creating LogFile: {filename}");
        writer = new StreamWriter(filename);
    }
    
    public void WriteLine(string message)
    {
        if (disposed)
            throw new ObjectDisposedException("LogFile");
        
        Console.WriteLine($"Writing line: {message}");
        writer.WriteLine($"[{DateTime.Now}] {message}");
    }
    
    public void Dispose()
    {
        if (!disposed)
        {
            Console.WriteLine("Disposing LogFile");
            writer?.Close();
            disposed = true;
        }
    }
}
```

**Using the LogFile class:**
```csharp
try
{
    using (LogFile log = new LogFile("app.log"))
    {
        log.WriteLine("Event A");
        log.WriteLine("Event B");
        log.WriteLine("Event C");
    }  // log.Dispose() called automatically
}
catch (IOException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

**C# 8+ using declaration (simpler syntax):**
```csharp
void LogMessages()
{
    using StreamWriter writer = new StreamWriter("log.txt");
    writer.WriteLine("Message 1");
    writer.WriteLine("Message 2");
    // Dispose called at end of method
}
```

## Common Mistakes to Avoid

- ❌ Not checking if resource is null before closing
- ❌ Forgetting to implement IDisposable for custom resources
- ❌ Calling Dispose() multiple times without check
- ❌ Not handling exceptions during cleanup
- ❌ Leaving resource handles open on errors

## Bonus Challenge

Enhance your logging system with these features:

1. **Batch logger** - Write multiple messages, close file in finally
2. **Rotation logger** - Create new file when size limit reached
3. **Multi-file logger** - Write to multiple files simultaneously
4. **Compressed logger** - Compress old log files
5. **Async logger** - Use async/await with proper disposal
6. **Transaction log** - Rollback writes if error occurs

Example bonus output:
```
=== Batch Logger ===
Batch started: 5 messages queued
Writing batch...
  ✓ Message 1/5
  ✓ Message 2/5
  ✓ Message 3/5
  ✗ Error at 4/5: Disk full
Closing batch file
Rolled back 3 messages

=== Rotation Logger ===
Writing to: log_2024-01-01_001.txt
File size: 950 KB
Size limit reached, rotating...
Created: log_2024-01-01_002.txt
Compressing: log_2024-01-01_001.txt
✓ Rotation complete
```

## What You're Learning

- Using finally blocks for guaranteed cleanup
- Automatic cleanup with using statements
- Implementing IDisposable interface
- Resource leak prevention
- Professional file handling patterns

---

**Time Estimate:** 40 minutes  
**Difficulty:** Medium  
**Type:** Resource Management

**Next:** After completing this exercise, you're ready for the [Mini Project: Robust Data Validator](../../projects/09-exception-handling/) to build a complete error-handling system.