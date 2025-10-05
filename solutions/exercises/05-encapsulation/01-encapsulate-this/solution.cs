using System;
using System.Collections.Generic;
using System.Linq;

class ShoppingCart
{
    // Private fields
    private string customerName;
    private List<(string Name, decimal Price)> items;
    private int maxItems;
    
    // Public properties with validation
    public string CustomerName
    {
        get { return customerName; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Customer name is required");
            customerName = value;
        }
    }
    
    public int MaxItems
    {
        get { return maxItems; }
    }
    
    // Computed properties
    public int ItemCount => items.Count;
    public decimal TotalPrice => items.Sum(i => i.Price);
    public bool IsFull => items.Count >= maxItems;
    
    // Constructor with validation
    public ShoppingCart(string customerName, int maxItems = 10)
    {
        if (maxItems <= 0)
            throw new ArgumentException("Max items must be positive");
        
        CustomerName = customerName;
        this.maxItems = maxItems;
        items = new List<(string, decimal)>();
    }
    
    // Methods with guard clauses
    public void AddItem(string itemName, decimal price)
    {
        if (string.IsNullOrWhiteSpace(itemName))
            throw new ArgumentException("Item name is required");
        
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative");
        
        if (IsFull)
            throw new InvalidOperationException($"Cannot add item - cart is full ({maxItems}/{maxItems})");
        
        items.Add((itemName, price));
        Console.WriteLine($"✓ Added {itemName} (${price:F2})");
    }
    
    public void RemoveItem(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
            throw new ArgumentException("Item name is required");
        
        var item = items.FirstOrDefault(i => i.Name == itemName);
        if (item == default)
            throw new InvalidOperationException($"Item '{itemName}' not found in cart");
        
        items.Remove(item);
        Console.WriteLine($"✓ Removed {itemName}");
    }
    
    public void Clear()
    {
        items.Clear();
        Console.WriteLine("✓ Cart cleared");
    }
    
    public IReadOnlyList<string> GetItemNames()
    {
        return items.Select(i => i.Name).ToList().AsReadOnly();
    }
    
    public void DisplayCart()
    {
        Console.WriteLine($"\nCart for {CustomerName}:");
        Console.WriteLine($"Items: {ItemCount}/{MaxItems}");
        Console.WriteLine($"Total: ${TotalPrice:F2}");
        if (IsFull)
            Console.WriteLine("Status: Full");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Creating shopping cart for Alice...\n");
        
        ShoppingCart cart = new ShoppingCart("Alice", 10);
        
        Console.WriteLine("Adding items:");
        cart.AddItem("Laptop", 999.99m);
        cart.AddItem("Mouse", 29.99m);
        cart.AddItem("Keyboard", 79.99m);
        
        cart.DisplayCart();
        
        Console.WriteLine("\nAdding more items to reach limit:");
        cart.AddItem("Monitor", 299.99m);
        cart.AddItem("Webcam", 89.99m);
        cart.AddItem("Headset", 149.99m);
        cart.AddItem("USB Cable", 9.99m);
        cart.AddItem("HDMI Cable", 12.99m);
        cart.AddItem("Mousepad", 19.99m);
        cart.AddItem("Desk Lamp", 39.99m);
        
        cart.DisplayCart();
        
        Console.WriteLine("\nTesting cart limits:");
        try
        {
            cart.AddItem("Extra Item", 50.00m);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        Console.WriteLine("\nTesting invalid operations:");
        try
        {
            cart.AddItem("", 50.00m);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        try
        {
            cart.AddItem("Item", -10.00m);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        Console.WriteLine($"\nCart remains valid with {cart.ItemCount} items totaling ${cart.TotalPrice:F2}");
    }
}