# Chapter 1 – Review of Programming Fundamentals

Before we build big object-oriented systems, let's make sure our coding muscles are warm.

## Learning Objectives

By the end of this chapter, you will be able to:

- Refresh core programming concepts from C# Kickstart - Part 1
- Write and run C# console programs confidently
- Use variables, conditionals, loops, and methods effectively
- Understand the difference between static and dynamic typing
- Handle basic user input and errors with try/catch

## Prerequisites

This chapter assumes you've completed **C# Kickstart - Part 1** and have:

- .NET SDK installed on your computer
- Visual Studio Code with C# Dev Kit extension
- Basic understanding of C# syntax and console applications

If you need to refresh your setup, refer to [Chapter 1 of Part 1](../../../c-sharp-kickstart/book/chapters/01-intro.md).

---

## Mission Brief

In this chapter you will:

- Refresh the most important ideas from **C# Kickstart - Part 1**
- Review how a **C# console program** is structured
- Practice using variables, loops, and methods
- Discover what **static typing** means compared to dynamic languages

When you complete this chapter you'll be ready to move into **object-oriented programming**.

---

## Concept Power-Up – What You Already Know

From *C# Kickstart - Part 1* you probably remember:

- Writing step-by-step instructions
- Using **variables** to store data
- Making choices with **if-statements**
- Repeating work with **loops**

These are the building blocks of all programming. In Part 1, you learned them in C#. Now we'll review and deepen your understanding.

---

## 💻 Try It – Your First C# Program

```csharp
// HelloWorld.cs
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, C# world!");
    }
}
```

**What happens:**

- `using System;` gives access to built-in features
- `Main()` is where every C# program starts
- `Console.WriteLine()` prints text on the screen

**Mini-task:**
Change the text and run the program again.

> **💡 Quick Reminder**: Run your program with `dotnet run` in the terminal.

---

## Core Concepts

### Concept 1 – Variables and Data Types

C# is strongly typed. You must declare what kind of data each variable holds.

```csharp
int age = 17;
double price = 19.95;
string name = "Alex";
bool isStudent = true;
```

**Try this:**
Create variables for your name, birth year, and favourite number. Then print a sentence using them.

```csharp
Console.WriteLine($"{name} is {2025 - birthYear} years old.");
```

> **🏋️ Practice Now**: Complete [Exercise 01-01: Variables Practice](../../exercises/01-intro/01-variables/) to work with different data types.

---

### Concept 2 – Decisions (If Statements)

```csharp
if (age >= 18)
{
    Console.WriteLine("You are an adult.");
}
else
{
    Console.WriteLine("You are under 18.");
}
```

**Challenge:**
Ask the user for their age using `Console.ReadLine()`. Convert it with `int.Parse()` and test the result.

> **🏋️ Practice Now**: Complete [Exercise 01-02: If Statements](../../exercises/01-intro/02-if-statements/) to practice making decisions in code.

---

### Concept 3 – Loops

**For loop:**

```csharp
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Loop {i}");
}
```

**While loop:**

```csharp
int count = 3;
while (count > 0)
{
    Console.WriteLine(count);
    count--;
}
```

**Try this:**
Make a countdown from 10 to 1 that ends with "Blast off!".

> **🏋️ Practice Now**: Complete [Exercise 01-03: Loops Practice](../../exercises/01-intro/03-loops/) to master iteration.

---

### Concept 4 – Methods and Parameters

Methods group actions into reusable pieces of code.

```csharp
static void Greet(string name)
{
    Console.WriteLine($"Hello {name}!");
}

static void Main()
{
    Greet("Sofia");
    Greet("Omar");
}
```

**Challenge:**
Write a method `AddNumbers(int a, int b)` that prints the sum.

> **🏋️ Practice Now**: Complete [Exercise 01-04: Methods](../../exercises/01-intro/04-methods/) to create reusable code blocks.

---

### Concept 5 – Static vs Dynamic Typing

| Idea | Example | What it means |
|------|---------|---------------|
| Static typing (C#) | `int age = 17;` | Type is fixed at compile time → safer, faster |
| Dynamic typing (Python) | `age = 17` | Type decided while running → flexible but risky |

**Why it matters:**
C# finds many mistakes before your program runs — less surprise, more stability.

---

## 🚀 Mini Project – Build Your Own Calculator

**Goal:** Combine everything you've learned in this chapter to create YOUR own working calculator!

This is more than just an exercise—it's YOUR chance to:
- Apply all Chapter 1 concepts in one complete program
- Make design decisions about features and user experience
- Add creative features that make your calculator unique
- Practice building something you can demonstrate and be proud of

**Minimum Requirements:**
1. All four basic operations (+, -, *, /)
2. Clear menu and user interaction
3. Error handling for invalid input and division by zero
4. Organized code structure using methods
5. Ability to perform multiple calculations

**Make It Yours:**
After meeting the requirements, add YOUR creative touches! Ideas include:
- Calculation history
- Additional operations (square root, power, percentage)
- Visual polish with colors or formatting
- Scientific calculator features
- Unit conversions

**Error Handling Example:**
```csharp
try
{
    double number = double.Parse(Console.ReadLine());
    // Process the number
}
catch (FormatException)
{
    Console.WriteLine("Error: Please enter a valid number.");
}
```

> **🚀 Mini Project**: Ready to build something real? Complete the [Mini Project: Build Your Own Calculator](../../projects/01-intro/) to create a complete program that combines all Chapter 1 concepts.

---

## Common Mistakes

Here are some typical errors beginners make when reviewing these concepts:

### Forgetting to Parse User Input

```csharp
// ❌ Wrong - ReadLine() returns a string
int age = Console.ReadLine();

// ✅ Correct - Convert string to int
int age = int.Parse(Console.ReadLine());
```

### Missing Curly Braces

```csharp
// ❌ Wrong - Missing braces can cause logic errors
if (age >= 18)
    Console.WriteLine("Adult");
    Console.WriteLine("Can vote");  // This always runs!

// ✅ Correct - Use braces
if (age >= 18)
{
    Console.WriteLine("Adult");
    Console.WriteLine("Can vote");
}
```

### Off-by-One Errors in Loops

```csharp
// ❌ Common mistake - Loops 11 times instead of 10
for (int i = 0; i <= 10; i++)

// ✅ Correct - Loops exactly 10 times
for (int i = 0; i < 10; i++)
```

---

## Hands-On Practice

Now that you've reviewed the concepts, practice with these exercises:

### Exercise Progression

1. **[Exercise 01-01: Variables Practice](../../exercises/01-intro/01-variables/)** - Work with different data types
2. **[Exercise 01-02: If Statements](../../exercises/01-intro/02-if-statements/)** - Practice conditional logic
3. **[Exercise 01-03: Loops Practice](../../exercises/01-intro/03-loops/)** - Master iteration patterns
4. **[Exercise 01-04: Methods](../../exercises/01-intro/04-methods/)** - Create reusable code

Each exercise reinforces the concepts from this chapter. Complete them in order for the best learning experience.

> **💡 Tip**: Don't just copy code. Type it yourself to build muscle memory!

---

## Why It Matters

Every complex system — games, websites, robots — is built from these same building blocks: variables, decisions, loops, and methods. Master them now, and OOP will feel natural later.

---

## ✅ Checkpoint

You should now be able to:

- Write and run a simple C# program
- Use variables, conditionals, and loops
- Create and call your own methods
- Handle basic input errors

---

## 🪞 Reflection Questions

1. What part of this chapter felt easiest or hardest?
2. How is C# different from other languages you have tried?
3. Why do you think programmers use try/catch even for small programs?

---

## 🔮 Next Chapter Preview

In **Chapter 2 – Why We Code the Way We Do**, you'll explore how programming evolved — and why object-oriented programming was invented to manage complexity.

---

## Key Terms

- **Static Typing**: Variable types are determined at compile time and cannot change
- **Dynamic Typing**: Variable types are determined at runtime and can change
- **try/catch**: Error handling mechanism to gracefully handle exceptions
- **FormatException**: Error thrown when trying to convert invalid text to a number
- **Method**: A reusable block of code that performs a specific task
- **Parameter**: A variable passed into a method
- **Compiled Language**: Code is translated to machine code before running

---

**Notes for the writer:**
- Keeps vocabulary and syntax at *C# Kickstart - Part 1 level*
- No OOP terms yet — purely procedural refresh
- Exercises provide hands-on practice for each concept
- Mini project integrates all concepts together
- Ready for automatic testing if needed (inputs + outputs)