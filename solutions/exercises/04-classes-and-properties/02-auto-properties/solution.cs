using System;

class Person
{
    // Auto-properties
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    // Properties with private setters
    public int Id { get; private set; }
    public DateTime CreatedDate { get; init; }
    
    // Custom property with validation
    private int age;
    public int Age
    {
        get { return age; }
        set
        {
            if (value < 0 || value > 150)
            {
                Console.WriteLine("Error: Age must be between 0 and 150");
                return;
            }
            age = value;
        }
    }
    
    // Computed property using expression body
    public string FullName => $"{FirstName} {LastName}";
    
    // Constructor
    public Person(string firstName, string lastName, int id)
    {
        FirstName = firstName;
        LastName = lastName;
        Id = id;
        CreatedDate = DateTime.Now;
    }
    
    // Display person information
    public void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Name: {FullName}");
        Console.WriteLine($"Email: {Email}");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"Created: {CreatedDate:yyyy-MM-dd}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Creating people...\n");
        
        // Create first person
        Person person1 = new Person("Alice", "Johnson", 1001);
        person1.Email = "alice@example.com";
        person1.Age = 28;
        
        Console.WriteLine("Person 1:");
        person1.DisplayInfo();
        
        // Create second person
        Person person2 = new Person("Bob", "Smith", 1002);
        person2.Email = "bob@example.com";
        person2.Age = 35;
        
        Console.WriteLine("\nPerson 2:");
        person2.DisplayInfo();
        
        // Test validation
        Console.WriteLine("\nTesting validation...");
        person1.Age = 200;  // Should show error
        
        Console.WriteLine($"\nAlice is still {person1.Age} years old");
    }
}