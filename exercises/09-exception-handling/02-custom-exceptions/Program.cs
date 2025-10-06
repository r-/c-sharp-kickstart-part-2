using System;

namespace Part2.Ch09.Ex02
{
    // TODO: Create InsufficientFundsException class
    // Should inherit from Exception
    // Include Balance and RequestedAmount properties
    
    // TODO: Create InvalidAmountException class
    // Should inherit from Exception
    // Include Amount property
    
    // TODO: Create AccountLockedException class
    // Should inherit from Exception
    // Include Reason property
    
    // TODO: Create BankAccount class
    // Properties: AccountNumber, Owner, Balance (private set), IsLocked
    // Methods: Deposit, Withdraw, Lock, Unlock
    // Throw appropriate exceptions for rule violations
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Bank Account System ===\n");
            
            // TODO: Create bank account and test all scenarios
            // TODO: Demonstrate InsufficientFundsException
            // TODO: Demonstrate InvalidAmountException
            // TODO: Demonstrate AccountLockedException
            // TODO: Use multiple catch blocks to handle each type
        }
    }
}