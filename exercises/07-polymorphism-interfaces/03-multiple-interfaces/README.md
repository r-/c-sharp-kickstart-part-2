# Exercise 07-03: Multiple Interfaces

## Goal

Learn to implement multiple interfaces in a single class. You'll create game entities that have different combinations of behaviors through interface composition.

## Background

Unlike class inheritance (single), a class can implement multiple interfaces. This allows you to mix and match behaviors freely—a game character could be drawable, movable, damageable, and interactive all at once.

## Instructions

You will create a game entity system with multiple interface contracts.

### Part 1: Define the Interfaces

Create these four interfaces:

1. **IDrawable**
   - Property: `string Sprite { get; }`
   - Method: `void Draw()`

2. **IMovable**
   - Properties: `int X { get; set; }`, `int Y { get; set; }`
   - Method: `void Move(int deltaX, int deltaY)`

3. **IDamageable**
   - Properties: `int Health { get; }`, `int MaxHealth { get; }`
   - Method: `void TakeDamage(int amount)`
   - Method: `bool IsAlive()`

4. **IInteractable**
   - Property: `string InteractionText { get; }`
   - Method: `void Interact()`

### Part 2: Implement Game Entities

Create these entity classes implementing different interface combinations:

1. **Player** implements ALL four interfaces
   - Full implementation of all interface members
   - Health starts at MaxHealth (100)
   - Sprite: "🧑" (player character)
   - InteractionText: "Press E to open inventory"

2. **Enemy** implements `IDrawable`, `IMovable`, `IDamageable`
   - Health starts at MaxHealth (50)
   - Sprite: "👾" (enemy sprite)
   - AI: moves randomly

3. **Chest** implements `IDrawable`, `IInteractable`
   - Sprite: "📦" (chest icon)
   - InteractionText: "Press E to open chest"
   - Stores items (optional)

4. **Wall** implements only `IDrawable`
   - Sprite: "🧱" (wall tile)
   - Cannot move, damage, or interact

### Part 3: Demonstrate Interface Segregation

Create these methods showing how interfaces separate concerns:

- `void RenderEntity(IDrawable entity)` - draws any drawable
- `void MoveEntity(IMovable entity, int dx, int dy)` - moves any movable
- `void AttackEntity(IDamageable entity, int damage)` - damages any damageable
- `void UseEntity(IInteractable entity)` - interacts with any interactable

Test each method with appropriate entities.

## Requirements

Your solution must:
1. Define all four interfaces
2. Create all four entity classes with appropriate interface implementations
3. Implement multiple interfaces where specified
4. Create the four type-specific methods
5. Demonstrate that each method works with any class implementing its interface
6. Show compile-time safety (walls can't be damaged, chests can't move, etc.)

## Expected Output

```
Game Entity System
==================

Creating entities:
✓ Player created at (5, 5)
✓ Enemy created at (10, 8)
✓ Chest created at (3, 3)
✓ Wall created at (7, 7)

Rendering all drawable entities:
🧑 Player at (5, 5)
👾 Enemy at (10, 8)
📦 Chest at (3, 3)
🧱 Wall at (7, 7)

Moving movable entities:
Player moved to (6, 5)
Enemy moved to (11, 9)

Attacking damageable entities:
Player took 10 damage. Health: 90/100
Enemy took 25 damage. Health: 25/50

Interacting with interactive entities:
Press E to open inventory
Press E to open chest

Game Over: Enemy defeated!
```

## Hints

**Interface definitions:**
```csharp
interface IDrawable
{
    string Sprite { get; }
    void Draw();
}

interface IMovable
{
    int X { get; set; }
    int Y { get; set; }
    void Move(int deltaX, int deltaY);
}

interface IDamageable
{
    int Health { get; }
    int MaxHealth { get; }
    void TakeDamage(int amount);
    bool IsAlive();
}

interface IInteractable
{
    string InteractionText { get; }
    void Interact();
}
```

**Player implementing multiple interfaces:**
```csharp
class Player : IDrawable, IMovable, IDamageable, IInteractable
{
    // IDrawable
    public string Sprite => "🧑";
    
    // IMovable
    public int X { get; set; }
    public int Y { get; set; }
    
    // IDamageable
    private int health;
    public int Health 
    { 
        get => health;
        private set => health = Math.Max(0, value);
    }
    public int MaxHealth { get; } = 100;
    
    // IInteractable
    public string InteractionText => "Press E to open inventory";
    
    public Player(int x, int y)
    {
        X = x;
        Y = y;
        Health = MaxHealth;
    }
    
    public void Draw()
    {
        Console.WriteLine($"{Sprite} Player at ({X}, {Y})");
    }
    
    public void Move(int deltaX, int deltaY)
    {
        X += deltaX;
        Y += deltaY;
    }
    
    public void TakeDamage(int amount)
    {
        Health -= amount;
        Console.WriteLine($"Player took {amount} damage. Health: {Health}/{MaxHealth}");
    }
    
    public bool IsAlive() => Health > 0;
    
    public void Interact()
    {
        Console.WriteLine(InteractionText);
    }
}
```

**Type-specific methods:**
```csharp
void RenderEntity(IDrawable entity)
{
    entity.Draw();
}

void MoveEntity(IMovable entity, int dx, int dy)
{
    entity.Move(dx, dy);
    Console.WriteLine($"{entity.GetType().Name} moved to ({entity.X}, {entity.Y})");
}

void AttackEntity(IDamageable entity, int damage)
{
    entity.TakeDamage(damage);
    if (!entity.IsAlive())
    {
        Console.WriteLine("Game Over: Enemy defeated!");
    }
}

void UseEntity(IInteractable entity)
{
    entity.Interact();
}
```

**Using the methods:**
```csharp
Player player = new Player(5, 5);
Enemy enemy = new Enemy(10, 8);
Chest chest = new Chest(3, 3);
Wall wall = new Wall(7, 7);

// Render all drawables
List<IDrawable> drawables = new List<IDrawable> { player, enemy, chest, wall };
foreach (var drawable in drawables)
{
    RenderEntity(drawable);
}

// Move only movables
MoveEntity(player, 1, 0);
MoveEntity(enemy, 1, 1);
// MoveEntity(wall, 0, 0);  // Won't compile - Wall isn't IMovable!

// Attack only damageables
AttackEntity(player, 10);
AttackEntity(enemy, 25);
// AttackEntity(chest, 10);  // Won't compile - Chest isn't IDamageable!

// Interact with interactables
UseEntity(player);
UseEntity(chest);
// UseEntity(enemy);  // Won't compile - Enemy isn't IInteractable!
```

## Common Mistakes to Avoid

- ❌ Forgetting to implement all members of all interfaces
- ❌ Trying to call interface methods on objects that don't implement them
- ❌ Not using interface types for method parameters
- ❌ Implementing unnecessary interfaces (Wall shouldn't be IDamageable)

## Bonus Challenge

1. Add `ICollectable` interface with `void Collect()` method
2. Create `Coin` and `HealthPotion` classes implementing `IDrawable` and `ICollectable`
3. Add collision detection: `bool CheckCollision(IMovable entity1, IMovable entity2)`
4. Create `Game` class that manages all entities in a list
5. Add turn-based movement where entities move in sequence

## What You're Learning

- How to implement multiple interfaces in one class
- Interface segregation principle (focused interfaces)
- Compile-time type safety through interfaces
- How interfaces enable behavior composition
- The flexibility of mixing and matching contracts

---

**Next:** After completing this exercise, move to [Exercise 07-04: Polymorphic Collections](../04-polymorphic-collections/) to master working with collections of interface types.