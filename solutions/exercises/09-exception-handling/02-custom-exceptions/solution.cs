using System;

namespace Part2.Ch09.Ex02
{
    // Custom Exception Classes
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
    
    class InvalidAmountException : Exception
    {
        public decimal Amount { get; }
        
        public InvalidAmountException(decimal amount)
            : base($"Invalid amount: ${amount}. Amount must be positive.")
        {
            Amount = amount;
        }
    }
    
    class AccountLockedException : Exception
    {
        public string Reason { get; }
        
        public AccountLockedException(string reason)
            : base($"Account is locked: {reason}")
        {
            Reason = reason;
        }
    }
    
    // BankAccount Class
    class BankAccount
    {
        public string AccountNumber { get; set; }
        public string Owner { get; set; }
        public decimal Balance { get; private set; }
        public bool IsLocked { get; private set; }
        private string lockReason;
        
        public BankAccount(string accountNumber, string owner, decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Account number is required");
            if (string.IsNullOrWhiteSpace(owner))
                throw new ArgumentException("Owner name is required");
            if (initialBalance < 0)
                throw new ArgumentException("Initial balance cannot be negative");
            
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = initialBalance;
            IsLocked = false;
        }
        
        public void Deposit(decimal amount)
        {
            if (IsLocked)
                throw new AccountLockedException(lockReason);
            
            if (amount <= 0)
                throw new InvalidAmountException(amount);
            
            Balance += amount;
        }
        
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
        
        public void Lock(string reason)
        {
            IsLocked = true;
            lockReason = reason;
        }
        
        public void Unlock()
        {
            IsLocked = false;
            lockReason = null;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Bank Account System ===\n");
            
            try
            {
                // Create account
                Console.WriteLine("Creating account for Alice with $1000");
                BankAccount account = new BankAccount("12345", "Alice", 1000);
                Console.WriteLine("✓ Account created successfully\n");
                
                // Successful deposit
                Console.WriteLine("Depositing $500...");
                account.Deposit(500);
                Console.WriteLine($"✓ Deposit successful. New balance: ${account.Balance}\n");
                
                // Insufficient funds
                Console.WriteLine("Withdrawing $2000...");
                try
                {
                    account.Withdraw(2000);
                }
                catch (InsufficientFundsException ex)
                {
                    Console.WriteLine($"✗ Error: {ex.Message}");
                    Console.WriteLine("  Account balance unchanged.\n");
                }
                
                // Invalid amount
                Console.WriteLine("Withdrawing $-50...");
                try
                {
                    account.Withdraw(-50);
                }
                catch (InvalidAmountException ex)
                {
                    Console.WriteLine($"✗ Error: {ex.Message}\n");
                }
                
                // Lock account
                Console.WriteLine("Locking account (fraud detected)...");
                account.Lock("fraud detected");
                Console.WriteLine("✓ Account locked\n");
                
                // Attempt withdrawal on locked account
                Console.WriteLine("Attempting withdrawal on locked account...");
                try
                {
                    account.Withdraw(500);
                }
                catch (AccountLockedException ex)
                {
                    Console.WriteLine($"✗ Error: {ex.Message}");
                    Console.WriteLine("  Cannot perform operations on locked accounts.\n");
                }
                
                // Unlock account
                Console.WriteLine("Unlocking account...");
                account.Unlock();
                Console.WriteLine("✓ Account unlocked\n");
                
                // Successful withdrawal
                Console.WriteLine("Withdrawing $500...");
                account.Withdraw(500);
                Console.WriteLine($"✓ Withdrawal successful. New balance: ${account.Balance}\n");
                
                // Display final status
                Console.WriteLine("=== Final Account Status ===");
                Console.WriteLine($"Owner: {account.Owner}");
                Console.WriteLine($"Balance: ${account.Balance}");
                Console.WriteLine($"Status: {(account.IsLocked ? "Locked" : "Active")}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Setup Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
        }
    }
}