# Exercise 04-03: Property Validation

## Goal

Learn to add comprehensive validation to properties to prevent objects from entering invalid states. You'll practice defensive programming by rejecting bad data at the property boundary.

## Background

Validation in property setters is your first line of defense against bugs. By checking values before accepting them, you ensure objects remain in valid states throughout their lifetime.

## Instructions

You will create a `Temperature` class that validates temperature readings and prevents physically impossible or unrealistic values.

### Part 1: Create the Temperature Class

Create a class that stores temperature with these properties:
- `Celsius` - temperature in Celsius (validated)
- `LocationName` - where the reading was taken (validated)
- `Timestamp` - when the reading was taken (read-only)

### Part 2: Add Validation Rules

Implement these validation rules:

**Celsius property:**
- Minimum: -273.15 (absolute zero)
- Maximum: 100 (for this exercise)
- Reject values outside this range with clear error messages

**LocationName property:**
- Cannot be null, empty, or whitespace
- Minimum length: 3 characters
- Maximum length: 50 characters

**Timestamp property:**
- Set once in constructor
- Cannot be modified after creation

### Part 3: Add Computed Properties

Add read-only computed properties:
- `Fahrenheit` - converts Celsius to Fahrenheit
- `Kelvin` - converts Celsius to Kelvin

### Part 4: Test Validation

Create test cases that verify:
- Valid values are accepted
- Invalid values are rejected
- Objects maintain valid state even when given bad data
- Computed properties calculate correctly

## Requirements

Your program must:

1. Create a `Temperature` class with:
   - Validated `Celsius` property (range: -273.15 to 100)
   - Validated `LocationName` property (length: 3-50 chars)
   - Read-only `Timestamp` property
   - Computed `Fahrenheit` property
   - Computed `Kelvin` property

2. In `Main()`:
   - Create temperature objects with valid data
   - Attempt to set invalid values
   - Verify values are rejected with error messages
   - Display temperature in all three scales

3. Error handling:
   - Clear, specific error messages for each validation rule
   - Invalid values should not change the property
   - Object should remain in valid state

## Expected Output

```
Creating temperature readings...

Stockholm Temperature:
Location: Stockholm
Celsius: 22.00°C
Fahrenheit: 71.60°F
Kelvin: 295.15K
Recorded: 2024-01-15 14:30:00

Testing validation...
Error: Temperature cannot be below -273.15°C (absolute zero)
Error: Temperature cannot exceed 100°C
Error: Location name must be at least 3 characters
Error: Location name cannot exceed 50 characters

Stockholm temperature is still 22.00°C (valid state maintained)
```

## Hints

### Temperature Validation

```csharp
private double celsius;
public double Celsius
{
    get { return celsius; }
    set
    {
        if (value < -273.15)
        {
            Console.WriteLine("Error: Temperature cannot be below -273.15°C (absolute zero)");
            return;
        }
        if (value > 100)
        {
            Console.WriteLine("Error: Temperature cannot exceed 100°C");
            return;
        }
        celsius = value;
    }
}
```

### String Validation

```csharp
private string locationName;
public string LocationName
{
    get { return locationName; }
    set
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Console.WriteLine("Error: Location name is required");
            return;
        }
        if (value.Length < 3)
        {
            Console.WriteLine("Error: Location name must be at least 3 characters");
            return;
        }
        if (value.Length > 50)
        {
            Console.WriteLine("Error: Location name cannot exceed 50 characters");
            return;
        }
        locationName = value;
    }
}
```

### Computed Properties

```csharp
// Celsius to Fahrenheit: F = C * 9/5 + 32
public double Fahrenheit => celsius * 9.0 / 5.0 + 32.0;

// Celsius to Kelvin: K = C + 273.15
public double Kelvin => celsius + 273.15;
```

### Constructor with Validation

```csharp
public Temperature(string location, double celsius)
{
    LocationName = location;  // Uses property setter (validated)
    Celsius = celsius;        // Uses property setter (validated)
    Timestamp = DateTime.Now;
}
```

## Common Mistakes

**Not handling all edge cases:**
```csharp
// ❌ Wrong: Missing null check
public string LocationName
{
    set
    {
        if (value.Length < 3)  // Crashes if value is null!
            return;
        locationName = value;
    }
}

// ✅ Correct: Check null first
public string LocationName
{
    set
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Console.WriteLine("Error: Location name is required");
            return;
        }
        if (value.Length < 3 || value.Length > 50)
        {
            Console.WriteLine("Error: Location name must be 3-50 characters");
            return;
        }
        locationName = value;
    }
}
```

**Forgetting to reject invalid values:**
```csharp
// ❌ Wrong: Prints error but still accepts value
set
{
    if (value < -273.15)
        Console.WriteLine("Error: Too cold");
    celsius = value;  // STILL ASSIGNS IT!
}

// ✅ Correct: Return without assigning
set
{
    if (value < -273.15)
    {
        Console.WriteLine("Error: Too cold");
        return;  // Don't assign
    }
    celsius = value;
}
```

**Using field in computed property instead of property:**
```csharp
// ❌ Wrong: Direct field access bypasses validation
public double Fahrenheit => celsius * 9.0 / 5.0 + 32.0;

// ✅ Better: Use property (though same in this case)
public double Fahrenheit => Celsius * 9.0 / 5.0 + 32.0;
```

## Bonus Challenges

1. **Range warnings**: Print warning (not error) if temperature is unusual but valid (e.g., < -50°C or > 50°C)
2. **Description property**: Add computed property that returns "Freezing", "Cold", "Mild", "Warm", "Hot"
3. **Comfort index**: Add property that calculates if temperature is in comfortable range (18-24°C)
4. **Historical comparison**: Track min/max temperatures seen
5. **Multiple locations**: Extend to track multiple readings with average calculation

Example bonus output:
```
Stockholm Temperature: 22.00°C (Comfortable) 😊
Dubai Temperature: 45.00°C (Hot) ⚠️ Extreme heat warning!
Antarctica Temperature: -60.00°C (Freezing) ⚠️ Extreme cold warning!

Average temperature: 2.33°C
```

## Reflection

After completing this exercise, consider:
1. Why is it better to validate in the setter rather than in methods?
2. What happens if you forget to return after detecting an error?
3. How does validation help with debugging?
4. What validation rules would you add for a real weather application?

---

**Time Estimate:** 35 minutes  
**Difficulty:** Medium  
**Type:** Defensive Programming Practice

This exercise teaches comprehensive validation patterns used throughout professional C# development.