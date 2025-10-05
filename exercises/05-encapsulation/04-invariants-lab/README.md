# Exercise 05-04: Invariants Lab

## Goal

Practice identifying and enforcing complex business rules (invariants) in a real-world scenario. You'll learn to design classes that maintain multiple invariants simultaneously and prevent all invalid states.

## Background

Business rules are constraints that must always be true for a system to work correctly. In object-oriented programming, these rules become invariants - conditions that objects must maintain throughout their lifetime. This exercise simulates a real business domain with multiple interrelated rules.

## Instructions

You will create an `Order` class for an e-commerce system that enforces multiple business rules. The system must prevent orders from entering invalid states while allowing valid operations.

### Part 1: Understand the Business Rules

Your order system must enforce these invariants:

1. **Order ID**: Must be positive, cannot change
2. **Order status**: Must progress logically (Pending → Processing → Shipped → Delivered)
3. **Items**: Minimum 1 item, maximum 50 items
4. **Total price**: Must equal sum of item prices (always accurate)
5. **Shipping cost**: Free if total > $100, otherwise $10
6. **Customer**: Cannot be empty or change after placement
7. **Discounts**: 0-50% only, cannot exceed item total
8. **Status transitions**: Cannot ship before processing, cannot cancel after shipped

### Part 2: Design the Class

Create properties and methods that:
- Enforce each invariant
- Prevent invalid state transitions
- Maintain computed values automatically
- Validate all operations

### Part 3: Implement Invariant Checks

Add validation for:
- Constructor parameters
- State transitions
- Item operations (add/remove)
- Price calculations
- Discount applications

### Part 4: Test All Invariants

Create comprehensive tests:
- Valid operations succeed
- Each invariant violation is caught
- Multiple invariants are enforced together
- Object stays valid even after errors

## Requirements

Your program must:

1. Create an `Order` class with:
   - OrderId (read-only, positive)
   - Customer (read-only, validated)
   - Status enum (Pending, Processing, Shipped, Delivered, Cancelled)
   - Items list (1-50 items)
   - ItemTotal (computed from items)
   - DiscountAmount (0-50% of ItemTotal)
   - ShippingCost (computed: free if > $100)
   - FinalTotal (computed: ItemTotal - Discount + Shipping)

2. Methods with invariant checks:
   - `AddItem(string name, decimal price)` - validates item limit
   - `RemoveItem(string name)` - validates minimum items
   - `ApplyDiscount(decimal percentage)` - validates range
   - `UpdateStatus(OrderStatus newStatus)` - validates transitions
   - `Cancel()` - validates can cancel

3. Invariants enforced:
   - Order ID positive and immutable
   - Customer name not empty and immutable
   - Status transitions follow valid sequence
   - Item count between 1-50
   - All prices non-negative
   - Discount within 0-50%
   - Totals always accurate
   - Cannot modify shipped orders

4. In `Main()`:
   - Create order with items
   - Progress through valid status changes
   - Calculate totals correctly
   - Reject all invalid operations
   - Show comprehensive error messages

## Expected Output

```
Creating order #1001 for Alice...

Adding items:
✓ Added Laptop ($999.99)
✓ Added Mouse ($29.99)
✓ Added Keyboard ($79.99)

Order Summary:
Order ID: 1001
Customer: Alice
Status: Pending
Items: 3
Item Total: $1,109.97
Shipping: FREE (order > $100)
Discount: $0.00
Final Total: $1,109.97

Applying 10% discount:
✓ Discount applied: $111.00
Final Total: $998.97

Processing order:
✓ Status: Pending → Processing
✓ Status: Processing → Shipped
✓ Status: Shipped → Delivered

Testing invariants:

Item limits:
Error: Cannot add item - order limit is 50 items
Error: Cannot remove item - order must have at least 1 item

Discount validation:
Error: Discount must be between 0 and 50% (got: 75)
Error: Cannot apply discount - order already shipped

Status transitions:
Error: Cannot go from Delivered to Pending
Error: Cannot cancel - order already shipped

Price validation:
Error: Item price cannot be negative
Error: Item price cannot be zero

Order integrity maintained:
Order #1001 remains in valid state: Delivered, 3 items, $998.97
```

## Hints

### Order Status Enum

```csharp
enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}
```

### Class Structure

```csharp
class Order
{
    private List<(string Name, decimal Price)> items;
    private decimal discountPercentage;
    
    // Read-only properties
    public int OrderId { get; }
    public string Customer { get; }
    public OrderStatus Status { get; private set; }
    
    // Computed properties
    public int ItemCount => items.Count;
    
    public decimal ItemTotal => items.Sum(i => i.Price);
    
    public decimal DiscountAmount => ItemTotal * (discountPercentage / 100);
    
    public decimal ShippingCost => ItemTotal > 100 ? 0 : 10;
    
    public decimal FinalTotal => ItemTotal - DiscountAmount + ShippingCost;
    
    public bool CanModify => Status != OrderStatus.Shipped && 
                             Status != OrderStatus.Delivered;
}
```

### Constructor Validation

```csharp
public Order(int orderId, string customer)
{
    // Invariant: OrderId must be positive
    if (orderId <= 0)
        throw new ArgumentException("Order ID must be positive");
    
    // Invariant: Customer cannot be empty
    if (string.IsNullOrWhiteSpace(customer))
        throw new ArgumentException("Customer name is required");
    
    OrderId = orderId;
    Customer = customer;
    Status = OrderStatus.Pending;
    items = new List<(string, decimal)>();
    discountPercentage = 0;
}
```

### AddItem with Multiple Invariants

```csharp
public void AddItem(string name, decimal price)
{
    // Invariant: Cannot modify shipped orders
    if (!CanModify)
        throw new InvalidOperationException("Cannot add items - order already shipped");
    
    // Invariant: Item name required
    if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Item name is required");
    
    // Invariant: Price must be positive
    if (price <= 0)
        throw new ArgumentOutOfRangeException(nameof(price), "Price must be positive");
    
    // Invariant: Maximum 50 items
    if (items.Count >= 50)
        throw new InvalidOperationException("Cannot add item - order limit is 50 items");
    
    items.Add((name, price));
}
```

### Status Transition Validation

```csharp
public void UpdateStatus(OrderStatus newStatus)
{
    // Define valid transitions
    var validTransitions = new Dictionary<OrderStatus, List<OrderStatus>>
    {
        { OrderStatus.Pending, new List<OrderStatus> { OrderStatus.Processing, OrderStatus.Cancelled } },
        { OrderStatus.Processing, new List<OrderStatus> { OrderStatus.Shipped, OrderStatus.Cancelled } },
        { OrderStatus.Shipped, new List<OrderStatus> { OrderStatus.Delivered } },
        { OrderStatus.Delivered, new List<OrderStatus>() },  // Terminal state
        { OrderStatus.Cancelled, new List<OrderStatus>() }   // Terminal state
    };
    
    // Check if transition is valid
    if (!validTransitions[Status].Contains(newStatus))
    {
        throw new InvalidOperationException(
            $"Cannot transition from {Status} to {newStatus}"
        );
    }
    
    Status = newStatus;
}
```

### Discount Validation

```csharp
public void ApplyDiscount(decimal percentage)
{
    // Invariant: Cannot modify shipped orders
    if (!CanModify)
        throw new InvalidOperationException("Cannot apply discount - order already shipped");
    
    // Invariant: Discount must be 0-50%
    if (percentage < 0 || percentage > 50)
        throw new ArgumentOutOfRangeException(
            nameof(percentage),
            $"Discount must be between 0 and 50% (got: {percentage})"
        );
    
    discountPercentage = percentage;
}
```

## Common Mistakes

**Not checking all related invariants:**
```csharp
// ❌ Wrong - only checks item limit
public void AddItem(string name, decimal price)
{
    if (items.Count >= 50)
        throw new InvalidOperationException();
    items.Add((name, price));  // Missing: status check, price check, name check
}

// ✅ Correct - checks everything
public void AddItem(string name, decimal price)
{
    if (!CanModify)
        throw new InvalidOperationException();
    if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException();
    if (price <= 0)
        throw new ArgumentOutOfRangeException();
    if (items.Count >= 50)
        throw new InvalidOperationException();
    items.Add((name, price));
}
```

**Allowing invalid state transitions:**
```csharp
// ❌ Wrong - any transition allowed
public void UpdateStatus(OrderStatus newStatus)
{
    Status = newStatus;  // Can go from Delivered to Pending!
}

// ✅ Correct - validates transitions
public void UpdateStatus(OrderStatus newStatus)
{
    if (!IsValidTransition(Status, newStatus))
        throw new InvalidOperationException();
    Status = newStatus;
}
```

**Storing computed values:**
```csharp
// ❌ Wrong - total gets out of sync
private decimal total;
public decimal Total
{
    get { return total; }
}

public void AddItem(string name, decimal price)
{
    items.Add((name, price));
    total += price;  // What about discounts? Shipping?
}

// ✅ Correct - always computed
public decimal FinalTotal => ItemTotal - DiscountAmount + ShippingCost;
```

## Bonus Challenges

1. **Order history**: Track all status changes with timestamps
2. **Item quantity**: Support multiple quantities of same item
3. **Tax calculation**: Add tax based on location
4. **Payment status**: Track payment separate from order status
5. **Refund support**: Partial or full refunds with rules
6. **Express shipping**: Different shipping tiers and costs
7. **Coupon codes**: Named discounts with expiration
8. **Inventory check**: Validate item availability
9. **Customer tier**: VIP customers get better discounts
10. **Minimum order**: Enforce minimum order value

Example bonus output:
```
Order #1001 (Alice - VIP Customer)
Status: Delivered
Payment: Paid ($998.97)

Items (3):
  Laptop x1 @ $999.99 = $999.99
  Mouse x2 @ $29.99 = $59.98
  Keyboard x1 @ $79.99 = $79.99

Subtotal: $1,139.96
VIP Discount (15%): -$171.00
Tax (8%): $77.52
Shipping (Express): $15.00
Final Total: $1,061.48

Order History:
  2024-01-15 10:00 - Created
  2024-01-15 10:05 - Processing
  2024-01-15 14:30 - Shipped
  2024-01-16 09:15 - Delivered
```

## Reflection

After completing this exercise, consider:
1. How many invariants did you identify?
2. What happens when invariants conflict?
3. Why enforce invariants instead of documenting rules?
4. How do invariants help with debugging?
5. What's the cost of checking invariants?

---

**Time Estimate:** 50 minutes  
**Difficulty:** Medium-Hard  
**Type:** Complex Business Logic

This exercise teaches you to handle real-world complexity with multiple interrelated business rules, preparing you for production systems.