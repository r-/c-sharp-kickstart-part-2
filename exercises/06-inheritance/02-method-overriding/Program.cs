using System;

namespace Part2.Ch06.Ex02
{
    // TODO: Create Payment base class here
    // Should have: Amount (decimal), TransactionId (string)
    // Should have: Constructor, virtual ProcessPayment(), virtual GetReceipt()
    
    
    // TODO: Create CreditCardPayment derived class
    // Should add: CardNumber property
    // Should override: ProcessPayment() and GetReceipt()
    
    
    // TODO: Create PayPalPayment derived class
    // Should add: Email property
    // Should override: ProcessPayment() and GetReceipt()
    
    
    // TODO: Create BankTransferPayment derived class
    // Should add: AccountNumber property
    // Should override: ProcessPayment() and GetReceipt()
    
    
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Payment Processing System ===");
            Console.WriteLine();
            Console.WriteLine("Processing 3 payments...");
            Console.WriteLine();
            
            // TODO: Create array or list of Payment objects
            // Include one of each payment type
            
            
            // TODO: Loop through and process each payment
            
            
            Console.WriteLine("All payments processed!");
            Console.WriteLine();
            Console.WriteLine("=== Receipts ===");
            Console.WriteLine();
            
            // TODO: Loop through and print each receipt
        }
    }
}