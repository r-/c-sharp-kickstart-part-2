# Exercise 08-04: Generic Classes

## Goal

Learn to create your own generic classes by building a flexible data container library. You'll understand how `List<T>` and `Dictionary<K,V>` work internally by creating similar structures yourself.

## Background

Generic classes allow you to write reusable code that works with any type. You've been using generic classes like `List<T>` and `Dictionary<K,V>`—now you'll create your own. This exercise teaches the principles behind C#'s collection framework.

## Instructions

You will create three generic container classes that demonstrate different uses of generics.

### Part 1: Generic Stack<T>

Create a `Stack<T>` class (Last-In, First-Out):
- Use `List<T>` internally for storage
- `Push(T item)` - adds item to top
- `Pop()` - removes and returns top item
- `Peek()` - returns top item without removing
- `Count` property
- `IsEmpty()` - returns true if empty
- `Clear()` - removes all items

### Part 2: Generic Pair<T1, T2>

Create a `Pair<T1, T2>` class:
- Properties: `First` (T1) and `Second` (T2)
- Constructor that takes both values
- `Swap()` - exchanges First and Second
- `ToString()` override for display
- `Equals()` - compare two pairs

### Part 3: Generic Box<T> with Constraints

Create a `Box<T>` class where T must be IComparable:
- Property: `Item` of type T
- Constructor that takes initial item
- `IsGreaterThan(Box<T> other)` - compares items
- `IsLessThan(Box<T> other)` - compares items
- `Max(Box<T> other)` - returns box with larger item
- Use constraint: `where T : IComparable<T>`

### Part 4: Testing

In `Main()`, test all classes:
- Stack with `int` and `string`
- Pair with different type combinations
- Box with comparable types (int, double, string)
- Demonstrate all methods work correctly

## Requirements

Your solution must:
1. Create all three generic classes
2. Use proper generic syntax `<T>`, `<T1, T2>`
3. Use constraints where appropriate
4. Validate operations (no Pop from empty stack)
5. Test with multiple different types
6. Handle edge cases gracefully

## Expected Output

```
=== Testing Generic Classes ===

--- Stack<int> Test ---
Pushing: 10, 20, 30, 40, 50
Peek: 50
Popping: 50
Popping: 40
Remaining count: 3

--- Stack<string> Test ---
Pushing: Apple, Banana, Cherry
Popping: Cherry
Popping: Banana
Peek: Apple
Remaining count: 1

--- Pair<string, int> Test ---
Pair 1: (Alice, 25)
Pair 2: (Bob, 30)
After swap: (25, Alice)
Pairs equal: False

--- Pair<int, double> Test ---
Pair: (42, 3.14)
After swap: (3.14, 42)

--- Box<int> Test ---
Box A contains: 10
Box B contains: 20
A > B: False
A < B: True
Max box contains: 20

--- Box<string> Test ---
Box X contains: apple
Box Y contains: zebra
X > Y: False
X < Y: True
Max box contains: zebra

--- Box<double> Test ---
Box M contains: 3.14
Box N contains: 2.71
M > N: True
Max box contains: 3.14
```

## Hints

**Stack<T> class:**
```csharp
class Stack<T>
{
    private List<T> items;
    
    public int Count => items.Count;
    
    public Stack()
    {
        items = new List<T>();
    }
    
    public void Push(T item)
    {
        items.Add(item);
    }
    
    public T Pop()
    {
        if (items.Count == 0)
            throw new InvalidOperationException("Stack is empty");
        
        int lastIndex = items.Count - 1;
        T item = items[lastIndex];
        items.RemoveAt(lastIndex);
        return item;
    }
    
    public T Peek()
    {
        if (items.Count == 0)
            throw new InvalidOperationException("Stack is empty");
        
        return items[items.Count - 1];
    }
    
    public bool IsEmpty()
    {
        return items.Count == 0;
    }
    
    public void Clear()
    {
        items.Clear();
    }
}
```

**Pair<T1, T2> class:**
```csharp
class Pair<T1, T2>
{
    public T1 First { get; set; }
    public T2 Second { get; set; }
    
    public Pair(T1 first, T2 second)
    {
        First = first;
        Second = second;
    }
    
    public void Swap()
    {
        // Can't actually swap different types!
        // This demonstrates type system limitations
        Console.WriteLine("Cannot swap different types");
    }
    
    public override string ToString()
    {
        return $"({First}, {Second})";
    }
    
    public override bool Equals(object obj)
    {
        if (obj is Pair<T1, T2> other)
        {
            return First.Equals(other.First) && Second.Equals(other.Second);
        }
        return false;
    }
}
```

**Note on Pair.Swap():** For a real swap that works, you'd need `Pair<T, T>` (same type for both):
```csharp
class Pair<T>
{
    public T First { get; set; }
    public T Second { get; set; }
    
    public void Swap()
    {
        T temp = First;
        First = Second;
        Second = temp;
    }
}
```

**Box<T> with constraint:**
```csharp
class Box<T> where T : IComparable<T>
{
    public T Item { get; set; }
    
    public Box(T item)
    {
        Item = item;
    }
    
    public bool IsGreaterThan(Box<T> other)
    {
        return Item.CompareTo(other.Item) > 0;
    }
    
    public bool IsLessThan(Box<T> other)
    {
        return Item.CompareTo(other.Item) < 0;
    }
    
    public Box<T> Max(Box<T> other)
    {
        return IsGreaterThan(other) ? this : other;
    }
    
    public override string ToString()
    {
        return $"Box contains: {Item}";
    }
}
```

**Testing Stack:**
```csharp
Stack<int> numbers = new Stack<int>();
numbers.Push(10);
numbers.Push(20);
numbers.Push(30);

Console.WriteLine($"Peek: {numbers.Peek()}");
Console.WriteLine($"Popping: {numbers.Pop()}");
Console.WriteLine($"Count: {numbers.Count}");
```

**Testing Box with constraint:**
```csharp
Box<int> boxA = new Box<int>(10);
Box<int> boxB = new Box<int>(20);

Console.WriteLine($"A > B: {boxA.IsGreaterThan(boxB)}");
Console.WriteLine($"Max: {boxA.Max(boxB)}");
```

## Common Mistakes to Avoid

- ❌ Forgetting to check if stack is empty before Pop/Peek
- ❌ Not using constraints when comparing items
- ❌ Trying to use operators (<, >) on generic type T without constraint
- ❌ Mixing up type parameters (using wrong T1/T2)
- ❌ Not overriding ToString() for readable output

## Bonus Challenge

Create these advanced generic classes:

1. **Queue<T>** (First-In, First-Out)
   - `Enqueue(T item)` - adds to back
   - `Dequeue()` - removes from front
   - `Peek()` - see front item
   - Implement using List<T>

2. **CircularBuffer<T>** (Fixed-size rotating buffer)
   - Constructor takes max size
   - `Add(T item)` - overwrites oldest when full
   - `GetAll()` - returns items in order
   - Useful for log buffers, undo history

3. **PriorityQueue<T>** where T : IComparable
   - Items automatically sorted by priority
   - `Enqueue(T item)` - inserts in priority order
   - `Dequeue()` - removes highest priority
   - Demonstrate with different types

4. **Pair<T>** (single type version)
   - Both First and Second are same type T
   - `Swap()` actually works
   - Compare to Pair<T1, T2>

Example bonus output:
```
--- Queue<string> ---
Enqueuing: First, Second, Third
Dequeuing: First
Front: Second

--- CircularBuffer<int> (size 3) ---
Adding: 1, 2, 3, 4, 5
Buffer: [3, 4, 5] (oldest items removed)

--- PriorityQueue<int> ---
Adding: 5, 1, 3, 2, 4
Dequeuing in priority order: 5, 4, 3, 2, 1
```

## What You're Learning

- How to create generic classes
- When and how to use generic constraints
- The difference between `<T>` and `<T1, T2>`
- Why List<T> and Dictionary<K,V> work the way they do
- Building reusable, type-safe containers
- Understanding generic type parameters

---

**Time Estimate:** 50 minutes  
**Difficulty:** Medium-Hard  
**Type:** Generic Class Design

**Next:** After completing this exercise, you're ready for the [Mini Project: Task Manager](../../projects/08-generics-collections/) to build a complete collection-based application!