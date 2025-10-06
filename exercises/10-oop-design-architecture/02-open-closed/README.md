# Exercise 10-02: Open/Closed Principle

## Goal

Master the Open/Closed Principle by building an extensible discount system. You'll learn how to design code that can be extended with new features without modifying existing, working code.

## Background

The Open/Closed Principle states that classes should be **open for extension** but **closed for modification**. This means you should be able to add new functionality without changing existing code. This prevents bugs in working code and makes systems more maintainable.

The key technique is **polymorphism**—using interfaces and base classes to define extension points.

## Instructions

You will refactor a discount calculator that violates OCP into one that follows it.

### Part 1: Analyze the Problem

Study the provided `DiscountCalculator` class. Every time you add a new discount type, you must:
- Modify the `CalculateDiscount` method
- Add a new if-else branch
- Risk breaking existing discount logic

This violates OCP—the class is NOT closed for modification!

### Part 2: Create Extensible Design

Refactor using these components:

1. **IDiscountStrategy interface** - Defines how discounts work
2. **Concrete discount classes** - Each implements `IDiscountStrategy`
3. **DiscountCalculator** - Uses any `IDiscountStrategy` without knowing the details

### Part 3: Add New Discounts Without Modifying Code

Demonstrate that you can add new discount types (e.g., `SeasonalDiscount`, `BulkDiscount`) without changing `DiscountCalculator` or any existing discount classes.

## Requirements

Your solution must:
1. Create `IDiscountStrategy` interface with `CalculateDiscount` method
2. Implement at least 3 different discount strategies
3. Modify `DiscountCalculator` to use the interface
4. Add a new discount type without modifying existing code
5. Demonstrate all discounts working correctly
6. Show that the system is now open for extension, closed for modification

## Expected Output

```
=== Discount System ===

Original Price: $100

Applying Percentage Discount (20%):
Discount: $20.00
Final Price: $80.00

Applying Fixed Amount Discount ($15):
Discount: $15.00
Final Price: $85.00

Applying Buy One Get One Discount:
Discount: $50.00
Final Price: $50.00

Applying NEW Seasonal Discount (Summer 30% off):
Discount: $30.00
Final Price: $70.00

=== OCP Benefits ===
✓ Added SeasonalDiscount without modifying DiscountCalculator
✓ Added BulkDiscount without modifying other discount classes
✓ Each discount type is independent and testable
✓ No risk of breaking existing discounts when adding new ones

System is OPEN for extension, CLOSED for modification!
```

## Starter Code

```csharp
// ❌ This violates OCP - adding new discounts requires modifying this class
class DiscountCalculator
{
    public double CalculateDiscount(double price, string discountType, double value)
    {
        if (discountType == "Percentage")
        {
            return price * (value / 100);
        }
        else if (discountType == "FixedAmount")
        {
            return value;
        }
        else if (discountType == "BuyOneGetOne")
        {
            return price / 2;
        }
        // Every new discount type requires modifying this method!
        // This violates OCP
        
        return 0;
    }
    
    public double ApplyDiscount(double price, string discountType, double value)
    {
        double discount = CalculateDiscount(price, discountType, value);
        return price - discount;
    }
}
```

## Hints

**IDiscountStrategy interface:**
```csharp
interface IDiscountStrategy
{
    double CalculateDiscount(double price);
    string GetDescription();
}
```

**Percentage discount implementation:**
```csharp
class PercentageDiscount : IDiscountStrategy
{
    private double percentage;
    
    public PercentageDiscount(double percentage)
    {
        this.percentage = percentage;
    }
    
    public double CalculateDiscount(double price)
    {
        return price * (percentage / 100);
    }
    
    public string GetDescription()
    {
        return $"Percentage Discount ({percentage}%)";
    }
}
```

**Refactored DiscountCalculator:**
```csharp
class DiscountCalculator
{
    public double ApplyDiscount(double price, IDiscountStrategy strategy)
    {
        double discount = strategy.CalculateDiscount(price);
        Console.WriteLine($"\nApplying {strategy.GetDescription()}:");
        Console.WriteLine($"Discount: ${discount:F2}");
        double finalPrice = price - discount;
        Console.WriteLine($"Final Price: ${finalPrice:F2}");
        return finalPrice;
    }
}
```

**Usage example:**
```csharp
DiscountCalculator calculator = new DiscountCalculator();
double price = 100;

// Use different strategies
calculator.ApplyDiscount(price, new PercentageDiscount(20));
calculator.ApplyDiscount(price, new FixedAmountDiscount(15));
calculator.ApplyDiscount(price, new BuyOneGetOneDiscount());

// Add NEW discount without modifying DiscountCalculator!
calculator.ApplyDiscount(price, new SeasonalDiscount("Summer", 30));
```

## Common Mistakes to Avoid

- ❌ Using if-else chains for different behaviors (violates OCP)
- ❌ Modifying existing classes to add new features
- ❌ Not using interfaces for extensibility
- ❌ Tight coupling between calculator and discount logic

## Bonus Challenge

Extend your discount system with advanced features:

1. **Composite Discounts** - Combine multiple discounts
   ```csharp
   class CompositeDiscount : IDiscountStrategy
   {
       private List<IDiscountStrategy> discounts;
       
       public void AddDiscount(IDiscountStrategy discount)
       {
           discounts.Add(discount);
       }
       
       public double CalculateDiscount(double price)
       {
           // Apply all discounts sequentially
       }
   }
   ```

2. **Conditional Discounts** - Apply only if conditions are met
   ```csharp
   class ConditionalDiscount : IDiscountStrategy
   {
       private IDiscountStrategy discount;
       private Func<double, bool> condition;
       
       public double CalculateDiscount(double price)
       {
           return condition(price) ? discount.CalculateDiscount(price) : 0;
       }
   }
   ```

3. **Time-Based Discounts** - Different discounts based on time
   ```csharp
   class HappyHourDiscount : IDiscountStrategy
   {
       public double CalculateDiscount(double price)
       {
           int hour = DateTime.Now.Hour;
           if (hour >= 14 && hour <= 16)  // 2PM - 4PM
           {
               return price * 0.25;  // 25% off during happy hour
           }
           return 0;
       }
   }
   ```

4. **Loyalty Tier Discounts** - Based on customer status
   ```csharp
   class LoyaltyDiscount : IDiscountStrategy
   {
       private string tier;  // "Bronze", "Silver", "Gold"
       
       public double CalculateDiscount(double price)
       {
           switch (tier)
           {
               case "Gold": return price * 0.20;
               case "Silver": return price * 0.10;
               case "Bronze": return price * 0.05;
               default: return 0;
           }
       }
   }
   ```

Example bonus output:
```
=== Advanced Discount System ===

Original Price: $100

Applying Composite Discount:
  - Percentage 10%: $10
  - Fixed Amount: $5
  Total Discount: $15.00
  Final Price: $85.00

Applying Happy Hour Discount (2PM-4PM):
Current Time: 3:15 PM
Discount: $25.00 (25% Happy Hour Special!)
Final Price: $75.00

Applying Loyalty Discount (Gold Member):
Discount: $20.00
Final Price: $80.00

All new discounts added WITHOUT modifying existing code!
```

## What You're Learning

- How to design systems that are extensible without modification
- Using interfaces to define extension points
- The power of polymorphism for flexibility
- Strategy pattern for swappable behaviors
- How OCP prevents bugs in working code
- Building systems that grow gracefully

## Reflection Questions

After completing this exercise, consider:

1. Why is it dangerous to modify working code when adding features?
2. How does the interface make the system extensible?
3. What would happen if you needed 20 different discount types in the old design?
4. How does OCP make testing easier?
5. Can you think of other systems where OCP would be valuable?

## Real-World Applications

The Open/Closed Principle is used everywhere:

- **Payment processors** - Add new payment methods without changing checkout
- **File exporters** - Support new file formats without modifying export logic
- **Logging systems** - Add new log destinations without changing log calls
- **Plugin architectures** - Add features without modifying core application
- **Game systems** - Add new character abilities, items, or enemies

---

**Time Estimate:** 45 minutes  
**Difficulty:** Medium  
**Type:** Design Principle - OCP

**Next:** After completing this exercise, move to [Exercise 10-03: Dependency Inversion](../03-dependency-inversion/) to learn about loose coupling through interfaces.