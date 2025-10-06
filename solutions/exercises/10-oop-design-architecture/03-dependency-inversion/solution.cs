using System;
using System.Collections.Generic;

namespace Part2.Ch10.Ex03
{
    // ✅ FOLLOWS DIP - Depends on abstractions, not concrete implementations
    
    // Abstraction for notification sending
    interface INotificationSender
    {
        void Send(string recipient, string message);
    }
    
    // Abstraction for logging
    interface ILogger
    {
        void Log(string message);
    }
    
    // Concrete implementation: Email sender
    class EmailSender : INotificationSender
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine($"📧 Email sent to {recipient}: {message}");
        }
    }
    
    // Concrete implementation: SMS sender
    class SmsSender : INotificationSender
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine($"📱 SMS sent to {recipient}: {message}");
        }
    }
    
    // Concrete implementation: Console logger
    class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[CONSOLE] {message}");
        }
    }
    
    // Concrete implementation: File logger
    class FileLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[FILE] {message}");
            // In real code: File.AppendAllText("app.log", message + "\n");
        }
    }
    
    // Mock implementations for testing
    class MockNotificationSender : INotificationSender
    {
        public List<string> SentMessages { get; } = new List<string>();
        
        public void Send(string recipient, string message)
        {
            SentMessages.Add($"{recipient}: {message}");
            Console.WriteLine($"[MOCK] Notification sent to {recipient}: {message}");
        }
    }
    
    class MockLogger : ILogger
    {
        public List<string> LoggedMessages { get; } = new List<string>();
        
        public void Log(string message)
        {
            LoggedMessages.Add(message);
            Console.WriteLine($"[MOCK] {message}");
        }
    }
    
    // High-level class that depends on abstractions
    class UserService
    {
        private INotificationSender notificationSender;
        private ILogger logger;
        
        // Dependencies injected via constructor (Dependency Injection)
        public UserService(INotificationSender sender, ILogger log)
        {
            notificationSender = sender;
            logger = log;
        }
        
        public void RegisterUser(string username, string contact)
        {
            logger.Log($"Validating user: {username}");
            
            // Validation
            if (string.IsNullOrEmpty(username))
            {
                logger.Log("Error: Username cannot be empty");
                return;
            }
            
            // Business logic
            logger.Log($"User {username} registered successfully");
            
            // Send notification (doesn't know if it's email, SMS, etc.)
            notificationSender.Send(contact, $"Welcome {username}!");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== User Registration System (Following DIP) ===\n");
            
            // Configuration 1: Email + File Logger
            Console.WriteLine("--- With Email and File Logger ---");
            var service1 = new UserService(
                new EmailSender(),
                new FileLogger()
            );
            service1.RegisterUser("Alice", "alice@example.com");
            
            Console.WriteLine();
            
            // Configuration 2: SMS + Console Logger
            Console.WriteLine("--- With SMS and Console Logger ---");
            var service2 = new UserService(
                new SmsSender(),
                new ConsoleLogger()
            );
            service2.RegisterUser("Bob", "+1234567890");
            
            Console.WriteLine();
            
            // Configuration 3: Mock implementations for testing
            Console.WriteLine("--- With Mock for Testing ---");
            var mockSender = new MockNotificationSender();
            var mockLogger = new MockLogger();
            var service3 = new UserService(mockSender, mockLogger);
            service3.RegisterUser("TestUser", "test@example.com");
            
            Console.WriteLine($"\nMock captured {mockSender.SentMessages.Count} notifications");
            Console.WriteLine($"Mock captured {mockLogger.LoggedMessages.Count} log messages");
            
            // Show DIP benefits
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("\n=== DIP Benefits ===");
            Console.WriteLine("✓ UserService doesn't know about EmailSender or SmsSender");
            Console.WriteLine("✓ UserService doesn't know about FileLogger or ConsoleLogger");
            Console.WriteLine("✓ Can swap implementations without changing UserService");
            Console.WriteLine("✓ Easy to test with mock implementations");
            Console.WriteLine("✓ High-level code depends on abstractions, not details");
            
            // Demonstrate flexibility
            Console.WriteLine("\n=== Demonstrating Flexibility ===");
            
            // Easy to create different configurations
            Console.WriteLine("\nCreating production configuration:");
            var prodService = CreateProductionService();
            Console.WriteLine("✓ Production: Email + File Logger");
            
            Console.WriteLine("\nCreating development configuration:");
            var devService = CreateDevelopmentService();
            Console.WriteLine("✓ Development: SMS + Console Logger");
            
            Console.WriteLine("\nCreating test configuration:");
            var testService = CreateTestService();
            Console.WriteLine("✓ Testing: Mock sender + Mock logger");
            
            Console.WriteLine("\nAll configurations use the SAME UserService class!");
            Console.WriteLine("No code changes needed - just different dependency injection.");
        }
        
        // Factory methods showing configuration flexibility
        static UserService CreateProductionService()
        {
            return new UserService(
                new EmailSender(),
                new FileLogger()
            );
        }
        
        static UserService CreateDevelopmentService()
        {
            return new UserService(
                new SmsSender(),
                new ConsoleLogger()
            );
        }
        
        static UserService CreateTestService()
        {
            return new UserService(
                new MockNotificationSender(),
                new MockLogger()
            );
        }
    }
}