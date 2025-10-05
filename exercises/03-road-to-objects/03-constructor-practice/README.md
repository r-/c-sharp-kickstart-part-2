# Exercise 03-03: Constructor Practice

## Goal

Master constructors by creating a `Student` class with multiple ways to initialize objects. Learn when and why to use constructors for cleaner, more reliable code.

## Background

Constructors are special methods that run automatically when you create an object. They ensure objects start with valid, initialized data instead of default values.

Without constructors, you must manually set every field after creating an object, which is tedious and error-prone. Constructors solve this problem.

## Instructions

You will create a `Student` class that can be initialized in different ways using multiple constructors.

### Part 1: Basic Constructor

Create a `Student` class with:
- Fields: `Name` (string), `Age` (int), `Grade` (string)
- Constructor that takes name and age as parameters
- Method `DisplayInfo()` that prints student information

### Part 2: Default Constructor

Add a second constructor with no parameters that sets default values:
- Name = "Unknown"
- Age = 16
- Grade = "Not assigned"

### Part 3: Full Constructor

Add a third constructor that accepts all three values (name, age, grade).

## Requirements

Your program must include:

1. **Student Class** with:
   - Three fields: `Name`, `Age`, `Grade`
   - Three constructors:
     - Default constructor (no parameters)
     - Constructor with name and age
     - Constructor with name, age, and grade
   - Method `DisplayInfo()` to print all fields

2. **Main Method** that:
   - Creates one student using each constructor
   - Calls `DisplayInfo()` on each student to show the results

## Expected Output

```
Student: Unknown, Age: 16, Grade: Not assigned
Student: Alex, Age: 17, Grade: Not assigned
Student: Riley, Age: 18, Grade: A
```

## Starter Code

```csharp
using System;

class Student
{
    public string Name;
    public int Age;
    public string Grade;

    // TODO: Add default constructor
    
    
    // TODO: Add constructor with name and age
    
    
    // TODO: Add constructor with name, age, and grade
    
    
    public void DisplayInfo()
    {
        Console.WriteLine($"Student: {Name}, Age: {Age}, Grade: {Grade}");
    }
}

class Program
{
    static void Main()
    {
        // TODO: Create student using default constructor
        
        
        // TODO: Create student using name + age constructor
        
        
        // TODO: Create student using full constructor
        
        
        // TODO: Display all students
        
    }
}
```

## Hints

### Default Constructor
```csharp
public Student()
{
    Name = "Unknown";
    Age = 16;
    Grade = "Not assigned";
}
```

### Constructor with Parameters
```csharp
public Student(string name, int age)
{
    Name = name;
    Age = age;
    Grade = "Not assigned";  // Set default for missing parameter
}
```

### Using Constructors
```csharp
// Default constructor
Student student1 = new Student();

// Constructor with parameters
Student student2 = new Student("Alex", 17);

// Full constructor
Student student3 = new Student("Riley", 18, "A");
```

## Common Mistakes

**Using wrong variable names:**
```csharp
// ❌ Confusing: Which 'Name' is which?
public Student(string Name)
{
    Name = Name;  // This doesn't work!
}

// ✅ Clear: Use different parameter name
public Student(string studentName)
{
    Name = studentName;
}

// ✅ Also clear: Use 'this' keyword
public Student(string Name)
{
    this.Name = Name;  // this.Name = field, Name = parameter
}
```

**Forgetting to initialize all fields:**
```csharp
// ❌ Grade is not set
public Student(string name, int age)
{
    Name = name;
    Age = age;
    // Grade is left as null!
}

// ✅ Set all fields or give defaults
public Student(string name, int age)
{
    Name = name;
    Age = age;
    Grade = "Not assigned";
}
```

## Why Constructors Matter

**Without Constructors:**
```csharp
Student student = new Student();
student.Name = "Alex";
student.Age = 17;
student.Grade = "B";
// Easy to forget a field!
```

**With Constructors:**
```csharp
Student student = new Student("Alex", 17, "B");
// All fields set in one line, can't forget any!
```

## Bonus Challenges

1. **Validation**: Add validation in constructors to ensure:
   - Age is between 5 and 100
   - Name is not empty
   - Grade is A, B, C, D, or F

2. **Constructor Chaining**: Make one constructor call another:
```csharp
public Student(string name, int age) : this(name, age, "Not assigned")
{
    // Calls the full constructor
}
```

3. **Calculated Fields**: Add a `GraduationYear` field that's automatically calculated from age:
```csharp
public Student(string name, int age)
{
    Name = name;
    Age = age;
    GraduationYear = DateTime.Now.Year + (18 - age);
}
```

## Reflection Questions

1. What problem do constructors solve compared to setting fields manually?
2. Why might you want multiple constructors for the same class?
3. When would you use a default constructor vs. one with parameters?
4. What happens if you don't create any constructor? (C# creates a hidden default one!)

---

**Time Estimate:** 25 minutes  
**Difficulty:** Medium  
**Type:** Hands-On Coding

This exercise teaches proper object initialization - a fundamental skill for all OOP programming!