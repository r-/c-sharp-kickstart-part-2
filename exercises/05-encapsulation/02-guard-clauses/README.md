# Exercise 05-02: Guard Clauses

## Goal

Master defensive programming by adding comprehensive guard clauses to methods. You'll learn to validate inputs, check preconditions, and fail fast with clear error messages.

## Background

Guard clauses are the first line of defense against invalid data. They check conditions at the start of methods and immediately reject bad inputs before any processing occurs. This prevents cascading errors and makes debugging easier.

## Instructions

You will add guard clauses to an unsafe `Ticket` class for an event management system. The current implementation allows invalid operations that break the system.

### Part 1: Analyze the Unsafe Code

Study this dangerous class:

```csharp
class Ticket
{
    public string EventName;
    public decimal Price;
    public int AvailableSeats;
    
    public void BookSeats(int count)
    {
        AvailableSeats -= count;
    }
    
    public void ApplyDiscount(decimal percentage)
    {
        Price = Price * (1 - percentage / 100);
    }
}
```

### Part 2: Identify Required Guards

Determine what needs to be checked:
- EventName should not be empty
- Price should be positive
- AvailableSeats should not go negative
- Booking count should be positive and not exceed available
- Discount percentage should be 0-100

### Part 3: Add Comprehensive Guard Clauses

Transform the class to:
- Validate all parameters in every method
- Check all preconditions before processing
- Throw appropriate exceptions with clear messages
- Make invalid states impossible

### Part 4: Test Your Guards

Create test cases that verify:
- Valid operations succeed
- Invalid operations are rejected immediately
- Error messages are clear and helpful
- Object state remains valid even after errors

## Requirements

Your program must:

1. Create a `Ticket` class with:
   - EventName property (validated: not empty, 3-100 chars)
   - Price property (validated: positive)
   - AvailableSeats property (validated: non-negative)
   - Constructor with validation
   - Methods with comprehensive guard clauses

2. Methods with guards:
   - `BookSeats(int count)` - checks positive count, availability
   - `CancelSeats(int count)` - checks positive count
   - `ApplyDiscount(decimal percentage)` - checks 0-100 range
   - `ChangePrice(decimal newPrice)` - checks positive

3. Guard clause requirements:
   - Check parameter validity first
   - Check object state preconditions
   - Throw specific exception types
   - Provide detailed error messages
   - Maintain object validity on error

4. In `Main()`:
   - Create valid ticket
   - Perform valid operations
   - Attempt each type of invalid operation
   - Verify object stays in valid state

## Expected Output

```
Creating ticket for 'Tech Conference 2024'...
Initial state: 100 seats @ $50.00

Valid operations:
✓ Booked 25 seats
✓ Applied 10% discount
Current state: 75 seats @ $45.00

Testing guard clauses:

Booking validation:
Error: Seat count must be positive (got: -5)
Error: Cannot book 100 seats - only 75 available

Discount validation:
Error: Discount percentage must be between 0 and 100 (got: 150)
Error: Discount percentage must be between 0 and 100 (got: -10)

Price validation:
Error: Price must be positive (got: -20)
Error: Price must be positive (got: 0)

Event name validation:
Error: Event name is required
Error: Event name must be 3-100 characters (got: 2)

Final state (protected): 75 seats @ $45.00
```

## Hints

### Guard Clause Pattern

```csharp
public void BookSeats(int count)
{
    // Guard clauses at the start
    if (count <= 0)
    {
        throw new ArgumentOutOfRangeException(
            nameof(count),
            $"Seat count must be positive (got: {count})"
        );
    }
    
    if (count > AvailableSeats)
    {
        throw new InvalidOperationException(
            $"Cannot book {count} seats - only {AvailableSeats} available"
        );
    }
    
    // Main logic - we know inputs are valid
    AvailableSeats -= count;
}
```

### Constructor Validation

```csharp
public Ticket(string eventName, decimal price, int availableSeats)
{
    // Validate event name
    if (string.IsNullOrWhiteSpace(eventName))
        throw new ArgumentException("Event name is required", nameof(eventName));
    
    if (eventName.Length < 3 || eventName.Length > 100)
        throw new ArgumentException("Event name must be 3-100 characters", nameof(eventName));
    
    // Validate price
    if (price <= 0)
        throw new ArgumentOutOfRangeException(nameof(price), "Price must be positive");
    
    // Validate seats
    if (availableSeats < 0)
        throw new ArgumentOutOfRangeException(nameof(availableSeats), "Seats cannot be negative");
    
    EventName = eventName;
    Price = price;
    AvailableSeats = availableSeats;
}
```

### Property with Validation

```csharp
private string eventName;

public string EventName
{
    get { return eventName; }
    private set
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Event name is required");
        
        if (value.Length < 3 || value.Length > 100)
            throw new ArgumentException("Event name must be 3-100 characters");
        
        eventName = value;
    }
}
```

### Multiple Guard Clauses

```csharp
public void ApplyDiscount(decimal percentage)
{
    // Guard 1: Check range minimum
    if (percentage < 0)
    {
        throw new ArgumentOutOfRangeException(
            nameof(percentage),
            $"Discount percentage must be between 0 and 100 (got: {percentage})"
        );
    }
    
    // Guard 2: Check range maximum
    if (percentage > 100)
    {
        throw new ArgumentOutOfRangeException(
            nameof(percentage),
            $"Discount percentage must be between 0 and 100 (got: {percentage})"
        );
    }
    
    // Main logic
    Price = Price * (1 - percentage / 100);
}
```

## Common Mistakes

**Checking after operation:**
```csharp
// ❌ Wrong - damage already done
public void BookSeats(int count)
{
    AvailableSeats -= count;
    if (AvailableSeats < 0)  // Too late!
        AvailableSeats += count;  // Trying to undo
}

// ✅ Correct - check first
public void BookSeats(int count)
{
    if (count > AvailableSeats)
        throw new InvalidOperationException();
    AvailableSeats -= count;
}
```

**Generic error messages:**
```csharp
// ❌ Wrong - unhelpful
if (count <= 0)
    throw new Exception("Invalid");

// ✅ Correct - specific and helpful
if (count <= 0)
    throw new ArgumentOutOfRangeException(
        nameof(count),
        $"Seat count must be positive (got: {count})"
    );
```

**Missing precondition checks:**
```csharp
// ❌ Wrong - only checks parameter
public void BookSeats(int count)
{
    if (count <= 0)
        throw new ArgumentException();
    // Missing: check if enough seats available!
    AvailableSeats -= count;
}

// ✅ Correct - check everything
public void BookSeats(int count)
{
    if (count <= 0)
        throw new ArgumentOutOfRangeException();
    if (count > AvailableSeats)
        throw new InvalidOperationException();
    AvailableSeats -= count;
}
```

**Wrong exception types:**
```csharp
// ❌ Wrong - ArgumentException for state check
if (count > AvailableSeats)
    throw new ArgumentException();

// ✅ Correct - InvalidOperationException for state
if (count > AvailableSeats)
    throw new InvalidOperationException();
```

## Bonus Challenges

1. **Booking ID system**: Add unique ID validation for bookings
2. **Date validation**: Add event date, reject past dates
3. **Tiered pricing**: Validate different price categories
4. **Seat reservation**: Add time-limited reservations
5. **Cancellation policy**: Add rules for cancellation deadlines
6. **Bulk operations**: Guard clauses for batch booking
7. **Capacity limits**: Add maximum capacity validation
8. **Price history**: Track and validate price changes

Example bonus output:
```
Ticket: Tech Conference 2024 (Date: 2024-12-15)
Price: $50.00 (Early Bird - expires 2024-10-01)
Available: 75/100 seats
Reserved: 20 seats (release in: 15 minutes)

Error: Cannot book past event
Error: Cannot apply discount - early bird period ended
Error: Cancellation deadline passed (24 hours before event)
```

## Reflection

After completing this exercise, consider:
1. Why check parameters before state conditions?
2. When should you use ArgumentException vs InvalidOperationException?
3. How do clear error messages help debugging?
4. What happens if you forget a guard clause?

---

**Time Estimate:** 35 minutes  
**Difficulty:** Medium  
**Type:** Defensive Programming

This exercise teaches you to write robust, fail-fast code that prevents errors before they cause problems.