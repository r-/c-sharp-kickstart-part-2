using System;
using System.Collections.Generic;
using System.Linq;

class PhoneBook
{
    private Dictionary<string, string> contacts;
    
    public int Count => contacts.Count;
    
    public PhoneBook()
    {
        contacts = new Dictionary<string, string>();
    }
    
    // Part 1: Basic Dictionary Operations
    
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
    
    public void RemoveContact(string name)
    {
        if (contacts.Remove(name))
        {
            Console.WriteLine($"✓ Removed {name}");
        }
        else
        {
            Console.WriteLine($"✗ {name} not found");
        }
    }
    
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
    
    // Part 2: Lookup and Search Operations
    
    public string GetPhoneNumber(string name)
    {
        if (contacts.TryGetValue(name, out string phone))
        {
            return phone;
        }
        return null;
    }
    
    public bool ContainsContact(string name)
    {
        return contacts.ContainsKey(name);
    }
    
    public void UpdateContact(string name, string newPhone)
    {
        if (!contacts.ContainsKey(name))
        {
            Console.WriteLine($"✗ {name} not found");
            return;
        }
        
        if (string.IsNullOrWhiteSpace(newPhone))
        {
            Console.WriteLine("Phone cannot be empty");
            return;
        }
        
        contacts[name] = newPhone;
        Console.WriteLine($"✓ Updated {name} to {newPhone}");
    }
    
    public bool TryGetPhoneNumber(string name, out string phone)
    {
        return contacts.TryGetValue(name, out phone);
    }
    
    // Part 3: Advanced Dictionary Operations
    
    public List<string> GetAllNames()
    {
        return new List<string>(contacts.Keys);
    }
    
    public List<string> GetAllPhoneNumbers()
    {
        return new List<string>(contacts.Values);
    }
    
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
    
    public void Clear()
    {
        contacts.Clear();
        Console.WriteLine("✓ Phone book cleared");
    }
    
    public bool IsEmpty()
    {
        return contacts.Count == 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Phone Book Manager ===\n");
        
        PhoneBook phoneBook = new PhoneBook();
        
        // Test adding contacts
        Console.WriteLine("Adding contacts:");
        phoneBook.AddContact("Alice", "555-0101");
        phoneBook.AddContact("Bob", "555-0202");
        phoneBook.AddContact("Carol", "555-0303");
        phoneBook.AddContact("David", "555-0404");
        phoneBook.AddContact("Alice", "555-9999");  // Duplicate
        
        // Display phone book
        phoneBook.DisplayContacts();
        
        // Test looking up contacts
        Console.WriteLine("\nLooking up contacts:");
        string aliceNumber = phoneBook.GetPhoneNumber("Alice");
        string eveNumber = phoneBook.GetPhoneNumber("Eve");
        Console.WriteLine($"Alice's number: {aliceNumber ?? "Not found"}");
        Console.WriteLine($"Eve's number: {eveNumber ?? "Not found"}");
        
        // Test ContainsContact
        Console.WriteLine("\nChecking if contacts exist:");
        Console.WriteLine($"Contains 'Bob': {phoneBook.ContainsContact("Bob")}");
        Console.WriteLine($"Contains 'Frank': {phoneBook.ContainsContact("Frank")}");
        
        // Test updating a contact
        Console.WriteLine("\nUpdating Bob's number:");
        phoneBook.UpdateContact("Bob", "555-9999");
        
        // Test TryGetPhoneNumber
        Console.WriteLine("\nUsing TryGetPhoneNumber:");
        if (phoneBook.TryGetPhoneNumber("Carol", out string carolPhone))
        {
            Console.WriteLine($"✓ Found Carol: {carolPhone}");
        }
        
        if (phoneBook.TryGetPhoneNumber("Eve", out string evePhone2))
        {
            Console.WriteLine($"✓ Found Eve: {evePhone2}");
        }
        else
        {
            Console.WriteLine("✗ Could not find Eve");
        }
        
        // Test FindContactsByAreaCode
        Console.WriteLine("\nFinding contacts with area code '555':");
        Dictionary<string, string> areaContacts = phoneBook.FindContactsByAreaCode("555");
        Console.WriteLine($"Found {areaContacts.Count} contacts:");
        foreach (var pair in areaContacts)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
        
        // Test removing a contact
        Console.WriteLine("\nRemoving Bob:");
        phoneBook.RemoveContact("Bob");
        
        // Display final phone book
        phoneBook.DisplayContacts();
    }
}