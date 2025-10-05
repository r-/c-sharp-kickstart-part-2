# Exercise 04-04: Access Modifiers

## Goal

Master the use of access modifiers (public, private, protected) to control visibility and create well-encapsulated classes. You'll learn to hide implementation details and expose only necessary interfaces.

## Background

Access modifiers determine what code can see and use your class members. Choosing the right modifier is crucial for maintainability, security, and creating clean APIs that are easy to use and hard to misuse.

## Instructions

You will analyze and fix a poorly designed class that exposes too much of its internal state, then redesign it with appropriate access modifiers.

### Part 1: Analyze the Broken Design

Study this class that violates encapsulation:

```csharp
class GameCharacter
{
    public string name;
    public int health;
    public int maxHealth;
    public int experience;
    public int level;
    public bool isAlive;
}
```

### Part 2: Identify Problems

List what's wrong with the design:
- All fields are public (anyone can change anything)
- No validation (health can go negative, above max, etc.)
- Computed values are fields (isAlive should be computed)
- Internal logic is exposed (level calculation)

### Part 3: Redesign with Proper Modifiers

Create a new version with:
- `private` fields for internal state
- `public` properties with validation where needed
- `public` methods for allowed operations
- Computed properties for derived values

### Part 4: Add Helper Methods

Add these methods with appropriate modifiers:
- `TakeDamage(int amount)` - public method to reduce health
- `Heal(int amount)` - public method to increase health
- `GainExperience(int amount)` - public method to add XP
- `CalculateLevel()` - private helper to compute level from XP

## Requirements

Your program must:

1. Create a `GameCharacter` class with:
   - Private fields for internal state
   - Public properties with appropriate access (some read-only)
   - Public methods for safe operations
   - Private helper methods for internal calculations
   - Computed properties where appropriate

2. Access modifier rules:
   - `name` - public get/set
   - `health` - public get, private set
   - `maxHealth` - public get, private set  
   - `experience` - public get, private set
   - `level` - public get only (computed)
   - `isAlive` - public get only (computed)

3. In `Main()`:
   - Create character objects
   - Use public methods to modify state
   - Demonstrate that direct field access is blocked
   - Show computed properties work correctly

## Expected Output

```
Creating characters...

Warrior Status:
Name: Thorin
Health: 100/100
Level: 1
Experience: 0
Status: Alive

Testing combat...
Thorin takes 30 damage!
Thorin takes 80 damage!
Warning: Health is critical!

Thorin heals for 40 HP
Thorin gained 150 experience!
Leveled up to 2!

Final Status:
Name: Thorin
Health: 30/100
Level: 2
Experience: 150
Status: Alive

Attempting invalid operations...
Error: Cannot access private fields directly
Error: Cannot set read-only properties
```

## Hints

### Private Fields with Public Properties

```csharp
class GameCharacter
{
    // Private fields - internal state
    private string name;
    private int health;
    private int maxHealth;
    private int experience;
    
    // Public properties with controlled access
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    
    // Read-only from outside, writable inside
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
    
    // Computed property - no setter at all
    public bool IsAlive => health > 0;
    
    public int Level => CalculateLevel();
}
```

### Private Helper Method

```csharp
// Private - only this class can call it
private int CalculateLevel()
{
    return 1 + (experience / 100);
}
```

### Public Methods for Safe Operations

```csharp
public void TakeDamage(int amount)
{
    if (amount < 0)
    {
        Console.WriteLine("Error: Damage cannot be negative");
        return;
    }
    
    Health -= amount;  // Uses private setter
    
    if (health <= 20)
        Console.WriteLine("Warning: Health is critical!");
}

public void Heal(int amount)
{
    if (amount <= 0)
    {
        Console.WriteLine("Error: Heal amount must be positive");
        return;
    }
    
    Health += amount;  // Automatically capped at maxHealth
    Console.WriteLine($"{Name} heals for {amount} HP");
}
```

## Common Mistakes

**Making everything public:**
```csharp
// ❌ Wrong: Everything is accessible
public int health;
public int maxHealth;

character.health = 999999;  // Cheating!

// ✅ Correct: Hide implementation
private int health;
public int Health { get; private set; }

public void TakeDamage(int amount)
{
    Health -= amount;  // Controlled access
}
```

**Using public setter when should be read-only:**
```csharp
// ❌ Wrong: Level should be computed, not settable
public int Level { get; set; }

character.Level = 99;  // Shouldn't be allowed!

// ✅ Correct: Computed property
public int Level => experience / 100;
```

**Making helper methods public:**
```csharp
// ❌ Wrong: Internal logic exposed
public int CalculateLevel()
{
    return experience / 100;
}

// ✅ Correct: Keep internals private
private int CalculateLevel()
{
    return experience / 100;
}
```

**Forgetting validation in private setters:**
```csharp
// ❌ Wrong: No validation even though private
private set { health = value; }  // Can still go negative!

// ✅ Correct: Validate even in private setter
private set
{
    if (value < 0)
        health = 0;
    else if (value > maxHealth)
        health = maxHealth;
    else
        health = value;
}
```

## Bonus Challenges

1. **Equipment system**: Add private equipment fields with public equip/unequip methods
2. **Stats system**: Add derived stats (strength affects max health, etc.)
3. **Skill system**: Add private skill list with public learn/use methods
4. **Death handling**: Make character respawn with partial health when isAlive becomes false
5. **Experience curve**: Use non-linear level calculation (harder to level up at high levels)

Example bonus output:
```
Warrior Status:
Name: Thorin
Health: 100/120 (Strength bonus: +20)
Level: 5 (Experience: 1500/2000)
Equipment: Iron Sword, Leather Armor
Skills: Power Strike (Level 2), Block (Level 1)
Status: Alive
```

## Reflection

After completing this exercise, consider:
1. Why should fields almost always be private?
2. When would you use `private set` instead of no setter?
3. What's the benefit of keeping helper methods private?
4. How do access modifiers make your class easier to use?

## Access Modifier Decision Table

Use this table to decide which modifier to use:

| Scenario | Modifier | Example |
|----------|----------|---------|
| Internal state storage | `private` field | `private int health;` |
| Derived value | `public` computed property | `public bool IsAlive => health > 0;` |
| Protected data | `public` get, `private` set | `public int Health { get; private set; }` |
| Safe operation | `public` method | `public void TakeDamage(int amount)` |
| Internal calculation | `private` method | `private int CalculateLevel()` |
| Simple data | `public` auto-property | `public string Name { get; set; }` |

---

**Time Estimate:** 40 minutes  
**Difficulty:** Medium  
**Type:** Design & Encapsulation Practice

This exercise teaches you to design classes with proper encapsulation, a fundamental skill for professional development.