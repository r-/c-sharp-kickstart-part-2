using System;
using System.Collections.Generic;

namespace Part2.Ch10.Ex02
{
    // ❌ This class violates OCP - adding new discount types requires modification
    class DiscountCalculator
    {
        public double CalculateDiscount(double price, string discountType, double value)
        {
            if (discountType == "Percentage")
            {
                return price * (value / 100);
            }
            else if (discountType == "FixedAmount")
            {
                return value;
            }
            else if (discountType == "BuyOneGetOne")
            {
                return price / 2;
            }
            // Every new discount type requires modifying this method!
            // This breaks the Open/Closed Principle
            
            return 0;
        }
        
        public double ApplyDiscount(double price, string discountType, double value)
        {
            double discount = CalculateDiscount(price, discountType, value);
            Console.WriteLine($"\nApplying {discountType} Discount:");
            Console.WriteLine($"Discount: ${discount:F2}");
            double finalPrice = price - discount;
            Console.WriteLine($"Final Price: ${finalPrice:F2}");
            return finalPrice;
        }
    }
    
    // TODO: Create these following OCP:
    // - IDiscountStrategy interface
    // - PercentageDiscount class
    // - FixedAmountDiscount class
    // - BuyOneGetOneDiscount class
    // - Refactored DiscountCalculator that uses IDiscountStrategy
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Discount System ===\n");
            
            double price = 100;
            Console.WriteLine($"Original Price: ${price}\n");
            
            // Current implementation (violates OCP)
            Console.WriteLine("❌ Current Design (violates OCP):");
            DiscountCalculator oldCalculator = new DiscountCalculator();
            oldCalculator.ApplyDiscount(price, "Percentage", 20);
            oldCalculator.ApplyDiscount(price, "FixedAmount", 15);
            oldCalculator.ApplyDiscount(price, "BuyOneGetOne", 0);
            
            Console.WriteLine("\nProblem: To add a new discount type, we must modify DiscountCalculator!");
            Console.WriteLine("This violates OCP and risks breaking existing code.\n");
            
            Console.WriteLine(new string('=', 60));
            
            // TODO: Your refactored implementation
            Console.WriteLine("\n✅ Refactored Design (follows OCP):");
            Console.WriteLine("TODO: Implement your refactored classes here\n");
            
            // TODO: Example of how it should work:
            // DiscountCalculator calculator = new DiscountCalculator();
            // 
            // calculator.ApplyDiscount(price, new PercentageDiscount(20));
            // calculator.ApplyDiscount(price, new FixedAmountDiscount(15));
            // calculator.ApplyDiscount(price, new BuyOneGetOneDiscount());
            // 
            // // Add NEW discount without modifying DiscountCalculator!
            // calculator.ApplyDiscount(price, new SeasonalDiscount("Summer", 30));
            
            Console.WriteLine("\n=== OCP Benefits ===");
            Console.WriteLine("✓ Can add new discount types without modifying DiscountCalculator");
            Console.WriteLine("✓ Each discount is independent and testable");
            Console.WriteLine("✓ No risk of breaking existing discounts");
            Console.WriteLine("✓ System is OPEN for extension, CLOSED for modification");
        }
    }
}