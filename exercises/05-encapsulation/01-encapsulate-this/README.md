# Exercise 05-01: Encapsulate This

## Goal

Transform a poorly designed class into a properly encapsulated one by identifying and enforcing invariants. You'll learn to recognize encapsulation violations and fix them systematically.

## Background

Many codebases have classes that expose too much of their internal state, making them fragile and error-prone. This exercise teaches you to identify these problems and apply encapsulation principles to fix them.

## Instructions

You are given a broken `ShoppingCart` class that violates encapsulation principles. Your task is to fix it by adding proper encapsulation while maintaining the same functionality.

### Part 1: Analyze the Broken Design

Study this poorly designed class:

```csharp
class ShoppingCart
{
    public List<string> Items;
    public decimal TotalPrice;
    public int MaxItems = 10;
    public string CustomerName;
}
```

### Part 2: Identify Invariants

Determine what rules should always be true:
- Total price should equal sum of item prices
- Number of items should never exceed MaxItems
- Customer name should not be empty
- Items list should never be null

### Part 3: Redesign with Encapsulation

Create a new version that:
- Makes all fields private
- Exposes only necessary properties (with validation)
- Provides methods for safe operations
- Enforces all invariants
- Prevents invalid states

### Part 4: Add Safe Operations

Implement these methods:
- `AddItem(string item, decimal price)` - adds item if cart not full
- `RemoveItem(string item)` - removes specific item
- `Clear()` - removes all items
- `GetItems()` - returns read-only view of items

## Requirements

Your program must:

1. Create a `ShoppingCart` class with:
   - Private fields for all internal state
   - CustomerName property (validated: not empty)
   - MaxItems property (read-only, set in constructor)
   - ItemCount property (read-only, computed)
   - TotalPrice property (read-only, computed)
   - IsFull property (read-only, computed)

2. Methods with guard clauses:
   - `AddItem(string item, decimal price)` with validation
   - `RemoveItem(string item)` 
   - `Clear()`
   - `GetItemNames()` returning read-only collection

3. Invariants enforced:
   - Customer name never empty
   - Never more than MaxItems
   - TotalPrice always accurate
   - Items list never null
   - Prices always >= 0

4. In `Main()`:
   - Create cart and add items
   - Attempt to add too many items
   - Attempt invalid operations
   - Show that cart maintains valid state

## Expected Output

```
Creating shopping cart for Alice...

Adding items:
✓ Added Laptop ($999.99)
✓ Added Mouse ($29.99)
✓ Added Keyboard ($79.99)

Cart status:
Items: 3/10
Total: $1109.97

Testing cart limits:
✓ Added Monitor ($299.99)
Error: Cannot add item - cart is full (10/10)

Current cart:
Item count: 10
Total price: $2,109.87
Status: Full

Testing invalid operations:
Error: Item name cannot be empty
Error: Price cannot be negative
Error: Customer name cannot be empty

Cart remains valid with 10 items totaling $2,109.87
```

## Hints

### Private Fields and Properties

```csharp
class ShoppingCart
{
    private string customerName;
    private List<(string Name, decimal Price)> items;
    private int maxItems;
    
    public string CustomerName
    {
        get { return customerName; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Customer name is required");
            customerName = value;
        }
    }
    
    public int MaxItems
    {
        get { return maxItems; }
    }
    
    // Computed properties
    public int ItemCount => items.Count;
    public decimal TotalPrice => items.Sum(i => i.Price);
    public bool IsFull => items.Count >= maxItems;
}
```

### Constructor with Validation

```csharp
public ShoppingCart(string customerName, int maxItems = 10)
{
    if (maxItems <= 0)
        throw new ArgumentException("Max items must be positive");
    
    CustomerName = customerName;  // Uses property validation
    this.maxItems = maxItems;
    items = new List<(string, decimal)>();
}
```

### AddItem with Guard Clauses

```csharp
public void AddItem(string itemName, decimal price)
{
    if (string.IsNullOrWhiteSpace(itemName))
        throw new ArgumentException("Item name is required");
    
    if (price < 0)
        throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative");
    
    if (IsFull)
        throw new InvalidOperationException($"Cannot add item - cart is full ({maxItems}/{maxItems})");
    
    items.Add((itemName, price));
}
```

### Returning Read-Only Collection

```csharp
public IReadOnlyList<string> GetItemNames()
{
    return items.Select(i => i.Name).ToList().AsReadOnly();
}
```

## Common Mistakes

**Exposing mutable collections:**
```csharp
// ❌ Wrong - caller can modify internal list
public List<string> GetItems()
{
    return items;
}

// Caller can do: cart.GetItems().Clear();

// ✅ Correct - return read-only view
public IReadOnlyList<string> GetItemNames()
{
    return items.Select(i => i.Name).ToList().AsReadOnly();
}
```

**Not validating in methods:**
```csharp
// ❌ Wrong - no checks
public void AddItem(string item, decimal price)
{
    items.Add((item, price));  // Allows empty names, negative prices!
}

// ✅ Correct - validate everything
public void AddItem(string item, decimal price)
{
    if (string.IsNullOrWhiteSpace(item))
        throw new ArgumentException("Item name is required");
    if (price < 0)
        throw new ArgumentOutOfRangeException();
    if (IsFull)
        throw new InvalidOperationException("Cart is full");
    items.Add((item, price));
}
```

**Computed values as settable properties:**
```csharp
// ❌ Wrong - TotalPrice can get out of sync
public decimal TotalPrice { get; set; }

// ✅ Correct - always computed from items
public decimal TotalPrice => items.Sum(i => i.Price);
```

## Bonus Challenges

1. **Item quantity**: Track quantity per item instead of individual entries
2. **Discount system**: Apply percentage discount to total
3. **Item limits**: Maximum quantity per item type
4. **Category tracking**: Group items by category
5. **Remove by index**: Safe removal of specific item
6. **Price history**: Track total before/after discounts
7. **Validation**: Reject duplicate item names
8. **Statistics**: Average item price, most expensive item

Example bonus output:
```
Shopping Cart for Alice:
Laptop x2 @ $999.99 each = $1,999.98
Mouse x1 @ $29.99 = $29.99
Discount (10%): -$203.00
Total: $1,826.97

Statistics:
Items: 3 (4 units)
Average price: $676.65
Most expensive: Laptop ($999.99)
```

## Reflection

After completing this exercise, consider:
1. What problems did the original design have?
2. How does encapsulation prevent these problems?
3. Why use computed properties instead of storing calculated values?
4. When should you throw exceptions vs. return false?

---

**Time Estimate:** 40 minutes  
**Difficulty:** Medium  
**Type:** Refactoring & Encapsulation

This exercise teaches you to recognize and fix encapsulation violations, a critical skill for maintaining codebases.