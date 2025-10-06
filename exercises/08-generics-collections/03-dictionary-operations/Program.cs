using System;
using System.Collections.Generic;

class PhoneBook
{
    // TODO: Add private Dictionary<string, string> field for contacts
    
    
    // TODO: Add Count property that returns contacts.Count
    
    
    // TODO: Add constructor that initializes the dictionary
    
    
    // TODO: Part 1 - Implement AddContact method
    // Hint: Check for null/empty, check if key exists, then add
    
    
    // TODO: Part 1 - Implement RemoveContact method
    // Hint: Use contacts.Remove(name)
    
    
    // TODO: Part 1 - Implement DisplayContacts method
    // Hint: Iterate with foreach over contacts
    
    
    // TODO: Part 2 - Implement GetPhoneNumber method
    // Hint: Use TryGetValue for safe lookup
    
    
    // TODO: Part 2 - Implement ContainsContact method
    
    
    // TODO: Part 2 - Implement UpdateContact method
    // Hint: Check if key exists first
    
    
    // TODO: Part 2 - Implement TryGetPhoneNumber method
    // Hint: Return contacts.TryGetValue(name, out phone)
    
    
    // TODO: Part 3 - Implement GetAllNames method
    // Hint: Return new List<string>(contacts.Keys)
    
    
    // TODO: Part 3 - Implement GetAllPhoneNumbers method
    // Hint: Return new List<string>(contacts.Values)
    
    
    // TODO: Part 3 - Implement FindContactsByAreaCode method
    // Hint: Create new dictionary, loop and check if phone StartsWith area code
    
    
    // TODO: Part 3 - Implement Clear method
    
    
    // TODO: Part 3 - Implement IsEmpty method
    
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Phone Book Manager ===\n");
        
        PhoneBook phoneBook = new PhoneBook();
        
        // TODO: Test adding contacts
        Console.WriteLine("Adding contacts:");
        
        
        // TODO: Display phone book
        
        
        // TODO: Test looking up contacts
        Console.WriteLine("\nLooking up contacts:");
        
        
        // TODO: Test ContainsContact
        Console.WriteLine("\nChecking if contacts exist:");
        
        
        // TODO: Test updating a contact
        
        
        // TODO: Test TryGetPhoneNumber
        Console.WriteLine("\nUsing TryGetPhoneNumber:");
        
        
        // TODO: Test FindContactsByAreaCode
        
        
        // TODO: Test removing a contact
        
        
        // TODO: Display final phone book
        
    }
}