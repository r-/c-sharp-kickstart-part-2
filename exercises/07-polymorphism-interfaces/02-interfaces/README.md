# Exercise 07-02: Interfaces

## Goal

Master interface definition and implementation. You'll create a storage system that works with different types of data stores through a common interface contract.

## Background

Interfaces define pure contracts—what a class must do without dictating how. This exercise shows how interfaces enable flexibility: you can swap implementations without changing code that uses them.

## Instructions

You will create a data storage system using interfaces.

### Part 1: Define the Interface

Create an interface `IDataStore` with these members:
- Property: `string StorageName { get; }`
- Property: `int ItemCount { get; }`
- Method: `void Save(string key, string value)`
- Method: `string Load(string key)`
- Method: `bool Delete(string key)`
- Method: `void Clear()`

### Part 2: Implement Three Storage Types

Implement the interface in three different ways:

1. **MemoryStore**
   - Use `Dictionary<string, string>` for in-memory storage
   - `StorageName`: return "Memory Storage"
   - Implement all interface methods using the dictionary

2. **FileStore**
   - Simulate file storage with a dictionary (pretend it's writing to files)
   - `StorageName`: return "File Storage"
   - Add Console output showing "file operations" (e.g., "Writing to file...")
   - Implement all interface methods

3. **DatabaseStore**
   - Simulate database storage with a dictionary
   - `StorageName`: return "Database Storage"
   - Add Console output showing "database operations" (e.g., "Executing INSERT...")
   - Implement all interface methods

### Part 3: Demonstrate Polymorphism

Create a method `void TestStorage(IDataStore storage)` that:
1. Displays the storage name
2. Saves three key-value pairs
3. Loads and displays one value
4. Shows the item count
5. Deletes one item
6. Shows the updated count

Call this method with each storage type to prove they all work through the same interface.

## Requirements

Your solution must:
1. Define `IDataStore` interface with all specified members
2. Implement the interface in three different classes
3. Use `Dictionary<string, string>` for storage in all implementations
4. Create polymorphic `TestStorage()` method
5. Demonstrate that all three implementations work identically through the interface
6. Handle cases where keys don't exist

## Expected Output

```
Testing Storage Systems
=======================

Testing: Memory Storage
Saved: user1 = Alice
Saved: user2 = Bob  
Saved: user3 = Carol
Loaded user1: Alice
Item count: 3
Deleted user2
Item count after delete: 2

Testing: File Storage
Writing to file: user1 = Alice
Writing to file: user2 = Bob
Writing to file: user3 = Carol
Reading from file: user1
Loaded user1: Alice
Item count: 3
Deleting file: user2
Item count after delete: 2

Testing: Database Storage
Executing INSERT: user1 = Alice
Executing INSERT: user2 = Bob
Executing INSERT: user3 = Carol
Executing SELECT: user1
Loaded user1: Alice
Item count: 3
Executing DELETE: user2
Item count after delete: 2
```

## Hints

**Interface definition:**
```csharp
interface IDataStore
{
    string StorageName { get; }
    int ItemCount { get; }
    
    void Save(string key, string value);
    string Load(string key);
    bool Delete(string key);
    void Clear();
}
```

**Memory implementation:**
```csharp
class MemoryStore : IDataStore
{
    private Dictionary<string, string> data = new Dictionary<string, string>();
    
    public string StorageName => "Memory Storage";
    public int ItemCount => data.Count;
    
    public void Save(string key, string value)
    {
        data[key] = value;
        Console.WriteLine($"Saved: {key} = {value}");
    }
    
    public string Load(string key)
    {
        if (data.ContainsKey(key))
            return data[key];
        return null;
    }
    
    // Implement Delete and Clear...
}
```

**Polymorphic testing:**
```csharp
void TestStorage(IDataStore storage)
{
    Console.WriteLine($"\nTesting: {storage.StorageName}");
    
    storage.Save("user1", "Alice");
    storage.Save("user2", "Bob");
    storage.Save("user3", "Carol");
    
    string value = storage.Load("user1");
    Console.WriteLine($"Loaded user1: {value}");
    
    Console.WriteLine($"Item count: {storage.ItemCount}");
    
    storage.Delete("user2");
    Console.WriteLine($"Item count after delete: {storage.ItemCount}");
}
```

**Using the interface:**
```csharp
IDataStore memory = new MemoryStore();
IDataStore file = new FileStore();
IDataStore database = new DatabaseStore();

TestStorage(memory);
TestStorage(file);
TestStorage(database);
```

## Common Mistakes to Avoid

- ❌ Forgetting to implement all interface members
- ❌ Not using interface type for parameters (use `IDataStore`, not concrete types)
- ❌ Making interface members private (all are implicitly public)
- ❌ Adding implementation to the interface itself

## Bonus Challenge

1. Add `List<string> GetAllKeys()` method to the interface
2. Implement it in all storage classes
3. Add validation: throw exception if key is null or empty
4. Create a `CachedDatabaseStore` that implements `IDataStore` and uses both memory and "database"
5. Add a `bool Exists(string key)` method

## What You're Learning

- How interfaces define pure contracts
- Implementing the same interface in different ways
- Polymorphism through interface references
- The power of "programming to interfaces, not implementations"
- How interfaces enable swappable implementations

---

**Next:** After completing this exercise, move to [Exercise 07-03: Multiple Interfaces](../03-multiple-interfaces/) to learn how one class can implement multiple contracts.