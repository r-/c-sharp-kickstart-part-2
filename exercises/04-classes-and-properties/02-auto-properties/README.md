# Exercise 04-02: Auto-Properties

## Goal

Master auto-properties for cleaner code and understand when to use them versus custom properties. You'll learn the shorthand syntax and when it's appropriate.

## Background

Auto-properties let the compiler create the backing field automatically, reducing boilerplate code. They're perfect when you don't need validation or custom logic but might want to add it later.

## Instructions

You will create a `Person` class using auto-properties, then identify which properties should be upgraded to custom properties when validation is needed.

### Part 1: Create Basic Auto-Properties

Create a `Person` class with these auto-properties:
- `FirstName` (string)
- `LastName` (string)  
- `Age` (int)
- `Email` (string)

### Part 2: Add a Computed Property

Add a read-only computed property:
- `FullName` - returns "FirstName LastName"

### Part 3: Add Properties with Different Access

Add properties with private setters:
- `Id` - can only be set internally
- `CreatedDate` - set once in constructor

### Part 4: Upgrade One to Custom Property

Convert `Age` from auto-property to custom property with validation (0-150 range).

## Requirements

Your program must:

1. Create a `Person` class with:
   - Auto-properties for FirstName, LastName, Email
   - Custom property for Age with validation
   - Computed property for FullName
   - Read-only properties for Id and CreatedDate

2. In `Main()`:
   - Create at least two Person objects
   - Demonstrate all property types
   - Test that Age validation works
   - Display formatted person information

## Expected Output

```
Creating people...

Person 1:
ID: 1001
Name: Alice Johnson
Email: alice@example.com
Age: 28
Created: 2024-01-15

Person 2:
ID: 1002
Name: Bob Smith
Email: bob@example.com
Age: 35
Created: 2024-01-15

Testing validation...
Error: Age must be between 0 and 150

Alice is still 28 years old
```

## Hints

### Auto-Property Syntax

```csharp
// Simple auto-property
public string FirstName { get; set; }

// Auto-property with private setter
public int Id { get; private set; }

// Init-only auto-property (set once in constructor)
public DateTime CreatedDate { get; init; }
```

### Computed Property

```csharp
public string FullName
{
    get { return $"{FirstName} {LastName}"; }
}

// Or with expression body
public string FullName => $"{FirstName} {LastName}";
```

### Converting to Custom Property

```csharp
// Before: Auto-property
public int Age { get; set; }

// After: Custom property with validation
private int age;
public int Age
{
    get { return age; }
    set
    {
        if (value < 0 || value > 150)
        {
            Console.WriteLine("Error: Age must be between 0 and 150");
            return;
        }
        age = value;
    }
}
```

### Constructor Usage

```csharp
public Person(string firstName, string lastName, int id)
{
    FirstName = firstName;
    LastName = lastName;
    Id = id;
    CreatedDate = DateTime.Now;
}
```

## Common Mistakes

**Using auto-property when you need validation:**
```csharp
// ❌ Wrong: Can't add validation to auto-property
public int Age { get; set; }  // Allows any value

// ✅ Correct: Use custom property for validation
private int age;
public int Age
{
    get { return age; }
    set
    {
        if (value >= 0 && value <= 150)
            age = value;
    }
}
```

**Forgetting expression body syntax:**
```csharp
// ❌ Works but verbose
public string FullName
{
    get { return FirstName + " " + LastName; }
}

// ✅ Cleaner with expression body
public string FullName => $"{FirstName} {LastName}";
```

**Trying to set init-only property after construction:**
```csharp
Person person = new Person("Alice", "Johnson", 1001);
person.Id = 2000;  // ❌ ERROR: Cannot assign to init-only property
```

## Bonus Challenges

1. **Email validation**: Convert Email to custom property with basic validation (contains @)
2. **Name formatting**: Make FirstName and LastName auto-capitalize first letter
3. **Age category**: Add computed property `AgeCategory` (Child/Teen/Adult/Senior)
4. **Display name**: Add property that returns "Last, First" format
5. **Username generation**: Add computed property that creates username from name

Example bonus output:
```
Person Details:
Name: Alice Johnson
Display Name: Johnson, Alice
Username: ajohnson
Email: alice@example.com
Age: 28 (Adult)
```

## Reflection

After completing this exercise, consider:
1. When should you use auto-properties vs custom properties?
2. What's the advantage of using `{ get; private set; }` instead of `{ get; }`?
3. How do computed properties differ from regular properties?
4. When would you use `init` instead of `private set`?

---

**Time Estimate:** 30 minutes  
**Difficulty:** Easy-Medium  
**Type:** Syntax Practice & Application

This exercise teaches you to choose the right property type for each situation, a key skill for clean C# code.