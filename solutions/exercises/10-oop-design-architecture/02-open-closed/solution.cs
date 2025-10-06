using System;
using System.Collections.Generic;

namespace Part2.Ch10.Ex02
{
    // ✅ FOLLOWS OCP - Open for extension, closed for modification
    
    // Interface defines the contract for all discount strategies
    interface IDiscountStrategy
    {
        double CalculateDiscount(double price);
        string GetDescription();
    }
    
    // Percentage discount implementation
    class PercentageDiscount : IDiscountStrategy
    {
        private double percentage;
        
        public PercentageDiscount(double percentage)
        {
            this.percentage = percentage;
        }
        
        public double CalculateDiscount(double price)
        {
            return price * (percentage / 100);
        }
        
        public string GetDescription()
        {
            return $"Percentage Discount ({percentage}%)";
        }
    }
    
    // Fixed amount discount implementation
    class FixedAmountDiscount : IDiscountStrategy
    {
        private double amount;
        
        public FixedAmountDiscount(double amount)
        {
            this.amount = amount;
        }
        
        public double CalculateDiscount(double price)
        {
            return Math.Min(amount, price); // Can't discount more than price
        }
        
        public string GetDescription()
        {
            return $"Fixed Amount Discount (${amount})";
        }
    }
    
    // Buy one get one discount
    class BuyOneGetOneDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double price)
        {
            return price / 2; // 50% off
        }
        
        public string GetDescription()
        {
            return "Buy One Get One Discount";
        }
    }
    
    // NEW discount type - Added WITHOUT modifying existing code!
    class SeasonalDiscount : IDiscountStrategy
    {
        private string season;
        private double percentage;
        
        public SeasonalDiscount(string season, double percentage)
        {
            this.season = season;
            this.percentage = percentage;
        }
        
        public double CalculateDiscount(double price)
        {
            return price * (percentage / 100);
        }
        
        public string GetDescription()
        {
            return $"Seasonal Discount ({season} {percentage}% off)";
        }
    }
    
    // NEW discount type - Bulk purchase discount
    class BulkDiscount : IDiscountStrategy
    {
        private int quantity;
        private double discountPerItem;
        
        public BulkDiscount(int quantity, double discountPerItem)
        {
            this.quantity = quantity;
            this.discountPerItem = discountPerItem;
        }
        
        public double CalculateDiscount(double price)
        {
            return quantity * discountPerItem;
        }
        
        public string GetDescription()
        {
            return $"Bulk Discount ({quantity} items at ${discountPerItem} off each)";
        }
    }
    
    // Calculator class - NEVER needs modification when adding new discounts!
    class DiscountCalculator
    {
        public double ApplyDiscount(double price, IDiscountStrategy strategy)
        {
            double discount = strategy.CalculateDiscount(price);
            Console.WriteLine($"\nApplying {strategy.GetDescription()}:");
            Console.WriteLine($"Discount: ${discount:F2}");
            double finalPrice = Math.Max(0, price - discount);
            Console.WriteLine($"Final Price: ${finalPrice:F2}");
            return finalPrice;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Discount System (Following OCP) ===\n");
            
            double price = 100;
            Console.WriteLine($"Original Price: ${price}");
            
            DiscountCalculator calculator = new DiscountCalculator();
            
            // Use different discount strategies
            calculator.ApplyDiscount(price, new PercentageDiscount(20));
            calculator.ApplyDiscount(price, new FixedAmountDiscount(15));
            calculator.ApplyDiscount(price, new BuyOneGetOneDiscount());
            
            // Add NEW discount types WITHOUT modifying DiscountCalculator!
            calculator.ApplyDiscount(price, new SeasonalDiscount("Summer", 30));
            calculator.ApplyDiscount(price, new BulkDiscount(5, 3));
            
            // Show OCP benefits
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("\n=== OCP Benefits ===");
            Console.WriteLine("✓ Added SeasonalDiscount without modifying DiscountCalculator");
            Console.WriteLine("✓ Added BulkDiscount without modifying other discount classes");
            Console.WriteLine("✓ Each discount type is independent and testable");
            Console.WriteLine("✓ No risk of breaking existing discounts when adding new ones");
            Console.WriteLine("\nSystem is OPEN for extension, CLOSED for modification!");
            
            // Demonstrate easy extension
            Console.WriteLine("\n=== Demonstrating Easy Extension ===");
            
            // Create a list of different discount strategies
            List<IDiscountStrategy> discounts = new List<IDiscountStrategy>
            {
                new PercentageDiscount(10),
                new PercentageDiscount(25),
                new FixedAmountDiscount(20),
                new SeasonalDiscount("Winter", 40)
            };
            
            Console.WriteLine("\nFinding best discount for $100 item:");
            double bestDiscount = 0;
            IDiscountStrategy bestStrategy = null;
            
            foreach (var discount in discounts)
            {
                double discountAmount = discount.CalculateDiscount(price);
                if (discountAmount > bestDiscount)
                {
                    bestDiscount = discountAmount;
                    bestStrategy = discount;
                }
            }
            
            Console.WriteLine($"\nBest discount: {bestStrategy.GetDescription()}");
            Console.WriteLine($"Saves: ${bestDiscount:F2}");
            Console.WriteLine($"Final price: ${price - bestDiscount:F2}");
        }
    }
}