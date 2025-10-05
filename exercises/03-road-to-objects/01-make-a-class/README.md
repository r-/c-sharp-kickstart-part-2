# Exercise 03-01: Make Your First Class

## Goal

Create your first C# class from scratch and understand how to define fields, methods, and use objects. By the end, you'll have built a working `Pet` class that models a simple pet with a name and happiness level.

## Background

A class is a blueprint that defines what something IS (data/fields) and what it can DO (behavior/methods). Once you have a class, you can create many objects from it, each with its own independent data.

This exercise guides you step-by-step through creating your first class.

## Instructions

You will create a `Pet` class that tracks a pet's name and happiness level. The pet can perform actions that affect its happiness.

### Part 1: Define the Class Structure

Create a file called `Program.cs` and start with this structure:

```csharp
using System;

class Pet
{
    // TODO: Add fields here
    
    // TODO: Add methods here
}

class Program
{
    static void Main()
    {
        // TODO: Create and test Pet objects here
    }
}
```

### Part 2: Add Fields

Add two fields to the `Pet` class:
- `Name` (string) - the pet's name
- `Happiness` (int) - happiness level from 0-100

### Part 3: Add Methods

Add three methods:
1. `Play()` - increases happiness by 10
2. `Feed()` - increases happiness by 15
3. `ShowStatus()` - prints the pet's name and happiness level

### Part 4: Create and Test Objects

In `Main()`, create two different pets and interact with them.

## Requirements

Your program must:

1. Define a `Pet` class with:
   - `public string Name` field
   - `public int Happiness` field
   - `public void Play()` method that adds 10 to Happiness
   - `public void Feed()` method that adds 15 to Happiness
   - `public void ShowStatus()` method that prints name and happiness

2. In `Main()`:
   - Create two different Pet objects
   - Set their names
   - Set initial happiness to 50
   - Call Play() and/or Feed() on each pet
   - Call ShowStatus() to display results

## Expected Output

Your output should look similar to this:

```
Fluffy's happiness: 75
Rex's happiness: 60
```

The exact numbers will depend on how many times you call Play() and Feed().

## Hints

### Defining Fields
```csharp
class Pet
{
    public string Name;
    public int Happiness;
}
```

### Defining Methods
```csharp
public void Play()
{
    Happiness += 10;
}
```

### Creating Objects
```csharp
Pet pet1 = new Pet();
pet1.Name = "Fluffy";
pet1.Happiness = 50;
```

### Calling Methods
```csharp
pet1.Play();
pet1.Feed();
pet1.ShowStatus();
```

## Common Mistakes

**Forgetting `new`:**
```csharp
// ❌ Wrong
Pet pet1;
pet1.Name = "Fluffy";  // ERROR!

// ✅ Correct
Pet pet1 = new Pet();
pet1.Name = "Fluffy";
```

**Using class instead of object:**
```csharp
// ❌ Wrong
Pet.Play();  // ERROR! Pet is the class, not an object

// ✅ Correct
pet1.Play();  // Call method on the object
```

## Bonus Challenges

1. **Happiness Cap**: Prevent happiness from going above 100
2. **Status Message**: Make ShowStatus() print a happy or sad message based on happiness level
3. **More Actions**: Add `Sleep()` method that increases happiness by 20
4. **Validation**: Prevent happiness from going below 0

Example bonus output:
```
Fluffy's happiness: 100 - Very Happy! 😊
Rex's happiness: 35 - Needs attention 😢
```

---

**Time Estimate:** 20 minutes  
**Difficulty:** Easy  
**Type:** Hands-On Creation

This exercise teaches the fundamental structure of classes. Take your time and make sure you understand each part!