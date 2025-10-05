# Mini Project: Build YOUR Bank Account System

## Project Overview

Create YOUR own banking system with validated data and safe operations! This is YOUR chance to apply properties, validation, and access modifiers to build a realistic financial management program. While there are minimum requirements, you're encouraged to add YOUR creative banking features.

## Why This Project?

This project lets you:
- Apply everything from Chapter 4 in one complete program
- Practice defensive programming with validation
- See how properties protect against invalid states
- Build something that mirrors real-world software
- Make design decisions about data protection and user experience

## Learning Goals

- Use properties with appropriate validation
- Apply access modifiers correctly (public, private)
- Prevent objects from entering invalid states
- Design safe operations that maintain data integrity
- Handle edge cases and errors gracefully

## Minimum Requirements

Your bank account system MUST include these core features:

### 1. BankAccount Class

Create a `BankAccount` class with:
- Owner property (validated: not empty)
- Balance property (read-only from outside, validated: never negative)
- Account ID (read-only, set once in constructor)
- Deposit method with validation
- Withdraw method preventing overdraft
- Display account information method

### 2. Property Requirements

Your properties must:
- Use private fields for internal storage
- Validate data in setters
- Use `private set` where appropriate
- Prevent direct field access
- Reject invalid values with clear error messages

### 3. Validation Rules

Implement these validations:
- Owner name cannot be empty or whitespace
- Balance cannot go negative (reject insufficient withdrawals)
- Transaction amounts must be positive
- Account ID cannot change after creation

### 4. User Interaction

Your program should:
- Create at least two different accounts
- Perform both deposits and withdrawals
- Attempt invalid operations and show they're blocked
- Display formatted account information
- Demonstrate that validation works

## Expected Output Example

```
Creating accounts...
Account #1001 created for Alice

=== Alice's Account ===
ID: 1001
Owner: Alice Johnson
Balance: $150.00

Attempting invalid operations...
Error: Amount must be positive
Error: Cannot withdraw $200.00 - insufficient funds (Balance: $150.00)
Error: Owner name cannot be empty

Final balances:
Alice Johnson (ID: 1001): $150.00
Bob Smith (ID: 1002): $75.00
```

## Implementation Steps

1. **Design BankAccount Class** - Plan fields and properties
2. **Add Properties with Validation** - Implement all required properties
3. **Create Safe Methods** - Add Deposit and Withdraw with validation
4. **Test Validation** - Verify invalid values are rejected
5. **Add Features** - Implement YOUR unique banking features
6. **Polish** - Clean code, add comments, thorough testing

## Hints

### Property with Validation
```csharp
class BankAccount
{
    private string owner;
    
    public string Owner
    {
        get { return owner; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Error: Owner name cannot be empty");
                return;
            }
            owner = value;
        }
    }
}
```

### Read-Only Property (Set Once)
```csharp
public int AccountId { get; init; }

// Or with constructor
private int accountId;
public int AccountId
{
    get { return accountId; }
}

public BankAccount(string owner, int id)
{
    Owner = owner;
    accountId = id;
    Balance = 0;
}
```

### Private Setter for Balance
```csharp
private decimal balance;

public decimal Balance
{
    get { return balance; }
    private set
    {
        if (value < 0)
        {
            Console.WriteLine("Error: Balance cannot be negative");
            return;
        }
        balance = value;
    }
}
```

### Safe Deposit Method
```csharp
public void Deposit(decimal amount)
{
    if (amount <= 0)
    {
        Console.WriteLine("Error: Amount must be positive");
        return;
    }
    
    Balance += amount;
    Console.WriteLine($"Deposited ${amount:F2}. New balance: ${Balance:F2}");
}
```

### Safe Withdraw Method
```csharp
public void Withdraw(decimal amount)
{
    if (amount <= 0)
    {
        Console.WriteLine("Error: Amount must be positive");
        return;
    }
    
    if (amount > Balance)
    {
        Console.WriteLine($"Error: Cannot withdraw ${amount:F2} - insufficient funds (Balance: ${Balance:F2})");
        return;
    }
    
    Balance -= amount;
    Console.WriteLine($"Withdrew ${amount:F2}. New balance: ${Balance:F2}");
}
```

## Make It YOUR Own!

After meeting minimum requirements, add YOUR creative features! Here are ideas:

### Feature Ideas

- 💳 **Account Types** - Checking vs Savings with different rules
- 📜 **Transaction History** - Track all deposits and withdrawals
- 💰 **Interest Calculation** - Add monthly interest to savings accounts
- 🔄 **Transfer Between Accounts** - Move money safely between accounts
- 🛡️ **Overdraft Protection** - Allow small negative balances with fees
- 📊 **Account Limits** - Maximum balance or withdrawal amounts
- 💵 **Transaction Fees** - Charge for certain operations
- 📄 **Statement Printing** - Formatted account summary
- 🌍 **Multiple Currencies** - Handle different currency types
- 🔐 **Security Features** - PIN or password protection
- 📈 **Statistics** - Average transaction, largest deposit/withdrawal
- 🎯 **Savings Goals** - Track progress toward savings target
- ⚠️ **Low Balance Alerts** - Warnings when balance falls below threshold
- 🏦 **Multiple Owners** - Joint accounts

### Design Decisions YOU Make

Think about:
- What makes YOUR bank account system unique?
- How should error messages be formatted?
- Should there be a transaction limit per day?
- How can you make the account display visually appealing?
- What additional account information should you track?
- Should there be account opening bonuses?

**Remember**: There's no single "correct" bank system. YOUR creativity and problem-solving approach matter!

## Self-Assessment Checklist

Test your bank account system thoroughly:

### Core Functionality
- [ ] BankAccount class with all required properties
- [ ] All fields are private
- [ ] Properties use appropriate access modifiers
- [ ] Owner validation works (rejects empty names)
- [ ] Balance validation works (prevents negative)
- [ ] Deposit method validates positive amounts
- [ ] Withdraw method prevents overdraft
- [ ] Account ID is read-only and set once
- [ ] Program runs without crashes

### Code Quality
- [ ] Properties chosen correctly (auto vs custom)
- [ ] Access modifiers used appropriately
- [ ] Validation logic is clear and complete
- [ ] Error messages are helpful and specific
- [ ] Code is well-organized
- [ ] Methods are focused and simple

### Edge Cases
- [ ] Handles zero amounts correctly
- [ ] Handles very large amounts
- [ ] Handles decimal precision properly
- [ ] Rejects null/empty owner names
- [ ] Maintains valid state even with bad input

### YOUR Additions
- [ ] Added at least one unique feature
- [ ] Tested custom features thoroughly
- [ ] Features enhance user experience
- [ ] Code remains clean and understandable

## What Makes a Great Project?

Your project will be evaluated on:

- **Functionality** (40%): All requirements work, validation prevents invalid states
- **Code Quality** (40%): Proper properties, correct access modifiers, clean validation
- **User Experience** (10%): Clear messages, formatted output, handles errors gracefully
- **Creativity** (10%): YOUR unique features and enhancements

## Common Pitfalls to Avoid

**Problem**: Public fields instead of properties
```csharp
// ❌ Wrong - No validation possible
public decimal balance;

account.balance = -1000;  // Disaster!

// ✅ Correct - Property with validation
private decimal balance;
public decimal Balance
{
    get { return balance; }
    private set
    {
        if (value < 0) return;
        balance = value;
    }
}
```

**Problem**: Forgetting to return after validation error
```csharp
// ❌ Wrong - Still assigns invalid value!
set
{
    if (value < 0)
        Console.WriteLine("Error");
    balance = value;  // STILL RUNS!
}

// ✅ Correct - Return prevents assignment
set
{
    if (value < 0)
    {
        Console.WriteLine("Error");
        return;  // Don't assign
    }
    balance = value;
}
```

**Problem**: Public setter on Balance
```csharp
// ❌ Wrong - Balance can be set directly
public decimal Balance { get; set; }

account.Balance = 1000000;  // Cheating!

// ✅ Correct - Only methods can change balance
public decimal Balance { get; private set; }

public void Deposit(decimal amount)
{
    Balance += amount;  // Controlled access
}
```

**Problem**: Missing validation in methods
```csharp
// ❌ Wrong - No checks
public void Deposit(decimal amount)
{
    Balance += amount;  // Allows negative deposits!
}

// ✅ Correct - Validate parameters
public void Deposit(decimal amount)
{
    if (amount <= 0)
    {
        Console.WriteLine("Error: Amount must be positive");
        return;
    }
    Balance += amount;
}
```

## Showcase YOUR Work!

When you're done:
- Demonstrate YOUR bank account system
- Explain YOUR validation choices
- Show how YOUR system prevents invalid states
- Share unique features YOU added
- Discuss design decisions YOU made

**This is YOUR project** - make it robust and professional!

## Resources

- Review Chapter 4 concepts for properties and validation
- Look at exercises 01-04 for property examples
- Remember: properties + validation = safe, maintainable code

## Extension Ideas for Advanced Students

**Level 1: Enhanced Validation**
- Maximum single transaction limit
- Daily transaction limit
- Minimum balance requirement
- Account status (active, frozen, closed)

**Level 2: Transaction System**
- Transaction class with date, type, amount
- Complete transaction history
- Statement generation by date range
- Search/filter transactions

**Level 3: Multiple Account Types**
- CheckingAccount with overdraft
- SavingsAccount with interest
- Inheritance for shared behavior
- Different validation rules per type

**Level 4: Banking Features**
- Multiple accounts per customer
- Scheduled transfers
- Automatic bill payments
- Account notifications

**Level 5: Persistence**
- Save account data to file
- Load existing accounts
- Export transaction history
- Backup and restore

---

**Good luck!** Remember: validation in properties is your first line of defense against bugs. Make YOUR bank account system safe, robust, and impossible to break!