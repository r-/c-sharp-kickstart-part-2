# Chapter 6 – Inheritance and Class Hierarchies

In Chapter 5, we learned to protect our classes with encapsulation. Now we'll discover how to create **class families**—where some classes share behavior while adding their own unique features. This is inheritance, and it's one of the most powerful tools in object-oriented programming.

## Learning Objectives

By the end of this chapter, you will be able to:

- Explain what inheritance means in OOP
- Create derived classes that extend base classes
- Override methods to customize behavior
- Use the `base` keyword to access parent functionality
- Understand when to use `virtual` and `override`
- Design simple class hierarchies
- Decide when inheritance is appropriate

## Prerequisites

This chapter assumes you've completed **Chapter 5** and can:

- Create classes with properties and methods
- Use access modifiers (public, private, protected)
- Implement encapsulation with guard clauses
- Validate data in constructors and properties
- Write defensive code that prevents invalid states

---

## Mission Brief

In this chapter you will:

- Learn to model "is-a" relationships between classes
- Discover how derived classes inherit from base classes
- Master method overriding to customize behavior
- Use the `protected` access modifier effectively
- Build class hierarchies that share common functionality
- Understand when inheritance helps and when it hurts

This chapter teaches you to design flexible class families that reduce code duplication.

---

## Concept Power-Up – The Problem with Duplication

Let's say you're building a game with different character types.

```csharp
class Warrior
{
    public string Name { get; set; }
    public int Health { get; private set; }
    public int AttackPower { get; set; }
    
    public Warrior(string name)
    {
        Name = name;
        Health = 100;
        AttackPower = 15;
    }
    
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
    }
}

class Mage
{
    public string Name { get; set; }
    public int Health { get; private set; }
    public int MagicPower { get; set; }
    
    public Mage(string name)
    {
        Name = name;
        Health = 80;
        MagicPower = 25;
    }
    
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
    }
}
```

**The Problem:**
- Name, Health, and TakeDamage are duplicated
- If we add Archer, Healer, Thief—more duplication
- Bug in TakeDamage? Must fix in multiple places
- Want to add a Level property? Change every class!

> **Key Insight**: When multiple classes share common features, inheritance eliminates duplication by creating a shared parent class.

---

## Core Concepts

### Concept 1 – Base Classes: The Shared Foundation

A **base class** (also called parent or superclass) contains common properties and methods.

```csharp
class Character
{
    public string Name { get; set; }
    public int Health { get; private set; }
    
    public Character(string name, int health)
    {
        Name = name;
        Health = health;
    }
    
    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage cannot be negative");
        
        Health -= damage;
        if (Health < 0) Health = 0;
    }
    
    public bool IsAlive => Health > 0;
}
```

Now all characters share Name, Health, TakeDamage, and IsAlive.

### Concept 2 – Derived Classes: Extending the Base

A **derived class** (also called child or subclass) inherits from a base class using `:`.

```csharp
class Warrior : Character
{
    public int AttackPower { get; set; }
    
    public Warrior(string name) : base(name, 100)
    {
        AttackPower = 15;
    }
    
    public void MeleeAttack()
    {
        Console.WriteLine($"{Name} swings their sword for {AttackPower} damage!");
    }
}

class Mage : Character
{
    public int MagicPower { get; set; }
    
    public Mage(string name) : base(name, 80)
    {
        MagicPower = 25;
    }
    
    public void CastSpell()
    {
        Console.WriteLine($"{Name} casts a spell for {MagicPower} damage!");
    }
}
```

**How it works:**
- `Warrior : Character` means Warrior inherits from Character
- `: base(name, 100)` calls Character's constructor
- Warrior gets Name, Health, TakeDamage automatically
- Warrior adds its own AttackPower and MeleeAttack

**Using derived classes:**
```csharp
Warrior warrior = new Warrior("Conan");
warrior.MeleeAttack();       // Warrior's method
warrior.TakeDamage(20);      // Inherited from Character
Console.WriteLine(warrior.IsAlive);  // Inherited property
```

### Concept 3 – The `base` Keyword: Accessing the Parent

Use `base` to call the parent class's constructor or methods.

```csharp
class Character
{
    public string Name { get; set; }
    public int Health { get; protected set; }
    
    public Character(string name, int health)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");
        if (health <= 0)
            throw new ArgumentException("Health must be positive");
        
        Name = name;
        Health = health;
    }
    
    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
        Console.WriteLine($"{Name} took {damage} damage. Health: {Health}");
    }
}

class ArmoredKnight : Character
{
    public int Armor { get; set; }
    
    public ArmoredKnight(string name, int armor) : base(name, 120)
    {
        Armor = armor;
    }
    
    public override void TakeDamage(int damage)
    {
        // Reduce damage by armor
        int reducedDamage = Math.Max(0, damage - Armor);
        base.TakeDamage(reducedDamage);  // Call parent's TakeDamage
    }
}
```

**Test it:**
```csharp
ArmoredKnight knight = new ArmoredKnight("Sir Galahad", 5);
knight.TakeDamage(15);  // Takes only 10 damage (15 - 5 armor)
// Output: Sir Galahad took 10 damage. Health: 110
```

### Concept 4 – Virtual and Override: Customizing Behavior

Mark base class methods as `virtual` to allow derived classes to override them.

```csharp
class Animal
{
    public string Name { get; set; }
    
    public Animal(string name)
    {
        Name = name;
    }
    
    // Virtual method can be overridden
    public virtual void MakeSound()
    {
        Console.WriteLine($"{Name} makes a sound");
    }
}

class Dog : Animal
{
    public Dog(string name) : base(name) { }
    
    // Override the base method
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} says Woof!");
    }
}

class Cat : Animal
{
    public Cat(string name) : base(name) { }
    
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} says Meow!");
    }
}
```

**Using polymorphism:**
```csharp
Animal animal1 = new Dog("Buddy");
Animal animal2 = new Cat("Whiskers");

animal1.MakeSound();  // Buddy says Woof!
animal2.MakeSound();  // Whiskers says Meow!
```

**Rules:**
- Base class method must be marked `virtual`
- Derived class method must be marked `override`
- Override method must have same signature
- Without `virtual`, you cannot override

### Concept 5 – Protected Access: Sharing with Children

The `protected` modifier allows derived classes to access members.

```csharp
class BankAccount
{
    private decimal balance;           // Only this class
    protected string accountId;        // This class + derived classes
    public string Owner { get; set; }  // Everyone
    
    protected void LogTransaction(string message)
    {
        // Derived classes can call this
        Console.WriteLine($"[{accountId}] {message}");
    }
}

class SavingsAccount : BankAccount
{
    public void AddInterest(decimal rate)
    {
        // Can access protected members
        LogTransaction($"Adding interest at {rate}%");
        // But NOT private members
        // balance += interest;  // ERROR: balance is private
    }
}
```

**Access levels:**
- `private`: Only this class
- `protected`: This class + derived classes
- `public`: Everyone

> **🏋️ Practice Now**: Complete [Exercise 06-01: First Inheritance](../../exercises/06-inheritance/01-first-inheritance/) to create your first class hierarchy.

---

## Try It – Vehicle Hierarchy

Let's build a complete vehicle system with inheritance:

```csharp
// Base class
class Vehicle
{
    public string Model { get; set; }
    public int Speed { get; protected set; }
    public int MaxSpeed { get; protected set; }
    
    public Vehicle(string model, int maxSpeed)
    {
        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model is required");
        if (maxSpeed <= 0)
            throw new ArgumentException("MaxSpeed must be positive");
        
        Model = model;
        MaxSpeed = maxSpeed;
        Speed = 0;
    }
    
    public virtual void Accelerate(int amount)
    {
        Speed += amount;
        if (Speed > MaxSpeed) Speed = MaxSpeed;
        Console.WriteLine($"{Model} accelerating to {Speed} km/h");
    }
    
    public void Brake()
    {
        Speed = 0;
        Console.WriteLine($"{Model} has stopped");
    }
}

// Derived class 1
class Car : Vehicle
{
    public int Passengers { get; set; }
    
    public Car(string model, int maxSpeed, int passengers) 
        : base(model, maxSpeed)
    {
        Passengers = passengers;
    }
    
    public override void Accelerate(int amount)
    {
        Console.WriteLine($"Car engine revving...");
        base.Accelerate(amount);
    }
    
    public void Honk()
    {
        Console.WriteLine($"{Model} says BEEP BEEP!");
    }
}

// Derived class 2
class Motorcycle : Vehicle
{
    public bool HasSidecar { get; set; }
    
    public Motorcycle(string model, int maxSpeed) 
        : base(model, maxSpeed)
    {
        HasSidecar = false;
    }
    
    public override void Accelerate(int amount)
    {
        // Motorcycles accelerate faster
        Console.WriteLine($"Motorcycle zooming...");
        base.Accelerate(amount * 2);
    }
    
    public void Wheelie()
    {
        if (Speed > 50)
            Console.WriteLine($"{Model} does a wheelie!");
        else
            Console.WriteLine("Need more speed for a wheelie!");
    }
}
```

**Test it:**
```csharp
Car car = new Car("Volvo V90", 180, 5);
Motorcycle bike = new Motorcycle("Harley", 200);

car.Accelerate(50);      // Car engine revving... Volvo V90 accelerating to 50 km/h
bike.Accelerate(50);     // Motorcycle zooming... Harley accelerating to 100 km/h

car.Honk();              // Volvo V90 says BEEP BEEP!
bike.Wheelie();          // Harley does a wheelie!

// Both can use base class methods
car.Brake();             // Volvo V90 has stopped
bike.Brake();            // Harley has stopped
```

> **🏋️ Practice Now**: Complete [Exercise 06-02: Method Overriding](../../exercises/06-inheritance/02-method-overriding/) to practice customizing inherited behavior.

---

## Challenge – Employee Management System

Create a class hierarchy for employees:

1. Base class `Employee` with:
   - Name, ID, BaseSalary
   - Virtual method CalculatePay()
   - Virtual method GetInfo()

2. Derived class `Manager` with:
   - Bonus property
   - Override CalculatePay() to add bonus
   - Override GetInfo() to show "Manager: Name"

3. Derived class `Developer` with:
   - HoursWorked property
   - Override CalculatePay() based on hours
   - Override GetInfo() to show "Developer: Name"

4. Create instances and display their information

**Hints:**
```csharp
class Employee
{
    public string Name { get; set; }
    public int Id { get; set; }
    protected decimal BaseSalary { get; set; }
    
    public Employee(string name, int id, decimal baseSalary)
    {
        Name = name;
        Id = id;
        BaseSalary = baseSalary;
    }
    
    public virtual decimal CalculatePay()
    {
        return BaseSalary;
    }
    
    public virtual string GetInfo()
    {
        return $"Employee: {Name} (ID: {Id})";
    }
}
```

> **🏋️ Practice Now**: Complete [Exercise 06-03: Base Class Constructors](../../exercises/06-inheritance/03-base-constructors/) to master constructor chaining.

---

## Mini Project – Build YOUR RPG Character System

**Goal:** Create YOUR own role-playing game character system with a rich class hierarchy!

This is YOUR project to design and implement. You'll create a complete character system with base and derived classes that share behavior while having unique abilities.

### Why This Project?

Real game systems use inheritance extensively. This project teaches you to design flexible hierarchies that professional game developers use every day.

### Learning Goals

- Design effective class hierarchies
- Share common functionality in base classes
- Override methods to customize behavior
- Use protected members appropriately
- Build polymorphic systems

### Minimum Requirements

YOUR character system MUST include:

1. **Base Character class with:**
   - Name, Health, Level (encapsulated)
   - Constructor with validation
   - Virtual method Attack()
   - Virtual method Defend()
   - TakeDamage() method
   - IsAlive computed property
   - LevelUp() method

2. **At least 3 derived classes:**
   - Each with unique properties
   - Each overriding Attack() differently
   - Each with at least one special ability
   - Examples: Warrior, Mage, Archer, Rogue, Healer

3. **Demonstration program:**
   - Create multiple characters
   - Show inheritance working (base methods)
   - Show polymorphism (overridden methods)
   - Combat simulation between characters
   - Level up demonstration

### Expected Output

```
=== RPG Character System ===

Creating characters...
✓ Warrior "Conan" created - Health: 120, Attack: 15
✓ Mage "Gandalf" created - Health: 80, Mana: 100
✓ Archer "Legolas" created - Health: 90, Arrows: 20

Combat Round 1:
Conan attacks with mighty sword strike for 15 damage!
Gandalf takes 15 damage. Health: 65/80
Gandalf casts fireball for 25 damage!
Conan takes 25 damage. Health: 95/120

Special Abilities:
Conan uses Berserker Rage! Attack doubled!
Gandalf restores 20 mana
Legolas fires triple shot!

Status Report:
Warrior: Conan - Level 1 - HP: 95/120 - Alive
Mage: Gandalf - Level 1 - HP: 65/80 - Alive  
Archer: Legolas - Level 1 - HP: 90/90 - Alive
```

### Implementation Steps

1. Design Character base class
2. Implement encapsulation and validation
3. Create first derived class (e.g., Warrior)
4. Test inheritance works
5. Add more derived classes
6. Implement virtual/override methods
7. Create combat simulation
8. Add special abilities
9. Polish and test edge cases

### Hints

**Base Character Class:**
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
            throw new ArgumentException("Name required");
        if (maxHealth <= 0)
            throw new ArgumentException("Health must be positive");
        
        Name = name;
        this.maxHealth = maxHealth;
        Health = maxHealth;
        Level = 1;
    }
    
    public virtual int Attack()
    {
        return 10;  // Base attack
    }
    
    public virtual void Defend(int damage)
    {
        Console.WriteLine($"{Name} defends!");
    }
    
    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage cannot be negative");
        
        Health -= damage;
        Console.WriteLine($"{Name} takes {damage} damage. Health: {Health}/{maxHealth}");
    }
    
    public void LevelUp()
    {
        Level++;
        maxHealth += 10;
        Health = maxHealth;
        Console.WriteLine($"{Name} leveled up to {Level}!");
    }
}
```

**Warrior Example:**
```csharp
class Warrior : Character
{
    public int AttackPower { get; set; }
    private bool berserkerMode;
    
    public Warrior(string name) : base(name, 120)
    {
        AttackPower = 15;
        berserkerMode = false;
    }
    
    public override int Attack()
    {
        int damage = berserkerMode ? AttackPower * 2 : AttackPower;
        Console.WriteLine($"{Name} attacks with mighty sword for {damage} damage!");
        return damage;
    }
    
    public void ActivateBerserkerRage()
    {
        berserkerMode = true;
        Console.WriteLine($"{Name} enters berserker rage! Attack doubled!");
    }
    
    public override void Defend(int damage)
    {
        Console.WriteLine($"{Name} raises shield!");
        TakeDamage(damage / 2);  // Warriors take half damage when defending
    }
}
```

### Make It YOUR Own!

Add YOUR creative features:

- **More character classes** - Healer, Paladin, Necromancer, Bard
- **Stat system** - Strength, Intelligence, Dexterity
- **Equipment** - Weapons and armor that boost stats
- **Elemental attacks** - Fire, Ice, Lightning damage types
- **Status effects** - Poison, Stun, Regeneration
- **Team battles** - Multiple characters per side
- **Experience system** - Earn XP from combat
- **Critical hits** - Random chance for extra damage
- **Skill trees** - Unlock abilities as you level
- **Character classes hierarchy** - MeleeWarrior vs RangedWarrior
- **Inventory system** - Items and potions
- **Multiclass** - Characters with abilities from multiple classes
- **Boss enemies** - Special powerful characters

### Self-Assessment Checklist

- [ ] Base class contains shared functionality
- [ ] Derived classes properly inherit from base
- [ ] Constructor chaining works correctly (`:base()`)
- [ ] Virtual methods marked in base class
- [ ] Override methods marked in derived classes
- [ ] Protected members used appropriately
- [ ] Each class has unique abilities
- [ ] Encapsulation maintained throughout
- [ ] Validation in all constructors
- [ ] Polymorphism demonstrated
- [ ] Code is well-organized and readable

### What Makes a Great Project?

**Class Design (40%)**
- Clear hierarchy with logical inheritance
- Proper use of virtual/override
- Good separation of concerns
- Appropriate access modifiers

**Functionality (30%)**
- All requirements work correctly
- Inheritance benefits are clear
- Polymorphism demonstrated
- Combat system functional

**Code Quality (20%)**
- Clean, readable code
- Good encapsulation
- Comprehensive validation
- Meaningful method and property names

**Creativity (10%)**
- Unique character types
- Interesting abilities
- YOUR special features

> **🚀 Mini Project**: Ready to build YOUR game? Complete the [Mini Project: RPG Character System](../../projects/06-inheritance/) to create a complete class hierarchy.

---

## Common Mistakes

### Forgetting to Call Base Constructor

```csharp
// ❌ Wrong: Base constructor not called
class Warrior : Character
{
    public Warrior(string name)
    {
        // ERROR: Character needs name and health
    }
}

// ✅ Correct: Call base constructor
class Warrior : Character
{
    public Warrior(string name) : base(name, 100)
    {
        // Now Character is properly initialized
    }
}
```

### Not Marking Methods Virtual/Override

```csharp
// ❌ Wrong: Not marked virtual in base
class Animal
{
    public void MakeSound()  // Not virtual!
    {
        Console.WriteLine("Generic sound");
    }
}

class Dog : Animal
{
    public void MakeSound()  // Hides, doesn't override
    {
        Console.WriteLine("Woof");
    }
}

// ✅ Correct: Proper virtual/override
class Animal
{
    public virtual void MakeSound()
    {
        Console.WriteLine("Generic sound");
    }
}

class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Woof");
    }
}
```

### Misusing Protected

```csharp
// ❌ Wrong: Making everything protected
class BankAccount
{
    protected decimal balance;  // Too open!
    protected string owner;     // Too open!
}

// ✅ Correct: Only what derived classes need
class BankAccount
{
    private decimal balance;         // Implementation detail
    protected string accountId;      // Derived classes might need
    public string Owner { get; set; } // Public interface
}
```

### Inheritance When Composition Is Better

```csharp
// ❌ Wrong: Car is-a Engine? No!
class Car : Engine
{
    // This doesn't make sense
}

// ✅ Correct: Car has-a Engine
class Car
{
    private Engine engine;
    
    public Car()
    {
        engine = new Engine();
    }
}
```

**Rule of thumb:** Use inheritance for "is-a" relationships, composition for "has-a".

---

## Hands-On Practice

Practice inheritance with these exercises:

### Exercise Progression

1. **[Exercise 06-01: First Inheritance](../../exercises/06-inheritance/01-first-inheritance/)** - Create basic class hierarchy
2. **[Exercise 06-02: Method Overriding](../../exercises/06-inheritance/02-method-overriding/)** - Override methods to customize behavior
3. **[Exercise 06-03: Base Class Constructors](../../exercises/06-inheritance/03-base-constructors/)** - Master constructor chaining
4. **[Exercise 06-04: Virtual Methods](../../exercises/06-inheritance/04-virtual-methods/)** - Practice polymorphism

Complete these in order to build comprehensive inheritance skills.

> **💡 Tip**: Think about what's truly shared vs. what's unique to each type!

---

## Why It Matters

Inheritance is fundamental to professional software development because it:

1. **Eliminates duplication** - Write shared code once in the base class
2. **Models reality** - Real-world relationships are hierarchical
3. **Enables polymorphism** - Treat different types uniformly
4. **Supports extensibility** - Easy to add new types
5. **Encapsulates change** - Modify base class, all derived classes benefit
6. **Expresses intent** - Shows relationships clearly in code

Every major framework uses inheritance: .NET Base Class Library, Unity game engine, ASP.NET, and more.

---

## Checkpoint

You should now be able to:

- Create base classes with shared functionality
- Derive classes that extend base classes
- Use `:base()` to call parent constructors
- Mark methods virtual and override them
- Use protected members appropriately
- Design simple class hierarchies
- Understand when inheritance is appropriate

---

## Reflection Questions

1. How is inheritance different from just copying code into multiple classes?
2. Why must base class constructors be called from derived classes?
3. When would you use `protected` instead of `private` or `public`?
4. Can you think of a real-world hierarchy that would make a good class design?
5. When might composition be better than inheritance?

---

## Next Chapter Preview

In **Chapter 7 – Polymorphism and Interfaces**, you'll discover how to treat different types uniformly through their shared behaviors. You'll learn about abstract classes, interfaces, and how they enable truly flexible designs that can change and grow without breaking.

---

## Key Terms

- **Inheritance**: Mechanism where a class acquires properties and methods from another class
- **Base Class**: The parent class that provides shared functionality (also called superclass or parent)
- **Derived Class**: A class that inherits from a base class (also called subclass or child)
- **`base` keyword**: Used to access base class members or call base constructors
- **`virtual`**: Marks a method as overridable in derived classes
- **`override`**: Marks a method as replacing the base class implementation
- **`protected`**: Access modifier allowing access by derived classes
- **Polymorphism**: Ability to treat different types through a common interface
- **Constructor Chaining**: Calling base class constructor from derived class using `:base()`
- **Class Hierarchy**: Organization of classes in inheritance relationships
- **Method Overriding**: Replacing base class method implementation in derived class
- **Is-a Relationship**: When inheritance is appropriate (Dog is-a Animal)
- **Has-a Relationship**: When composition is better (Car has-a Engine)

---

**Teacher Notes:**

**Pedagogical Goals:**
- Transition from isolated classes to related class families
- Emphasize code reuse through inheritance
- Show when inheritance helps and when it doesn't
- Prepare for polymorphism and interfaces in Chapter 7

**Common Student Questions:**
- "When do I use inheritance vs. composition?" → Use inheritance for "is-a", composition for "has-a"
- "Why can't I access private members from derived classes?" → That's what protected is for
- "Do I have to call base constructor?" → Yes, unless base has parameterless constructor
- "What's the difference between virtual and override?" → virtual allows override, override provides override

**Assessment Hints:**
- Check proper use of `:base()` in constructors
- Verify virtual/override used correctly
- Ensure students understand access modifier levels
- Look for appropriate inheritance relationships
- Check they're not overusing inheritance

**Connection to Curriculum (PRRPRR02):**
- Core OOP principle (inheritance)
- Code reuse and DRY principle
- Modeling relationships
- Foundation for polymorphism

**Classroom Activities:**
- Discussion: Find real-world inheritance examples
- Pair exercise: Design class hierarchy for school system
- Live coding: Add new derived class to existing hierarchy
- Code review: Identify inheritance misuse

**Rubric Suggestions (Mini Project):**
- Proper class hierarchy design: 35%
- Correct virtual/override usage: 25%
- Constructor chaining: 15%
- Encapsulation maintained: 15%
- Code quality and creativity: 10%

---

*© C# Kickstart Part 2 Contributors*