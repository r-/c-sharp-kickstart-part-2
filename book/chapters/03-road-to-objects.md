# Chapter 3 – From Procedures to Programs: The Road to Objects

We've learned how to tell the computer what to do. Now we'll learn how to organize what we tell it.

## Learning Objectives

By the end of this chapter, you will be able to:

- Explain the difference between procedural code and object-based code
- Define what a class is and why we use them
- Create classes with fields and methods
- Use the `new` keyword to create objects from classes
- Write constructors to initialize objects
- Understand why grouping data and behavior makes programs easier to maintain

## Prerequisites

This chapter assumes you've completed **Chapters 1 and 2** and can:

- Write and call methods
- Use variables, loops, and conditionals
- Understand the evolution of programming paradigms
- Read simple C# programs and predict their output

---

## Mission Brief

In this chapter you will:

- See why procedural code breaks down as programs grow
- Learn to organize code around "things" instead of just "actions"
- Write your first C# class and create objects from it
- Understand the relationship between classes (blueprints) and objects (instances)

This is THE pivotal chapter in the course. Master this concept and everything else in Programming 2 becomes easier.

---

## Concept Power-Up – The Problem with Procedures

Let's start with a problem you can solve with what you already know.

### Tracking One Player (Procedural Style)

```csharp
static string playerName = "Alex";
static int score = 0;

static void AddPoints(int points)
{
    score += points;
}

static void Main()
{
    AddPoints(10);
    Console.WriteLine($"{playerName}: {score}");
    // Output: Alex: 10
}
```

This works fine! But what if you need **two players**?

### The Disaster: Two Players

```csharp
static string player1Name = "Alex";
static int player1Score = 0;
static string player2Name = "Riley";
static int player2Score = 0;

static void AddPointsPlayer1(int points)
{
    player1Score += points;
}

static void AddPointsPlayer2(int points)
{
    player2Score += points;
}

static void Main()
{
    AddPointsPlayer1(10);
    AddPointsPlayer2(20);
    Console.WriteLine($"{player1Name}: {player1Score}");
    Console.WriteLine($"{player2Name}: {player2Score}");
}
```

**What went wrong?**
- We duplicated everything: variables, methods, code
- Adding a third player means more duplication
- What if we need 100 players? This is impossible to maintain!

> **Key Insight**: Procedural code doesn't scale when you need multiple "things" with the same behavior.

---

## Core Concepts

### Concept 1 – Classes: Blueprints for Objects

A **class** is a blueprint that describes what something IS and what it can DO.

Think of it like a cookie cutter:
- The cookie cutter (class) is the template
- Each cookie (object) is made from that template
- All cookies have the same shape, but they're separate cookies

```csharp
class Player
{
    // Fields: What a Player HAS (data)
    public string Name;
    public int Score;

    // Methods: What a Player CAN DO (behavior)
    public void AddPoints(int points)
    {
        Score += points;
    }
}
```

**This is a class** - it's a blueprint. No actual player exists yet!

### Concept 2 – Objects: Instances Created from Classes

An **object** is a specific instance created from a class.

```csharp
// Create objects using the 'new' keyword
Player player1 = new Player();
Player player2 = new Player();

// Each object has its own data
player1.Name = "Alex";
player1.Score = 0;

player2.Name = "Riley";
player2.Score = 0;

// Each can use the shared behavior
player1.AddPoints(10);
player2.AddPoints(20);

Console.WriteLine($"{player1.Name}: {player1.Score}");  // Alex: 10
Console.WriteLine($"{player2.Name}: {player2.Score}");  // Riley: 20
```

**Key Understanding:**
- `Player` is the class (blueprint)
- `player1` and `player2` are objects (instances)
- Each object has its own `Name` and `Score`
- But they share the same `AddPoints()` method

**Try This:**
Create a third player. Notice how easy it is now!

### Concept 3 – Fields: Data That Belongs to Objects

**Fields** are variables that belong to an object.

```csharp
class Car
{
    public string Model;  // Each car has its own model
    public int Speed;     // Each car has its own speed
}

static void Main()
{
    Car car1 = new Car();
    car1.Model = "Volvo";
    car1.Speed = 0;

    Car car2 = new Car();
    car2.Model = "Tesla";
    car2.Speed = 0;
    
    // car1.Speed and car2.Speed are completely independent
}
```

### Concept 4 – Methods: Behavior That Uses Object Data

Methods inside a class can access that object's fields directly.

```csharp
class Car
{
    public string Model;
    public int Speed;

    public void Accelerate()
    {
        Speed += 10;  // Modifies THIS car's speed
    }

    public void ShowStatus()
    {
        Console.WriteLine($"{Model} is going {Speed} km/h");
    }
}

static void Main()
{
    Car car1 = new Car();
    car1.Model = "Volvo";
    car1.Accelerate();
    car1.Accelerate();
    car1.ShowStatus();  // Volvo is going 20 km/h

    Car car2 = new Car();
    car2.Model = "Tesla";
    car2.Accelerate();
    car2.ShowStatus();  // Tesla is going 10 km/h
}
```

**Important**: When you call `car1.Accelerate()`, it changes `car1.Speed`. When you call `car2.Accelerate()`, it changes `car2.Speed`. They're independent!

### Concept 5 – Constructors: Initializing Objects

Right now we create objects and then set their values:

```csharp
Player player1 = new Player();
player1.Name = "Alex";
player1.Score = 0;
```

We can make this cleaner with a **constructor** - a special method that runs when the object is created.

```csharp
class Player
{
    public string Name;
    public int Score;

    // Constructor - same name as the class, no return type
    public Player(string name)
    {
        Name = name;
        Score = 0;
    }

    public void AddPoints(int points)
    {
        Score += points;
    }
}

static void Main()
{
    // Now we can initialize in one line
    Player player1 = new Player("Alex");
    Player player2 = new Player("Riley");
    
    player1.AddPoints(10);
    player2.AddPoints(20);
}
```

**Constructor Rules:**
- Must have the same name as the class
- No return type (not even `void`)
- Called automatically when you use `new`
- Can have parameters to initialize fields

---

## Try It – Your First Class

Let's write a complete class step by step.

### Step 1: Define the Class

```csharp
class BankAccount
{
    // Fields
    public string Owner;
    public double Balance;
}
```

### Step 2: Add Methods

```csharp
class BankAccount
{
    public string Owner;
    public double Balance;

    // Method to deposit money
    public void Deposit(double amount)
    {
        Balance += amount;
    }

    // Method to check balance
    public void ShowBalance()
    {
        Console.WriteLine($"{Owner} has ${Balance}");
    }
}
```

### Step 3: Add a Constructor

```csharp
class BankAccount
{
    public string Owner;
    public double Balance;

    // Constructor
    public BankAccount(string owner)
    {
        Owner = owner;
        Balance = 0;  // Start with zero balance
    }

    public void Deposit(double amount)
    {
        Balance += amount;
    }

    public void ShowBalance()
    {
        Console.WriteLine($"{Owner} has ${Balance}");
    }
}
```

### Step 4: Use Your Class

```csharp
static void Main()
{
    BankAccount account1 = new BankAccount("Alex");
    BankAccount account2 = new BankAccount("Riley");

    account1.Deposit(100);
    account2.Deposit(50);

    account1.ShowBalance();  // Alex has $100
    account2.ShowBalance();  // Riley has $50
}
```

**Your Turn:**
1. Type this code yourself (don't copy-paste!)
2. Run it and verify it works
3. Add a third account
4. Add a `Withdraw()` method

---

## Common Mistakes

### Forgetting the `new` Keyword

```csharp
// ❌ Wrong: This doesn't create an object
Player player;
player.Name = "Alex";  // ERROR: player is null!

// ✅ Correct: Use 'new' to create the object
Player player = new Player();
player.Name = "Alex";
```

### Confusing Class with Object

```csharp
// ❌ Wrong thinking: "Player is an object"
// ✅ Correct: "Player is a class, player1 is an object"

class Player { }        // This is the CLASS (blueprint)

Player player1 = new Player();  // This is an OBJECT (instance)
Player player2 = new Player();  // This is another OBJECT
```

Think: Cookie cutter (class) vs. actual cookies (objects)

### Trying to Use Class Instead of Object

```csharp
// ❌ Wrong: Can't use the class directly
Player.AddPoints(10);  // ERROR: AddPoints needs an object!

// ✅ Correct: Create object first, then use it
Player player1 = new Player();
player1.AddPoints(10);
```

### Forgetting `this` in Constructors

```csharp
class Player
{
    public string Name;

    // ❌ Confusing: Which 'name' is which?
    public Player(string name)
    {
        name = name;  // This assigns parameter to itself!
    }

    // ✅ Clear: Use different names or 'this'
    public Player(string playerName)
    {
        Name = playerName;
    }

    // ✅ Also clear: Use 'this' keyword
    public Player(string Name)
    {
        this.Name = Name;  // this.Name = field, Name = parameter
    }
}
```

---

## Hands-On Practice

Now practice creating and using classes with these exercises:

### Exercise Progression

1. **[Exercise 03-01: Make Your First Class](../../exercises/03-road-to-objects/01-make-a-class/)** - Build a `Pet` class step by step with fields and methods
2. **[Exercise 03-02: Blueprint Thinking](../../exercises/03-road-to-objects/02-blueprint-thinking/)** - Conceptual exercise to clarify class vs. object distinction
3. **[Exercise 03-03: Constructor Practice](../../exercises/03-road-to-objects/03-constructor-practice/)** - Master object initialization with multiple constructors

These exercises progress from basic class creation to working with constructors. Complete them in order for the best learning experience.

> **Note**: Exercise 02 is conceptual (no coding), while exercises 01 and 03 are hands-on coding exercises.

### Mini Project

After completing the exercises, build YOUR own complete program:

**[Mini Project: Build YOUR Game Scoreboard](../../projects/03-road-to-objects/)** - Create YOUR own scoreboard system that tracks players and scores. This open-ended project lets you apply all Chapter 3 concepts while expressing YOUR creativity!

---

## Why It Matters

Classes and objects are the foundation of modern software development because they:

1. **Organize code naturally** - Group related data and behavior together
2. **Enable reuse** - One class creates unlimited objects
3. **Reduce errors** - Each object manages its own data
4. **Scale well** - Easy to add more objects without duplicating code
5. **Model reality** - Represent real-world things (players, cars, accounts)

Every major framework and library you'll use (ASP.NET, Unity, WPF) is built with classes and objects.

---

## Checkpoint

You should now be able to:

- Define a class with fields and methods
- Create objects using the `new` keyword
- Explain the difference between a class (blueprint) and an object (instance)
- Write constructors to initialize object data
- Understand why objects have their own data but share methods
- Create multiple independent objects from the same class

---

## Reflection Questions

1. How does using a class make it easier to add more players/cars/accounts to your program?
2. What's the difference between a class and an object? Use your own analogy.
3. Why do we need constructors? What problem do they solve?
4. Can you think of three real-world things that would make good classes? What fields and methods would they have?

---

## Next Chapter Preview

We can now create classes with public fields and methods. But there's a problem—anyone can change our data directly, even to invalid values!

In **Chapter 4 – Classes, Objects, and Properties**, you'll learn about **properties**—a safer way to control access to your data. You'll discover why direct field access is dangerous and how to add validation to prevent bugs. This is your first step toward writing professional, defensive code.

---

## Key Terms

- **Class**: A blueprint or template for creating objects; defines structure and behavior
- **Object**: A specific instance created from a class using the `new` keyword
- **Instance**: Another word for object; one specific realization of a class
- **Field**: A variable that belongs to a class; each object has its own copy
- **Method**: A function defined inside a class; shared by all objects of that class
- **Constructor**: A special method that initializes an object when it's created; same name as class
- **`new` keyword**: Creates a new object instance from a class
- **Blueprint**: The concept that classes are templates for creating objects
- **Dot notation**: Using `.` to access fields and methods of an object (e.g., `player1.Name`)
- **Initialization**: Setting up an object's initial state, usually in the constructor

---

**Notes for the writer:**
- This is the critical transition chapter - students must understand class vs object
- Use concrete analogies (cookie cutter, blueprint, etc.)
- Keep examples simple and compile-able
- Avoid properties, inheritance, or advanced topics - those are later chapters
- Focus on the mental shift from "actions" to "things"
- Emphasize that objects are independent even when created from same class
