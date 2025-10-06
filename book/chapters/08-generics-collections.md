# Chapter 8 – Generics and Collections

In Chapter 7, we learned polymorphism through interfaces and abstract classes. Now we'll discover **generics**—the ability to write type-safe code that works with any type. You'll master the powerful collection classes that make C# development productive and elegant.

## Learning Objectives

By the end of this chapter, you will be able to:

- Explain what generics are and why they matter
- Use `List<T>` for dynamic collections
- Use `Dictionary<K,V>` for key-value storage
- Understand generic type parameters (`<T>`)
- Create generic methods
- Create generic classes
- Choose the right collection for each scenario

## Prerequisites

This chapter assumes you've completed **Chapter 7** and can:

- Work with interfaces and polymorphism
- Understand abstract vs concrete types
- Use collections like `List<Employee>`
- Create and implement interfaces
- Apply encapsulation principles

---

## Mission Brief

In this chapter you will:

- Discover how generics provide type safety without sacrificing flexibility
- Master `List<T>` for managing dynamic collections
- Learn `Dictionary<K,V>` for fast lookups by key
- Understand when to use each collection type
- Create your own generic methods and classes
- Build systems that work with any type safely

This chapter teaches you to write flexible, reusable code that the compiler can verify at build time.

---

## Concept Power-Up – The Problem Generics Solve

Imagine you need collections for different types:

```csharp
// ❌ Without generics - need separate classes
class IntList
{
    private int[] items = new int[10];
    public void Add(int item) { /* ... */ }
}

class StringList
{
    private string[] items = new string[10];
    public void Add(string item) { /* ... */ }
}

// Need to write the same code for every type!
```

With generics:

```csharp
// ✅ With generics - one class works for all types
List<int> numbers = new List<int>();
List<string> names = new List<string>();
List<Player> players = new List<Player>();

// Same code, type-safe for any type!
```

> **Key Insight**: Generics let you write code once that works safely with any type, catching type errors at compile time instead of runtime.

---

## Core Concepts

### Concept 1 – List<T>: Dynamic Arrays

A `List<T>` is a dynamic collection that grows automatically. The `<T>` is a **type parameter**—a placeholder for any type.

```csharp
// Create lists for different types
List<int> scores = new List<int>();
List<string> names = new List<string>();
List<Player> players = new List<Player>();

// Add items
scores.Add(95);
scores.Add(87);
scores.Add(92);

names.Add("Alice");
names.Add("Bob");

// Access by index
Console.WriteLine(scores[0]);  // 95
Console.WriteLine(names[1]);   // Bob

// Get count
Console.WriteLine($"Scores: {scores.Count}");  // 3
```

**Common List operations:**

```csharp
List<string> fruits = new List<string>();

// Adding
fruits.Add("Apple");
fruits.Add("Banana");
fruits.Add("Cherry");

// Inserting at position
fruits.Insert(1, "Apricot");  // Insert at index 1

// Removing
fruits.Remove("Banana");      // Remove by value
fruits.RemoveAt(0);           // Remove by index

// Checking
bool hasApple = fruits.Contains("Apple");
int index = fruits.IndexOf("Cherry");

// Clearing
fruits.Clear();  // Remove all items
```

**Iterating over lists:**

```csharp
List<int> numbers = new List<int> { 10, 20, 30, 40, 50 };

// Using foreach
foreach (int num in numbers)
{
    Console.WriteLine(num);
}

// Using for loop with index
for (int i = 0; i < numbers.Count; i++)
{
    Console.WriteLine($"[{i}] = {numbers[i]}");
}
```

### Concept 2 – Dictionary<K,V>: Key-Value Pairs

A `Dictionary<K,V>` stores key-value pairs for fast lookups. `K` is the key type, `V` is the value type.

```csharp
// Create dictionary: string keys → int values
Dictionary<string, int> ages = new Dictionary<string, int>();

// Add key-value pairs
ages["Alice"] = 25;
ages["Bob"] = 30;
ages["Carol"] = 27;

// Or use Add method
ages.Add("David", 22);

// Access by key
Console.WriteLine(ages["Alice"]);  // 25

// Check if key exists
if (ages.ContainsKey("Bob"))
{
    Console.WriteLine($"Bob is {ages["Bob"]} years old");
}

// Safe access with TryGetValue
if (ages.TryGetValue("Eve", out int age))
{
    Console.WriteLine($"Found: {age}");
}
else
{
    Console.WriteLine("Eve not found");
}
```

**Iterating over dictionaries:**

```csharp
Dictionary<string, string> capitals = new Dictionary<string, string>
{
    { "Sweden", "Stockholm" },
    { "Norway", "Oslo" },
    { "Denmark", "Copenhagen" }
};

// Iterate over key-value pairs
foreach (var pair in capitals)
{
    Console.WriteLine($"{pair.Key}: {pair.Value}");
}

// Just keys
foreach (string country in capitals.Keys)
{
    Console.WriteLine(country);
}

// Just values
foreach (string capital in capitals.Values)
{
    Console.WriteLine(capital);
}
```

**Common dictionary operations:**

```csharp
Dictionary<int, string> students = new Dictionary<int, string>();

// Adding
students[101] = "Alice";
students[102] = "Bob";

// Updating
students[101] = "Alice Smith";  // Overwrites

// Removing
students.Remove(102);

// Checking
bool exists = students.ContainsKey(101);
bool hasValue = students.ContainsValue("Alice Smith");

// Count
int count = students.Count;

// Clearing
students.Clear();
```

### Concept 3 – Understanding Generic Type Parameters

The `<T>` syntax defines a **type parameter**—a placeholder filled in when you create the collection.

```csharp
// T is replaced with int
List<int> numbers = new List<int>();

// T is replaced with string
List<string> names = new List<string>();

// T is replaced with Player
List<Player> players = new List<Player>();
```

**Why this matters:**

```csharp
List<int> numbers = new List<int>();
numbers.Add(42);        // ✅ Allowed - int
numbers.Add("hello");   // ❌ Compiler error - wrong type!

// Without generics (old way with ArrayList):
ArrayList items = new ArrayList();
items.Add(42);
items.Add("hello");     // ✅ Compiles but causes runtime errors!
int x = (int)items[1];  // 💥 Runtime crash!
```

> **Type safety**: Generics catch errors at compile time, not runtime.

### Concept 4 – Creating Generic Methods

You can create your own generic methods:

```csharp
// Generic method - works with any type
T GetFirst<T>(List<T> items)
{
    if (items.Count == 0)
        throw new InvalidOperationException("List is empty");
    return items[0];
}

// Usage - compiler infers type
List<int> numbers = new List<int> { 1, 2, 3 };
int first = GetFirst(numbers);  // T = int

List<string> names = new List<string> { "Alice", "Bob" };
string firstName = GetFirst(names);  // T = string
```

**Generic swap method:**

```csharp
void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}

// Usage
int x = 5, y = 10;
Swap(ref x, ref y);
Console.WriteLine($"{x}, {y}");  // 10, 5

string s1 = "hello", s2 = "world";
Swap(ref s1, ref s2);
Console.WriteLine($"{s1}, {s2}");  // world, hello
```

**Generic method with constraints:**

```csharp
// Only works with types implementing IComparable
T Max<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) > 0 ? a : b;
}

int maxNum = Max(5, 10);           // 10
string maxStr = Max("abc", "xyz"); // xyz
```

### Concept 5 – Creating Generic Classes

You can create classes that work with any type:

```csharp
class Box<T>
{
    private T item;
    
    public T Item
    {
        get { return item; }
        set { item = value; }
    }
    
    public Box(T item)
    {
        this.item = item;
    }
    
    public void Display()
    {
        Console.WriteLine($"Box contains: {item}");
    }
}

// Usage
Box<int> numberBox = new Box<int>(42);
numberBox.Display();  // Box contains: 42

Box<string> textBox = new Box<string>("Hello");
textBox.Display();    // Box contains: Hello

Box<Player> playerBox = new Box<Player>(new Player("Alice"));
playerBox.Display();  // Box contains: Player: Alice
```

**Generic class with multiple type parameters:**

```csharp
class Pair<TFirst, TSecond>
{
    public TFirst First { get; set; }
    public TSecond Second { get; set; }
    
    public Pair(TFirst first, TSecond second)
    {
        First = first;
        Second = second;
    }
    
    public override string ToString()
    {
        return $"({First}, {Second})";
    }
}

// Usage
Pair<string, int> nameAge = new Pair<string, int>("Alice", 25);
Console.WriteLine(nameAge);  // (Alice, 25)

Pair<int, string> idName = new Pair<int, string>(101, "Bob");
Console.WriteLine(idName);   // (101, Bob)
```

---

## Try It – Student Grade Tracker

Let's build a grade tracking system using `Dictionary<K,V>`:

```csharp
class GradeTracker
{
    private Dictionary<string, List<int>> studentGrades;
    
    public GradeTracker()
    {
        studentGrades = new Dictionary<string, List<int>>();
    }
    
    public void AddStudent(string name)
    {
        if (!studentGrades.ContainsKey(name))
        {
            studentGrades[name] = new List<int>();
            Console.WriteLine($"Added student: {name}");
        }
        else
        {
            Console.WriteLine($"{name} already exists");
        }
    }
    
    public void AddGrade(string name, int grade)
    {
        if (!studentGrades.ContainsKey(name))
        {
            Console.WriteLine($"Student {name} not found");
            return;
        }
        
        if (grade < 0 || grade > 100)
        {
            Console.WriteLine("Grade must be 0-100");
            return;
        }
        
        studentGrades[name].Add(grade);
        Console.WriteLine($"Added grade {grade} for {name}");
    }
    
    public double GetAverage(string name)
    {
        if (!studentGrades.ContainsKey(name))
        {
            Console.WriteLine($"Student {name} not found");
            return 0;
        }
        
        List<int> grades = studentGrades[name];
        if (grades.Count == 0)
            return 0;
        
        int sum = 0;
        foreach (int grade in grades)
        {
            sum += grade;
        }
        return (double)sum / grades.Count;
    }
    
    public void DisplayReport()
    {
        Console.WriteLine("\n=== Grade Report ===");
        foreach (var pair in studentGrades)
        {
            string name = pair.Key;
            List<int> grades = pair.Value;
            double avg = GetAverage(name);
            
            Console.WriteLine($"\n{name}:");
            Console.WriteLine($"  Grades: {string.Join(", ", grades)}");
            Console.WriteLine($"  Average: {avg:F1}");
        }
    }
}
```

**Using the tracker:**

```csharp
GradeTracker tracker = new GradeTracker();

tracker.AddStudent("Alice");
tracker.AddStudent("Bob");

tracker.AddGrade("Alice", 95);
tracker.AddGrade("Alice", 87);
tracker.AddGrade("Alice", 92);

tracker.AddGrade("Bob", 78);
tracker.AddGrade("Bob", 85);

tracker.DisplayReport();
```

**Output:**
```
Added student: Alice
Added student: Bob
Added grade 95 for Alice
Added grade 87 for Alice
Added grade 92 for Alice
Added grade 78 for Bob
Added grade 85 for Bob

=== Grade Report ===

Alice:
  Grades: 95, 87, 92
  Average: 91.3

Bob:
  Grades: 78, 85
  Average: 81.5
```

> **🏋️ Practice Now**: Complete [Exercise 08-01: Generic Methods](../../exercises/08-generics-collections/01-generic-methods/) to create your own generic utility methods.

---

## Challenge – Inventory Manager

Create an inventory system using generics:

1. Create generic class `Inventory<T>` where items have an ID
2. Use `Dictionary<int, T>` internally for storage
3. Implement methods:
   - `AddItem(int id, T item)`
   - `GetItem(int id)` returns item or null
   - `RemoveItem(int id)` returns true if removed
   - `GetAllItems()` returns `List<T>`
   - `Count` property

4. Test with different types:
   - `Inventory<string>` for book titles
   - `Inventory<Product>` for products
   - `Inventory<Player>` for game characters

**Hints:**

```csharp
class Inventory<T>
{
    private Dictionary<int, T> items;
    
    public Inventory()
    {
        items = new Dictionary<int, T>();
    }
    
    public void AddItem(int id, T item)
    {
        if (items.ContainsKey(id))
        {
            Console.WriteLine($"Item {id} already exists");
            return;
        }
        items[id] = item;
    }
    
    public T GetItem(int id)
    {
        if (items.TryGetValue(id, out T item))
            return item;
        return default(T);  // null for reference types, 0 for int, etc.
    }
}
```

> **🏋️ Practice Now**: Complete [Exercise 08-02: List Operations](../../exercises/08-generics-collections/02-list-operations/) to master working with `List<T>`.

---

## Mini Project – Build YOUR Task Manager

**Goal:** Create YOUR own task management system using `List<T>` and `Dictionary<K,V>`!

This is YOUR project to design and implement. You'll combine collections, generics, and OOP to build a practical application that manages tasks across different categories.

### Why This Project?

Every productivity app uses collections to manage data. This project teaches you professional data management patterns using type-safe collections.

### Learning Goals

- Use `List<T>` for ordered collections
- Use `Dictionary<K,V>` for categorized data
- Combine multiple collection types
- Design classes that work with generics
- Apply encapsulation with collections
- Build CRUD operations (Create, Read, Update, Delete)

### Minimum Requirements

YOUR task manager MUST include:

1. **Task class:**
   - Properties: `Id` (int), `Title` (string), `Description` (string), `IsCompleted` (bool), `Priority` (enum: Low, Medium, High)
   - Constructor with validation
   - Method to mark task complete/incomplete

2. **TaskManager class:**
   - Store tasks in `Dictionary<string, List<Task>>` (category → tasks)
   - Methods:
     - `AddTask(string category, Task task)`
     - `RemoveTask(string category, int taskId)`
     - `GetTasksByCategory(string category)`
     - `GetAllTasks()`
     - `MarkTaskComplete(string category, int taskId)`
     - `DisplayTasks()`
     - `GetStatistics()`

3. **Features:**
   - Add tasks to categories (Work, Personal, Shopping, etc.)
   - Remove tasks
   - Mark tasks as complete
   - Display all tasks grouped by category
   - Show statistics (total tasks, completed, by category)
   - Filter tasks by priority
   - Search tasks by keyword

4. **User interaction:**
   - Create multiple categories
   - Add several tasks
   - Mark some complete
   - Display organized task list
   - Show meaningful statistics

### Expected Output

```
=== Task Manager ===

Adding tasks...
✓ Added task 1 to Work: Complete report
✓ Added task 2 to Work: Team meeting
✓ Added task 3 to Personal: Gym
✓ Added task 4 to Shopping: Buy groceries

=== All Tasks ===

Work (2 tasks):
  [1] Complete report (High) - Pending
  [2] Team meeting (Medium) - Pending

Personal (1 task):
  [3] Gym (Low) - Pending

Shopping (1 task):
  [4] Buy groceries (Medium) - Pending

Marking task 1 complete...
✓ Task 'Complete report' marked complete

=== Statistics ===
Total tasks: 4
Completed: 1
Pending: 3
Categories: 3

Tasks by priority:
  High: 1
  Medium: 2
  Low: 1
```

### Implementation Steps

1. Create `Task` class with properties
2. Create `Priority` enum
3. Build `TaskManager` with dictionary
4. Implement add/remove methods
5. Add display functionality
6. Implement statistics
7. Add search and filter
8. Polish and test

### Hints

**Task class:**

```csharp
enum Priority { Low, Medium, High }

class Task
{
    private static int nextId = 1;
    
    public int Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public bool IsCompleted { get; private set; }
    
    public Task(string title, string description, Priority priority)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title required");
        
        Id = nextId++;
        Title = title;
        Description = description;
        Priority = priority;
        IsCompleted = false;
    }
    
    public void MarkComplete()
    {
        IsCompleted = true;
    }
    
    public void MarkIncomplete()
    {
        IsCompleted = false;
    }
}
```

**TaskManager structure:**

```csharp
class TaskManager
{
    private Dictionary<string, List<Task>> tasksByCategory;
    
    public TaskManager()
    {
        tasksByCategory = new Dictionary<string, List<Task>>();
    }
    
    public void AddTask(string category, Task task)
    {
        if (!tasksByCategory.ContainsKey(category))
        {
            tasksByCategory[category] = new List<Task>();
        }
        tasksByCategory[category].Add(task);
    }
    
    public List<Task> GetAllTasks()
    {
        List<Task> allTasks = new List<Task>();
        foreach (var list in tasksByCategory.Values)
        {
            allTasks.AddRange(list);
        }
        return allTasks;
    }
}
```

### Make It YOUR Own!

Add YOUR creative features:

- **Due dates** - Track deadlines with `DateTime`
- **Tags** - Multiple tags per task
- **Subtasks** - Nested task lists
- **Recurring tasks** - Repeat daily/weekly/monthly
- **Color coding** - Priority-based visual grouping
- **Task history** - Completed task archive
- **Time tracking** - Estimate vs actual time
- **Export** - Save to file, load from file
- **Search** - Find tasks by keyword
- **Sort options** - By priority, date, category
- **Reminders** - Alert for due tasks
- **Task dependencies** - Task must complete before another
- **Progress tracking** - Completion percentages
- **Notes** - Additional comments on tasks

### Self-Assessment Checklist

- [ ] Task class properly encapsulated
- [ ] TaskManager uses Dictionary correctly
- [ ] Can add tasks to multiple categories
- [ ] Can remove and mark tasks complete
- [ ] Display shows organized output
- [ ] Statistics calculate correctly
- [ ] Collections used appropriately
- [ ] Input validation implemented
- [ ] No duplicate task IDs
- [ ] Clean, readable code

### What Makes a Great Project?

- **Collection Usage** (40%): Proper use of List and Dictionary, appropriate data structures
- **Functionality** (30%): All core features work reliably, handles edge cases
- **Code Quality** (20%): Well-organized, follows OOP principles, good naming
- **Creativity** (10%): YOUR unique features and enhancements

> **🚀 Mini Project**: Ready to build a real application? Complete the [Mini Project: Task Manager](../../projects/08-generics-collections/) to create YOUR task management system.

---

## Common Mistakes

### Using Wrong Collection Type

```csharp
// ❌ Wrong - List for lookups by key
List<Student> students = new List<Student>();
// Must loop through entire list to find student by ID

// ✅ Correct - Dictionary for fast lookups
Dictionary<int, Student> students = new Dictionary<int, Student>();
Student s = students[101];  // Instant lookup
```

### Modifying Collection While Iterating

```csharp
// ❌ Wrong - can't modify during foreach
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
foreach (int num in numbers)
{
    if (num % 2 == 0)
        numbers.Remove(num);  // ❌ Runtime error!
}

// ✅ Correct - iterate backwards or create new list
for (int i = numbers.Count - 1; i >= 0; i--)
{
    if (numbers[i] % 2 == 0)
        numbers.RemoveAt(i);
}
```

### Not Checking Key Exists

```csharp
// ❌ Wrong - will throw exception if key missing
Dictionary<string, int> ages = new Dictionary<string, int>();
int age = ages["Unknown"];  // ❌ KeyNotFoundException!

// ✅ Correct - check first
if (ages.ContainsKey("Unknown"))
{
    int age = ages["Unknown"];
}

// ✅ Better - use TryGetValue
if (ages.TryGetValue("Unknown", out int age))
{
    Console.WriteLine(age);
}
```

### Forgetting Count vs Length

```csharp
int[] array = new int[5];
List<int> list = new List<int>();

// Arrays use Length
int arraySize = array.Length;

// Collections use Count
int listSize = list.Count;  // Not .Length!
```

---

## Hands-On Practice

Practice generics and collections with these exercises:

### Exercise Progression

1. **[Exercise 08-01: Generic Methods](../../exercises/08-generics-collections/01-generic-methods/)** - Create reusable generic utility methods
2. **[Exercise 08-02: List Operations](../../exercises/08-generics-collections/02-list-operations/)** - Master List<T> manipulation
3. **[Exercise 08-03: Dictionary Operations](../../exercises/08-generics-collections/03-dictionary-operations/)** - Work with key-value data
4. **[Exercise 08-04: Generic Classes](../../exercises/08-generics-collections/04-generic-classes/)** - Build your own generic containers

Complete these in order to build comprehensive collection skills.

> **💡 Tip**: Choose List for ordered data, Dictionary for lookups by key!

---

## Why It Matters

Generics and collections are fundamental to professional C# development because they:

1. **Provide type safety** - Errors caught at compile time, not runtime
2. **Eliminate duplication** - Write once, use with any type
3. **Enable performance** - No boxing/unboxing, no type casting
4. **Support LINQ** - Foundation for powerful query operations (Chapter 12)
5. **Model real data** - Every app manages collections of things
6. **Industry standard** - Used everywhere in .NET, Unity, ASP.NET

Every C# application uses `List<T>` and `Dictionary<K,V>` extensively. Master them now and you'll write code that's fast, safe, and professional.

---

## Checkpoint

You should now be able to:

- Explain what generics are and their benefits
- Use `List<T>` for dynamic collections
- Use `Dictionary<K,V>` for key-value storage
- Understand generic type parameters (`<T>`)
- Create generic methods
- Create generic classes
- Choose appropriate collections for different scenarios

---

## Reflection Questions

1. What problems do generics solve compared to using `object` everywhere?
2. When should you use `List<T>` vs `Dictionary<K,V>`?
3. How do generics provide compile-time type safety?
4. What's the difference between `Contains` and `ContainsKey`?
5. Can you think of a scenario where you'd need a `Dictionary<string, List<T>>`?

---

## Next Chapter Preview

In **Chapter 9 – Exception Handling and Robust Code**, you'll learn to handle errors gracefully. You'll master `try/catch/finally`, create custom exceptions, and write code that never crashes unexpectedly. Exception handling is the difference between amateur and professional code.

---

## Key Terms

- **Generics**: Code that works with type parameters instead of specific types
- **Type Parameter**: Placeholder (like `<T>`) that gets replaced with actual type
- **List<T>**: Dynamic array that grows automatically
- **Dictionary<K,V>**: Key-value collection for fast lookups
- **Type Safety**: Compiler ensures correct types are used
- **Generic Method**: Method with type parameters
- **Generic Class**: Class with type parameters
- **Type Inference**: Compiler automatically determines type parameter
- **Boxing**: Converting value type to object (slow, avoided by generics)
- **Collection**: Group of items stored together
- **Key**: Unique identifier in dictionary
- **Value**: Data associated with key in dictionary

---

**Teacher Notes:**

**Pedagogical Goals:**
- Transition from concrete types to generic abstractions
- Emphasize type safety as compile-time verification
- Show practical use of collections in real applications
- Prepare for LINQ and advanced data manipulation

**Common Student Questions:**
- "Why not just use `object` for everything?" → Show boxing penalty and runtime errors
- "When do I use List vs Dictionary?" → List for ordered data, Dictionary for lookups
- "What does `<T>` mean?" → Type parameter - placeholder filled in at usage
- "Can I have multiple type parameters?" → Yes, like `Dictionary<K,V>` or `Pair<T1,T2>`

**Assessment Hints:**
- Check proper collection choice for scenario
- Verify type parameters used correctly
- Ensure students check keys before dictionary access
- Look for appropriate use of generics vs concrete types
- Check understanding of when to use each collection

**Connection to Curriculum (PRRPRR02):**
- Core C# feature used everywhere
- Foundation for LINQ (later chapters)
- Professional data management patterns
- Type system understanding

**Classroom Activities:**
- Live coding: Convert ArrayList to List<T>
- Group exercise: Choose collection for different scenarios
- Discussion: Why is type safety important?
- Code review: Find type safety violations

**Rubric Suggestions (Mini Project):**
- Collection usage: 35%
- Generic implementation: 25%
- Functionality: 25%
- Code organization: 10%
- Error handling: 5%

---

*© C# Kickstart Part 2 Contributors*