# Chapter 2 – Why We Code the Way We Do

Before learning object-oriented programming, we need to understand why it was invented.

## Learning Objectives

By the end of this chapter, you will be able to:

- Define what a programming paradigm is and why it matters
- Explain the evolution from imperative to object-oriented programming
- Identify which paradigm fits different types of problems
- Understand why modern C# uses multiple paradigms together
- Compare procedural and object-oriented approaches to the same problem

## Prerequisites

This chapter assumes you've completed **Chapter 1** and understand:

- Basic C# syntax and program structure
- How to write and call methods
- Variables, conditionals, and loops
- The difference between data and behavior

---

## Mission Brief

In this chapter you will:

- Explore how programming paradigms evolved to solve real problems
- Discover why Object-Oriented Programming became dominant
- Learn that good programmers choose the right tool for each job
- Compare procedural and OOP versions of the same code

When you complete this chapter you'll understand the "why" behind OOP, making the "how" much easier to learn.

---

## Concept Power-Up – What Is a Paradigm?

A **programming paradigm** is a way of thinking about how to organize code.

Each paradigm answers this question differently:
> "How should we structure our program?"

Think of paradigms like building materials:
- Wood is great for houses but terrible for bridges
- Steel works for bridges but is overkill for birdhouses
- Modern buildings often combine multiple materials

Programming is the same - we choose paradigms based on the problem we're solving.

---

## The Evolution of Programming

### The Problem: Growing Complexity

In the 1950s, programs were simple - maybe 100 lines of code.

By the 1980s, programs had grown to hundreds of thousands of lines.

**The challenge:** How do you manage millions of lines of code without going insane?

Each new paradigm was invented to solve the problems of the previous one.

---

## Core Concepts

### Concept 1 – Imperative Programming (1950s-1970s)

Early programs were written as step-by-step instructions.

```csharp
// Imperative style: Tell the computer exactly what to do
int total = 0;
total = total + 10;
total = total + 20;
total = total + 5;
Console.WriteLine(total);  // Output: 35
```

**Benefits:**
- Simple and direct
- Easy to understand for small programs

**Problems:**
- Hard to modify without breaking things
- No structure for large programs
- Code becomes "spaghetti" - all tangled together

> **Key Insight**: We need a way to organize behavior into reusable pieces.

---

### Concept 2 – Procedural Programming (1970s-1980s)

Programmers started grouping code into **procedures** (functions/methods).

```csharp
static int CalculateTotal(int a, int b, int c)
{
    return a + b + c;
}

static void Main()
{
    int result = CalculateTotal(10, 20, 5);
    Console.WriteLine(result);  // Output: 35
}
```

**Benefits:**
- Code reuse - write once, use many times
- Easier to test individual pieces
- Clearer program structure

**Problems:**
- Functions shared global data
- No clear ownership of data
- As programs grew, chaos returned

**Example Problem:**

```csharp
// Who can modify the balance? Anyone!
static double balance = 100;

static void Deposit(double amount)
{
    balance += amount;  // Modifies global data
}

static void Withdraw(double amount)
{
    balance -= amount;  // Also modifies global data
}

// Problem: Any function anywhere can change balance
// This becomes a nightmare in large programs
```

> **Key Insight**: Functions organize behavior, but not the data they operate on.

---

### Concept 3 – Object-Oriented Programming (1980s-2000s)

OOP groups **data and behavior together** into objects.

```csharp
class BankAccount
{
    private double balance = 100;  // Data is protected

    public void Deposit(double amount)  // Behavior
    {
        balance += amount;
    }

    public void Withdraw(double amount)  // Behavior
    {
        if (amount <= balance)
            balance -= amount;
    }

    public double GetBalance()
    {
        return balance;
    }
}

// Usage
static void Main()
{
    BankAccount account = new BankAccount();
    account.Deposit(50);
    Console.WriteLine(account.GetBalance());  // Output: 150
}
```

**Benefits:**
- Data is protected inside objects (encapsulation)
- Objects model real-world things (cars, accounts, users)
- Easier to understand and maintain large systems
- Code reuse through inheritance and polymorphism

**Why This Works:**
- Clear ownership: `balance` belongs to `BankAccount`
- Controlled access: Only `BankAccount` methods can change `balance`
- Easier to reason about: "What can this object do?"

> **Key Insight**: Bundle data with the functions that operate on it.

---

### Concept 4 – Modern Multi-Paradigm Approach (2000s-Present)

Today's programs mix paradigms based on what fits best.

```csharp
// Modern C#: Combines OOP + Functional + Async
class WeatherService
{
    // OOP: Class with data and methods
    private HttpClient client = new HttpClient();

    public async Task<List<double>> GetTemperaturesAsync(List<string> cities)
    {
        // Functional style: Transform data without mutation
        var temperatures = cities
            .Select(city => GetTempForCity(city))
            .Where(temp => temp > 0)
            .ToList();

        return temperatures;
    }

    // Async: Handle waiting without blocking
    private async Task<double> GetTempForCity(string city)
    {
        // Implementation here
        return 20.5;
    }
}
```

**Modern Approach:**
- Use OOP for structure and identity
- Use functional methods for data transformations
- Use async for operations that wait
- Use procedural scripts for quick automation

> **Key Insight**: Good programmers don't pick one paradigm - they compose solutions.

---

## Try It – Compare Paradigms

Let's solve the same problem three different ways:

**Problem:** Track a player's score in a game.

### Version 1: Procedural

```csharp
static int playerScore = 0;  // Global data

static void AddPoints(int points)
{
    playerScore += points;
}

static void Main()
{
    AddPoints(10);
    AddPoints(25);
    Console.WriteLine($"Score: {playerScore}");
}
```

**Issues:**
- `playerScore` is global - anyone can change it
- Hard to have multiple players
- No validation on points

### Version 2: Object-Oriented

```csharp
class Player
{
    private int score = 0;

    public void AddPoints(int points)
    {
        if (points > 0)
            score += points;
    }

    public int GetScore()
    {
        return score;
    }
}

static void Main()
{
    Player player1 = new Player();
    Player player2 = new Player();
    
    player1.AddPoints(10);
    player1.AddPoints(25);
    player2.AddPoints(50);
    
    Console.WriteLine($"Player 1: {player1.GetScore()}");
    Console.WriteLine($"Player 2: {player2.GetScore()}");
}
```

**Benefits:**
- Each player owns their score
- Easy to create multiple players
- Validation built into `AddPoints()`
- Clear what operations are allowed

**Try This:**
Type both versions and run them. Which feels easier to extend?

> **Practice Now**: Complete [Exercise 02-01: Paradigm Detective](../../exercises/02-paradigms/01-paradigm-detective/) to practice recognizing different paradigm styles in code.

---

## Common Mistakes

### Thinking OOP Is Always Best

```csharp
// ❌ Overkill: Creating a class for simple calculation
class Adder
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}

// ✅ Better: Just use a method
static int Add(int a, int b)
{
    return a + b;
}
```

**Lesson:** Use OOP when you need to manage state and identity. For simple calculations, a method is fine.

> **Practice Now**: Complete [Exercise 02-02: The Right Tool](../../exercises/02-paradigms/02-right-tool/) to practice choosing the right paradigm for different problems.

### Confusing Object with Class

```csharp
// ❌ Wrong thinking: "BankAccount is an object"
// ✅ Correct: "BankAccount is a class"
// "account1 and account2 are objects"

class BankAccount { }  // This is a CLASS (blueprint)

BankAccount account1 = new BankAccount();  // This is an OBJECT (instance)
BankAccount account2 = new BankAccount();  // This is another OBJECT
```

### Making Everything Public

```csharp
// ❌ Bad: Anyone can change the balance
class BankAccount
{
    public double balance = 100;  // Exposed!
}

// ✅ Good: Balance is protected
class BankAccount
{
    private double balance = 100;  // Hidden
    
    public void Deposit(double amount)
    {
        if (amount > 0)
            balance += amount;
    }
}
```

---

## Hands-On Practice

Now that you understand different programming paradigms, practice recognizing and choosing them:

### Exercise Progression

1. **[Exercise 02-01: Paradigm Detective](../../exercises/02-paradigms/01-paradigm-detective/)** - Recognize imperative, procedural, and OOP styles in code
2. **[Exercise 02-02: The Right Tool](../../exercises/02-paradigms/02-right-tool/)** - Match paradigms to appropriate problems

These exercises reinforce the conceptual understanding from this chapter. Complete them before moving to Chapter 3 where you'll start implementing OOP.

> **Note**: These are concept exercises (no coding required). They prepare you to write object-oriented code in the next chapter.

---

## Why It Matters

Understanding paradigms helps you:

1. **Choose the right approach** for each problem
2. **Read existing code** better (most C# code is OOP-based)
3. **Understand design decisions** in frameworks and libraries
4. **Communicate with other programmers** using shared concepts

You don't need to memorize dates or names. Focus on understanding the problems each paradigm solves.

---

## Checkpoint

You should now be able to:

- Explain what a programming paradigm is
- Describe the evolution from imperative to OOP
- Compare procedural and object-oriented approaches
- Understand why OOP groups data and behavior together
- Recognize that modern code often mixes paradigms

---

## Reflection Questions

1. What problem was Object-Oriented Programming invented to solve?
2. Why do modern programmers often use several paradigms together instead of just one?
3. Can you think of a time when a simple function would be better than creating a class?

---

## Next Chapter Preview

In **Chapter 3 – Classes and Objects**, you'll learn how to create your first classes in C# and understand the difference between classes (blueprints) and objects (instances).

---

## Key Terms

- **Programming Paradigm**: A way of thinking about how to structure and organize code
- **Imperative Programming**: Writing code as step-by-step instructions
- **Procedural Programming**: Organizing code into reusable procedures/functions
- **Object-Oriented Programming (OOP)**: Grouping data and behavior together into objects
- **Encapsulation**: Hiding data inside objects and controlling access through methods
- **Global Data**: Data that can be accessed from anywhere in a program
- **Multi-Paradigm**: Using multiple programming approaches in the same program
- **Class**: A blueprint or template for creating objects
- **Object**: An instance created from a class

---

**Notes for the writer:**
- This chapter is conceptual - focus on "why" not "how"
- Students should understand the motivation before learning syntax
- No OOP implementation details yet - that's Chapter 3
- Keep examples simple and relatable
- Emphasize that paradigms are tools, not religions
