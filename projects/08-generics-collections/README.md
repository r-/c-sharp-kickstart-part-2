# Mini Project: Task Manager System

## Project Overview

This is YOUR task manager to design and build! You'll create a practical application that manages tasks across multiple categories using `List<T>` and `Dictionary<K,V>`. This project combines everything from Chapter 8 to build a real productivity tool.

## Why This Project?

Every professional uses task management tools. This project teaches you how collections power real applications. You'll use the same patterns that apps like Todoist, Microsoft To Do, and Trello use to manage data efficiently.

## Learning Goals

By completing this project, you will:

- Master `Dictionary<K,V>` for categorized data storage
- Use `List<T>` for ordered task collections
- Combine multiple collection types effectively
- Apply encapsulation with generic collections
- Implement full CRUD operations (Create, Read, Update, Delete)
- Design user-friendly console interfaces
- Handle edge cases and validation

## Minimum Requirements

YOUR task manager MUST include:

### 1. Task Class

Create a `Task` class with:
- **Properties:**
  - `Id` (int, read-only, auto-generated)
  - `Title` (string, required, 3-100 characters)
  - `Description` (string, optional)
  - `Priority` (enum: Low, Medium, High)
  - `IsCompleted` (bool, private setter)
  - `CreatedDate` (DateTime, read-only)

- **Methods:**
  - Constructor with validation
  - `MarkComplete()` - sets IsCompleted to true
  - `MarkIncomplete()` - sets IsCompleted to false
  - `ToString()` override for display

- **Priority Enum:**
  ```csharp
  enum Priority { Low, Medium, High }
  ```

### 2. TaskManager Class

Create a `TaskManager` class with:
- **Internal Storage:**
  - `Dictionary<string, List<Task>>` for category → tasks mapping
  - Static ID counter for unique task IDs

- **Core Methods:**
  - `AddTask(string category, Task task)` - adds task to category
  - `RemoveTask(string category, int taskId)` - removes task by ID
  - `GetTasksByCategory(string category)` - returns category's tasks
  - `GetAllTasks()` - returns all tasks from all categories
  - `MarkTaskComplete(string category, int taskId)` - marks task done
  - `FindTaskById(int taskId)` - searches all categories
  - `GetCategoryNames()` - returns list of all categories

- **Display Methods:**
  - `DisplayAllTasks()` - shows organized by category
  - `DisplayCategory(string category)` - shows specific category
  - `DisplayStatistics()` - shows counts and percentages

- **Query Methods:**
  - `GetTasksByPriority(Priority priority)` - filters by priority
  - `GetCompletedTasks()` - returns completed tasks
  - `GetPendingTasks()` - returns incomplete tasks
  - `SearchTasks(string keyword)` - finds tasks by keyword

### 3. User Interaction

In `Main()`, demonstrate:
- Creating task manager
- Adding tasks to multiple categories (Work, Personal, Shopping, etc.)
- Displaying all tasks organized by category
- Marking tasks complete
- Searching and filtering
- Showing statistics
- Removing tasks
- Clear, professional output formatting

### 4. Validation Requirements

- No null or empty task titles
- Category names cannot be empty
- No duplicate task IDs
- Handle non-existent categories gracefully
- Handle non-existent task IDs gracefully
- Validate priority enum values

## Expected Output

```
=== YOUR Task Manager ===

Welcome! Let's organize YOUR tasks.

Creating categories and tasks...
✓ Added task 1 to Work: Complete quarterly report
✓ Added task 2 to Work: Schedule team meeting
✓ Added task 3 to Personal: Gym workout
✓ Added task 4 to Personal: Call mom
✓ Added task 5 to Shopping: Buy groceries
✓ Added task 6 to Shopping: Pick up medication

====================
ALL TASKS
====================

📁 Work (2 tasks):
  [1] Complete quarterly report (High) - Pending
      Created: 2025-01-15 10:30
  [2] Schedule team meeting (Medium) - Pending
      Created: 2025-01-15 10:31

📁 Personal (2 tasks):
  [3] Gym workout (Low) - Pending
      Created: 2025-01-15 10:32
  [4] Call mom (High) - Pending
      Created: 2025-01-15 10:33

📁 Shopping (2 tasks):
  [5] Buy groceries (Medium) - Pending
      Created: 2025-01-15 10:34
  [6] Pick up medication (High) - Pending
      Created: 2025-01-15 10:35

====================

Marking task 1 complete...
✓ Task 'Complete quarterly report' marked complete

Marking task 3 complete...
✓ Task 'Gym workout' marked complete

====================
STATISTICS
====================
Total tasks: 6
Completed: 2 (33%)
Pending: 4 (67%)
Categories: 3

Tasks by category:
  Work: 2 tasks
  Personal: 2 tasks
  Shopping: 2 tasks

Tasks by priority:
  High: 3 tasks
  Medium: 2 tasks
  Low: 1 task

====================
HIGH PRIORITY TASKS
====================
[1] Complete quarterly report (Work) - Completed
[4] Call mom (Personal) - Pending
[6] Pick up medication (Shopping) - Pending

Searching for 'meeting'...
Found 1 task:
  [2] Schedule team meeting (Work) - Pending

Removing task 6...
✓ Removed task 'Pick up medication' from Shopping

Final task count: 5 tasks across 3 categories
```

## Implementation Steps

Follow these steps to build YOUR task manager:

### Step 1: Create Priority Enum
```csharp
enum Priority { Low, Medium, High }
```

### Step 2: Build Task Class
- Add all properties
- Implement constructor with validation
- Add mark complete/incomplete methods
- Override ToString()

### Step 3: Create TaskManager Foundation
- Add Dictionary field
- Implement constructor
- Create helper method to ensure category exists

### Step 4: Implement Core Operations
- AddTask
- RemoveTask
- GetTasksByCategory
- MarkTaskComplete

### Step 5: Add Search and Filter
- FindTaskById
- GetTasksByPriority
- SearchTasks

### Step 6: Implement Display Methods
- DisplayAllTasks
- DisplayStatistics
- Format output professionally

### Step 7: Build Main Menu
- Test all functionality
- Add error handling
- Polish user experience

## Hints

### Task Class Structure:
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
    public DateTime CreatedDate { get; }
    
    public Task(string title, string description, Priority priority)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required");
        
        if (title.Length < 3 || title.Length > 100)
            throw new ArgumentException("Title must be 3-100 characters");
        
        Id = nextId++;
        Title = title;
        Description = description ?? "";
        Priority = priority;
        IsCompleted = false;
        CreatedDate = DateTime.Now;
    }
    
    public void MarkComplete()
    {
        IsCompleted = true;
    }
    
    public void MarkIncomplete()
    {
        IsCompleted = false;
    }
    
    public override string ToString()
    {
        string status = IsCompleted ? "Completed" : "Pending";
        return $"[{Id}] {Title} ({Priority}) - {status}";
    }
}
```

### TaskManager Foundation:
```csharp
class TaskManager
{
    private Dictionary<string, List<Task>> tasksByCategory;
    
    public TaskManager()
    {
        tasksByCategory = new Dictionary<string, List<Task>>();
    }
    
    private void EnsureCategoryExists(string category)
    {
        if (!tasksByCategory.ContainsKey(category))
        {
            tasksByCategory[category] = new List<Task>();
        }
    }
    
    public void AddTask(string category, Task task)
    {
        if (string.IsNullOrWhiteSpace(category))
        {
            Console.WriteLine("Category cannot be empty");
            return;
        }
        
        EnsureCategoryExists(category);
        tasksByCategory[category].Add(task);
        Console.WriteLine($"✓ Added task {task.Id} to {category}: {task.Title}");
    }
}
```

### Display All Tasks:
```csharp
public void DisplayAllTasks()
{
    if (tasksByCategory.Count == 0)
    {
        Console.WriteLine("No tasks yet!");
        return;
    }
    
    Console.WriteLine("\n====================");
    Console.WriteLine("ALL TASKS");
    Console.WriteLine("====================\n");
    
    foreach (var category in tasksByCategory)
    {
        Console.WriteLine($"📁 {category.Key} ({category.Value.Count} tasks):");
        
        foreach (Task task in category.Value)
        {
            Console.WriteLine($"  {task}");
            Console.WriteLine($"      Created: {task.CreatedDate:yyyy-MM-dd HH:mm}");
            if (!string.IsNullOrEmpty(task.Description))
            {
                Console.WriteLine($"      {task.Description}");
            }
        }
        Console.WriteLine();
    }
}
```

### Statistics:
```csharp
public void DisplayStatistics()
{
    List<Task> allTasks = GetAllTasks();
    int total = allTasks.Count;
    int completed = allTasks.Count(t => t.IsCompleted);
    int pending = total - completed;
    
    Console.WriteLine("\n====================");
    Console.WriteLine("STATISTICS");
    Console.WriteLine("====================");
    Console.WriteLine($"Total tasks: {total}");
    Console.WriteLine($"Completed: {completed} ({(total > 0 ? completed * 100 / total : 0)}%)");
    Console.WriteLine($"Pending: {pending} ({(total > 0 ? pending * 100 / total : 0)}%)");
    Console.WriteLine($"Categories: {tasksByCategory.Count}");
    
    Console.WriteLine("\nTasks by category:");
    foreach (var category in tasksByCategory)
    {
        Console.WriteLine($"  {category.Key}: {category.Value.Count} tasks");
    }
}
```

## Make It YOUR Own!

Add YOUR creative features to make this task manager unique:

### Data Persistence (Recommended)
- **Save to file** - Export tasks to text/JSON file
- **Load from file** - Import tasks on startup
- **Auto-save** - Save after each change
- **Backup** - Create timestamped backups

### Enhanced Features
- **Due dates** - Add `DateTime DueDate` property
- **Reminders** - Alert for tasks due soon
- **Tags** - Add `List<string> Tags` per task
- **Subtasks** - Nested task lists
- **Notes** - Additional comments on tasks
- **Attachments** - File paths or links
- **Time tracking** - Estimated vs actual time
- **Recurring tasks** - Daily/weekly/monthly repeats

### Advanced Functionality
- **Sort options** - By priority, date, alphabetically
- **Filter combinations** - Category AND priority
- **Task history** - Archive completed tasks
- **Undo/Redo** - Stack-based command pattern
- **Bulk operations** - Complete all in category
- **Import/Export** - CSV or JSON format
- **Statistics graphs** - ASCII charts
- **Color coding** - Console colors by priority

### User Experience
- **Interactive menu** - Number-based menu system
- **Keyboard shortcuts** - Quick actions
- **Search autocomplete** - Suggest categories/tags
- **Confirmation prompts** - Before deletions
- **Help system** - Built-in documentation
- **Tutorial mode** - First-time user guide

### Professional Features
- **Multiple users** - Dictionary<string, TaskManager>
- **Collaboration** - Shared task lists
- **Priority algorithms** - Auto-sort by urgency
- **Analytics** - Completion rates over time
- **Goals** - Weekly/monthly task goals
- **Templates** - Reusable task templates
- **Categories hierarchy** - Nested categories
- **Custom fields** - User-defined properties

## Self-Assessment Checklist

Before considering YOUR task manager complete:

- [ ] Task class properly encapsulated with validation
- [ ] TaskManager uses Dictionary<string, List<Task>>
- [ ] Can add tasks to multiple categories
- [ ] Can mark tasks complete/incomplete
- [ ] Can remove tasks by ID
- [ ] Search functionality works
- [ ] Filter by priority works
- [ ] Statistics calculate correctly
- [ ] Display is organized and clear
- [ ] No duplicate IDs
- [ ] All inputs validated
- [ ] Handles missing categories gracefully
- [ ] Handles missing tasks gracefully
- [ ] Code is well-organized
- [ ] Methods follow single responsibility
- [ ] Clear error messages
- [ ] Professional output formatting

## What Makes a Great Project?

YOUR project will be evaluated on:

- **Collection Usage (40%)**: 
  - Proper use of Dictionary and List
  - Efficient data structures
  - Appropriate collection choices

- **Functionality (30%)**:
  - All core features work
  - Search and filter work correctly
  - Statistics accurate
  - Handles edge cases

- **Code Quality (20%)**:
  - Well-organized classes
  - Good method names
  - Encapsulation applied
  - Validation throughout

- **Creativity (10%)**:
  - YOUR unique features
  - Polished user experience
  - Thoughtful enhancements

## Common Pitfalls to Avoid

- ❌ Not validating category names before use
- ❌ Forgetting to check if task ID exists
- ❌ Allowing duplicate IDs across categories
- ❌ Not handling empty categories
- ❌ Poor output formatting
- ❌ Missing validation on Task properties
- ❌ Not using meaningful variable names
- ❌ Cramming too much in one method

## Stretch Goals

If you finish early and want a challenge:

1. **Build an interactive menu system**
   - Number-based menu
   - Loop until user quits
   - Input validation

2. **Implement task dependencies**
   - Tasks that must complete before others
   - Dependency graph visualization
   - Circular dependency detection

3. **Add productivity metrics**
   - Tasks completed per day
   - Average completion time
   - Productivity trends

4. **Create a project view**
   - Multi-category projects
   - Project-level statistics
   - Project templates

## Resources

Useful C# documentation:
- Dictionary<K,V> methods
- List<T> operations
- DateTime formatting
- String manipulation
- Console colors (ConsoleColor)

## Final Thoughts

This is YOUR task manager. Make design decisions that make sense to YOU. There's no single "correct" solution—focus on creating something useful, well-structured, and uniquely yours.

The skills you learn building this (collections, data management, user interaction) form the foundation of professional C# development. Every app manages data—now you know how!

---

**Time Estimate:** 2-3 hours  
**Difficulty:** Medium  
**Type:** Comprehensive Application

This project brings together everything from Chapter 8. Take your time, experiment, and build something you're proud of!