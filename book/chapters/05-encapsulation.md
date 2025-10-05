# Chapter 5 – Encapsulation and Data Protection

In Chapter 4, we learned to use properties with validation. Now we'll formalize these techniques into the professional practice of **encapsulation**—designing classes that enforce business rules and prevent invalid states. You'll learn the patterns that make your code bulletproof.

## Learning Objectives

By the end of this chapter, you will be able to:

- Explain encapsulation and information hiding
- Enforce invariants using guard clauses
- Use read-only properties and `init` accessors
- Understand basic immutability concepts
- Write defensive code that prevents invalid states
- Design classes that cannot be broken by users

## Prerequisites

This chapter assumes you've completed **Chapter 4** and can:

- Create properties with validation
- Use access modifiers (public, private, protected)
- Distinguish between fields and properties
- Add validation to property setters
- Use auto-properties and custom properties appropriately

---

## Mission Brief

In this chapter you will:

- Learn what encapsulation really means
- Discover invariants and why they matter
- Master guard clauses for defensive programming
- Create truly read-only properties
- Understand when objects should be immutable
- Write classes that protect their own integrity

This chapter teaches you to write bulletproof classes that cannot enter invalid states.

---

## Concept Power-Up – What is Encapsulation?

**Encapsulation** means bundling data and methods together while hiding internal details.

Think of a car:
- You use the steering wheel and pedals (public interface)
- You don't mess with the engine internals (hidden implementation)
- The car enforces rules (can't shift into reverse while going forward)

```csharp
// ❌ Without encapsulation - anyone can break the rules
class BankAccount
{
    public decimal Balance;
}

account.Balance = -1000;  // Disaster! Negative balance allowed

// ✅ With encapsulation - rules are enforced
class BankAccount
{
    private decimal balance;
    
    public decimal Balance { get; private set; }
    
    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
            throw new InvalidOperationException("Insufficient funds");
        Balance -= amount;
    }
}
```

> **Key Insight**: Encapsulation isn't just hiding data - it's about enforcing rules so objects can't be broken.

---

## Core Concepts

### Concept 1 – Invariants: Rules That Must Always Be True

An **invariant** is a condition that must always be true for an object.

Examples:
- Bank account balance ≥ 0
- Student grade between 0-100
- Player health between 0-MaxHealth
- Person's age ≥ 0

```csharp
class Student
{
    private int grade;
    
    // Invariant: Grade must be 0-100
    public int Grade
    {
        get { return grade; }
        set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), 
                    "Grade must be between 0 and 100"
                );
            }
            grade = value;
        }
    }
}
```

**Why invariants matter:**
- Prevent impossible states
- Make debugging easier
- Document assumptions in code
- Enable safe assumptions in methods

### Concept 2 – Guard Clauses: Enforcing Rules Early

A **guard clause** checks conditions at the start of methods and rejects invalid inputs.

```csharp
class BankAccount
{
    private decimal balance;
    
    public void Deposit(decimal amount)
    {
        // Guard clause - check validity first
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Deposit amount must be positive"
            );
        }
        
        // Now we know amount is valid
        balance += amount;
    }
    
    public void Withdraw(decimal amount)
    {
        // Multiple guard clauses
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Withdrawal amount must be positive"
            );
        }
        
        if (amount > balance)
        {
            throw new InvalidOperationException(
                $"Insufficient funds. Balance: ${balance}, Requested: ${amount}"
            );
        }
        
        balance -= amount;
    }
}
```

**Guard clause benefits:**
- Fail fast with clear error messages
- Keep main logic clean and readable
- Document requirements explicitly
- Prevent cascading errors

### Concept 3 – Read-Only Properties: Set Once, Never Change

Some properties should never change after object creation:

```csharp
class Player
{
    // Option 1: No setter at all
    public int Id { get; }
    
    // Option 2: Init-only (C# 9+)
    public int AccountNumber { get; init; }
    
    // Option 3: Private setter (can change internally)
    public DateTime CreatedDate { get; private set; }
    
    public Player(int id, int accountNumber)
    {
        Id = id;
        AccountNumber = accountNumber;
        CreatedDate = DateTime.Now;
    }
}
```

**Usage:**
```csharp
Player player = new Player(1, 12345);
Console.WriteLine(player.Id);       // ✅ Can read

player.Id = 2;                      // ❌ ERROR: no setter
player.AccountNumber = 99999;       // ❌ ERROR: init-only
player.CreatedDate = DateTime.Now;  // ❌ ERROR: private setter
```

**When to use:**
- Identity fields (ID, AccountNumber)
- Creation metadata (CreatedDate, Version)
- Configuration that shouldn't change
- Immutable object design

### Concept 4 – Computed Properties: Derived Values

Computed properties calculate values from other properties:

```csharp
class BankAccount
{
    private decimal balance;
    private decimal overdraftLimit = 100;
    
    public decimal Balance
    {
        get { return balance; }
        private set { balance = value; }
    }
    
    // Computed property - no backing field
    public decimal AvailableFunds => Balance + overdraftLimit;
    
    // Another computed property
    public bool IsOverdrawn => Balance < 0;
    
    public bool CanWithdraw(decimal amount)
    {
        return amount <= AvailableFunds;
    }
}
```

**Benefits:**
- Always current (no stale data)
- Can't be set incorrectly
- Clear intent in code
- No storage overhead

### Concept 5 – Constructor Validation: Start Valid, Stay Valid

Objects should be valid from the moment they're created:

```csharp
class BankAccount
{
    public string Owner { get; }
    public decimal Balance { get; private set; }
    
    public BankAccount(string owner, decimal openingBalance)
    {
        // Validate in constructor
        if (string.IsNullOrWhiteSpace(owner))
        {
            throw new ArgumentException(
                "Owner name is required",
                nameof(owner)
            );
        }
        
        if (openingBalance < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(openingBalance),
                "Opening balance cannot be negative"
            );
        }
        
        Owner = owner;
        Balance = openingBalance;
    }
}
```

**Result**: Impossible to create an invalid object!

```csharp
// ✅ Valid - succeeds
var account = new BankAccount("Alice", 100);

// ❌ Invalid - throws exception immediately
var bad = new BankAccount("", -50);  // Caught at creation!
```

---

## Try It – BankAccount with Complete Encapsulation

Let's build a bulletproof bank account:

```csharp
class BankAccount
{
    private decimal balance;
    
    // Read-only identity
    public int AccountId { get; }
    public string Owner { get; }
    
    // Read-only balance from outside
    public decimal Balance
    {
        get { return balance; }
        private set
        {
            if (value < 0)
            {
                throw new InvalidOperationException(
                    "Balance cannot be negative"
                );
            }
            balance = value;
        }
    }
    
    // Computed properties
    public bool IsEmpty => Balance == 0;
    public bool IsActive => Balance > 0;
    
    // Constructor with validation
    public BankAccount(int accountId, string owner, decimal openingBalance)
    {
        if (accountId <= 0)
            throw new ArgumentException("Account ID must be positive");
        
        if (string.IsNullOrWhiteSpace(owner))
            throw new ArgumentException("Owner name is required");
        
        if (openingBalance < 0)
            throw new ArgumentOutOfRangeException("Opening balance cannot be negative");
        
        AccountId = accountId;
        Owner = owner;
        Balance = openingBalance;
    }
    
    // Safe operations with guard clauses
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Deposit amount must be positive"
            );
        }
        
        Balance += amount;
    }
    
    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Withdrawal amount must be positive"
            );
        }
        
        if (amount > Balance)
        {
            throw new InvalidOperationException(
                "Insufficient funds"
            );
        }
        
        Balance -= amount;
    }
}
```

**Test it:**
```csharp
// ✅ Valid usage
var account = new BankAccount(1001, "Alice", 100);
account.Deposit(50);
account.Withdraw(30);
Console.WriteLine($"Balance: ${account.Balance}");  // $120

// ❌ All these fail with clear errors:
account.Deposit(-10);      // Error: amount must be positive
account.Withdraw(200);     // Error: insufficient funds
account.Balance = 1000;    // Error: setter is private
```

> **🏋️ Practice Now**: Complete [Exercise 05-01: Encapsulate This](../../exercises/05-encapsulation/01-encapsulate-this/) to practice adding encapsulation to existing code.

---

## Challenge – Add Transaction Tracking

Enhance your BankAccount to track transactions:

1. Add a private `List<string>` for transaction history
2. Make TransactionCount a read-only property
3. Add private method `LogTransaction(string description)`
4. Call it from Deposit and Withdraw
5. Add `GetRecentTransactions(int count)` method
6. Ensure the list cannot be modified from outside

**Hints:**
```csharp
private List<string> transactions = new List<string>();

public int TransactionCount => transactions.Count;

private void LogTransaction(string description)
{
    transactions.Add($"{DateTime.Now:yyyy-MM-dd HH:mm}: {description}");
}

public void Deposit(decimal amount)
{
    // validation...
    Balance += amount;
    LogTransaction($"Deposited ${amount}");
}
```

> **🏋️ Practice Now**: Complete [Exercise 05-02: Guard Clauses](../../exercises/05-encapsulation/02-guard-clauses/) to master defensive programming.

---

## Mini Project – Build YOUR Student Registry

**Goal:** Create YOUR own student management system with complete encapsulation and invariant enforcement!

This is YOUR project to design and implement. You'll apply all Chapter 5 concepts to build a robust system that cannot be broken by invalid data.

### Why This Project?

Real student information systems must maintain data integrity rigorously. This project teaches you enterprise-level defensive programming patterns.

### Learning Goals

- Enforce multiple invariants simultaneously
- Use guard clauses throughout
- Design read-only and computed properties
- Prevent all invalid states
- Handle complex business rules

### Minimum Requirements

YOUR student registry MUST include:

1. **Student class with:**
   - Student ID (read-only, set once, must be positive)
   - Name (validated: not empty, 2-50 characters)
   - Grade (validated: 0-100 range)
   - Enrollment date (read-only, set in constructor)
   - Methods: UpdateGrade with validation
   - Computed property: IsPassing (grade >= 50)

2. **Registry class with:**
   - Private list of students
   - Add student method (reject null, duplicate IDs)
   - Find student by ID
   - Get passing students
   - Display all students

3. **Invariants to enforce:**
   - No duplicate student IDs
   - All students have valid names
   - Grades always 0-100
   - Cannot add null students
   - Registry maintains sorted order (optional)

4. **User interaction:**
   - Create multiple students
   - Attempt invalid operations
   - Show they're rejected with clear errors
   - Display formatted student list

### Expected Output

```
Creating Student Registry...

Adding students:
✓ Student 1001 (Alice) added successfully
✓ Student 1002 (Bob) added successfully
Error: Student ID must be positive
Error: Duplicate student ID: 1001
Error: Name must be 2-50 characters

Student Registry:
1001: Alice - Grade: 85 (Passing)
1002: Bob - Grade: 45 (Not Passing)

Passing students: 1
Total students: 2

Testing grade updates:
✓ Alice's grade updated to 92
Error: Grade must be between 0 and 100
Alice's grade is still 92 (protected)
```

### Implementation Steps

1. Design Student class with all invariants
2. Add comprehensive validation
3. Create Registry class
4. Implement student management methods
5. Add guard clauses everywhere
6. Test all edge cases
7. Polish and document

### Hints

**Student Class:**
```csharp
class Student
{
    private string name;
    private int grade;
    
    public int Id { get; }
    public DateTime EnrollmentDate { get; }
    
    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name is required");
            if (value.Length < 2 || value.Length > 50)
                throw new ArgumentException("Name must be 2-50 characters");
            name = value;
        }
    }
    
    public int Grade
    {
        get { return grade; }
        private set
        {
            if (value < 0 || value > 100)
                throw new ArgumentOutOfRangeException("Grade must be 0-100");
            grade = value;
        }
    }
    
    public bool IsPassing => Grade >= 50;
    
    public Student(int id, string name, int initialGrade)
    {
        if (id <= 0)
            throw new ArgumentException("ID must be positive");
        
        Id = id;
        Name = name;  // Uses property validation
        Grade = initialGrade;
        EnrollmentDate = DateTime.Now;
    }
    
    public void UpdateGrade(int newGrade)
    {
        Grade = newGrade;  // Uses property validation
    }
}
```

**Registry Class:**
```csharp
class StudentRegistry
{
    private List<Student> students = new List<Student>();
    
    public int StudentCount => students.Count;
    
    public void AddStudent(Student student)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student));
        
        if (students.Any(s => s.Id == student.Id))
            throw new InvalidOperationException($"Duplicate student ID: {student.Id}");
        
        students.Add(student);
    }
    
    public Student FindStudent(int id)
    {
        return students.FirstOrDefault(s => s.Id == id);
    }
    
    public List<Student> GetPassingStudents()
    {
        return students.Where(s => s.IsPassing).ToList();
    }
}
```

### Make It YOUR Own!

Add YOUR creative features:

- **Grade history** - Track all grade changes with dates
- **Attendance tracking** - Add attendance percentage
- **Course enrollment** - Multiple courses per student
- **GPA calculation** - Weighted average across courses
- **Honor roll** - Students with grade ≥ 90
- **Risk students** - Automatic alerts for grade < 50
- **Grade categories** - A, B, C, D, F classification
- **Bulk operations** - Update multiple students
- **Search features** - Find by name, grade range
- **Statistics** - Average grade, pass rate, etc.
- **Export** - Generate reports or CSV files
- **Immutable grades** - Grades can only increase
- **Prerequisites** - Course dependencies

### Self-Assessment Checklist

- [ ] Student class enforces all invariants
- [ ] Constructor validates all parameters
- [ ] Guard clauses in all methods
- [ ] Read-only properties used appropriately
- [ ] Computed properties for derived values
- [ ] Registry prevents duplicate IDs
- [ ] Cannot add invalid students
- [ ] All fields are private
- [ ] Clear, specific error messages
- [ ] Edge cases handled properly

### What Makes a Great Project?

- **Encapsulation** (40%): All invariants enforced, no invalid states possible
- **Code Quality** (30%): Clean guard clauses, appropriate properties, good naming
- **Functionality** (20%): Works reliably, handles all edge cases
- **Creativity** (10%): YOUR unique features and enhancements

> **🚀 Mini Project**: Ready to build something bulletproof? Complete the [Mini Project: Student Registry](../../projects/05-encapsulation/) to create a fully encapsulated system.

---

## Common Mistakes

### Validating After Operation Instead of Before

```csharp
// ❌ Wrong - damage already done
public void Withdraw(decimal amount)
{
    Balance -= amount;
    if (Balance < 0)  // Too late!
        Balance += amount;  // Trying to undo
}

// ✅ Correct - validate first
public void Withdraw(decimal amount)
{
    if (amount > Balance)
        throw new InvalidOperationException("Insufficient funds");
    Balance -= amount;
}
```

### Returning Mutable Collections

```csharp
// ❌ Wrong - caller can modify internal list
private List<Student> students;
public List<Student> GetStudents()
{
    return students;  // Danger!
}

// Caller can do: registry.GetStudents().Clear();

// ✅ Correct - return read-only view or copy
public IReadOnlyList<Student> GetStudents()
{
    return students.AsReadOnly();
}

// Or return a copy
public List<Student> GetStudents()
{
    return new List<Student>(students);
}
```

### Forgetting to Validate in Constructor

```csharp
// ❌ Wrong - object created in invalid state
public Student(int id, string name)
{
    Id = id;
    Name = name;  // No validation!
}

var bad = new Student(-1, "");  // Invalid but succeeds!

// ✅ Correct - validate everything
public Student(int id, string name)
{
    if (id <= 0)
        throw new ArgumentException("ID must be positive");
    if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Name is required");
    
    Id = id;
    Name = name;
}
```

### Using Properties for Validation That Can Fail Silently

```csharp
// ❌ Wrong - silent failure
public int Grade
{
    set
    {
        if (value < 0 || value > 100)
            return;  // Silently ignored!
        grade = value;
    }
}

// ✅ Correct - throw exception for invalid data
public int Grade
{
    set
    {
        if (value < 0 || value > 100)
            throw new ArgumentOutOfRangeException();
        grade = value;
    }
}
```

---

## Hands-On Practice

Practice encapsulation with these exercises:

### Exercise Progression

1. **[Exercise 05-01: Encapsulate This](../../exercises/05-encapsulation/01-encapsulate-this/)** - Add encapsulation to poorly designed class
2. **[Exercise 05-02: Guard Clauses](../../exercises/05-encapsulation/02-guard-clauses/)** - Implement defensive programming
3. **[Exercise 05-03: Read-Only & Computed](../../exercises/05-encapsulation/03-readonly-computed/)** - Master immutability patterns
4. **[Exercise 05-04: Invariants Lab](../../exercises/05-encapsulation/04-invariants-lab/)** - Enforce complex business rules

Complete these in order to build comprehensive encapsulation skills.

> **💡 Tip**: Think about what can go wrong, then prevent it with code!

---

## Why It Matters

Encapsulation is fundamental to professional software development because it:

1. **Prevents bugs** - Invalid states are impossible
2. **Simplifies debugging** - Errors caught at the source
3. **Enables change** - Internal implementation can change freely
4. **Documents intent** - Invariants make requirements explicit
5. **Supports testing** - Well-encapsulated code is easier to test
6. **Builds trust** - Users can't break your objects

Every production system relies on encapsulation. Master it now and you'll write code that's robust, maintainable, and professional.

---

## Checkpoint

You should now be able to:

- Explain what encapsulation means beyond just "hiding data"
- Identify and enforce invariants in your classes
- Write comprehensive guard clauses
- Create truly read-only properties
- Use computed properties for derived values
- Validate data in constructors
- Design classes that cannot enter invalid states

---

## Reflection Questions

1. What's the difference between encapsulation and just making fields private?
2. Why should invariants be checked in the constructor?
3. When should you throw an exception vs. return false?
4. How do guard clauses make your code more maintainable?
5. Can you think of a class where most properties should be read-only?

---

## Next Chapter Preview

In **Chapter 6 – Inheritance and Class Hierarchies**, you'll learn to create class families that share behavior. You'll discover how derived classes extend base classes, when to use inheritance vs. composition, and how to design flexible class hierarchies.

---

## Key Terms

- **Encapsulation**: Bundling data and methods while hiding internal details and enforcing rules
- **Invariant**: A condition that must always be true for an object
- **Guard Clause**: Early validation check that rejects invalid inputs
- **Read-Only Property**: Property that can only be set during object construction
- **Computed Property**: Property calculated from other properties (no backing field)
- **Information Hiding**: Concealing implementation details from users
- **Defensive Programming**: Writing code that anticipates and prevents errors
- **Immutability**: Objects whose state cannot change after creation
- **`init` accessor**: Property setter that only works during object initialization
- **`IReadOnlyList<T>`**: Interface for read-only collection access

---

**Teacher Notes:**

**Pedagogical Goals:**
- Move from "properties are nice" to "encapsulation is essential"
- Emphasize fail-fast principle (guard clauses)
- Connect invariants to real-world business rules
- Prepare for inheritance and polymorphism

**Common Student Questions:**
- "Why throw exceptions instead of returning false?" → Exceptions make errors explicit and impossible to ignore
- "When do I use init vs private set?" → init for true immutability, private set when class needs to change it
- "Isn't all this validation overkill?" → Show production bug caused by missing validation

**Assessment Hints:**
- Check that students validate in constructors, not just properties
- Ensure guard clauses come before main logic
- Look for appropriate use of exceptions
- Verify students understand when to use read-only properties

**Connection to Curriculum (PRRPRR02):**
- Core OOP principle (encapsulation)
- Defensive programming patterns
- Error handling foundations
- Design for maintainability

**Classroom Activities:**
- Pair exercise: Find encapsulation violations in code
- Discussion: What invariants exist in a shopping cart?
- Live coding: Add guard clauses to unsafe code
- Code review: Critique property usage and validation

**Rubric Suggestions (Mini Project):**
- Invariants enforced: 35%
- Guard clauses implemented: 25%
- Read-only properties used appropriately: 20%
- Code organization and clarity: 15%
- Error messages helpful: 5%

---

*© C# Kickstart Part 2 Contributors*