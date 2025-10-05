using System;

class Person
{
    // Private fields
    private string firstName;
    private string lastName;
    
    // Read-only properties (no setter)
    public int Id { get; }
    public DateTime BirthDate { get; }
    public DateTime CreatedAt { get; }
    
    // Init-only property
    public string SocialSecurity { get; init; }
    
    // Changeable properties with validation
    public string FirstName
    {
        get { return firstName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("First name is required");
            firstName = value;
        }
    }
    
    public string LastName
    {
        get { return lastName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Last name is required");
            lastName = value;
        }
    }
    
    // Computed properties
    public int Age
    {
        get
        {
            var today = DateTime.Today;
            int age = today.Year - BirthDate.Year;
            if (BirthDate.Date > today.AddYears(-age))
                age--;
            return age;
        }
    }
    
    public bool IsAdult => Age >= 18;
    
    public string FullName => $"{FirstName} {LastName}";
    
    public int YearsRegistered
    {
        get
        {
            var span = DateTime.Now - CreatedAt;
            return (int)(span.TotalDays / 365.25);
        }
    }
    
    public DateTime NextBirthday
    {
        get
        {
            var today = DateTime.Today;
            var next = new DateTime(today.Year, BirthDate.Month, BirthDate.Day);
            if (next < today)
                next = next.AddYears(1);
            return next;
        }
    }
    
    public int DaysUntilBirthday
    {
        get
        {
            return (NextBirthday - DateTime.Today).Days;
        }
    }
    
    // Constructor with validation
    public Person(int id, string firstName, string lastName, DateTime birthDate, string ssn)
    {
        if (id <= 0)
            throw new ArgumentException("ID must be positive");
        
        if (birthDate > DateTime.Now)
            throw new ArgumentException("Birth date cannot be in future");
        
        Id = id;
        BirthDate = birthDate;
        FirstName = firstName;
        LastName = lastName;
        SocialSecurity = ssn;
        CreatedAt = DateTime.Now;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Name: {FullName}");
        Console.WriteLine($"Social Security: {SocialSecurity}");
        Console.WriteLine($"Birth Date: {BirthDate:yyyy-MM-dd}");
        Console.WriteLine($"Age: {Age} years");
        Console.WriteLine($"Status: {(IsAdult ? "Adult" : "Minor")}");
        Console.WriteLine($"Created: {CreatedAt:yyyy-MM-dd}");
        Console.WriteLine($"Years Registered: {YearsRegistered}");
        Console.WriteLine($"Next Birthday: {NextBirthday:yyyy-MM-dd} (in {DaysUntilBirthday} days)");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Creating people...\n");
        
        // Create first person
        Person person1 = new Person(
            1001,
            "Alice",
            "Johnson",
            new DateTime(1995, 5, 15),
            "123-45-6789"
        );
        
        Console.WriteLine("Person 1:");
        person1.DisplayInfo();
        
        // Create second person
        Person person2 = new Person(
            1002,
            "Bob",
            "Smith",
            new DateTime(2008, 8, 20),
            "987-65-4321"
        );
        
        Console.WriteLine("\nPerson 2:");
        person2.DisplayInfo();
        
        Console.WriteLine("\nTesting immutability:");
        Console.WriteLine("// person1.Id = 2000;           // ❌ Compile error: no setter");
        Console.WriteLine("// person1.BirthDate = ...;     // ❌ Compile error: no setter");
        Console.WriteLine("// person1.CreatedAt = ...;     // ❌ Compile error: no setter");
        Console.WriteLine("// person1.SocialSecurity = ... // ❌ Compile error: init-only");
        
        Console.WriteLine("\nTesting computed properties:");
        person1.FirstName = "Alice";
        person1.LastName = "Williams";
        Console.WriteLine($"Alice renamed to {person1.FullName}");
        Console.WriteLine($"Full name automatically updated: {person1.FullName}");
        Console.WriteLine($"Age still computed correctly: {person1.Age} years");
        Console.WriteLine($"Adult status: {(person1.IsAdult ? "Yes" : "No")}");
        
        Console.WriteLine("\nTesting validation:");
        try
        {
            var bad = new Person(-1, "Test", "User", DateTime.Now, "000-00-0000");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        try
        {
            var bad = new Person(1, "Test", "User", DateTime.Now.AddYears(1), "000-00-0000");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        Console.WriteLine("\nFinal state:");
        person1.DisplayInfo();
    }
}