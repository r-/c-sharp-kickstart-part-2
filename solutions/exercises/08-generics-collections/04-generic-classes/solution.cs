using System;
using System.Collections.Generic;

// Part 1: Generic Stack<T>
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

// Part 2: Generic Pair<T1, T2>
class Pair<T1, T2>
{
    public T1 First { get; set; }
    public T2 Second { get; set; }
    
    public Pair(T1 first, T2 second)
    {
        First = first;
        Second = second;
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
    
    public override int GetHashCode()
    {
        return HashCode.Combine(First, Second);
    }
}

// Part 3: Generic Box<T> with Constraints
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

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Testing Generic Classes ===\n");
        
        // Test Stack<int>
        Console.WriteLine("--- Stack<int> Test ---");
        Stack<int> numbers = new Stack<int>();
        Console.Write("Pushing: ");
        int[] nums = { 10, 20, 30, 40, 50 };
        Console.WriteLine(string.Join(", ", nums));
        foreach (int num in nums)
        {
            numbers.Push(num);
        }
        
        Console.WriteLine($"Peek: {numbers.Peek()}");
        Console.WriteLine($"Popping: {numbers.Pop()}");
        Console.WriteLine($"Popping: {numbers.Pop()}");
        Console.WriteLine($"Remaining count: {numbers.Count}");
        
        // Test Stack<string>
        Console.WriteLine("\n--- Stack<string> Test ---");
        Stack<string> words = new Stack<string>();
        Console.Write("Pushing: ");
        string[] fruits = { "Apple", "Banana", "Cherry" };
        Console.WriteLine(string.Join(", ", fruits));
        foreach (string fruit in fruits)
        {
            words.Push(fruit);
        }
        
        Console.WriteLine($"Popping: {words.Pop()}");
        Console.WriteLine($"Popping: {words.Pop()}");
        Console.WriteLine($"Peek: {words.Peek()}");
        Console.WriteLine($"Remaining count: {words.Count}");
        
        // Test Pair<string, int>
        Console.WriteLine("\n--- Pair<string, int> Test ---");
        Pair<string, int> pair1 = new Pair<string, int>("Alice", 25);
        Pair<string, int> pair2 = new Pair<string, int>("Bob", 30);
        Console.WriteLine($"Pair 1: {pair1}");
        Console.WriteLine($"Pair 2: {pair2}");
        Console.WriteLine($"Pairs equal: {pair1.Equals(pair2)}");
        
        // Test Pair<int, double>
        Console.WriteLine("\n--- Pair<int, double> Test ---");
        Pair<int, double> pair3 = new Pair<int, double>(42, 3.14);
        Console.WriteLine($"Pair: {pair3}");
        
        // Test Box<int>
        Console.WriteLine("\n--- Box<int> Test ---");
        Box<int> boxA = new Box<int>(10);
        Box<int> boxB = new Box<int>(20);
        Console.WriteLine($"Box A contains: {boxA.Item}");
        Console.WriteLine($"Box B contains: {boxB.Item}");
        Console.WriteLine($"A > B: {boxA.IsGreaterThan(boxB)}");
        Console.WriteLine($"A < B: {boxA.IsLessThan(boxB)}");
        Console.WriteLine($"Max box contains: {boxA.Max(boxB).Item}");
        
        // Test Box<string>
        Console.WriteLine("\n--- Box<string> Test ---");
        Box<string> boxX = new Box<string>("apple");
        Box<string> boxY = new Box<string>("zebra");
        Console.WriteLine($"Box X contains: {boxX.Item}");
        Console.WriteLine($"Box Y contains: {boxY.Item}");
        Console.WriteLine($"X > Y: {boxX.IsGreaterThan(boxY)}");
        Console.WriteLine($"X < Y: {boxX.IsLessThan(boxY)}");
        Console.WriteLine($"Max box contains: {boxX.Max(boxY).Item}");
        
        // Test Box<double>
        Console.WriteLine("\n--- Box<double> Test ---");
        Box<double> boxM = new Box<double>(3.14);
        Box<double> boxN = new Box<double>(2.71);
        Console.WriteLine($"Box M contains: {boxM.Item}");
        Console.WriteLine($"Box N contains: {boxN.Item}");
        Console.WriteLine($"M > N: {boxM.IsGreaterThan(boxN)}");
        Console.WriteLine($"Max box contains: {boxM.Max(boxN).Item}");
    }
}