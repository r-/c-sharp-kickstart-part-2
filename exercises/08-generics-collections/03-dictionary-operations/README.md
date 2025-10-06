# Exercise 08-03: Dictionary Operations

## Goal

Master `Dictionary<K,V>` by building a phone book application. You'll learn to store key-value pairs, perform fast lookups, and handle missing keys gracefully.

## Background

`Dictionary<K,V>` is essential when you need to look up values by a unique key. While lists work well for ordered collections, dictionaries excel at scenarios like "find person by ID" or "get price by product name". Understanding dictionaries is crucial for efficient data management.

## Instructions

You will create a `PhoneBook` class that stores contacts using a dictionary.

### Part 1: Basic Dictionary Operations

Create a `PhoneBook` class with:
- Private `Dictionary<string, string>` field (name → phone number)
- Constructor that initializes empty dictionary
- `AddContact(string name, string phone)` - adds new contact
- `RemoveContact(string name)` - removes contact by name
- `Count` property - returns number of contacts
- `DisplayContacts()` - shows all contacts

### Part 2: Lookup and Search Operations

Add these methods:
- `GetPhoneNumber(string name)` - returns phone number or null if not found
- `ContainsContact(string name)` - checks if contact exists
- `UpdateContact(string name, string newPhone)` - updates existing contact
- `TryGetPhoneNumber(string name, out string phone)` - safe lookup method

### Part 3: Advanced Dictionary Operations

Add these methods:
- `GetAllNames()` - returns `List<string>` of all names
- `GetAllPhoneNumbers()` - returns `List<string>` of all phone numbers
- `FindContactsByAreaCode(string areaCode)` - returns dictionary of matching contacts
- `Clear()` - removes all contacts
- `IsEmpty()` - returns true if no contacts

### Part 4: Testing

In `Main()`, demonstrate:
- Adding multiple contacts
- Attempting to add duplicate name
- Looking up existing and non-existing contacts
- Updating a contact
- Using TryGetPhoneNumber
- Finding by area code
- Displaying all contacts
- Removing contacts

## Requirements

Your solution must:
1. Use `Dictionary<string, string>` for storage
2. Validate inputs (no null/empty values)
3. Prevent duplicate keys (or update existing)
4. Use `TryGetValue` for safe lookups
5. Handle missing keys gracefully
6. Display clear messages for all operations

## Expected Output

```
=== Phone Book Manager ===

Adding contacts:
✓ Added: Alice -> 555-0101
✓ Added: Bob -> 555-0202
✓ Added: Carol -> 555-0303
✓ Added: David -> 555-0404
✗ Alice already exists

Current phone book (4 contacts):
Alice: 555-0101
Bob: 555-0202
Carol: 555-0303
David: 555-0404

Looking up contacts:
Alice's number: 555-0101
Eve's number: Not found

Checking if contacts exist:
Contains 'Bob': True
Contains 'Frank': False

Updating Bob's number:
✓ Updated Bob to 555-9999

Using TryGetPhoneNumber:
✓ Found Carol: 555-0303
✗ Could not find Eve

Finding contacts with area code '555':
Found 4 contacts:
Alice: 555-0101
Bob: 555-9999
Carol: 555-0303
David: 555-0404

Removing Bob:
✓ Removed Bob

Final phone book (3 contacts):
Alice: 555-0101
Carol: 555-0303
David: 555-0404
```

## Hints

**PhoneBook class structure:**
```csharp
class PhoneBook
{
    private Dictionary<string, string> contacts;
    
    public int Count => contacts.Count;
    
    public PhoneBook()
    {
        contacts = new Dictionary<string, string>();
    }
    
    public void AddContact(string name, string phone)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone))
        {
            Console.WriteLine("Name and phone cannot be empty");
            return;
        }
        
        if (contacts.ContainsKey(name))
        {
            Console.WriteLine($"✗ {name} already exists");
            return;
        }
        
        contacts[name] = phone;
        Console.WriteLine($"✓ Added: {name} -> {phone}");
    }
}
```

**Safe lookup with TryGetValue:**
```csharp
public string GetPhoneNumber(string name)
{
    if (contacts.TryGetValue(name, out string phone))
    {
        return phone;
    }
    return null;
}

public bool TryGetPhoneNumber(string name, out string phone)
{
    return contacts.TryGetValue(name, out phone);
}
```

**Updating contact:**
```csharp
public void UpdateContact(string name, string newPhone)
{
    if (!contacts.ContainsKey(name))
    {
        Console.WriteLine($"✗ {name} not found");
        return;
    }
    
    contacts[name] = newPhone;
    Console.WriteLine($"✓ Updated {name} to {newPhone}");
}
```

**Getting all names:**
```csharp
public List<string> GetAllNames()
{
    return new List<string>(contacts.Keys);
}
```

**Iterating over dictionary:**
```csharp
public void DisplayContacts()
{
    if (contacts.Count == 0)
    {
        Console.WriteLine("Phone book is empty");
        return;
    }
    
    Console.WriteLine($"\nCurrent phone book ({contacts.Count} contacts):");
    foreach (var pair in contacts)
    {
        Console.WriteLine($"{pair.Key}: {pair.Value}");
    }
}
```

**Finding by area code:**
```csharp
public Dictionary<string, string> FindContactsByAreaCode(string areaCode)
{
    Dictionary<string, string> results = new Dictionary<string, string>();
    
    foreach (var pair in contacts)
    {
        if (pair.Value.StartsWith(areaCode))
        {
            results[pair.Key] = pair.Value;
        }
    }
    
    return results;
}
```

## Common Mistakes to Avoid

- ❌ Accessing dictionary with `[]` without checking if key exists
- ❌ Not using `TryGetValue` for safe lookups
- ❌ Allowing duplicate keys without updating
- ❌ Not validating phone number format
- ❌ Modifying dictionary while iterating

## Bonus Challenge

Enhance the phone book with these features:

1. **Contact class:** Create a `Contact` class
   - Properties: `Name`, `Phone`, `Email`, `Address`
   - Use `Dictionary<string, Contact>`

2. **Multiple phone numbers:**
   - Use `Dictionary<string, List<string>>` for multiple numbers
   - Methods: `AddPhoneNumber`, `RemovePhoneNumber`

3. **Categories:**
   - Use `Dictionary<string, Dictionary<string, string>>` (category → contacts)
   - Categories: Family, Friends, Work, etc.

4. **Search features:**
   - `SearchByPartialName(string partial)` - finds names containing text
   - `SearchByNumber(string number)` - reverse lookup

5. **Export/Import:**
   - `SaveToFile(string filename)` - save contacts
   - `LoadFromFile(string filename)` - load contacts

Example bonus output:
```
=== Enhanced Phone Book ===

Contact: Alice
  Phone: 555-0101
  Email: alice@email.com
  Address: 123 Main St

Multiple numbers for Bob:
  Home: 555-0202
  Work: 555-0203
  Mobile: 555-0204

Work contacts (2):
  Alice: 555-0101
  David: 555-0404

Searching for 'ar':
  Carol: 555-0303

Reverse lookup '555-0101':
  Found: Alice

Saved 4 contacts to 'phonebook.txt'
```

## What You're Learning

- When to use Dictionary vs List
- Fast lookups by unique key
- Safe dictionary access patterns
- Working with KeyValuePairs
- Iterating over dictionaries
- Preventing common dictionary errors

---

**Time Estimate:** 45 minutes  
**Difficulty:** Medium  
**Type:** Key-Value Storage

**Next:** After completing this exercise, move to [Exercise 08-04: Generic Classes](../04-generic-classes/) to create your own generic types.