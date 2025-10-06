
# Chapter 10 – Object-Oriented Design and Software Architecture

In Chapter 9, we mastered exception handling to write robust, crash-resistant code. Now we'll learn the **design principles** that make code maintainable, flexible, and professional. You'll discover SOLID principles, learn when to favor composition over inheritance, and get your first glimpse into software architecture patterns used in real-world applications.

## Learning Objectives

By the end of this chapter, you will be able to:

- Explain the five SOLID principles of object-oriented design
- Apply the Single Responsibility Principle to create focused classes
- Use the Open/Closed Principle to write extensible code
- Implement Dependency Inversion to reduce coupling
- Choose between composition and inheritance effectively
- Recognize common architectural patterns used in professional software
- Refactor messy code into clean, maintainable designs

## Prerequisites

This chapter assumes you've completed **Chapter 9** and can:

- Handle exceptions with try/catch/finally
- Create and use custom exception types
- Work with interfaces and abstract classes
- Implement generic classes and methods
- Use polymorphism and interface-based design
- Write well-encapsulated classes

---

## Mission Brief

In this chapter you will:

- Discover the **SOLID principles**—five rules that make OOP code better
- Learn why some class designs are better than others
- Master the art of writing code that's easy to change
- Understand when to use composition instead of inheritance
- See how professional developers organize large applications
- Refactor bad code into good code using proven principles

This is the chapter that transforms you from a programmer who *writes code* into a developer who *designs systems*.

---

## Concept Power-Up – The Problem with Bad Design

Imagine you're building a game. At first, your code works:

```csharp
// ❌ Bad design - everything in one giant class
class Game
{
    private List<Enemy> enemies = new List<Enemy>();
    private int score;
    private string playerName;
    
    public void Start()
    {
        Console.WriteLine("Enter your name:");
        playerName = Console.ReadLine();
        
        // Initialize enemies
        enemies.Add(new Enemy("Goblin", 50));
        enemies.Add(new Enemy("Dragon", 200));
        
        // Game loop
        while (enemies.Count > 0)
        {
            Console.WriteLine($"Score: {score}");
            Console.WriteLine("1. Attack  2. Defend  3. Save Game");
            
            string choice = Console.ReadLine();
            
            // Handle input
            if (choice == "1") { /* attack logic */ }
            else if (choice == "2") { /* defend logic */ }
            else if (choice == "3") { SaveGameToFile(); }
        }
        
        Console.WriteLine($"Game over! Final score: {score}");
    }
    
    private void SaveGameToFile()
    {
        // File I/O code mixed with game logic
        File.WriteAllText("save.txt", $"{playerName},{score}");
    }
}
```

**Problems with this design:**
- One class does *everything*: UI, game logic, file I/O
- Hard to test—must run the whole game to test one feature
- Hard to change—fixing the UI breaks game logic
- Hard to reuse—can't use game logic in a different interface
- Hard to maintain—1000+ lines in one file

With **good design principles**:

```csharp
// ✅ Good design - separated concerns
class GameEngine
{
    public int Score { get; private set; }
    public List<Enemy> Enemies { get; private set; }
    
    public void Attack(Enemy target) { /* clean game logic */ }
    public void Defend() { /* clean game logic */ }
}

class ConsoleUI
{
    private GameEngine engine;
    
    public void ShowMenu() { /* just UI code */ }
    public void DisplayScore() { /* just UI code */ }
}

class SaveManager
{
    public void SaveGame(GameState state) { /* just file I/O */ }
    public GameState LoadGame() { /* just file I/O */ }
}
```

> **Key Insight**: Good design separates different responsibilities into different classes. This makes code easier to understand, test, change, and reuse.

---

## Core Concepts

### Concept 1 – Single Responsibility Principle (SRP)

**Definition**: A class should have only one reason to change.

Each class should do **one thing** and do it well. If a class handles multiple responsibilities, changes to one responsibility can break another.

#### Example: Email System

```csharp
// ❌ Violates SRP - class has multiple responsibilities
class Email
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    
    // Responsibility 1: Validate email
    public bool IsValid()
    {
        return To.Contains("@") && !string.IsNullOrEmpty(Subject);
    }
    
    // Responsibility 2: Format email for display
    public string FormatForDisplay()
    {
        return $"To: {To}\nSubject: {Subject}\n\n{Body}";
    }
    
    // Responsibility 3: Send email via SMTP
    public void Send()
    {
        // Complex SMTP logic here
        Console.WriteLine($"Sending email to {To}...");
    }
}
```

**Why this is bad:**
- If SMTP protocol changes, this class changes
- If display format changes, this class changes
- If validation rules change, this class changes
- Hard to test sending without validating
- Can't reuse validation in other contexts

```csharp
// ✅ Follows SRP - each class has one responsibility
class Email
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}

class EmailValidator
{
    public bool IsValid(Email email)
    {
        return email.To.Contains("@") && 
               !string.IsNullOrEmpty(email.Subject);
    }
}

class EmailFormatter
{
    public string FormatForDisplay(Email email)
    {
        return $"To: {email.To}\nSubject: {email.Subject}\n\n{email.Body}";
    }
}

class EmailSender
{
    public void Send(Email email)
    {
        // SMTP logic only
        Console.WriteLine($"Sending email to {email.To}...");
    }
}
```

**Benefits:**
- Each class has one reason to change
- Easy to test each responsibility separately
- Can reuse validator in other email-related classes
- Can swap out sender implementation (SMTP, SendGrid, Mock)

> **Remember**: "Do one thing, and do it well."

---

### Concept 2 – Open/Closed Principle (OCP)

**Definition**: Classes should be open for extension but closed for modification.

You should be able to add new functionality **without changing existing code**. This prevents bugs in working code when adding features.

#### Example: Shape Area Calculator

```csharp
// ❌ Violates OCP - must modify class to add new shapes
class AreaCalculator
{
    public double CalculateArea(object shape)
    {
        if (shape is Circle circle)
        {
            return Math.PI * circle.Radius * circle.Radius;
        }
        else if (shape is Rectangle rect)
        {
            return rect.Width * rect.Height;
        }
        // Every new shape requires modifying this method!
        else if (shape is Triangle triangle)
        {
            return 0.5 * triangle.Base * triangle.Height;
        }
        
        throw new ArgumentException("Unknown shape");
    }
}
```

**Why this is bad:**
- Adding new shapes requires modifying `CalculateArea`
- Risk of breaking existing calculations
- If-else chain grows forever
- Violates the "closed for modification" principle

```csharp
// ✅ Follows OCP - extend by adding new classes
interface IShape
{
    double CalculateArea();
}

class Circle : IShape
{
    public double Radius { get; set; }
    
    public double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}

class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public double CalculateArea()
    {
        return Width * Height;
    }
}

class Triangle : IShape
{
    public double Base { get; set; }
    public double Height { get; set; }
    
    public double CalculateArea()
    {
        return 0.5 * Base * Height;
    }
}

class AreaCalculator
{
    public double CalculateTotal(List<IShape> shapes)
    {
        double total = 0;
        foreach (var shape in shapes)
        {
            total += shape.CalculateArea();  // Polymorphism!
        }
        return total;
    }
}
```

**Benefits:**
- Add new shapes without changing `AreaCalculator`
- Each shape knows how to calculate its own area
- No risk of breaking existing shapes
- Clean, extensible design

> **Remember**: "Open for extension, closed for modification."

---

### Concept 3 – Composition Over Inheritance

**Principle**: Favor "has-a" relationships over "is-a" relationships.

Inheritance creates tight coupling between parent and child classes. Composition allows flexible combination of behaviors.

#### Example: Game Characters with Abilities

```csharp
// ❌ Inheritance explosion - rigid and hard to extend
class Character { }
class FlyingCharacter : Character { }
class SwimmingCharacter : Character { }
class FlyingSwimmingCharacter : Character { }  // Yikes!
class ShootingCharacter : Character { }
class FlyingShootingCharacter : Character { }  // This gets messy fast
```

**Problems:**
- Can't combine abilities flexibly
- Inheritance tree becomes complex
- Hard to add new ability combinations
- Violates the "favor composition" principle

```csharp
// ✅ Composition - flexible and extensible
interface IAbility
{
    void Execute();
}

class FlyAbility : IAbility
{
    public void Execute() => Console.WriteLine("Flying!");
}

class SwimAbility : IAbility
{
    public void Execute() => Console.WriteLine("Swimming!");
}

class ShootAbility : IAbility
{
    public void Execute() => Console.WriteLine("Shooting!");
}

class Character
{
    private List<IAbility> abilities = new List<IAbility>();
    
    public void AddAbility(IAbility ability)
    {
        abilities.Add(ability);
    }
    
    public void UseAbilities()
    {
        foreach (var ability in abilities)
        {
            ability.Execute();
        }
    }
}

// Usage
var hero = new Character();
hero.AddAbility(new FlyAbility());
hero.AddAbility(new ShootAbility());
hero.UseAbilities();  // Flying! Shooting!

var villain = new Character();
villain.AddAbility(new SwimAbility());
villain.AddAbility(new FlyAbility());
villain.AddAbility(new ShootAbility());
villain.UseAbilities();  // Swimming! Flying! Shooting!
```

**Benefits:**
- Combine abilities in any way at runtime
- Add new abilities without changing `Character`
- No complex inheritance hierarchies
- Each ability is independent and reusable

> **Remember**: "Has-a is often better than is-a."

---

### Concept 4 – Dependency Inversion Principle (DIP)

**Definition**: Depend on abstractions, not concrete implementations.

High-level code shouldn't depend on low-level details. Both should depend on interfaces.

#### Example: Notification System

```csharp
// ❌ Violates DIP - tightly coupled to EmailSender
class UserService
{
    private EmailSender emailSender = new EmailSender();
    
    public void RegisterUser(string username)
    {
        // Registration logic
        Console.WriteLine($"User {username} registered");
        
        // Send notification
        emailSender.SendEmail(username, "Welcome!");
    }
}

class EmailSender
{
    public void SendEmail(string to, string message)
    {
        Console.WriteLine($"Email to {to}: {message}");
    }
}
```

**Problems:**
- Can't switch to SMS notifications
- Can't test without sending real emails
- Hard to support multiple notification types
- `UserService` depends on concrete `EmailSender`

```csharp
// ✅ Follows DIP - depends on abstraction
interface INotificationSender
{
    void Send(string recipient, string message);
}

class EmailSender : INotificationSender
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"Email to {recipient}: {message}");
    }
}

class SmsSender : INotificationSender
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"SMS to {recipient}: {message}");
    }
}

class UserService
{
    private INotificationSender notificationSender;
    
    // Dependency is injected
    public UserService(INotificationSender sender)
    {
        notificationSender = sender;
    }
    
    public void RegisterUser(string username)
    {
        Console.WriteLine($"User {username} registered");
        notificationSender.Send(username, "Welcome!");
    }
}

// Usage
var service1 = new UserService(new EmailSender());
service1.RegisterUser("Alice");  // Sends email

var service2 = new UserService(new SmsSender());
service2.RegisterUser("Bob");  // Sends SMS
```

**Benefits:**
- Easy to switch notification methods
- Can inject mock sender for testing
- `UserService` doesn't know implementation details
- Flexible and maintainable

> **Remember**: "Depend on what things do, not how they do it."

---

### Concept 5 – SOLID in Practice

Let's see all principles working together in a complete example:

```csharp
// Define abstractions
interface IRepository
{
    void Save(string data);
    string Load();
}

interface ILogger
{
    void Log(string message);
}

// Implementations follow SRP
class FileRepository : IRepository
{
    public void Save(string data)
    {
        File.WriteAllText("data.txt", data);
    }
    
    public string Load()
    {
        return File.ReadAllText("data.txt");
    }
}

class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}

// High-level class depends on abstractions (DIP)
class DataManager
{
    private IRepository repository;
    private ILogger logger;
    
    public DataManager(IRepository repo, ILogger log)
    {
        repository = repo;
        logger = log;
    }
    
    public void SaveData(string data)
    {
        try
        {
            repository.Save(data);
            logger.Log("Data saved successfully");
        }
        catch (Exception ex)
        {
            logger.Log($"Error: {ex.Message}");
        }
    }
}

// Usage - all dependencies injected
var manager = new DataManager(
    new FileRepository(),
    new ConsoleLogger()
);

manager.SaveData("Hello, SOLID!");
```

**What makes this design good:**
- **SRP**: Each class has one responsibility
- **OCP**: Can add new repository/logger types without changing `DataManager`
- **DIP**: `DataManager` depends on interfaces, not concrete classes
- Easy to test with mock implementations
- Flexible and maintainable

---

## 💻 Try It – Refactor a Violation

Here's code that violates SOLID principles. Can you spot the problems?

```csharp
class ReportGenerator
{
    public void GenerateReport(List<string> data)
    {
        // Build report
        string report = "=== REPORT ===\n";
        foreach (var item in data)
        {
            report += item + "\n";
        }
        
        // Save to file
        File.WriteAllText("report.txt", report);
        
        // Send via email
        Console.WriteLine("Sending report via email...");
        
        // Print to console
        Console.WriteLine(report);
    }
}
```

**Problems:**
1. Violates SRP (generates, saves, sends, prints)
2. Violates OCP (can't add PDF output without modifying)
3. Violates DIP (depends on concrete File and Console)

**Your task**: Refactor into separate classes that follow SOLID.

<details>
<summary>💡 Hint</summary>

Create:
- `ReportBuilder` (builds the report)
- `IReportWriter` interface with implementations
- Main class that uses dependency injection

</details>

---

## 🎯 Challenge – Design a Plugin System

Design a plugin system where:
- Main application doesn't know what plugins exist
- Plugins can be added without modifying the application
- Each plugin does one specific task
- Plugins are loaded through an interface

**Requirements:**
- Define `IPlugin` interface
- Create at least 3 different plugin types
- Build a `PluginManager` that loads and executes plugins
- Demonstrate adding a new plugin without changing existing code

---

## Mini-Project – Library Management System Refactor

**Scenario**: You've inherited messy code for a library system. Your job is to refactor it using SOLID principles.

### The Messy Code

```csharp
// ❌ Everything in one giant class
class Library
{
    private List<string> books = new List<string>();
    private List<string> borrowers = new List<string>();
    
    public void AddBook(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            Console.WriteLine("Error: Title cannot be empty");
            return;
        }
        books.Add(title);
        File.AppendAllText("books.txt", title + "\n");
        Console.WriteLine($"Added: {title}");
    }
    
    public void BorrowBook(string title, string borrower)
    {
        if (!books.Contains(title))
        {
            Console.WriteLine("Book not found");
            return;
        }
        
        borrowers.Add($"{borrower}:{title}");
        books.Remove(title);
        File.AppendAllText("borrowed.txt", $"{borrower}:{title}\n");
        Console.WriteLine($"{borrower} borrowed {title}");
    }
    
    public void ShowBooks()
    {
        Console.WriteLine("=== Available Books ===");
        foreach (var book in books)
        {
            Console.WriteLine("- " + book);
        }
    }
}
```

### Your Mission

Refactor this code to:

1. **Separate Responsibilities**
   - Book management
   - Data storage
   - UI/Display
   - Validation

2. **Apply SOLID Principles**
   - SRP: Each class does one thing
   - OCP: Easy to add new storage types
   - DIP: Use interfaces for dependencies

3. **Create These Components**
   - `Book` class (represents a book)
   - `IBookRepository` interface (storage abstraction)
   - `FileBookRepository` implementation
   - `BookValidator` (validation logic)
   - `LibraryService` (business logic)
   - `LibraryUI` (console interface)

4. **Demonstrate Flexibility**
   - Show how easy it is to swap file storage for in-memory storage
   - Add a new book type (eBook) without changing existing code
   - Add logging without modifying core classes

### Expected Design

```
Program.cs
    └── Creates LibraryUI with dependencies

LibraryUI
    └── Uses LibraryService

LibraryService
    ├── Uses IBookRepository (abstraction)
    └── Uses BookValidator

IBookRepository (interface)
    ├── FileBookRepository (implementation)
    └── MemoryBookRepository (implementation)

Book (data class)
    └── Properties only

BookValidator
    └── Validation rules
```

### Success Criteria

✅ Each class has a single, clear responsibility  
✅ Can switch storage implementation easily  
✅ Can add new features without modifying existing classes  
✅ Dependencies are injected, not hard-coded  
✅ Code is testable and maintainable

---

## Common Mistakes

### ❌ Mistake 1: God Classes

```csharp
// Everything in one class
class Application
{
    // 50+ methods doing unrelated things
    public void DoEverything() { }
}
```

**Why it's wrong**: Violates SRP, impossible to maintain.

**Fix**: Split into focused classes with single responsibilities.

---

### ❌ Mistake 2: Concrete Dependencies

```csharp
// Hard-coded dependency
class Service
{
    private FileLogger logger = new FileLogger();  // Tight coupling!
}
```

**Why it's wrong**: Can't test or swap implementations.

**Fix**: Depend on `ILogger` interface and inject the dependency.

---

### ❌ Mistake 3: Inheritance for Code Reuse

```csharp
// Inheriting just to reuse methods
class Car { public void StartEngine() { } }
class Boat : Car { }  // Boats don't have car engines!
```

**Why it's wrong**: Wrong relationship, violates is-a principle.

**Fix**: Use composition—give Boat an `IEngine` component.

---

## Beyond SOLID: A Glimpse into Software Architecture

Now that you understand **design principles**, let's look at **architectural patterns**—the large-scale structures used to organize entire applications.

### What is Software Architecture?

**Software architecture** is the high-level organization of a system. While SOLID principles help you design individual classes, architectural patterns help you organize those classes into larger structures.

Think of it this way:
- **SOLID principles** = How to design a room
- **Architecture patterns** = How to design the entire building

### Common Architectural Patterns

#### 1. Layered Architecture (N-Tier)

Organizes code into distinct layers, each with a specific responsibility:

```
┌─────────────────────────┐
│  Presentation Layer     │  ← UI (Console, Web, Desktop)
├─────────────────────────┤
│  Business Logic Layer   │  ← Core application logic
├─────────────────────────┤
│  Data Access Layer      │  ← Database, files, APIs
└─────────────────────────┘
```

**Example in a console app:**

```csharp
// Presentation Layer
class ConsoleUI
{
    private OrderService orderService;
    
    public void ShowMenu()
    {
        // Display menu, get user input
        // Call orderService methods
    }
}

// Business Logic Layer
class OrderService
{
    private IOrderRepository repository;
    
    public void CreateOrder(Order order)
    {
        // Validate, calculate totals
        repository.Save(order);
    }
}

// Data Access Layer
interface IOrderRepository
{
    void Save(Order order);
    Order Load(int id);
}

class FileOrderRepository : IOrderRepository
{
    public void Save(Order order)
    {
        // File I/O logic
    }
}
```

**Benefits:**
- Clear separation of concerns
- Each layer can change independently
- Easy to test each layer in isolation
- Can swap out layers (e.g., Console UI → Web UI)

---

#### 2. Model-View-Controller (MVC)

Separates application into three interconnected components:

```
┌─────────┐      ┌────────────┐      ┌──────┐
│  View   │ ←──→ │ Controller │ ←──→ │ Model│
│  (UI)   │      │  (Logic)   │      │(Data)│
└─────────┘      └────────────┘      └──────┘
```

**What each component does:**

- **Model**: Represents data and business logic
- **View**: Displays data to the user (UI)
- **Controller**: Handles user input, updates Model and View

**Example in console app:**

```csharp
// Model - represents data
class TodoItem
{
    public string Task { get; set; }
    public bool IsComplete { get; set; }
}

class TodoList
{
    private List<TodoItem> items = new List<TodoItem>();
    
    public void AddTask(string task)
    {
        items.Add(new TodoItem { Task = task });
    }
    
    public List<TodoItem> GetTasks() => items;
}

// View - handles display
class ConsoleView
{
    public void ShowTasks(List<TodoItem> tasks)
    {
        Console.WriteLine("=== Your Tasks ===");
        for (int i = 0; i < tasks.Count; i++)
        {
            string status = tasks[i].IsComplete ? "✓" : " ";
            Console.WriteLine($"{i + 1}. [{status}] {tasks[i].Task}");
        }
    }
    
    public string GetUserInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}

// Controller - coordinates Model and View
class TodoController
{
    private TodoList model;
    private ConsoleView view;
    
    public TodoController(TodoList model, ConsoleView view)
    {
        this.model = model;
        this.view = view;
    }
    
    public void Run()
    {
        while (true)
        {
            view.ShowTasks(model.GetTasks());
            string task = view.GetUserInput("Add task (or 'quit'): ");
            
            if (task == "quit") break;
            
            model.AddTask(task);
        }
    }
}

// Usage
var app = new TodoController(new TodoList(), new ConsoleView());
app.Run();
```

**Benefits:**
- Separates data, display, and logic
- Can change UI without touching business logic
- Easy to test controller and model separately
- Common pattern in web and desktop applications

---

#### 3. Repository Pattern

Abstracts data access behind an interface, hiding storage details:

```csharp
// Repository interface
interface IUserRepository
{
    void Add(User user);
    User GetById(int id);
    List<User> GetAll();
    void Update(User user);
    void Delete(int id);
}

// File-based implementation
class FileUserRepository : IUserRepository
{
    public void Add(User user)
    {
        // Save to file
    }
    
    public User GetById(int id)
    {
        // Load from file
        return null;
    }
}

// In-memory implementation (for testing)
class MemoryUserRepository : IUserRepository
{
    private List<User> users = new List<User>();
    
    public void Add(User user)
    {
        users.Add(user);
    }
    
    public User GetById(int id)
    {
        return users.Find(u => u.Id == id);
    }
}

// Business logic doesn't care about storage
class UserService
{
    private IUserRepository repository;
    
    public UserService(IUserRepository repo)
    {
        repository = repo;
    }
    
    public void RegisterUser(User user)
    {
        // Validation logic
        repository.Add(user);
    }
}
```

**Benefits:**
- Business logic doesn't depend on storage details
- Easy to swap storage (file, database, cloud, memory)
- Simple to test with in-memory repository
- Clean separation of concerns

---

### When to Use Each Pattern

| Pattern | Best For | Example Projects |
|---------|----------|------------------|
| **Layered** | Most applications | Business apps, CRUD systems |
| **MVC** | Interactive UIs | Web apps, desktop apps |
| **Repository** | Data-heavy apps | E-commerce, CMS |

### Key Takeaway

These architectural patterns are built on the same SOLID principles you learned in this chapter:
- **SRP**: Each layer/component has one responsibility
- **DIP**: Components depend on interfaces, not implementations
- **OCP**: Easy to extend with new implementations

As you continue learning, you'll encounter these patterns in:
- ASP.NET Core (web applications)
- WPF/WinForms (desktop applications)
- Unity (game development)
- Enterprise applications

For now, focus on mastering SOLID principles in your console applications. The architectural patterns will become clearer when you start building larger systems.

> **Looking Ahead**: In professional development, you'll often work within established architectures. Understanding SOLID gives you the foundation to work effectively in any architectural pattern.

---

## Hands-On Practice

Time to apply these principles! Complete these exercises:

1. **Exercise 10-01**: [Single Responsibility Principle](../../exercises/10-oop-design-architecture/01-single-responsibility/)
   - Refactor a messy class into focused components
   - Practice identifying responsibilities

2. **Exercise 10-02**: [Open/Closed Principle](../../exercises/10-oop-design-architecture/02-open-closed/)
   - Build an extensible discount system
   - Add new features without modifying existing code

3. **Exercise 10-03**: [Dependency Inversion](../../exercises/10-oop-design-architecture/03-dependency-inversion/)
   - Refactor tightly-coupled code
   - Use interfaces and dependency injection

4. **Mini-Project**: [Library System Refactor](../../projects/10-oop-design-architecture/)
   - Apply all SOLID principles
   - Build a maintainable, testable system

---

## 🎭 Reflection

After completing this chapter, you should be able to answer:

1. What does "single responsibility" mean for a class?
2. How does the Open/Closed Principle prevent bugs in working code?
3. When should you favor composition over inheritance?
4. What problem does Dependency Inversion solve?
5. How do SOLID principles make code easier to test?
6. What is the difference between design principles and architectural patterns?

**Think about:**
- How would you refactor your previous projects using SOLID?
- Which principle do you find most useful?
- How does good design save time in the long run?

---

## Key Terms

- **SOLID**: Five principles for good OO design
- **Single Responsibility Principle (SRP)**: One class, one reason to change
- **Open/Closed Principle (OCP)**: Open for extension, closed for modification
- **Dependency Inversion Principle (DIP)**: Depend on abstractions
- **Composition**: Building objects from components (has-a)
- **Inheritance**: Deriving classes from base classes (is-a)
- **Dependency Injection**: Passing dependencies to a class
- **Abstraction**: Hiding implementation behind an interface
- **Coupling**: How tightly classes depend on each other
- **Cohesion**: How focused a class is on its responsibility
- **Layered Architecture**: Organizing code into presentation, business, and data layers
- **MVC**: Model-View-Controller pattern for separating UI and logic
- **Repository Pattern**: Abstracting data access behind an interface
- **Software Architecture**: High-level organization of a system

---

## Teacher