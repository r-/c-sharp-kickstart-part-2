using System;

class Product
{
    // Private fields - internal storage
    private string name;
    private decimal price;
    private int stock;
    
    // Public properties with validation
    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Error: Name cannot be empty");
                return;
            }
            name = value;
        }
    }
    
    public decimal Price
    {
        get { return price; }
        set
        {
            if (value < 0)
            {
                Console.WriteLine("Error: Price cannot be negative");
                return;
            }
            price = value;
        }
    }
    
    public int Stock
    {
        get { return stock; }
        set
        {
            if (value < 0)
            {
                Console.WriteLine("Error: Stock cannot be negative");
                return;
            }
            stock = value;
        }
    }
    
    // Display product information
    public void DisplayInfo()
    {
        Console.WriteLine($"Product: {Name}");
        Console.WriteLine($"Price: ${Price:F2}");
        Console.WriteLine($"Stock: {Stock}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Testing Product class...\n");
        
        // Create product
        Product product = new Product();
        
        Console.WriteLine("Setting valid values:");
        product.Name = "Laptop";
        product.Price = 999.99m;
        product.Stock = 15;
        product.DisplayInfo();
        
        Console.WriteLine("\nTesting invalid values:");
        product.Name = "";           // Should show error
        product.Price = -50;         // Should show error
        product.Stock = -10;         // Should show error
        
        Console.WriteLine("\nFinal product state:");
        product.DisplayInfo();
    }
}