using System;
using System.Collections.Generic;

namespace Part2.Ch10.Ex03
{
    // ❌ These classes are tightly coupled - violate DIP
    class EmailSender
    {
        public void SendEmail(string to, string message)
        {
            Console.WriteLine($"Email sent to {to}: {message}");
        }
    }
    
    class FileLogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[FILE] {message}");
            // In real code, would write to file
        }
    }
    
    class UserService
    {
        private EmailSender emailSender;
        private FileLogger logger;
        
        public UserService()
        {
            // Hard-coded dependencies - violates DIP!
            // High-level UserService depends on low-level EmailSender and FileLogger
            emailSender = new EmailSender();
            logger = new FileLogger();
        }
        
        public void RegisterUser(string username, string contact)
        {
            logger.Log($"Validating user: {username}");
            
            if (string.IsNullOrEmpty(username))
            {
                logger.Log("Error: Username cannot be empty");
                return;
            }
            
            logger.Log($"User {username} registered successfully");
            emailSender.SendEmail(contact, $"Welcome {username}!");
        }
    }
    
    // TODO: Create these to follow DIP:
    // - INotificationSender interface
    // - ILogger interface
    // - EmailSender implementing INotificationSender
    // - SmsSender implementing INotificationSender
    // - ConsoleLogger implementing ILogger
    // - FileLogger implementing ILogger
    // - Refactored UserService that accepts dependencies via constructor
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== User Registration System ===\n");
            
            // Current implementation (violates DIP)
            Console.WriteLine("❌ Current Design (violates DIP):");
            UserService oldService = new UserService();
            oldService.RegisterUser("Alice", "alice@example.com");
            
            Console.WriteLine("\nProblem: UserService is tightly coupled to EmailSender and FileLogger");
            Console.WriteLine("Cannot test without sending real emails");
            Console.WriteLine("Cannot switch to SMS without modifying UserService");
            Console.WriteLine("High-level code depends on low-level details!\n");
            
            Console.WriteLine(new string('=', 60));
            
            // TODO: Your refactored implementation
            Console.WriteLine("\n✅ Refactored Design (follows DIP):");
            Console.WriteLine("TODO: Implement your refactored classes here\n");
            
            // TODO: Example of how it should work:
            // Console.WriteLine("--- With Email and File Logger ---");
            // var service1 = new UserService(
            //     new EmailSender(),
            //     new FileLogger()
            // );
            // service1.RegisterUser("Alice", "alice@example.com");
            // 
            // Console.WriteLine("\n--- With SMS and Console Logger ---");
            // var service2 = new UserService(
            //     new SmsSender(),
            //     new ConsoleLogger()
            // );
            // service2.RegisterUser("Bob", "+1234567890");
            
            Console.WriteLine("\n=== DIP Benefits ===");
            Console.WriteLine("✓ UserService doesn't know about specific implementations");
            Console.WriteLine("✓ Can swap notification method without changing UserService");
            Console.WriteLine("✓ Can swap logger without changing UserService");
            Console.WriteLine("✓ Easy to test with mock implementations");
            Console.WriteLine("✓ High-level code depends on abstractions, not details");
        }
    }
}