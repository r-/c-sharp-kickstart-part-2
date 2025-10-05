using System;
using Xunit;

namespace ClassesAndPropertiesTests;

// Exercise 01: Fields vs Properties - Product class
public class ProductTests
{
    [Fact]
    public void Product_ValidValues_StoresCorrectly()
    {
        var product = new Product();
        product.Name = "Laptop";
        product.Price = 999.99m;
        product.Stock = 15;

        Assert.Equal("Laptop", product.Name);
        Assert.Equal(999.99m, product.Price);
        Assert.Equal(15, product.Stock);
    }

    [Fact]
    public void Product_EmptyName_DoesNotUpdate()
    {
        var product = new Product();
        product.Name = "Valid Name";
        product.Name = "";

        Assert.Equal("Valid Name", product.Name);
    }

    [Fact]
    public void Product_NegativePrice_DoesNotUpdate()
    {
        var product = new Product();
        product.Price = 50m;
        product.Price = -10m;

        Assert.Equal(50m, product.Price);
    }

    [Fact]
    public void Product_NegativeStock_DoesNotUpdate()
    {
        var product = new Product();
        product.Stock = 10;
        product.Stock = -5;

        Assert.Equal(10, product.Stock);
    }
}

// Exercise 02: Auto-Properties - Person class
public class PersonTests
{
    [Fact]
    public void Person_Constructor_InitializesCorrectly()
    {
        var person = new Person("Alice", "Johnson", 1001);

        Assert.Equal("Alice", person.FirstName);
        Assert.Equal("Johnson", person.LastName);
        Assert.Equal(1001, person.Id);
    }

    [Fact]
    public void Person_FullName_CombinesFirstAndLast()
    {
        var person = new Person("Bob", "Smith", 1002);

        Assert.Equal("Bob Smith", person.FullName);
    }

    [Fact]
    public void Person_ValidAge_SetsCorrectly()
    {
        var person = new Person("Charlie", "Brown", 1003);
        person.Age = 28;

        Assert.Equal(28, person.Age);
    }

    [Fact]
    public void Person_InvalidAge_DoesNotUpdate()
    {
        var person = new Person("Diana", "Prince", 1004);
        person.Age = 30;
        person.Age = 200;

        Assert.Equal(30, person.Age);
    }

    [Fact]
    public void Person_Email_CanBeSet()
    {
        var person = new Person("Eve", "Adams", 1005);
        person.Email = "eve@example.com";

        Assert.Equal("eve@example.com", person.Email);
    }

    [Fact]
    public void Person_CreatedDate_IsSet()
    {
        var person = new Person("Frank", "Castle", 1006);

        Assert.NotEqual(default(DateTime), person.CreatedDate);
    }
}

// Exercise 03: Property Validation - Temperature class
public class TemperatureTests
{
    [Fact]
    public void Temperature_ValidValues_StoresCorrectly()
    {
        var temp = new Temperature("Stockholm", 22.0);

        Assert.Equal("Stockholm", temp.LocationName);
        Assert.Equal(22.0, temp.Celsius);
    }

    [Fact]
    public void Temperature_BelowAbsoluteZero_DoesNotUpdate()
    {
        var temp = new Temperature("TestCity", 20.0);
        temp.Celsius = -300;

        Assert.Equal(20.0, temp.Celsius);
    }

    [Fact]
    public void Temperature_AboveMaximum_DoesNotUpdate()
    {
        var temp = new Temperature("TestCity", 25.0);
        temp.Celsius = 150;

        Assert.Equal(25.0, temp.Celsius);
    }

    [Fact]
    public void Temperature_Fahrenheit_ConvertsCorrectly()
    {
        var temp = new Temperature("TestCity", 0.0);

        Assert.Equal(32.0, temp.Fahrenheit);
    }

    [Fact]
    public void Temperature_Kelvin_ConvertsCorrectly()
    {
        var temp = new Temperature("TestCity", 0.0);

        Assert.Equal(273.15, temp.Kelvin);
    }

    [Fact]
    public void Temperature_LocationTooShort_DoesNotUpdate()
    {
        var temp = new Temperature("ValidCity", 20.0);
        temp.LocationName = "AB";

        Assert.Equal("ValidCity", temp.LocationName);
    }

    [Fact]
    public void Temperature_LocationTooLong_DoesNotUpdate()
    {
        var temp = new Temperature("ValidCity", 20.0);
        temp.LocationName = new string('X', 60);

        Assert.Equal("ValidCity", temp.LocationName);
    }

    [Fact]
    public void Temperature_EmptyLocation_DoesNotUpdate()
    {
        var temp = new Temperature("ValidCity", 20.0);
        temp.LocationName = "";

        Assert.Equal("ValidCity", temp.LocationName);
    }

    [Fact]
    public void Temperature_Timestamp_IsSet()
    {
        var temp = new Temperature("TestCity", 20.0);

        Assert.NotEqual(default(DateTime), temp.Timestamp);
    }
}

// Exercise 04: Access Modifiers - GameCharacter class
public class GameCharacterTests
{
    [Fact]
    public void GameCharacter_Constructor_InitializesCorrectly()
    {
        var character = new GameCharacter("Thorin", 100);

        Assert.Equal("Thorin", character.Name);
        Assert.Equal(100, character.MaxHealth);
        Assert.Equal(100, character.Health);
        Assert.Equal(0, character.Experience);
        Assert.True(character.IsAlive);
    }

    [Fact]
    public void GameCharacter_TakeDamage_ReducesHealth()
    {
        var character = new GameCharacter("Warrior", 100);
        character.TakeDamage(30);

        Assert.Equal(70, character.Health);
    }

    [Fact]
    public void GameCharacter_TakeFatalDamage_SetsHealthToZero()
    {
        var character = new GameCharacter("Warrior", 100);
        character.TakeDamage(150);

        Assert.Equal(0, character.Health);
        Assert.False(character.IsAlive);
    }

    [Fact]
    public void GameCharacter_Heal_IncreasesHealth()
    {
        var character = new GameCharacter("Warrior", 100);
        character.TakeDamage(30);
        character.Heal(20);

        Assert.Equal(90, character.Health);
    }

    [Fact]
    public void GameCharacter_HealAboveMax_CapsAtMaxHealth()
    {
        var character = new GameCharacter("Warrior", 100);
        character.TakeDamage(20);
        character.Heal(50);

        Assert.Equal(100, character.Health);
    }

    [Fact]
    public void GameCharacter_GainExperience_IncreasesExperience()
    {
        var character = new GameCharacter("Warrior", 100);
        character.GainExperience(50);

        Assert.Equal(50, character.Experience);
    }

    [Fact]
    public void GameCharacter_Level_CalculatesCorrectly()
    {
        var character = new GameCharacter("Warrior", 100);

        Assert.Equal(1, character.Level);

        character.GainExperience(100);
        Assert.Equal(2, character.Level);

        character.GainExperience(100);
        Assert.Equal(3, character.Level);
    }

    [Fact]
    public void GameCharacter_Name_CanBeChanged()
    {
        var character = new GameCharacter("Warrior", 100);
        character.Name = "Knight";

        Assert.Equal("Knight", character.Name);
    }

    [Fact]
    public void GameCharacter_IsAlive_ReflectsHealthStatus()
    {
        var character = new GameCharacter("Warrior", 100);

        Assert.True(character.IsAlive);

        character.TakeDamage(100);
        Assert.False(character.IsAlive);
    }
}

// Copy solution classes here for testing
// Students should copy their solution classes into this file to run tests

class Product
{
    private string name;
    private decimal price;
    private int stock;
    
    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Error: Name cannot be empty");
                return;
            }
            name = value;
        }
    }
    
    public decimal Price
    {
        get { return price; }
        set
        {
            if (value < 0)
            {
                Console.WriteLine("Error: Price cannot be negative");
                return;
            }
            price = value;
        }
    }
    
    public int Stock
    {
        get { return stock; }
        set
        {
            if (value < 0)
            {
                Console.WriteLine("Error: Stock cannot be negative");
                return;
            }
            stock = value;
        }
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"Product: {Name}");
        Console.WriteLine($"Price: ${Price:F2}");
        Console.WriteLine($"Stock: {Stock}");
    }
}

class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public int Id { get; private set; }
    public DateTime CreatedDate { get; init; }
    
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
    
    public string FullName => $"{FirstName} {LastName}";
    
    public Person(string firstName, string lastName, int id)
    {
        FirstName = firstName;
        LastName = lastName;
        Id = id;
        CreatedDate = DateTime.Now;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Name: {FullName}");
        Console.WriteLine($"Email: {Email}");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"Created: {CreatedDate:yyyy-MM-dd}");
    }
}

class Temperature
{
    private double celsius;
    private string locationName;
    
    public double Celsius
    {
        get { return celsius; }
        set
        {
            if (value < -273.15)
            {
                Console.WriteLine("Error: Temperature cannot be below -273.15°C (absolute zero)");
                return;
            }
            if (value > 100)
            {
                Console.WriteLine("Error: Temperature cannot exceed 100°C");
                return;
            }
            celsius = value;
        }
    }
    
    public string LocationName
    {
        get { return locationName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Error: Location name is required");
                return;
            }
            if (value.Length < 3)
            {
                Console.WriteLine("Error: Location name must be at least 3 characters");
                return;
            }
            if (value.Length > 50)
            {
                Console.WriteLine("Error: Location name cannot exceed 50 characters");
                return;
            }
            locationName = value;
        }
    }
    
    public DateTime Timestamp { get; init; }
    
    public double Fahrenheit => celsius * 9.0 / 5.0 + 32.0;
    public double Kelvin => celsius + 273.15;
    
    public Temperature(string location, double celsius)
    {
        LocationName = location;
        Celsius = celsius;
        Timestamp = DateTime.Now;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"Location: {LocationName}");
        Console.WriteLine($"Celsius: {Celsius:F2}°C");
        Console.WriteLine($"Fahrenheit: {Fahrenheit:F2}°F");
        Console.WriteLine($"Kelvin: {Kelvin:F2}K");
        Console.WriteLine($"Recorded: {Timestamp:yyyy-MM-dd HH:mm:ss}");
    }
}

class GameCharacter
{
    private string name;
    private int health;
    private int maxHealth;
    private int experience;
    
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    
    public int Health
    {
        get { return health; }
        private set
        {
            if (value < 0)
                health = 0;
            else if (value > maxHealth)
                health = maxHealth;
            else
                health = value;
        }
    }
    
    public int MaxHealth
    {
        get { return maxHealth; }
        private set { maxHealth = value; }
    }
    
    public int Experience
    {
        get { return experience; }
        private set { experience = value; }
    }
    
    public int Level => CalculateLevel();
    public bool IsAlive => health > 0;
    
    public GameCharacter(string name, int maxHealth)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
        Experience = 0;
    }
    
    public void TakeDamage(int amount)
    {
        if (amount < 0)
        {
            Console.WriteLine("Error: Damage cannot be negative");
            return;
        }
        
        Health -= amount;
        Console.WriteLine($"{Name} takes {amount} damage!");
        
        if (health <= 20 && health > 0)
            Console.WriteLine("Warning: Health is critical!");
    }
    
    public void Heal(int amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Error: Heal amount must be positive");
            return;
        }
        
        Health += amount;
        Console.WriteLine($"{Name} heals for {amount} HP");
    }
    
    public void GainExperience(int amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Error: Experience must be positive");
            return;
        }
        
        int oldLevel = Level;
        Experience += amount;
        Console.WriteLine($"{Name} gained {amount} experience!");
        
        if (Level > oldLevel)
            Console.WriteLine($"Leveled up to {Level}!");
    }
    
    private int CalculateLevel()
    {
        return 1 + (experience / 100);
    }
    
    public void DisplayStatus()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Health: {Health}/{MaxHealth}");
        Console.WriteLine($"Level: {Level}");
        Console.WriteLine($"Experience: {Experience}");
        Console.WriteLine($"Status: {(IsAlive ? "Alive" : "Dead")}");
    }
}