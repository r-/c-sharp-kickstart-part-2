# Exercise 08-02: List Operations

## Goal

Master the essential operations of `List<T>` by building a student roster management system. You'll learn to add, remove, search, and manipulate dynamic collections effectively.

## Background

`List<T>` is the most commonly used collection in C#. Unlike arrays, lists grow and shrink dynamically. Understanding list operations is fundamental to working with data in any C# application.

## Instructions

You will create a `StudentRoster` class that manages a list of student names with various operations.

### Part 1: Basic List Operations

Create a `StudentRoster` class with:
- Private `List<string>` field for student names
- Constructor that initializes empty list
- `AddStudent(string name)` - adds student if not duplicate
- `RemoveStudent(string name)` - removes student by name
- `Count` property - returns number of students
- `DisplayStudents()` - shows all students with numbers

### Part 2: Search and Query Operations

Add these methods:
- `Contains(string name)` - checks if student exists
- `IndexOf(string name)` - returns position of student (-1 if not found)
- `GetStudent(int index)` - returns student at index
- `IsEmpty()` - returns true if no students

### Part 3: Advanced List Operations

Add these methods:
- `InsertStudent(int index, string name)` - inserts at position
- `Clear()` - removes all students
- `GetStudentsByLetter(char letter)` - returns list of names starting with letter
- `SortStudents()` - sorts alphabetically
- `ReverseOrder()` - reverses the list

### Part 4: Testing

In `Main()`, demonstrate:
- Adding multiple students
- Attempting to add duplicate
- Removing a student
- Searching for students
- Inserting at specific position
- Getting students by first letter
- Sorting and reversing
- Displaying after each major operation

## Requirements

Your solution must:
1. Use `List<string>` for storage
2. Validate inputs (no null/empty names)
3. Prevent duplicate student names
4. Handle invalid indices gracefully
5. Display clear messages for all operations
6. Sort and reverse without modifying original (create new list)

## Expected Output

```
=== Student Roster Manager ===

Adding students:
✓ Added: Alice
✓ Added: Bob
✓ Added: Carol
✓ Added: David
✗ Alice already exists

Current roster (4 students):
1. Alice
2. Bob
3. Carol
4. David

Searching:
Contains 'Bob': True
Contains 'Eve': False
Index of 'Carol': 2

Removing 'Bob':
✓ Removed Bob

Current roster (3 students):
1. Alice
2. Carol
3. David

Inserting 'Emma' at position 1:
✓ Inserted Emma at index 1

Current roster (4 students):
1. Alice
2. Emma
3. Carol
4. David

Students starting with 'A':
- Alice

Sorted roster:
1. Alice
2. Carol
3. David
4. Emma

Reversed roster:
1. Emma
2. David
3. Carol
4. Alice

Clearing roster...
✓ Roster cleared
Is empty: True
```

## Hints

**StudentRoster class structure:**
```csharp
class StudentRoster
{
    private List<string> students;
    
    public int Count => students.Count;
    
    public StudentRoster()
    {
        students = new List<string>();
    }
    
    public void AddStudent(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be empty");
            return;
        }
        
        if (students.Contains(name))
        {
            Console.WriteLine($"✗ {name} already exists");
            return;
        }
        
        students.Add(name);
        Console.WriteLine($"✓ Added: {name}");
    }
}
```

**Searching with Contains and IndexOf:**
```csharp
public bool Contains(string name)
{
    return students.Contains(name);
}

public int IndexOf(string name)
{
    return students.IndexOf(name);  // Returns -1 if not found
}
```

**Getting student by index safely:**
```csharp
public string GetStudent(int index)
{
    if (index < 0 || index >= students.Count)
    {
        Console.WriteLine("Invalid index");
        return null;
    }
    return students[index];
}
```

**Inserting at position:**
```csharp
public void InsertStudent(int index, string name)
{
    if (index < 0 || index > students.Count)
    {
        Console.WriteLine("Invalid insertion index");
        return;
    }
    
    if (students.Contains(name))
    {
        Console.WriteLine($"✗ {name} already exists");
        return;
    }
    
    students.Insert(index, name);
    Console.WriteLine($"✓ Inserted {name} at index {index}");
}
```

**Filtering by first letter:**
```csharp
public List<string> GetStudentsByLetter(char letter)
{
    List<string> result = new List<string>();
    
    foreach (string name in students)
    {
        if (name.StartsWith(letter.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            result.Add(name);
        }
    }
    
    return result;
}
```

**Sorting (creates new sorted list):**
```csharp
public List<string> SortStudents()
{
    List<string> sorted = new List<string>(students);  // Copy
    sorted.Sort();
    return sorted;
}
```

**Displaying students:**
```csharp
public void DisplayStudents()
{
    if (students.Count == 0)
    {
        Console.WriteLine("Roster is empty");
        return;
    }
    
    Console.WriteLine($"\nCurrent roster ({students.Count} students):");
    for (int i = 0; i < students.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {students[i]}");
    }
}
```

## Common Mistakes to Avoid

- ❌ Not checking for null/empty input
- ❌ Modifying list while iterating with foreach
- ❌ Not handling index out of range
- ❌ Forgetting that IndexOf returns -1 when not found
- ❌ Allowing duplicate entries

## Bonus Challenge

Extend the roster with these features:

1. **Student grades:** Use `Dictionary<string, int>` to store grades
   - `AddGrade(string name, int grade)`
   - `GetGrade(string name)`
   - `GetAverageGrade()`

2. **Attendance tracking:** Add `List<bool>` for each student
   - `MarkPresent(string name)`
   - `MarkAbsent(string name)`
   - `GetAttendanceRate(string name)`

3. **Export roster:** Save to text file
   - `SaveToFile(string filename)`
   - `LoadFromFile(string filename)`

Example bonus output:
```
=== Enhanced Roster ===

Adding grades:
✓ Alice: 95
✓ Bob: 87
✓ Carol: 92

Class average: 91.3

Attendance:
Alice: 90% (9/10 days)
Bob: 80% (8/10 days)
Carol: 100% (10/10 days)

Roster saved to 'students.txt'
```

## What You're Learning

- Essential List<T> operations
- When to use Add vs Insert
- Searching with Contains and IndexOf
- Safe index access
- Creating new lists from existing ones
- Preventing common collection errors

---

**Time Estimate:** 40 minutes  
**Difficulty:** Medium  
**Type:** Collection Management

**Next:** After completing this exercise, move to [Exercise 08-03: Dictionary Operations](../03-dictionary-operations/) to master key-value storage.