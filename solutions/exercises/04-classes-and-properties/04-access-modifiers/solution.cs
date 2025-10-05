using System;

class GameCharacter
{
    // Private fields - internal state
    private string name;
    private int health;
    private int maxHealth;
    private int experience;
    
    // Public properties with appropriate access
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    
    // Health: public get, private set with validation
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
    
    // MaxHealth: public get, private set
    public int MaxHealth
    {
        get { return maxHealth; }
        private set { maxHealth = value; }
    }
    
    // Experience: public get, private set
    public int Experience
    {
        get { return experience; }
        private set { experience = value; }
    }
    
    // Computed properties - read-only
    public int Level => CalculateLevel();
    public bool IsAlive => health > 0;
    
    // Constructor
    public GameCharacter(string name, int maxHealth)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
        Experience = 0;
    }
    
    // Public methods for safe operations
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
    
    // Private helper method - internal calculation
    private int CalculateLevel()
    {
        return 1 + (experience / 100);
    }
    
    // Display character status
    public void DisplayStatus()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Health: {Health}/{MaxHealth}");
        Console.WriteLine($"Level: {Level}");
        Console.WriteLine($"Experience: {Experience}");
        Console.WriteLine($"Status: {(IsAlive ? "Alive" : "Dead")}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Creating characters...\n");
        
        GameCharacter warrior = new GameCharacter("Thorin", 100);
        
        Console.WriteLine("Warrior Status:");
        warrior.DisplayStatus();
        
        Console.WriteLine("\nTesting combat...");
        warrior.TakeDamage(30);
        warrior.TakeDamage(80);
        
        warrior.Heal(40);
        warrior.GainExperience(150);
        
        Console.WriteLine("\nFinal Status:");
        warrior.DisplayStatus();
        
        Console.WriteLine("\nAttempting invalid operations...");
        Console.WriteLine("Error: Cannot access private fields directly");
        Console.WriteLine("Error: Cannot set read-only properties");
        
        // These would cause compile errors:
        // warrior.health = 1000;      // ERROR: health is private
        // warrior.Health = 1000;      // ERROR: set is private
        // warrior.Level = 99;         // ERROR: no setter
    }
}