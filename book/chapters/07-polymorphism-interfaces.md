# Chapter 7 – Polymorphism and Interfaces

In Chapter 6, we learned to build class hierarchies through inheritance. Now we'll discover **polymorphism**—the ability to treat different types uniformly through their shared behaviors. You'll master abstract classes and interfaces, the foundations of flexible, extensible design.

## Learning Objectives

By the end of this chapter, you will be able to:

- Explain polymorphism and why it matters
- Create abstract classes with abstract methods
- Define and implement interfaces
- Use interface references to achieve polymorphism
- Implement multiple interfaces in a single class
- Choose between abstract classes and interfaces
- Design systems using contracts and abstraction

## Prerequisites

This chapter assumes you've completed **Chapter 6** and can:

- Create base and derived classes
- Use `virtual` and `override` keywords
- Call base class constructors with `:base()`
- Understand inheritance relationships
- Use the `protected` access modifier

---

## Mission Brief

In this chapter you will:

- Discover how polymorphism enables treating different types uniformly
- Learn when code can't be concrete and must be abstract
- Master interfaces as contracts that classes fulfill
- Implement multiple behaviors through multiple interfaces
- Design flexible systems that can grow without breaking

This chapter teaches you to write code that works with behaviors, not specific types.

---

## Concept Power-Up – The Power of Abstraction

Imagine you're building a game with many different enemy types. Without polymorphism:

```csharp
// ❌ Rigid - must know exact types
void AttackEnemy(Goblin enemy) { enemy.TakeDamage(10); }
void AttackEnemy(Dragon enemy) { enemy.TakeDamage(10); }
void AttackEnemy(Zombie enemy) { enemy.TakeDamage(10); }
// Add new enemy = add new method!
```

With polymorphism:

```csharp
// ✅ Flexible - works with any enemy
void AttackEnemy(IEnemy enemy)
{
    enemy.TakeDamage(10);  // Works for ANY enemy type!
}
```

> **Key Insight**: Polymorphism lets you write code once that works with many types, as long as they share the same behavior contract.

---

## Core Concepts

### Concept 1 – Abstract Classes: Incomplete Blueprints

An **abstract class** is a base class that cannot be instantiated directly. It defines common behavior but leaves some details for derived classes to implement.

```csharp
abstract class Shape
{
    public string Color { get; set; }
    
    // Abstract method - no implementation
    public abstract double CalculateArea();
    
    // Concrete method - has implementation
    public void Display()
    {
        Console.WriteLine($"{Color} shape with area {CalculateArea()}");
    }
}

class Circle : Shape
{
    public double Radius { get; set; }
    
    // Must implement abstract method
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}

class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public override double CalculateArea()
    {
        return Width * Height;
    }
}
```

**Using abstract classes:**
```csharp
// ❌ Cannot create abstract class directly
Shape shape = new Shape();  // ERROR!

// ✅ Create concrete derived classes
Shape circle = new Circle { Color = "Red", Radius = 5 };
Shape rectangle = new Rectangle { Color = "Blue", Width = 4, Height = 6 };

circle.Display();      // Red shape with area 78.54
rectangle.Display();   // Blue shape with area 24
```

**Key rules:**
- Abstract classes use `abstract` keyword
- Cannot instantiate abstract classes directly
- Abstract methods have no body
- Derived classes must override all abstract methods
- Can mix abstract and concrete members

### Concept 2 – Interfaces: Pure Contracts

An **interface** defines a contract—a set of methods and properties that implementing classes must provide.

```csharp
interface IDamageable
{
    int Health { get; set; }
    int MaxHealth { get; }
    
    void TakeDamage(int amount);
    void Heal(int amount);
    bool IsAlive();
}

class Player : IDamageable
{
    public int Health { get; set; }
    public int MaxHealth { get; private set; }
    
    public Player(int maxHealth)
    {
        MaxHealth = maxHealth;
        Health = maxHealth;
    }
    
    public void TakeDamage(int amount)
    {
        Health = Math.Max(0, Health - amount);
    }
    
    public void Heal(int amount)
    {
        Health = Math.Min(MaxHealth, Health + amount);
    }
    
    public bool IsAlive()
    {
        return Health > 0;
    }
}
```

**Interface features:**
- Use `I` prefix by convention (IDamageable, IComparable)
- No implementation—only signatures
- All members implicitly public
- Classes use `:` to implement interfaces
- Must implement ALL interface members

### Concept 3 – Polymorphism Through Interfaces

The real power: write code that works with the interface, not the concrete type.

```csharp
void ApplyDamage(IDamageable target, int damage)
{
    Console.WriteLine($"Attacking! Health before: {target.Health}");
    target.TakeDamage(damage);
    Console.WriteLine($"Health after: {target.Health}");
    
    if (!target.IsAlive())
    {
        Console.WriteLine("Target eliminated!");
    }
}

// Works with ANY class implementing IDamageable
Player player = new Player(100);
Enemy enemy = new Enemy(50);
Boss boss = new Boss(500);

ApplyDamage(player, 25);  // Works!
ApplyDamage(enemy, 50);   // Works!
ApplyDamage(boss, 100);   // Works!
```

This is **polymorphism in action**—same code, many forms!

### Concept 4 – Multiple Interface Implementation

A class can implement multiple interfaces:

```csharp
interface IDrawable
{
    void Draw();
}

interface IMovable
{
    void Move(int x, int y);
}

interface ICollidable
{
    bool CheckCollision(ICollidable other);
}

class GameObject : IDrawable, IMovable, ICollidable
{
    public int X { get; set; }
    public int Y { get; set; }
    
    public void Draw()
    {
        Console.WriteLine($"Drawing at ({X}, {Y})");
    }
    
    public void Move(int x, int y)
    {
        X += x;
        Y += y;
    }
    
    public bool CheckCollision(ICollidable other)
    {
        // Collision detection logic
        return false;
    }
}
```

**Using multiple interfaces:**
```csharp
GameObject player = new GameObject();

// Can treat as any implemented interface
IDrawable drawable = player;
IMovable movable = player;
ICollidable collidable = player;

drawable.Draw();
movable.Move(10, 20);
```

### Concept 5 – Abstract Classes vs. Interfaces: When to Use Which

**Use Abstract Classes when:**
- Classes share common implementation
- Need to provide default behavior
- Want to use constructors
- Have protected members
- Single inheritance is enough

**Use Interfaces when:**
- Defining a pure contract
- Need multiple inheritance
- Unrelated classes share behavior
- Want maximum flexibility
- Following dependency inversion

```csharp
// Abstract class - shared implementation
abstract class Animal
{
    public string Name { get; set; }
    protected int energy = 100;
    
    public abstract void MakeSound();
    
    public void Sleep()  // Shared behavior
    {
        energy = 100;
        Console.WriteLine($"{Name} is sleeping...");
    }
}

// Interface - pure contract
interface IFlyable
{
    int MaxAltitude { get; }
    void Fly();
    void Land();
}

// Can combine both!
class Bird : Animal, IFlyable
{
    public int MaxAltitude { get; } = 1000;
    
    public override void MakeSound()
    {
        Console.WriteLine("Chirp!");
    }
    
    public void Fly()
    {
        Console.WriteLine($"{Name} is flying!");
    }
    
    public void Land()
    {
        Console.WriteLine($"{Name} has landed.");
    }
}
```

---

## Try It – Payment Processing System

Let's build a flexible payment system using interfaces:

```csharp
interface IPaymentMethod
{
    string Name { get; }
    bool ProcessPayment(decimal amount);
    string GetReceipt();
}

class CreditCard : IPaymentMethod
{
    public string Name => "Credit Card";
    public string CardNumber { get; set; }
    private decimal balance = 1000;
    
    public bool ProcessPayment(decimal amount)
    {
        if (amount > balance)
        {
            Console.WriteLine("Insufficient credit limit");
            return false;
        }
        
        balance -= amount;
        Console.WriteLine($"Charged ${amount} to card ending in {CardNumber[^4..]}");
        return true;
    }
    
    public string GetReceipt()
    {
        return $"Credit Card Payment - Remaining credit: ${balance}";
    }
}

class PayPal : IPaymentMethod
{
    public string Name => "PayPal";
    public string Email { get; set; }
    private decimal accountBalance = 500;
    
    public bool ProcessPayment(decimal amount)
    {
        if (amount > accountBalance)
        {
            Console.WriteLine("Insufficient PayPal balance");
            return false;
        }
        
        accountBalance -= amount;
        Console.WriteLine($"Paid ${amount} via PayPal account {Email}");
        return true;
    }
    
    public string GetReceipt()
    {
        return $"PayPal Payment - Account: {Email}, Balance: ${accountBalance}";
    }
}

class Cash : IPaymentMethod
{
    public string Name => "Cash";
    public decimal AmountTendered { get; set; }
    
    public bool ProcessPayment(decimal amount)
    {
        if (AmountTendered < amount)
        {
            Console.WriteLine("Insufficient cash provided");
            return false;
        }
        
        decimal change = AmountTendered - amount;
        Console.WriteLine($"Cash payment received. Change: ${change}");
        return true;
    }
    
    public string GetReceipt()
    {
        return "Cash Payment - Thank you!";
    }
}
```

**Using the system:**
```csharp
void ProcessPurchase(IPaymentMethod payment, decimal amount)
{
    Console.WriteLine($"\nProcessing ${amount} payment via {payment.Name}...");
    
    if (payment.ProcessPayment(amount))
    {
        Console.WriteLine("Payment successful!");
        Console.WriteLine(payment.GetReceipt());
    }
    else
    {
        Console.WriteLine("Payment failed!");
    }
}

// Works with ANY payment method!
IPaymentMethod card = new CreditCard { CardNumber = "1234567890123456" };
IPaymentMethod paypal = new PayPal { Email = "user@email.com" };
IPaymentMethod cash = new Cash { AmountTendered = 100 };

ProcessPurchase(card, 50);
ProcessPurchase(paypal, 75);
ProcessPurchase(cash, 40);
```

> **🏋️ Practice Now**: Complete [Exercise 07-01: Abstract Classes](../../exercises/07-polymorphism-interfaces/01-abstract-classes/) to practice creating abstract base classes.

---

## Challenge – Notification System

Create a notification system with multiple delivery methods:

1. Create `INotification` interface with:
   - `string Recipient { get; set; }`
   - `string Message { get; set; }`
   - `void Send()`
   - `bool IsDelivered { get; }`

2. Implement three classes:
   - `EmailNotification` (sends to email address)
   - `SMSNotification` (sends to phone number)
   - `PushNotification` (sends to device ID)

3. Create `NotificationService` class with:
   - `void SendNotification(INotification notification)`
   - `void SendBulk(List<INotification> notifications)`

4. Test sending multiple notification types

**Hints:**
```csharp
interface INotification
{
    string Recipient { get; set; }
    string Message { get; set; }
    bool IsDelivered { get; }
    void Send();
}

class EmailNotification : INotification
{
    public string Recipient { get; set; }
    public string Message { get; set; }
    public bool IsDelivered { get; private set; }
    
    public void Send()
    {
        Console.WriteLine($"Sending email to {Recipient}: {Message}");
        IsDelivered = true;
    }
}
```

> **🏋️ Practice Now**: Complete [Exercise 07-02: Interfaces](../../exercises/07-polymorphism-interfaces/02-interfaces/) to master interface implementation.

---

## Mini Project – Build YOUR Plugin System

**Goal:** Create YOUR own extensible plugin system using interfaces and polymorphism!

This is YOUR project to design and implement. You'll build a system where new features can be added without modifying existing code—the essence of professional software architecture.

### Why This Project?

Professional applications use plugin systems to allow extensibility. This project teaches you the Open/Closed Principle (open for extension, closed for modification)—a cornerstone of SOLID design.

### Learning Goals

- Design with interfaces for maximum flexibility
- Implement multiple interface contracts
- Use polymorphism to treat plugins uniformly
- Build systems that extend without modification
- Apply professional architectural patterns

### Minimum Requirements

YOUR plugin system MUST include:

1. **Core interfaces:**
   - `IPlugin` interface with:
     - `string Name { get; }`
     - `string Version { get; }`
     - `void Initialize()`
     - `void Execute()`
   
2. **At least three plugin types:**
   - Logger plugin (writes to console/file)
   - Calculator plugin (performs operations)
   - Greeter plugin (displays welcome messages)
   - Each implements `IPlugin`

3. **PluginManager class:**
   - Load and register plugins
   - Execute all plugins
   - Execute plugins by name
   - List all registered plugins
   - Handle plugin errors gracefully

4. **Advanced interface (optional):**
   - `IConfigurable` for plugins with settings
   - `IDisposable` for cleanup
   - Plugins can implement multiple interfaces

### Expected Output

```
Plugin System Starting...

Registering plugins:
✓ Logger v1.0 initialized
✓ Calculator v1.0 initialized
✓ Greeter v1.0 initialized

All registered plugins:
- Logger v1.0
- Calculator v1.0
- Greeter v1.0

Executing all plugins:

[Logger] Writing log entry...
[Calculator] Ready for calculations
[Greeter] Welcome to the plugin system!

Executing specific plugin: Calculator
[Calculator] 5 + 3 = 8
[Calculator] 10 * 2 = 20

Plugin system terminated.
```

### Implementation Steps

1. Define `IPlugin` interface
2. Create plugin base implementations
3. Build PluginManager to handle plugins
4. Test plugin loading and execution
5. Add error handling
6. Extend with additional interfaces
7. Polish and document

### Hints

**IPlugin Interface:**
```csharp
interface IPlugin
{
    string Name { get; }
    string Version { get; }
    void Initialize();
    void Execute();
}
```

**Sample Plugin:**
```csharp
class LoggerPlugin : IPlugin
{
    public string Name => "Logger";
    public string Version => "1.0";
    
    public void Initialize()
    {
        Console.WriteLine($"[{Name}] Plugin initialized");
    }
    
    public void Execute()
    {
        Console.WriteLine($"[{Name}] Writing log entry...");
        // Actual logging logic
    }
}
```

**PluginManager:**
```csharp
class PluginManager
{
    private List<IPlugin> plugins = new List<IPlugin>();
    
    public void RegisterPlugin(IPlugin plugin)
    {
        plugin.Initialize();
        plugins.Add(plugin);
        Console.WriteLine($"✓ {plugin.Name} v{plugin.Version} initialized");
    }
    
    public void ExecuteAll()
    {
        foreach (var plugin in plugins)
        {
            plugin.Execute();
        }
    }
    
    public void ExecuteByName(string name)
    {
        var plugin = plugins.FirstOrDefault(p => p.Name == name);
        if (plugin != null)
        {
            plugin.Execute();
        }
        else
        {
            Console.WriteLine($"Plugin '{name}' not found");
        }
    }
}
```

### Make It YOUR Own!

Add YOUR creative features:

- **Plugin priorities** - Control execution order
- **Plugin dependencies** - Plugins that require other plugins
- **Configuration system** - Load plugin settings from files
- **Hot reload** - Add/remove plugins at runtime
- **Plugin marketplace** - Discover and install new plugins
- **Event system** - Plugins communicate via events
- **Resource management** - Plugins with IDisposable cleanup
- **Plugin versioning** - Compatibility checks
- **Security** - Sandboxed plugin execution
- **Performance monitoring** - Track plugin execution time
- **Plugin categories** - Group plugins by type
- **Dynamic loading** - Load plugins from DLL files
- **Plugin API** - Shared services for all plugins

### Self-Assessment Checklist

- [ ] IPlugin interface clearly defined
- [ ] At least three different plugins implemented
- [ ] PluginManager handles registration
- [ ] Can execute all plugins
- [ ] Can execute specific plugin by name
- [ ] Plugins properly initialized
- [ ] Error handling implemented
- [ ] Code follows interface segregation
- [ ] System extensible without modification
- [ ] Clear output and user feedback

### What Makes a Great Project?

- **Architecture** (40%): Clean interface design, proper abstraction, extensible
- **Functionality** (30%): All features work, handles errors, robust
- **Code Quality** (20%): Well-organized, follows patterns, readable
- **Creativity** (10%): YOUR unique features and innovations

> **🚀 Mini Project**: Ready to build professional architecture? Complete the [Mini Project: Plugin System](../../projects/07-polymorphism-interfaces/) to create an extensible system using interfaces.

---

## Common Mistakes

### Instantiating Abstract Classes

```csharp
// ❌ Wrong - cannot create abstract class
abstract class Animal { }
Animal animal = new Animal();  // ERROR!

// ✅ Correct - create concrete derived class
class Dog : Animal { }
Animal animal = new Dog();
```

### Forgetting to Implement All Interface Members

```csharp
interface IDrawable
{
    void Draw();
    void Clear();
}

// ❌ Wrong - missing Clear() implementation
class Circle : IDrawable
{
    public void Draw() { }
    // Forgot Clear()!  Compiler error!
}

// ✅ Correct - implement everything
class Circle : IDrawable
{
    public void Draw() { }
    public void Clear() { }
}
```

### Not Overriding Abstract Methods

```csharp
abstract class Shape
{
    public abstract double GetArea();
}

// ❌ Wrong - didn't override abstract method
class Circle : Shape
{
    // ERROR: must override GetArea()
}

// ✅ Correct - override required
class Circle : Shape
{
    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }
}
```

### Using Implementation Instead of Interface

```csharp
// ❌ Wrong - tied to specific implementation
void ProcessPayment(CreditCard card)
{
    card.ProcessPayment(100);
}

// ✅ Correct - use interface for flexibility
void ProcessPayment(IPaymentMethod payment)
{
    payment.ProcessPayment(100);
}
```

---

## Hands-On Practice

Practice polymorphism and interfaces with these exercises:

### Exercise Progression

1. **[Exercise 07-01: Abstract Classes](../../exercises/07-polymorphism-interfaces/01-abstract-classes/)** - Create abstract base classes with abstract methods
2. **[Exercise 07-02: Interfaces](../../exercises/07-polymorphism-interfaces/02-interfaces/)** - Define and implement interface contracts
3. **[Exercise 07-03: Multiple Interfaces](../../exercises/07-polymorphism-interfaces/03-multiple-interfaces/)** - Implement multiple interface contracts
4. **[Exercise 07-04: Polymorphic Collections](../../exercises/07-polymorphism-interfaces/04-polymorphic-collections/)** - Work with collections of interface types

Complete these in order to build comprehensive polymorphism skills.

> **💡 Tip**: Think about behaviors first, types second!

---

## Why It Matters

Polymorphism and interfaces are fundamental to professional software development because they:

1. **Enable flexibility** - Code works with any type implementing the contract
2. **Support testing** - Easy to create mock implementations
3. **Follow SOLID** - Open/Closed and Dependency Inversion principles
4. **Reduce coupling** - Code depends on abstractions, not concrete types
5. **Allow extensibility** - Add new types without modifying existing code
6. **Model reality** - Different objects can share behaviors

Every major framework uses interfaces extensively: .NET, Unity, ASP.NET, Entity Framework. Master them now and you'll write code that's flexible, testable, and professional.

---

## Checkpoint

You should now be able to:

- Explain what polymorphism means and why it's powerful
- Create abstract classes with abstract and concrete members
- Define interfaces with properties and methods
- Implement single and multiple interfaces
- Use interface references for polymorphism
- Choose between abstract classes and interfaces
- Design extensible systems using contracts

---

## Reflection Questions

1. How does polymorphism make code more flexible and maintainable?
2. When would you use an abstract class instead of an interface?
3. Why can a class implement multiple interfaces but inherit from only one class?
4. How do interfaces support the Open/Closed Principle?
5. Can you think of a real-world scenario where multiple interface implementation would be useful?

---

## Next Chapter Preview

In **Chapter 8 – Generics and Collections**, you'll learn to write code that works with any type safely. You'll master `List<T>`, `Dictionary<K,V>`, and create your own generic classes and methods. Generics combine the flexibility of polymorphism with the safety of compile-time type checking.

---

## Key Terms

- **Polymorphism**: Ability to treat different types uniformly through shared interfaces or base classes
- **Abstract Class**: Base class that cannot be instantiated; may contain abstract methods
- **Abstract Method**: Method declared without implementation; must be overridden in derived classes
- **Interface**: Contract defining methods and properties that implementing classes must provide
- **Contract**: Agreement that a class will implement specific members
- **Implementation**: Providing concrete code for interface members or abstract methods
- **Multiple Inheritance**: Implementing multiple interfaces in a single class (C# doesn't support multiple class inheritance)
- **Interface Segregation**: Principle of creating focused, specific interfaces
- **Dependency Inversion**: Depending on abstractions (interfaces) rather than concrete types
- **Open/Closed Principle**: Open for extension, closed for modification
- **Upcasting**: Converting derived type to base type or interface type
- **Runtime Polymorphism**: Behavior determined at runtime based on actual object type

---

**Teacher Notes:**

**Pedagogical Goals:**
- Transition from concrete inheritance to abstract contracts
- Emphasize "programming to interfaces, not implementations"
- Show how polymorphism enables extensibility
- Prepare for SOLID principles and design patterns

**Common Student Questions:**
- "Why use interfaces when abstract classes can do the same?" → Multiple inheritance, pure contracts, dependency inversion
- "When do I use abstract vs interface?" → Abstract for shared implementation, interfaces for pure contracts
- "Can abstract classes have constructors?" → Yes, called by derived classes
- "Why the 'I' prefix on interfaces?" → Convention for clarity, not required but widely used

**Assessment Hints:**
- Check proper use of abstract keyword
- Verify all interface members implemented
- Ensure students use interface references for polymorphism
- Look for appropriate abstraction choices
- Check understanding of when to use abstract vs interface

**Connection to Curriculum (PRRPRR02):**
- Core OOP principle (polymorphism)
- Foundation for SOLID principles
- Design patterns preparation
- Professional architectural patterns

**Classroom Activities:**
- Live coding: Convert inheritance hierarchy to use interfaces
- Group exercise: Design interface for common behavior
- Discussion: When is polymorphism better than switch statements?
- Code review: Identify interface opportunities in existing code

**Rubric Suggestions (Mini Project):**
- Interface design quality: 30%
- Polymorphism implementation: 25%
- Plugin functionality: 25%
- Code organization: 15%
- Extensibility demonstrated: 5%

---

*© C# Kickstart Part 2 Contributors*