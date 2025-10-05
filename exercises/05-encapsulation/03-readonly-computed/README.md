# Exercise 05-03: Read-Only & Computed Properties

## Goal

Master immutability patterns using read-only and computed properties. You'll learn when properties should never change, how to compute values on-the-fly, and why immutability makes code safer.

## Background

Some data should never change after object creation (like an ID or creation date). Other values should always be calculated from existing data (like age from birth date). This exercise teaches you to design classes with appropriate immutability.

## Instructions

You will create a `Person` class that demonstrates various immutability patterns and computed properties. The class should have unchangeable identity information and dynamically computed values.

### Part 1: Identify Immutable Properties

Determine what should never change:
- Person ID (set once, never changes)
- Birth date (set once, never changes)
- Creation timestamp (set in constructor)
- Social security number (set once, read-only)

### Part 2: Identify Computed Properties

Determine what should be calculated:
- Age (computed from birth date)
- Is adult (computed from age)
- Days since creation (computed from timestamp)
- Full name (computed from first + last name)

### Part 3: Implement Three Immutability Patterns

Learn three ways to make properties read-only:

**Pattern 1: No setter at all**
```csharp
public int Id { get; }  // Can only set in constructor
```

**Pattern 2: Init-only setter**
```csharp
public string SocialSecurity { get; init; }  // Set during object creation
```

**Pattern 3: Private setter**
```csharp
public DateTime CreatedAt { get; private set; }  // Class can change, users cannot
```

### Part 4: Add Computed Properties

Create properties that derive values:
- Age from BirthDate
- IsAdult from Age
- YearsRegistered from CreatedAt
- DisplayName from FirstName + LastName

## Requirements

Your program must:

1. Create a `Person` class with:
   - Id (read-only, set in constructor, validated positive)
   - BirthDate (read-only, set in constructor, validated not future)
   - CreatedAt (read-only, set in constructor to now)
   - FirstName (changeable, validated)
   - LastName (changeable, validated)
   - SocialSecurity (init-only, validated format)

2. Computed properties:
   - Age (int) - years old based on birth date
   - IsAdult (bool) - true if age >= 18
   - FullName (string) - "FirstName LastName"
   - YearsRegistered (int) - years since CreatedAt
   - NextBirthday (DateTime) - date of next birthday

3. Validation:
   - Id must be positive
   - BirthDate cannot be in future
   - Names cannot be empty
   - SocialSecurity must match pattern (simplified: XXX-XX-XXXX)

4. In `Main()`:
   - Create persons with valid data
   - Display all properties including computed ones
   - Attempt to modify read-only properties (show compile errors)
   - Update changeable properties
   - Show computed properties update automatically

## Expected Output

```
Creating people...

Person 1:
ID: 1001
Name: Alice Johnson
Social Security: 123-45-6789
Birth Date: 1995-05-15
Age: 29 years
Status: Adult
Created: 2024-01-15
Years Registered: 1
Next Birthday: 2024-05-15

Person 2:
ID: 1002
Name: Bob Smith
Age: 16 years  
Status: Minor
Next Birthday: 2024-08-20 (in 228 days)

Testing immutability:
// person1.Id = 2000;           // ❌ Compile error: no setter
// person1.BirthDate = ...;     // ❌ Compile error: no setter
// person1.CreatedAt = ...;     // ❌ Compile error: private setter
// person1.SocialSecurity = ... // ❌ Compile error: init-only

Testing computed properties:
Alice renamed to Alice Williams
Full name automatically updated: Alice Williams
Age still computed correctly: 29 years
```

## Hints

### Read-Only Properties

```csharp
class Person
{
    // Pattern 1: No setter (only in constructor)
    public int Id { get; }
    public DateTime BirthDate { get; }
    
    // Pattern 2: Init-only
    public string SocialSecurity { get; init; }
    
    // Pattern 3: Private setter (class can modify)
    public DateTime CreatedAt { get; private set; }
    
    public Person(int id, DateTime birthDate, string ssn)
    {
        if (id <= 0)
            throw new ArgumentException("ID must be positive");
        
        if (birthDate > DateTime.Now)
            throw new ArgumentException("Birth date cannot be in future");
        
        Id = id;
        BirthDate = birthDate;
        SocialSecurity = ssn;
        CreatedAt = DateTime.Now;
    }
}
```

### Computed Properties

```csharp
// Computed from BirthDate
public int Age
{
    get
    {
        var today = DateTime.Today;
        int age = today.Year - BirthDate.Year;
        if (BirthDate.Date > today.AddYears(-age))
            age--;
        return age;
    }
}

// Or using expression body syntax
public bool IsAdult => Age >= 18;

public string FullName => $"{FirstName} {LastName}";

public int YearsRegistered
{
    get
    {
        var span = DateTime.Now - CreatedAt;
        return (int)(span.TotalDays / 365.25);
    }
}

public DateTime NextBirthday
{
    get
    {
        var today = DateTime.Today;
        var next = new DateTime(today.Year, BirthDate.Month, BirthDate.Day);
        if (next < today)
            next = next.AddYears(1);
        return next;
    }
}
```

### Init-Only Pattern

```csharp
// Can set during object initialization
var person = new Person(1001, new DateTime(1995, 5, 15), "123-45-6789")
{
    FirstName = "Alice",
    LastName = "Johnson"
};

// But not after
person.SocialSecurity = "999-99-9999";  // ❌ ERROR: init-only
```

## Common Mistakes

**Storing computed values:**
```csharp
// ❌ Wrong - age gets stale
private int age;
public int Age
{
    get { return age; }
}

public Person(DateTime birthDate)
{
    BirthDate = birthDate;
    age = DateTime.Now.Year - birthDate.Year;  // Never updates!
}

// ✅ Correct - always fresh
public int Age
{
    get
    {
        var today = DateTime.Today;
        int age = today.Year - BirthDate.Year;
        if (BirthDate.Date > today.AddYears(-age))
            age--;
        return age;
    }
}
```

**Making ID changeable:**
```csharp
// ❌ Wrong - ID can change
public int Id { get; set; }

person.Id = 9999;  // Identity crisis!

// ✅ Correct - ID is immutable
public int Id { get; }

public Person(int id)
{
    Id = id;  // Can only set here
}
```

**Not validating immutable properties:**
```csharp
// ❌ Wrong - accepts invalid data permanently
public Person(int id, DateTime birthDate)
{
    Id = id;  // Could be negative!
    BirthDate = birthDate;  // Could be future!
}

// ✅ Correct - validate in constructor
public Person(int id, DateTime birthDate)
{
    if (id <= 0)
        throw new ArgumentException("ID must be positive");
    if (birthDate > DateTime.Now)
        throw new ArgumentException("Birth date cannot be in future");
    
    Id = id;
    BirthDate = birthDate;
}
```

## Bonus Challenges

1. **Zodiac sign**: Compute astrological sign from birth date
2. **Generation**: Compute generation (Gen Z, Millennial, etc.)
3. **Leap year birthday**: Special handling for Feb 29
4. **Time until birthday**: Days/hours/minutes countdown
5. **Birth day of week**: What day of week they were born
6. **Chinese zodiac**: Year animal based on birth year
7. **Life milestones**: Key ages (voting, retirement, etc.)
8. **Bio summary**: Generated description of person

Example bonus output:
```
Person Profile:
Name: Alice Johnson
ID: 1001
Birth: May 15, 1995 (Thursday)
Age: 29 years, 3 months, 8 days
Zodiac: Taurus ♉
Chinese Zodiac: Pig 🐷
Generation: Millennial
Status: Adult (can vote, drive, drink)

Next Birthday: May 15, 2025 (in 127 days)
Countdown: 127 days, 5 hours, 23 minutes

Bio: 29-year-old Millennial, born in 1995, 
registered member for 1 year.
```

## Reflection

After completing this exercise, consider:
1. Why use computed properties instead of storing calculated values?
2. When should a property be truly immutable (no setter)?
3. When should you use init vs. no setter?
4. How does immutability make code more predictable?
5. What's the performance impact of computed properties?

---

**Time Estimate:** 35 minutes  
**Difficulty:** Medium  
**Type:** Immutability Patterns

This exercise teaches you to design classes with appropriate immutability and computed values, crucial for maintainable code.