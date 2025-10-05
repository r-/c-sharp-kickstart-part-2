# Chapter 4 – Classes, Objects, and Properties

In Chapter 3, we learned to create classes with public fields. But we discovered a problem—anyone can change our data directly, even to invalid values! Now we'll learn **properties**—a safer way to control access to our data.

## Learning Objectives

By the end of this chapter, you will be able to:

- Distinguish between fields and properties
- Use auto-properties for cleaner code
- Add validation using custom property setters
- Apply access modifiers to control data access
- Understand why direct field access can be dangerous
- Write safer classes that prevent invalid states

## Prerequisites

This chapter assumes you've completed **Chapter 3** and can:

- Create classes with fields and methods
- Use the `new` keyword to create objects
- Write constructors to initialize objects
- Understand the relationship between classes and objects

---

## Mission Brief

In this chapter you will:

- Learn why public fields create maintenance problems
- Discover properties as safe gateways to data
- Master auto-properties for quick implementation
- Add validation to prevent invalid data
- Use access modifiers to hide implementation details

This chapter teaches you to write defensive code that protects against errors.

---

## Concept Power-Up – The Problem with Public Fields

Let's revisit our Player class from Chapter 3:

```csharp
class Player
{
    public string Name;
    public int Score;
    
    public void AddPoints(int points)
    {
        Score += points;
    }
}
```

This works, but it has a **critical flaw**. Watch what happens:

```csharp
Player player = new Player();
player.Name = "";           // Empty name? That's weird!
player.Score = -500;        // Negative score? That's broken!
player.Score = 999999999;   // Score too high? Unrealistic!
```

**The Problem:**
- Anyone can set any value directly
- No validation or safety checks
- Objects can enter invalid states
- Hard to find where bad data came from

> **Key Insight**: Public fields give unlimited access. We need controlled gateways.

---

## Core Concepts

### Concept 1 – Properties: Safe Gateways to Data

A **property** looks like a field but acts like a method.

```csharp
class Player
{
    private int score;  // Field (hidden storage)
    
    // Property (controlled access)
    public int Score
    {
        get { return score; }
        set { score = value; }
    }
}
```

**How it works:**
- `get` runs when you read the property
- `set` runs when you write to the property
- `value` is the new value being assigned
- The field `score` stores the actual data

**Using it looks the same:**
```csharp
Player player = new Player();
player.Score = 10;              // Calls set
Console.WriteLine(player.Score); // Calls get
```

But now we have a place to add validation!

### Concept 2 – Auto-Properties: The Quick Way

When you don't need validation, use **auto-properties**:

```csharp
class Player
{
    // Auto-property (compiler creates hidden field)
    public string Name { get; set; }
    public int Score { get; set; }
}
```

This is shorthand for:
```csharp
private string name;
public string Name
{
    get { return name; }
    set { name = value; }
}
```

**When to use:**
- You need a simple property without validation
- You might add validation later
- You want clean, readable code

**When NOT to use:**
- You need validation or logic in get/set
- You need computed values
- You need to trigger side effects

### Concept 3 – Adding Validation with Custom Setters

Now we can prevent invalid data:

```csharp
class Player
{
    private int score;
    
    public int Score
    {
        get { return score; }
        set
        {
            if (value < 0)
            {
                Console.WriteLine("Score cannot be negative!");
                return;  // Reject the value
            }
            score = value;  // Accept the value
        }
    }
}
```

**Try this:**
```csharp
Player player = new Player();
player.Score = 100;   // ✅ Accepted
player.Score = -50;   // ❌ Rejected with message
Console.WriteLine(player.Score);  // Still 100
```

The invalid value is blocked at the gateway!

### Concept 4 – Access Modifiers Control Visibility

**Access modifiers** control who can see and use your code.

```csharp
class BankAccount
{
    private decimal balance;        // Only this class can access
    public string Owner { get; set; }  // Anyone can access
    
    protected decimal GetBalance()  // This class + derived classes
    {
        return balance;
    }
}
```

**Common modifiers:**
- `public` - Anyone can access
- `private` - Only this class can access
- `protected` - This class and derived classes (Chapter 6)
- `internal` - Same assembly only (advanced)

**Best practice:**
- Make fields `private` by default
- Expose only what's needed through `public` properties
- Hide implementation details

### Concept 5 – Read-Only and Write-Only Properties

Sometimes you want one-way access:

```csharp
class Player
{
    private int id;
    private int score;
    
    // Read-only: Can get but not set
    public int Id
    {
        get { return id; }
    }
    
    // Or using init (set only during construction)
    public int Id { get; init; }
    
    // Write-only: Can set but not get (rare)
    private int tempScore;
    public int Score
    {
        set { tempScore = value; }
    }
}
```

**Example usage:**
```csharp
class Player
{
    public string Name { get; set; }
    public int Id { get; init; }  // Set once at creation
    
    public Player(string name, int id)
    {
        Name = name;
        Id = id;
    }
}

Player player = new Player("Alex", 1);
player.Id = 2;  // ❌ ERROR: init-only property
```

---

## Try It – BankAccount with Properties

Let's rebuild our BankAccount class with properties:

```csharp
class BankAccount
{
    private decimal balance;  // Hidden field
    
    public string Owner { get; set; }  // Auto-property
    
    // Property with validation
    public decimal Balance
    {
        get { return balance; }
        private set  // Only this class can set
        {
            if (value < 0)
            {
                throw new ArgumentException("Balance cannot be negative");
            }
            balance = value;
        }
    }
    
    public BankAccount(string owner)
    {
        Owner = owner;
        Balance = 0;
    }
    
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Amount must be positive");
            return;
        }
        Balance += amount;
    }
    
    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
        {
            Console.WriteLine("Insufficient funds");
            return;
        }
        Balance -= amount;
    }
}
```

**Test it:**
```csharp
BankAccount account = new BankAccount("Alex");
account.Deposit(100);
account.Withdraw(30);
Console.WriteLine($"{account.Owner}: ${account.Balance}");
// Output: Alex: $70

account.Balance = 1000;  // ❌ ERROR: set is private
```

---

## Challenge – Upgrade Your Player Class

Take your Player class from Chapter 3 and upgrade it:

1. Make all fields private
2. Add Name property with validation (not empty)
3. Add Score property that prevents negative values
4. Add a read-only Level property computed from Score
5. Test that invalid values are rejected

**Hints:**
```csharp
class Player
{
    private string name;
    private int score;
    
    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                // Reject empty names
                return;
            }
            name = value;
        }
    }
    
    // Your code here for Score property
    
    // Computed property
    public int Level
    {
        get { return score / 100; }
    }
}
```

> **🏋️ Practice Now**: Complete [Exercise 04-01: Fields vs Properties](../../exercises/04-classes-and-properties/01-fields-vs-properties/) to understand the difference.

---

## Mini Project – Build YOUR Bank Account System

**Goal:** Create YOUR own banking system with validated data and safe operations!

This is YOUR project to own and customize. You'll apply properties and validation to build a realistic bank account manager.

### Why This Project?

Real banking software must protect data rigorously. This project teaches you defensive programming patterns used in production systems.

### Learning Goals

- Apply properties with validation
- Use access modifiers correctly
- Prevent invalid states
- Handle edge cases gracefully

### Minimum Requirements

YOUR bank account system MUST include:

1. **BankAccount class with:**
   - Owner property (validated: not empty)
   - Balance property (read-only from outside, validated: never negative)
   - Account ID (read-only, set once)
   - Deposit method with validation
   - Withdraw method preventing overdraft

2. **Validation rules:**
   - Owner name cannot be empty or whitespace
   - Balance cannot go negative
   - Transaction amounts must be positive
   - Account ID cannot change after creation

3. **User interaction:**
   - Create at least two accounts
   - Perform deposits and withdrawals
   - Display account information
   - Show error messages for invalid operations

### Expected Output

```
Creating accounts...
Account #1001 created for Alice

Alice's Account:
- ID: 1001
- Balance: $150.00

Attempting invalid operations...
Error: Cannot withdraw more than balance
Error: Amount must be positive

Final balances:
Alice: $150.00
Bob: $75.00
```

### Implementation Steps

1. Create BankAccount class with private fields
2. Add properties with appropriate access modifiers
3. Implement validation in property setters
4. Add Deposit and Withdraw methods
5. Test with valid and invalid inputs
6. Display formatted account information

### Hints

**Property with validation:**
```csharp
private string owner;
public string Owner
{
    get { return owner; }
    set
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Owner required");
        owner = value;
    }
}
```

**Read-only property:**
```csharp
public int AccountId { get; init; }
```

**Private setter:**
```csharp
public decimal Balance { get; private set; }
```

### Make It Your Own!

After meeting the requirements, add YOUR creative features:

- **Transaction history** - Track all deposits and withdrawals
- **Account types** - Savings vs. checking with different rules
- **Interest calculation** - Add monthly interest to balance
- **Transfer between accounts** - Move money safely
- **Overdraft protection** - Allow small negative balances with fees
- **Account limits** - Maximum balance or withdrawal amounts
- **Transaction fees** - Charge for certain operations
- **Statement printing** - Formatted account summary
- **Multiple currencies** - Handle different currency types
- **Security features** - PIN or password protection

### Self-Assessment Checklist

- [ ] All fields are private
- [ ] Properties use appropriate access modifiers
- [ ] Validation prevents invalid states
- [ ] Error messages are clear and helpful
- [ ] Code is well-organized and readable
- [ ] Edge cases are handled (zero, negative, very large numbers)
- [ ] Account cannot enter an impossible state

### What Makes a Great Project?

**Functionality (40%)**
- All requirements work correctly
- Validation prevents all invalid states
- Edge cases are handled

**Code Quality (40%)**
- Properties used appropriately
- Access modifiers chosen correctly
- Validation logic is clear
- Methods are focused and simple

**User Experience (20%)**
- Clear error messages
- Formatted output
- Easy to understand what's happening

> **🚀 Mini Project**: Ready to build something real? Complete the [Mini Project: Build YOUR Bank Account System](../../projects/04-classes-and-properties/) to create a complete banking system with validated data.

---

## Common Mistakes

### Using Public Fields Instead of Properties

```csharp
// ❌ Wrong: Direct field access, no validation
class Player
{
    public string Name;
    public int Score;
}

// ✅ Correct: Properties with controlled access
class Player
{
    public string Name { get; set; }
    private int score;
    
    public int Score
    {
        get { return score; }
        set
        {
            if (value < 0) return;
            score = value;
        }
    }
}
```

### Forgetting to Validate in Setter

```csharp
// ❌ Wrong: No validation
public int Age
{
    get { return age; }
    set { age = value; }  // Allows negative ages!
}

// ✅ Correct: Guard against invalid values
public int Age
{
    get { return age; }
    set
    {
        if (value < 0 || value > 150)
        {
            throw new ArgumentOutOfRangeException();
        }
        age = value;
    }
}
```

### Making Setters Public When They Should Be Private

```csharp
// ❌ Wrong: Balance can be changed directly
public decimal Balance { get; set; }

account.Balance = 1000000;  // Cheating!

// ✅ Correct: Balance changes only through methods
public decimal Balance { get; private set; }

public void Deposit(decimal amount)
{
    Balance += amount;  // Controlled access
}
```

### Confusing Auto-Properties with Fields

```csharp
// ❌ Wrong: Trying to add validation to auto-property
public string Name { get; set; }  // Can't add logic here

// ✅ Correct: Use custom property for validation
private string name;
public string Name
{
    get { return name; }
    set
    {
        if (!string.IsNullOrWhiteSpace(value))
            name = value;
    }
}
```

---

## Hands-On Practice

Now practice properties and validation with these exercises:

### Exercise Progression

1. **[Exercise 04-01: Fields vs Properties](../../exercises/04-classes-and-properties/01-fields-vs-properties/)** - Convert public fields to properties and understand the difference
2. **[Exercise 04-02: Auto-Properties](../../exercises/04-classes-and-properties/02-auto-properties/)** - Use auto-properties for cleaner code
3. **[Exercise 04-03: Property Validation](../../exercises/04-classes-and-properties/03-property-validation/)** - Add validation to prevent invalid data
4. **[Exercise 04-04: Access Modifiers](../../exercises/04-classes-and-properties/04-access-modifiers/)** - Practice choosing the right access level

Complete these exercises in order to build mastery step by step.

> **💡 Tip**: Focus on understanding WHY properties are better, not just HOW to use them.

---

## Why It Matters

Properties and access modifiers are fundamental to professional C# development because they:

1. **Prevent bugs** - Invalid data is caught early at the property boundary
2. **Enable change** - You can add validation later without breaking code
3. **Hide complexity** - Users don't need to know how data is stored
4. **Support debugging** - Set breakpoints in getters/setters to track changes
5. **Follow standards** - All .NET libraries use properties, not public fields

Every professional C# codebase uses properties extensively. Master them now and your future code will be more robust and maintainable.

---

## Checkpoint

You should now be able to:

- Explain why public fields are problematic
- Create auto-properties for simple cases
- Write custom properties with validation
- Use access modifiers to control visibility
- Prevent objects from entering invalid states
- Choose between fields, auto-properties, and custom properties

---

## Reflection Questions

1. Why is a property better than a public field even when you don't have validation?
2. When would you use an auto-property versus a custom property?
3. How do access modifiers help you write better code?
4. Can you think of a real-world class that needs validation in its properties?

---

## Next Chapter Preview

In **Chapter 5 – Encapsulation and Data Protection**, you'll learn to enforce invariants using guard clauses, create immutable objects, and master the principle of information hiding. You'll discover why keeping implementation details private makes your code safer and easier to maintain.

---

## Key Terms

- **Property**: A member that provides controlled access to a field with get/set accessors
- **Auto-Property**: Compiler-generated property with hidden backing field
- **Backing Field**: The private field that stores data for a property
- **Getter**: The `get` accessor that returns a property's value
- **Setter**: The `set` accessor that assigns a property's value
- **Access Modifier**: Keyword that controls visibility (public, private, protected)
- **Validation**: Code that checks if data is valid before accepting it
- **`value`**: Implicit parameter in a setter representing the new value
- **`init`**: Accessor that allows setting a property only during object construction
- **Encapsulation**: Hiding internal details and exposing only what's necessary

---

**Teacher Notes:**

**Pedagogical Goals:**
- Transition from "it works" to "it's safe and maintainable"
- Emphasize validation as a form of defensive programming
- Connect properties to the concept of abstraction
- Prepare students for encapsulation principles in Chapter 5

**Common Student Questions:**
- "Why not just use public fields?" → Show maintenance scenario where validation is needed later
- "When do I use auto-property vs custom?" → Simple rule: Use auto unless you need validation/logic
- "Is `private set` different from no setter?" → Yes: class can still modify internally vs truly read-only

**Assessment Hints:**
- Check that students validate in setters, not methods
- Ensure they understand when to use private vs public
- Look for appropriate use of auto-properties vs custom
- Verify they can explain WHY properties are better than fields

**Connection to Curriculum (PRRPRR02):**
- Aligns with object-oriented programming principles
- Demonstrates data encapsulation and abstraction
- Introduces defensive programming concepts
- Prepares for SOLID principles in later chapters

**Classroom Activities:**
- Live coding: Convert a public-field class to properties
- Pair exercise: Find validation bugs in sample code
- Discussion: When would you make a setter private?
- Code review: Critique property usage in provided examples

**Rubric Suggestions (Mini Project):**
- Correct use of properties: 30%
- Appropriate access modifiers: 25%
- Comprehensive validation: 25%
- Code organization: 10%
- Error handling: 10%

---

*© C# Kickstart Part 2 Contributors*