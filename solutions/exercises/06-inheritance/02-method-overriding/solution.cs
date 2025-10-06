using System;

namespace Part2.Ch06.Ex02.Solution
{
    // Base Payment class with virtual methods
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
    
    // Credit Card payment implementation
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
            base.ProcessPayment();
            Console.WriteLine($"💳 Credit Card charged successfully (Card ending in {CardNumber})");
        }
        
        public override string GetReceipt()
        {
            return base.GetReceipt() + $"\nMethod: Credit Card ****{CardNumber}\n--------------";
        }
    }
    
    // PayPal payment implementation
    class PayPalPayment : Payment
    {
        public string Email { get; }
        
        public PayPalPayment(decimal amount, string email)
            : base(amount)
        {
            Email = email;
        }
        
        public override void ProcessPayment()
        {
            base.ProcessPayment();
            Console.WriteLine($"📧 PayPal payment sent to {Email}");
        }
        
        public override string GetReceipt()
        {
            return base.GetReceipt() + $"\nMethod: PayPal ({Email})\n--------------";
        }
    }
    
    // Bank transfer payment implementation
    class BankTransferPayment : Payment
    {
        public string AccountNumber { get; }
        
        public BankTransferPayment(decimal amount, string accountNumber)
            : base(amount)
        {
            AccountNumber = accountNumber;
        }
        
        public override void ProcessPayment()
        {
            base.ProcessPayment();
            Console.WriteLine($"🏦 Bank transfer initiated from account ****{AccountNumber}");
        }
        
        public override string GetReceipt()
        {
            return base.GetReceipt() + $"\nMethod: Bank Transfer ****{AccountNumber}\n--------------";
        }
    }
    
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Payment Processing System ===");
            Console.WriteLine();
            Console.WriteLine("Processing 3 payments...");
            Console.WriteLine();
            
            // Create array of Payment objects (polymorphism)
            Payment[] payments = new Payment[]
            {
                new CreditCardPayment(99.99m, "5678"),
                new PayPalPayment(49.50m, "user@example.com"),
                new BankTransferPayment(200.00m, "4321")
            };
            
            // Process each payment (polymorphic method calls)
            foreach (Payment payment in payments)
            {
                payment.ProcessPayment();
                Console.WriteLine();
            }
            
            Console.WriteLine("All payments processed!");
            Console.WriteLine();
            Console.WriteLine("=== Receipts ===");
            Console.WriteLine();
            
            // Print all receipts (polymorphic method calls)
            foreach (Payment payment in payments)
            {
                Console.WriteLine(payment.GetReceipt());
                Console.WriteLine();
            }
        }
    }
}