# Exercise 09-02: Multiple Catches and Custom Exceptions

## Goal

Learn to create custom exception types and handle multiple error scenarios with specific catch blocks. You'll build a bank account system that throws domain-specific exceptions for different business rule violations.

## Background

While built-in exceptions like `ArgumentException` are useful, creating custom exception types lets you model domain-specific errors. This makes your code more expressive, easier to debug, and allows calling code to handle different business rule violations appropriately.

## Instructions

You will create a banking system with custom exceptions for different error scenarios.

### Part 1: Create Custom Exception Classes

Create three custom exception classes:

1. **`InsufficientFundsException`**
   - Properties: `Balance` (decimal), `RequestedAmount` (decimal)
   - Constructor accepts both amounts
   - Message: "Insufficient funds. Balance: $X, Requested: $Y"

2. **`InvalidAmountException`**
   - Property: `Amount` (decimal)
   - Constructor accepts amount
   - Message: "Invalid amount: $X. Amount must be positive."

3. **`AccountLockedException`**
   - Property: `Reason` (string)
   - Constructor accepts reason
   - Message: "Account is locked: {reason}"

### Part 2: Create BankAccount Class

Implement a `BankAccount` class with:
- Properties: `AccountNumber` (string), `Owner` (string), `Balance` (decimal, private set), `IsLocked` (bool)
- Constructor validates owner name and initial balance
- Method `Deposit(decimal amount)` - adds money, validates amount
- Method `Withdraw(decimal amount)` - removes money, validates amount and balance
- Method `Lock(string reason)` - locks the account
- Method `Unlock()` - unlocks the account

### Part 3: Test the System

Create a `Main()` method that:
- Creates a bank account
- Performs various operations
- Catches and handles each exception type differently
- Demonstrates all three custom exceptions
- Shows recovery from errors

## Requirements

Your solution must:
1. Create all three custom exception classes
2. All custom exceptions inherit from `Exception`
3. Implement complete `BankAccount` class
4. Throw appropriate exceptions for rule violations
5. Use multiple catch blocks to handle different errors
6. Provide specific, helpful error messages for each case
7. Demonstrate all exception types in action

## Expected Output

```
=== Bank Account System ===

Creating account for Alice with $1000
✓ Account created successfully

Depositing $500...
✓ Deposit successful. New balance: $1500

Withdrawing $2000...
✗ Error: Insufficient funds. Balance: $1500, Requested: $2000
  Account balance unchanged.

Withdrawing $-50...
✗ Error: Invalid amount: $-50. Amount must be positive.

Locking account (fraud detected)...
✓ Account locked

Attempting withdrawal on locked account...
✗ Error: Account is locked: fraud detected
  Cannot perform operations on locked accounts.

Unlocking account...
✓ Account unlocked

Withdrawing $500...
✓ Withdrawal successful. New balance: $1000

=== Final Account Status ===
Owner: Alice
Balance: $1000
Status: Active
```

## Hints

**Custom exception structure:**
```csharp
class InsufficientFundsException : Exception
{
    public decimal Balance { get; }
    public decimal RequestedAmount { get; }
    
    public InsufficientFundsException(decimal balance, decimal requested)
        : base($"Insufficient funds. Balance: ${balance}, Requested: ${requested}")
    {
        Balance = balance;
        RequestedAmount = requested;
    }
}
```

**BankAccount withdraw method:**
```csharp
public void Withdraw(decimal amount)
{
    if (IsLocked)
        throw new AccountLockedException(lockReason);
    
    if (amount <= 0)
        throw new InvalidAmountException(amount);
    
    if (amount > Balance)
        throw new InsufficientFundsException(Balance, amount);
    
    Balance -= amount;
}
```

**Handling multiple exception types:**
```csharp
try
{
    account.Withdraw(2000);
    Console.WriteLine("✓ Withdrawal successful");
}
catch (AccountLockedException ex)
{
    Console.WriteLine($"✗ Error: {ex.Message}");
    Console.WriteLine("  Cannot perform operations on locked accounts.");
}
catch (InsufficientFundsException ex)
{
    Console.WriteLine($"✗ Error: {ex.Message}");
    Console.WriteLine("  Account balance unchanged.");
}
catch (InvalidAmountException ex)
{
    Console.WriteLine($"✗ Error: {ex.Message}");
}
```

## Common Mistakes to Avoid

- ❌ Not calling base constructor with message
- ❌ Forgetting to make exception properties read-only
- ❌ Catching exceptions in wrong order (general before specific)
- ❌ Not including helpful context in exception properties
- ❌ Using `Exception` instead of custom types

## Bonus Challenge

Enhance the banking system with these features:

1. **Transaction history** - Log all operations (successful and failed)
2. **Daily withdrawal limit** - Custom `DailyLimitExceededException`
3. **Minimum balance** - Custom `MinimumBalanceException`
4. **Transfer between accounts** - Handle both account errors
5. **Overdraft protection** - Allow negative balance with limit
6. **Transaction rollback** - Undo failed operations

Example bonus output:
```
=== Enhanced Bank Account ===

Withdrawing $5000 (Daily limit: $3000)...
✗ Error: Daily limit exceeded
  Requested: $5000
  Remaining limit today: $3000
  Resets at: 2024-01-02 00:00:00

Withdrawing $900 (Minimum balance: $100)...
✗ Error: Would violate minimum balance requirement
  Current: $1000
  After withdrawal: $100
  Minimum required: $100

Transaction History:
  [SUCCESS] Deposit: +$1000 (Balance: $1000)
  [FAILED]  Withdrawal: -$5000 (Limit exceeded)
  [FAILED]  Withdrawal: -$900 (Minimum balance)
  [SUCCESS] Withdrawal: -$500 (Balance: $500)
```

## What You're Learning

- Creating custom exception classes for domain errors
- Using multiple catch blocks for different scenarios
- Handling exceptions with specific, targeted responses
- Including helpful data in exception properties
- Building robust business logic with error handling

---

**Time Estimate:** 45 minutes  
**Difficulty:** Medium  
**Type:** Custom Exceptions and Business Logic

**Next:** After completing this exercise, move to [Exercise 09-03: Finally and Resource Cleanup](../03-finally-cleanup/) to master resource management.