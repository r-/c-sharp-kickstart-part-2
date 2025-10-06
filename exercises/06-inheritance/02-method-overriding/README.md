# Exercise 06-02: Method Overriding

## Goal

Learn how to override base class methods in derived classes to customize behavior. You'll practice using `virtual` and `override` keywords to create polymorphic behavior.

## Background

Method overriding allows derived classes to provide their own implementation of methods defined in the base class. This is a key feature of polymorphism—different types can respond differently to the same method call.

## Instructions

You're building a payment processing system. Different payment methods (credit card, PayPal, bank transfer) all process payments, but each has its own specific way of doing it.

1. Create a `Payment` base class with a virtual `ProcessPayment()` method
2. Create three derived classes that override the method with specific behavior
3. Demonstrate polymorphism by treating all payment types uniformly

## Requirements

1. **Payment base class must have:**
   - Amount property (decimal, read-only from outside)
   - TransactionId property (string, read-only)
   - Constructor that initializes amount and generates transaction ID
   - Virtual method `ProcessPayment()` that displays generic message
   - Virtual method `GetReceipt()` that returns formatted receipt string

2. **CreditCardPayment derived class must:**
   - Add CardNumber property (last 4 digits only)
   - Override ProcessPayment() to show credit card-specific message
   - Override GetReceipt() to include card information
   - Call base constructor

3. **PayPalPayment derived class must:**
   - Add Email property
   - Override ProcessPayment() to show PayPal-specific message
   - Override GetReceipt() to include email
   - Call base constructor

4. **BankTransferPayment derived class must:**
   - Add AccountNumber property (last 4 digits)
   - Override ProcessPayment() to show bank transfer message
   - Override GetReceipt() to include account info
   - Call base constructor

5. **In Main() demonstrate:**
   - Create one instance of each payment type
   - Store them in a Payment array or list
   - Process all payments using polymorphism
   - Print all receipts

## Expected Output

```
=== Payment Processing System ===

Processing 3 payments...

Payment #TXN-1234: Processing $99.99
💳 Credit Card charged successfully (Card ending in 5678)

Payment #TXN-1235: Processing $49.50
📧 PayPal payment sent to user@example.com

Payment #TXN-1236: Processing $200.00
🏦 Bank transfer initiated from account ****4321

All payments processed!

=== Receipts ===

--- Receipt ---
Transaction: TXN-1234
Amount: $99.99
Method: Credit Card ****5678
--------------

--- Receipt ---
Transaction: TXN-1235
Amount: $49.50
Method: PayPal (user@example.com)
--------------

--- Receipt ---
Transaction: TXN-1236
Amount: $200.00
Method: Bank Transfer ****4321
--------------
```

## Hints

**Payment base class:**
```csharp
class Payment
{
    public decimal Amount { get; }
    public string TransactionId { get; }
    
    public Payment(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");
        
        Amount = amount;
        TransactionId = "TXN-" + new Random().Next(1000, 9999);
    }
    
    public virtual void ProcessPayment()
    {
        Console.WriteLine($"Payment #{TransactionId}: Processing ${Amount:F2}");
    }
    
    public virtual string GetReceipt()
    {
        return $"--- Receipt ---\nTransaction: {TransactionId}\nAmount: ${Amount:F2}";
    }
}
```

**CreditCardPayment example:**
```csharp
class CreditCardPayment : Payment
{
    public string CardNumber { get; }
    
    public CreditCardPayment(decimal amount, string cardNumber) 
        : base(amount)
    {
        CardNumber = cardNumber;
    }
    
    public override void ProcessPayment()
    {
        base.ProcessPayment();  // Call base method first
        Console.WriteLine($"💳 Credit Card charged successfully (Card ending in {CardNumber})");
    }
    
    public override string GetReceipt()
    {
        return base.GetReceipt() + $"\nMethod: Credit Card ****{CardNumber}\n--------------";
    }
}
```

**Using polymorphism:**
```csharp
Payment[] payments = new Payment[]
{
    new CreditCardPayment(99.99m, "5678"),
    new PayPalPayment(49.50m, "user@example.com"),
    new BankTransferPayment(200.00m, "4321")
};

foreach (Payment payment in payments)
{
    payment.ProcessPayment();  // Calls the correct override!
    Console.WriteLine();
}
```

## Bonus Challenge

Enhance your understanding with these additions:

1. **Add validation** - Validate card numbers (4 digits), email format, account numbers
2. **Add processing fees** - Different payment types have different fees (credit card 3%, PayPal 2.5%, bank transfer $1)
3. **Add status tracking** - Track payment status (Pending, Processing, Completed, Failed)
4. **Add refund method** - Virtual RefundPayment() method with type-specific implementations
5. **Add payment history** - Static list to track all processed payments
6. **Add cancellation** - Virtual CancelPayment() method (some types can cancel, others can't)

## What You'll Learn

- How to mark base class methods as `virtual`
- How to use `override` in derived classes
- How to call base class methods with `base.MethodName()`
- The power of polymorphism—treating different types uniformly
- When and why to override methods
- How virtual methods enable flexible designs

Remember: Virtual methods define behavior that can be customized. Override provides the customization.