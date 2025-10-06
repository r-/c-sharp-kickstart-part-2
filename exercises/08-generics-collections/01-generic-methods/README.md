# Exercise 08-01: Generic Methods

## Goal

Learn to create generic methods that work with any type while maintaining type safety. You'll build a utility library of reusable generic methods that demonstrate the power and flexibility of generics.

## Background

Generic methods allow you to write a single method that works with any type. Instead of creating separate methods for `int`, `string`, `Player`, etc., you write one method with a type parameter `<T>`. The compiler ensures type safety while giving you maximum code reuse.

## Instructions

You will create a `GenericUtility` class with several useful generic methods.

### Part 1: Basic Generic Methods

Create these generic methods in the `GenericUtility` class:

1. **`Swap<T>(ref T a, ref T b)`**
   - Swaps two values of any type

2. **`GetFirst<T>(List<T> items)`**
   - Returns first item in list
   - Throws exception if list is empty

3. **`GetLast<T>(List<T> items)`**
   - Returns last item in list
   - Throws exception if list is empty

### Part 2: Generic Methods with Constraints

4. **`Max<T>(T a, T b) where T : IComparable<T>`**
   - Returns the larger of two values
   - Uses `CompareTo` method

5. **`Min<T>(T a, T b) where T : IComparable<T>`**
   - Returns the smaller of two values

### Part 3: Advanced Generic Methods

6. **`PrintAll<T>(List<T> items)`**
   - Prints each item on a new line
   - Shows count before printing

7. **`CreatePair<T1, T2>(T1 first, T2 second)`**
   - Returns a tuple `(T1, T2)` with both values

### Part 4: Testing

In `Main()`, test all methods with different types:
- Test `Swap` with `int`, `string`, and `double`
- Test `GetFirst`/`GetLast` with `List<int>` and `List<string>`
- Test `Max`/`Min` with `int`, `double`, and `string`
- Test `PrintAll` with different list types
- Test `CreatePair` with various type combinations

## Requirements

Your solution must:
1. Create `GenericUtility` class with all seven methods
2. All methods must be `static`
3. Use proper generic syntax with `<T>`
4. Include `where T : IComparable<T>` constraint where needed
5. Test each method with at least 2 different types
6. Handle empty list cases appropriately

## Expected Output

```
=== Testing Generic Methods ===

--- Swap Test ---
Before swap: x = 5, y = 10
After swap: x = 10, y = 5

Before swap: s1 = hello, s2 = world
After swap: s1 = world, s2 = hello

--- GetFirst/GetLast Test ---
Numbers: 1, 2, 3, 4, 5
First: 1
Last: 5

Names: Alice, Bob, Carol
First: Alice
Last: Carol

--- Max/Min Test ---
Max(15, 7) = 15
Min(15, 7) = 7

Max(3.14, 2.71) = 3.14
Min(3.14, 2.71) = 2.71

Max("apple", "zebra") = zebra
Min("apple", "zebra") = apple

--- PrintAll Test ---
Printing 3 integers:
42
17
99

Printing 2 strings:
Hello
World

--- CreatePair Test ---
Pair: (Alice, 25)
Pair: (100, Active)
```

## Hints

**Swap method:**
```csharp
public static void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}
```

**GetFirst with error handling:**
```csharp
public static T GetFirst<T>(List<T> items)
{
    if (items.Count == 0)
        throw new InvalidOperationException("List is empty");
    return items[0];
}
```

**Max with constraint:**
```csharp
public static T Max<T>(T a, T b) where T : IComparable<T>
{
    // CompareTo returns:
    //   > 0 if a > b
    //   = 0 if a == b
    //   < 0 if a < b
    return a.CompareTo(b) > 0 ? a : b;
}
```

**PrintAll method:**
```csharp
public static void PrintAll<T>(List<T> items)
{
    Console.WriteLine($"Printing {items.Count} items:");
    foreach (T item in items)
    {
        Console.WriteLine(item);
    }
}
```

**CreatePair using tuples:**
```csharp
public static (T1, T2) CreatePair<T1, T2>(T1 first, T2 second)
{
    return (first, second);
}

// Usage:
var pair = GenericUtility.CreatePair("Alice", 25);
Console.WriteLine($"Pair: {pair}");
```

## Common Mistakes to Avoid

- ❌ Forgetting `ref` keyword in Swap parameters
- ❌ Not adding `where T : IComparable<T>` for Max/Min
- ❌ Trying to use comparison operators (<, >) on generic type T
- ❌ Not handling empty list cases

## Bonus Challenge

Add these advanced generic methods:

1. **`Contains<T>(List<T> items, T value)`**
   - Returns true if list contains value
   - Must work with any type

2. **`Reverse<T>(List<T> items)`**
   - Returns new list with items in reverse order

3. **`FindAll<T>(List<T> items, Func<T, bool> predicate)`**
   - Returns list of items matching condition
   - Example: `FindAll(numbers, n => n > 10)`

4. **`Transform<TIn, TOut>(List<TIn> items, Func<TIn, TOut> converter)`**
   - Converts list of one type to another
   - Example: Convert `List<int>` to `List<string>`

Example bonus output:
```
--- Bonus Methods ---
Contains 42: True
Contains 99: False

Reversed: 5, 4, 3, 2, 1

Numbers > 10: 15, 20, 42

Transformed to strings: "1", "2", "3"
```

## What You're Learning

- How to create methods that work with any type
- The power of generic type parameters
- When and how to use generic constraints
- Type inference - compiler determines `<T>` automatically
- Building reusable utility libraries

---

**Time Estimate:** 45 minutes  
**Difficulty:** Medium  
**Type:** Generic Programming Fundamentals

**Next:** After completing this exercise, move to [Exercise 08-02: List Operations](../02-list-operations/) to master working with `List<T>`.