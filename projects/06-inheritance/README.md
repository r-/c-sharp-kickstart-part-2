# Mini Project: Build YOUR RPG Character System

## Project Overview

This is YOUR chance to design and build a complete role-playing game character system using inheritance! You'll create a class hierarchy that models different character types, each with unique abilities while sharing common functionality.

**This is YOUR project.** While there are minimum requirements, the creative direction, character types, abilities, and features are entirely up to YOU. Make it YOUR own!

## Why This Project?

Real game development relies heavily on inheritance. Games like Skyrim, World of Warcraft, and Diablo all use class hierarchies to manage different character types. This project teaches you the same patterns professional game developers use.

You'll learn to:
- Design effective class hierarchies
- Share functionality through base classes
- Customize behavior with method overriding
- Use polymorphism to treat different types uniformly
- Build systems that are easy to extend

## Learning Goals

- Design a clear inheritance hierarchy
- Properly use base class constructors
- Override methods to customize behavior
- Use `virtual` and `override` effectively
- Apply encapsulation in an inheritance context
- Use protected members appropriately
- Demonstrate polymorphism in action

## Minimum Requirements

YOUR RPG character system MUST include:

### 1. Base Character Class

Your `Character` base class must have:

- **Properties:**
  - Name (string, validated)
  - Health (int, protected setter, validated: 0 to MaxHealth)
  - MaxHealth (int, set in constructor)
  - Level (int, starts at 1)
  - IsAlive (computed property: Health > 0)

- **Constructor:**
  - Accept name and maxHealth
  - Validate all parameters (no empty names, positive health)
  - Initialize all properties

- **Methods:**
  - `virtual int Attack()` - Returns base attack damage
  - `virtual void Defend(int damage)` - Handle taking damage
  - `void TakeDamage(int damage)` - Reduce health (with validation)
  - `void LevelUp()` - Increase level and max health
  - `string GetStatus()` - Return formatted status string

### 2. Three Derived Character Classes

Create at least THREE different character types (examples: Warrior, Mage, Archer, Rogue, Healer, Paladin). Each must:

- **Inherit from Character**
- **Add unique properties** (e.g., Mana, Rage, Arrows, Energy)
- **Override Attack()** with class-specific behavior
- **Override Defend()** if needed (some classes defend differently)
- **Add at least one special ability** (unique method)
- **Call base constructor** properly

### 3. Demonstration Program

Your Main() method must:

- Create multiple characters of different types
- Show inherited methods work (TakeDamage, LevelUp)
- Show overridden methods work (Attack, Defend)
- Demonstrate polymorphism (treat different types as Character)
- Show special abilities
- Display a combat simulation
- Show character status throughout

## Expected Output Example

```
=== RPG Character System ===

Creating adventuring party...
✓ Warrior "Conan" created - HP: 120/120, Level 1
✓ Mage "Gandalf" created - HP: 80/80, Mana: 100, Level 1
✓ Archer "Legolas" created - HP: 90/90, Arrows: 20, Level 1

=== Combat Simulation ===

Round 1:
Conan attacks with mighty sword strike!
Damage: 15
Gandalf takes 15 damage. HP: 65/80

Gandalf casts fireball!
Damage: 25
Conan takes 25 damage. HP: 95/120

Legolas fires precise arrow!
Damage: 20
Enemy takes 20 damage.

Round 2:
Conan uses special ability: Berserker Rage!
Attack power doubled for next attack!

Conan attacks with mighty sword strike!
Damage: 30 (Rage active!)
Gandalf takes 30 damage. HP: 35/80

Gandalf restores 30 mana
Current mana: 100/100

=== Level Up! ===
Conan leveled up! Now level 2
HP increased: 120 → 130

=== Final Status ===
Character: Conan (Warrior)
Level: 2
HP: 95/130
Status: Alive
Special: Rage available

Character: Gandalf (Mage)
Level: 1
HP: 35/80
Mana: 100/100
Status: Alive

Character: Legolas (Archer)
Level: 1
HP: 90/90
Arrows: 17/20
Status: Alive

=== Polymorphism Demo ===
All characters attack:
Conan attacks with mighty sword strike! (15 damage)
Gandalf casts fireball! (25 damage)
Legolas fires precise arrow! (20 damage)
```

## Implementation Steps

Follow these steps to build YOUR system:

1. **Design your character types** - Decide which classes you'll create
2. **Create the Character base class** - Start with shared functionality
3. **Test the base class** - Make sure it works before proceeding
4. **Create first derived class** - Test inheritance works
5. **Add virtual/override methods** - Practice polymorphism
6. **Create remaining derived classes** - Build out your roster
7. **Add special abilities** - Make each class unique
8. **Create combat simulation** - Show everything working together
9. **Add polish** - Format output, add color, improve user experience
10. **Test edge cases** - Try invalid data, zero health, etc.

## Code Examples and Hints

### Character Base Class Structure

```csharp
class Character
{
    public string Name { get; set; }
    protected int health;
    protected int maxHealth;
    public int Level { get; private set; }
    
    public int Health
    {
        get { return health; }
        protected set
        {
            health = value;
            if (health > maxHealth) health = maxHealth;
            if (health < 0) health = 0;
        }
    }
    
    public bool IsAlive => Health > 0;
    
    public Character(string name, int maxHealth)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");
        if (maxHealth <= 0)
            throw new ArgumentException("Max health must be positive");
        
        Name = name;
        this.maxHealth = maxHealth;
        Health = maxHealth;
        Level = 1;
    }
    
    public virtual int Attack()
    {
        int damage = 10;
        Console.WriteLine($"{Name} attacks!");
        Console.WriteLine($"Damage: {damage}");
        return damage;
    }
    
    public virtual void Defend(int damage)
    {
        Console.WriteLine($"{Name} defends!");
        TakeDamage(damage);
    }
    
    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage cannot be negative");
        
        Health -= damage;
        Console.WriteLine($"{Name} takes {damage} damage. HP: {Health}/{maxHealth}");
    }
    
    public void LevelUp()
    {
        Level++;
        maxHealth += 10;
        Health = maxHealth;
        Console.WriteLine($"{Name} leveled up! Now level {Level}");
        Console.WriteLine($"HP increased: {maxHealth - 10} → {maxHealth}");
    }
    
    public string GetStatus()
    {
        return $"{Name} - Level {Level} - HP: {Health}/{maxHealth} - {(IsAlive ? "Alive" : "Defeated")}";
    }
}
```

### Example Derived Class: Warrior

```csharp
class Warrior : Character
{
    public int AttackPower { get; set; }
    private bool rageActive;
    
    public Warrior(string name) : base(name, 120)
    {
        AttackPower = 15;
        rageActive = false;
    }
    
    public override int Attack()
    {
        int damage = rageActive ? AttackPower * 2 : AttackPower;
        Console.WriteLine($"{Name} attacks with mighty sword strike!");
        Console.WriteLine($"Damage: {damage}" + (rageActive ? " (Rage active!)" : ""));
        
        if (rageActive)
            rageActive = false;  // Rage lasts one attack
        
        return damage;
    }
    
    public override void Defend(int damage)
    {
        Console.WriteLine($"{Name} raises shield!");
        int reducedDamage = damage / 2;
        TakeDamage(reducedDamage);  // Warriors take half damage when defending
    }
    
    public void ActivateBerserkerRage()
    {
        rageActive = true;
        Console.WriteLine($"{Name} uses special ability: Berserker Rage!");
        Console.WriteLine("Attack power doubled for next attack!");
    }
}
```

### Example Derived Class: Mage

```csharp
class Mage : Character
{
    private int mana;
    private int maxMana;
    
    public int Mana
    {
        get { return mana; }
        private set
        {
            mana = value;
            if (mana > maxMana) mana = maxMana;
            if (mana < 0) mana = 0;
        }
    }
    
    public Mage(string name) : base(name, 80)
    {
        maxMana = 100;
        Mana = maxMana;
    }
    
    public override int Attack()
    {
        if (Mana < 20)
        {
            Console.WriteLine($"{Name} is out of mana! Weak attack!");
            return 5;
        }
        
        Mana -= 20;
        int damage = 25;
        Console.WriteLine($"{Name} casts fireball!");
        Console.WriteLine($"Damage: {damage}");
        Console.WriteLine($"Mana: {Mana}/{maxMana}");
        return damage;
    }
    
    public void RestoreMana(int amount)
    {
        Mana += amount;
        Console.WriteLine($"{Name} restores {amount} mana");
        Console.WriteLine($"Current mana: {Mana}/{maxMana}");
    }
}
```

### Using Polymorphism

```csharp
// Create different character types
Character[] party = new Character[]
{
    new Warrior("Conan"),
    new Mage("Gandalf"),
    new Archer("Legolas")
};

// Treat them all as Characters
Console.WriteLine("All characters attack:");
foreach (Character character in party)
{
    int damage = character.Attack();  // Calls the correct override!
}

// Level up everyone
foreach (Character character in party)
{
    character.LevelUp();
}
```

## Make It YOUR Own!

After meeting the minimum requirements, add YOUR creative features! Here are ideas to inspire you:

### Character Enhancement Ideas

- **More character classes** - Healer, Paladin, Necromancer, Bard, Assassin
- **Stat systems** - Strength, Intelligence, Dexterity, Constitution
- **Equipment** - Weapons and armor that modify stats
- **Inventory** - Items, potions, scrolls
- **Skill trees** - Unlock abilities as you level up
- **Multi-classing** - Characters with abilities from multiple classes

### Combat System Ideas

- **Elemental damage** - Fire, Ice, Lightning, Poison
- **Status effects** - Poison, Stun, Regeneration, Burning
- **Critical hits** - Random chance for extra damage
- **Dodging** - Chance to avoid attacks completely
- **Team battles** - Multiple characters per side
- **Turn-based combat** - Proper combat round system
- **Boss enemies** - Special powerful characters with unique abilities

### Progression Ideas

- **Experience points** - Earn XP from combat to level up
- **Ability cooldowns** - Special abilities can't be spammed
- **Mana/energy systems** - Resource management for abilities
- **Resting** - Recover health and resources
- **Death and revival** - Handle character defeat
- **Permadeath** - Hardcore mode where death is permanent

### Advanced Features

- **Save/load** - Persist character data to files
- **Character creation** - Let player choose class and customize
- **Procedural enemies** - Generate random encounters
- **Quests** - Simple quest/mission system
- **Character relationships** - Party morale and synergies
- **AI behavior** - Different enemy types use different strategies

## Self-Assessment Checklist

Use this checklist to verify your project meets the requirements:

### Class Design (40%)
- [ ] Character base class with all required properties
- [ ] Proper use of `protected` for Health setter
- [ ] At least three well-designed derived classes
- [ ] Each derived class adds unique properties
- [ ] Clear inheritance relationships
- [ ] Appropriate access modifiers throughout

### Virtual/Override Implementation (25%)
- [ ] Attack() marked virtual in base class
- [ ] Each derived class overrides Attack()
- [ ] Defend() properly implemented
- [ ] At least one method calls `base.Method()`
- [ ] Polymorphism demonstrated in Main()

### Constructor Chaining (15%)
- [ ] Base constructor validates parameters
- [ ] All derived classes call `:base()`
- [ ] All constructors initialize properly
- [ ] No invalid objects can be created

### Functionality (10%)
- [ ] All methods work correctly
- [ ] Combat simulation runs without errors
- [ ] Level up system works
- [ ] Special abilities function properly
- [ ] Edge cases handled gracefully

### Code Quality (10%)
- [ ] Clean, readable code
- [ ] Good naming conventions
- [ ] Comments where needed
- [ ] No code duplication
- [ ] Proper formatting

## What Makes a Great Project?

Your project will be evaluated on:

**Class Hierarchy Design (35%)**
- Clear, logical inheritance structure
- Appropriate use of base class
- Well-designed derived classes
- Good separation of concerns

**Technical Implementation (35%)**
- Proper virtual/override usage
- Correct constructor chaining
- Appropriate access modifiers
- Encapsulation maintained

**Functionality (20%)**
- Works reliably
- All requirements met
- Combat system functions
- Special abilities work

**Creativity & Polish (10%)**
- YOUR unique character types
- YOUR special features
- Good user experience
- Clean output formatting

## Getting Started

1. **Plan first** - Sketch out your character types and their unique features
2. **Start simple** - Get base class working before adding complexity
3. **Test often** - Verify each piece works before moving on
4. **Iterate** - Add features gradually, don't try to do everything at once
5. **Have fun** - This is YOUR project, make it something YOU're proud of!

## Starter Code Template

```csharp
using System;

namespace Part2.Ch06.MiniProject
{
    // TODO: Create your Character base class here
    
    
    // TODO: Create your derived character classes here
    
    
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== YOUR RPG Character System ===");
            Console.WriteLine();
            
            // TODO: Create your characters
            
            // TODO: Demonstrate inheritance
            
            // TODO: Show polymorphism
            
            // TODO: Run combat simulation
            
            // TODO: Display final status
        }
    }
}
```

## Tips for Success

- **Start with the base class** - Get it working perfectly first
- **Test each derived class** - Make sure inheritance works before adding more
- **Use polymorphism** - Store different types in Character arrays/lists
- **Add validation** - Prevent negative damage, health, etc.
- **Format output nicely** - Make it easy to see what's happening
- **Comment your code** - Explain your design decisions
- **Have fun with it** - Add personality to YOUR character types!

Remember: This is YOUR project. The minimum requirements are just the starting point. What makes it special is YOUR creativity, YOUR design choices, and YOUR unique features!

---

*Ready to build YOUR RPG? Create a character system you'll be proud to demonstrate!*