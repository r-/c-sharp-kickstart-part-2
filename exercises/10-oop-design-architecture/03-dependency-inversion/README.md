# Exercise 10-03: Dependency Inversion Principle

## Goal

Master the Dependency Inversion Principle by refactoring tightly-coupled code into a flexible, interface-based design. You'll learn how depending on abstractions instead of concrete implementations makes code testable, flexible, and maintainable.

## Background

The Dependency Inversion Principle states:
1. **High-level modules should not depend on low-level modules. Both should depend on abstractions.**
2. **Abstractions should not depend on details. Details should depend on abstractions.**

In simple terms: Your important business logic shouldn't be tied to specific implementations. Use interfaces to invert the dependency direction.

## Instructions

You will refactor a notification system that has tight coupling between components.

### Part 1: Analyze the Problem

Study the provided code. The `UserService` class directly creates and uses `EmailSender` and `FileLogger`. This creates tight coupling:
- Can't test `UserService` without sending real emails
- Can't switch to SMS notifications
- Can't use different logging systems
- Hard to reuse `UserService` in different contexts

### Part 2: Apply Dependency Inversion

Refactor using these steps:

1. **Create interfaces** - `INotificationSender` and `ILogger`
2. **Implement concrete classes** - `EmailSender`, `SmsSender`, `ConsoleLogger`, `FileLogger`
3. **Inject dependencies** - Pass interfaces to `UserService` constructor
4. **Demonstrate flexibility** - Show swapping implementations easily

### Part 3: Show the Benefits

Create examples showing:
- Testing with mock implementations
- Switching from email to SMS without changing `UserService`
- Using different loggers for different environments
- How DIP makes the code more flexible

## Requirements

Your solution must:
1. Create `INotificationSender` and `ILogger` interfaces
2. Implement at least 2 notification senders and 2 loggers
3. Modify `UserService` to accept dependencies via constructor
4. Demonstrate dependency injection in action
5. Show testing with mock implementations
6. Prove that high-level code doesn't depend on low-level details

## Expected Output

```
=== User Registration System ===

--- With Email and File Logger ---
[FILE] Validating user: Alice
[FILE] User Alice registered successfully
Email sent to alice@example.com: Welcome Alice!

--- With SMS and Console Logger ---
[CONSOLE] Validating user: Bob
[CONSOLE] User Bob registered successfully
SMS sent to +1234567890: Welcome Bob!

--- With Mock for Testing ---
[MOCK] Validating user: TestUser
[MOCK] User TestUser registered successfully
[MOCK] Notification sent to test@example.com: Welcome TestUser!

=== DIP Benefits ===
✓ UserService doesn't know about EmailSender or SmsSender
✓ UserService doesn't know about FileLogger or ConsoleLogger
✓ Can swap implementations without changing UserService
✓ Easy to test with mock implementations
✓ High-level code depends on abstractions, not details
```

## Starter Code

```csharp
// ❌ This violates DIP - tight coupling to concrete classes
class UserService
{
    private EmailSender emailSender;
    private FileLogger logger;
    
    public UserService()
    {
        // Hard-coded dependencies - violates DIP!
        emailSender = new EmailSender();
        logger = new FileLogger();
    }
    
    public void RegisterUser(string username, string email)
    {
        logger.Log($"Validating user: {username}");
        
        // Validation logic
        if (string.IsNullOrEmpty(username))
        {
            logger.Log("Error: Username cannot be empty");
            return;
        }
        
        logger.Log($"User {username} registered successfully");
        emailSender.SendEmail(email, $"Welcome {username}!");
    }
}

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
```

## Hints

**INotificationSender interface:**
```csharp
interface INotificationSender
{
    void Send(string recipient, string message);
}
```

**ILogger interface:**
```csharp
interface ILogger
{
    void Log(string message);
}
```

**Email implementation:**
```csharp
class EmailSender : INotificationSender
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"Email sent to {recipient}: {message}");
    }
}
```

**SMS implementation:**
```csharp
class SmsSender : INotificationSender
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"SMS sent to {recipient}: {message}");
    }
}
```

**Console logger:**
```csharp
class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[CONSOLE] {message}");
    }
}
```

**Refactored UserService with dependency injection:**
```csharp
class UserService
{
    private INotificationSender notificationSender;
    private ILogger logger;
    
    // Dependencies injected via constructor
    public UserService(INotificationSender sender, ILogger log)
    {
        notificationSender = sender;
        logger = log;
    }
    
    public void RegisterUser(string username, string recipient)
    {
        logger.Log($"Validating user: {username}");
        
        if (string.IsNullOrEmpty(username))
        {
            logger.Log("Error: Username cannot be empty");
            return;
        }
        
        logger.Log($"User {username} registered successfully");
        notificationSender.Send(recipient, $"Welcome {username}!");
    }
}
```

**Usage demonstrating flexibility:**
```csharp
// Production: Email + File logger
var service1 = new UserService(
    new EmailSender(),
    new FileLogger()
);
service1.RegisterUser("Alice", "alice@example.com");

// Production: SMS + Console logger
var service2 = new UserService(
    new SmsSender(),
    new ConsoleLogger()
);
service2.RegisterUser("Bob", "+1234567890");

// Testing: Mock implementations
var service3 = new UserService(
    new MockNotificationSender(),
    new MockLogger()
);
service3.RegisterUser("TestUser", "test@example.com");
```

## Common Mistakes to Avoid

- ❌ Creating dependencies inside the class with `new`
- ❌ Depending on concrete classes instead of interfaces
- ❌ Not providing a way to inject dependencies
- ❌ Making high-level code know about low-level details

## Bonus Challenge

Extend your system with advanced dependency injection patterns:

1. **Factory Pattern** - Create dependencies dynamically
   ```csharp
   interface INotificationFactory
   {
       INotificationSender CreateSender(string type);
   }
   
   class NotificationFactory : INotificationFactory
   {
       public INotificationSender CreateSender(string type)
       {
           switch (type)
           {
               case "email": return new EmailSender();
               case "sms": return new SmsSender();
               case "push": return new PushNotificationSender();
               default: throw new ArgumentException("Unknown type");
           }
       }
   }
   ```

2. **Multiple Notification Channels** - Send via multiple channels
   ```csharp
   class MultiChannelNotifier : INotificationSender
   {
       private List<INotificationSender> senders;
       
       public MultiChannelNotifier(params INotificationSender[] senders)
       {
           this.senders = new List<INotificationSender>(senders);
       }
       
       public void Send(string recipient, string message)
       {
           foreach (var sender in senders)
           {
               sender.Send(recipient, message);
           }
       }
   }
   
   // Usage: Send via both email AND SMS
   var multiChannel = new MultiChannelNotifier(
       new EmailSender(),
       new SmsSender()
   );
   ```

3. **Logging Levels** - Different importance levels
   ```csharp
   interface ILogger
   {
       void Info(string message);
       void Warning(string message);
       void Error(string message);
   }
   
   class ConsoleLogger : ILogger
   {
       public void Info(string msg) => Console.WriteLine($"[INFO] {msg}");
       public void Warning(string msg) => Console.WriteLine($"[WARN] {msg}");
       public void Error(string msg) => Console.WriteLine($"[ERROR] {msg}");
   }
   ```

4. **Configuration-Based Injection** - Choose implementation from config
   ```csharp
   class ServiceConfigurator
   {
       public static UserService CreateUserService(string env)
       {
           if (env == "production")
           {
               return new UserService(
                   new EmailSender(),
                   new FileLogger()
               );
           }
           else if (env == "development")
           {
               return new UserService(
                   new ConsoleNotificationSender(),
                   new ConsoleLogger()
               );
           }
           else  // testing
           {
               return new UserService(
                   new MockNotificationSender(),
                   new MockLogger()
               );
           }
       }
   }
   ```

Example bonus output:
```
=== Advanced DIP System ===

--- Multi-Channel Notification ---
Registering user: Alice
Email sent to alice@example.com: Welcome!
SMS sent to +1234567890: Welcome!
Push notification sent: Welcome!

--- Logging Levels ---
[INFO] User validation started
[WARN] Username contains special characters
[ERROR] Registration failed: Invalid email

--- Configuration-Based ---
Environment: Production
Using: EmailSender + FileLogger
[FILE] User registered: alice@example.com

Environment: Testing  
Using: MockSender + MockLogger
[MOCK] Test user registered: test@example.com
```

## What You're Learning

- How to invert dependencies using interfaces
- The difference between depending on abstractions vs. concrete classes
- Dependency injection through constructors
- How DIP enables testing with mocks
- Making high-level code independent of low-level details
- Building flexible, maintainable systems

## Reflection Questions

After completing this exercise, consider:

1. Why is it easier to test code that follows DIP?
2. How does DIP make it easier to swap implementations?
3. What's the difference between "dependency injection" and "dependency inversion"?
4. How does DIP relate to the other SOLID principles?
5. When might you NOT want to use DIP?

## Real-World Applications

DIP is fundamental to modern software development:

- **ASP.NET Core** - Built-in dependency injection container
- **Testing frameworks** - Mock dependencies for unit tests
- **Plugin systems** - Load different implementations at runtime
- **Multi-environment deployment** - Different implementations for dev/staging/production
- **Enterprise applications** - Swap database providers, cloud services, etc.

## Connection to Other Principles

Notice how DIP supports the other SOLID principles:

- **SRP**: Each implementation has one responsibility
- **OCP**: Can add new implementations without modifying high-level code
- **LSP**: Interfaces define contracts that implementations must follow
- **ISP**: Focused interfaces instead of fat ones

DIP is often considered the most important SOLID principle because it enables all the others!

---

**Time Estimate:** 45 minutes  
**Difficulty:** Medium-Hard  
**Type:** Design Principle - DIP

**Next:** After completing this exercise, move to the [Mini-Project: Library System Refactor](../../projects/10-oop-design-architecture/) to apply all SOLID principles together in a complete system.