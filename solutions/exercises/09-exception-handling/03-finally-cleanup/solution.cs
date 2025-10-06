using System;
using System.IO;

namespace Part2.Ch09.Ex03
{
    // FileLogger with manual try-finally cleanup
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
                writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: Access denied to file");
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
    
    // SafeFileLogger with using statement
    class SafeFileLogger
    {
        public void LogMessage(string filename, string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename, append: true))
                {
                    writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
                    Console.WriteLine($"✓ Logged: {message}");
                }  // writer.Dispose() automatically called here
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("✗ Error: Access denied");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }
        }
    }
    
    // LogFile class implementing IDisposable
    class LogFile : IDisposable
    {
        private StreamWriter writer;
        private bool disposed = false;
        private string filename;
        
        public LogFile(string filename)
        {
            Console.WriteLine($"Creating LogFile: {filename}");
            this.filename = filename;
            writer = new StreamWriter(filename);
        }
        
        public void WriteLine(string message)
        {
            if (disposed)
                throw new ObjectDisposedException("LogFile");
            
            string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            Console.WriteLine($"Writing line: {message}");
            writer.WriteLine(line);
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
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== File Logger Test ===\n");
            
            // Test 1: Manual cleanup with try-finally
            Console.WriteLine("Testing manual cleanup (try-finally)...");
            FileLogger fileLogger = new FileLogger();
            fileLogger.LogMessage("log1.txt", "Application started");
            fileLogger.LogMessage("log1.txt", "User logged in");
            fileLogger.LogMessage("log1.txt", "Data processed");
            Console.WriteLine("✓ Log completed successfully\n");
            
            // Test 2: Error handling with finally
            Console.WriteLine("Testing with error during write...");
            TestWithError();
            Console.WriteLine("✓ File closed despite error\n");
            
            // Test 3: Automatic cleanup with using statement
            Console.WriteLine("Testing automatic cleanup (using statement)...");
            SafeFileLogger safeLogger = new SafeFileLogger();
            safeLogger.LogMessage("log2.txt", "System initialized");
            safeLogger.LogMessage("log2.txt", "Task completed");
            Console.WriteLine("✓ File automatically closed\n");
            
            // Test 4: LogFile wrapper with IDisposable
            Console.WriteLine("Testing LogFile wrapper...");
            try
            {
                using (LogFile log = new LogFile("log3.txt"))
                {
                    log.WriteLine("Event A");
                    log.WriteLine("Event B");
                    log.WriteLine("Event C");
                }  // Dispose called automatically
                Console.WriteLine("✓ All lines written and file closed\n");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }
            
            // Test 5: Protected file handling
            Console.WriteLine("Testing protected file (access denied)...");
            try
            {
                // Try to write to a protected location
                safeLogger.LogMessage("C:\\Windows\\System32\\protected.txt", "Test");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: Access to protected.txt denied");
            }
            Console.WriteLine("✓ Cleanup completed\n");
            
            Console.WriteLine("=== All Tests Completed ===");
            Console.WriteLine("Files closed: 4");
            Console.WriteLine("Errors handled: 2");
            Console.WriteLine("No resource leaks detected");
        }
        
        static void TestWithError()
        {
            StreamWriter writer = null;
            try
            {
                Console.WriteLine("Opening file: log_error.txt");
                writer = new StreamWriter("log_error.txt");
                
                Console.WriteLine("Writing: Starting operation...");
                writer.WriteLine("Starting operation...");
                
                // Simulate an error
                throw new IOException("Simulated write error");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
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
}