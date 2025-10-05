# Exercise 04-01: Fields vs Properties

## Goal

Understand the difference between public fields and properties by converting a field-based class to use properties. You'll learn why properties provide better control and maintainability.

## Background

Public fields expose data directly, making it impossible to add validation or change implementation later. Properties act as controlled gateways that look like fields but behave like methods, giving you flexibility to add logic when needed.

## Instructions

You will convert a `Product` class that uses public fields into one that uses properties, then add validation to demonstrate why properties are superior.

### Part 1: Analyze the Field-Based Class

Study this class that uses public fields:

```csharp
class Product
{
    public string Name;
    public decimal Price;
    public int Stock;
}
```

### Part 2: Convert to Properties

Convert the class to use properties instead:
1. Make all fields private
2. Create public properties for each field
3. Keep the same functionality initially

### Part 3: Add Validation

Add validation logic:
- `Name` cannot be null or empty
- `Price` cannot be negative
- `Stock` cannot be negative

### Part 4: Test Your Implementation

Create test cases that demonstrate:
- Valid values are accepted
- Invalid values are rejected with messages
- Objects cannot enter invalid states

## Requirements

Your program must:

1. Create a `Product` class with:
   - Private fields for name, price, and stock
   - Public properties with appropriate validation
   - Clear error messages for invalid values

2. In `Main()`:
   - Create a Product object
   - Test setting valid values (should work)
   - Test setting invalid values (should be rejected)
   - Display the product information

3. Validation rules:
   - Name: Cannot be null, empty, or whitespace
   - Price: Must be >= 0
   - Stock: Must be >= 0

## Expected Output

```
Testing Product class...

Setting valid values:
Product: Laptop
Price: $999.99
Stock: 15

Testing invalid values:
Error: Name cannot be empty
Error: Price cannot be negative
Error: Stock cannot be negative

Final product state:
Product: Laptop
Price: $999.99
Stock: 15
```

## Hints

### Converting Field to Property

```csharp
// Before: Public field
public string Name;

// After: Property with private backing field
private string name;
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
```

### Testing Invalid Values

```csharp
Product product = new Product();
product.Name = "Laptop";      // Valid
product.Name = "";            // Should show error
product.Price = 999.99m;      // Valid
product.Price = -50;          // Should show error
```

## Common Mistakes

**Keeping fields public:**
```csharp
// ❌ Wrong: Field is still public
public string name;
public string Name { get; set; }

// ✅ Correct: Field is private
private string name;
public string Name { get; set; }
```

**Forgetting to validate:**
```csharp
// ❌ Wrong: No validation
public decimal Price
{
    get { return price; }
    set { price = value; }  // Allows negative prices!
}

// ✅ Correct: Validate before accepting
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
```

## Bonus Challenges

1. **Enhanced validation**: Add a maximum price limit (e.g., $10,000)
2. **Computed property**: Add `IsInStock` property that returns true if Stock > 0
3. **Percentage discount**: Add `DiscountedPrice` property that applies a discount
4. **Stock warnings**: Print warning if stock falls below 5 items

Example bonus output:
```
Product: Laptop
Price: $999.99 (In Stock)
Discounted Price: $799.99
⚠️ Low stock warning: Only 3 items remaining
```

## Reflection

After completing this exercise, consider:
1. What advantages do properties have over public fields?
2. When might you still use a public field (if ever)?
3. How does validation in properties prevent bugs?

---

**Time Estimate:** 25 minutes  
**Difficulty:** Easy  
**Type:** Concept Application

This exercise teaches the fundamental difference between fields and properties. Understanding this distinction is critical for writing professional C# code.