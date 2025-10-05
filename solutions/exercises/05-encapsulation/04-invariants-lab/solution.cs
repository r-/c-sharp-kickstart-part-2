using System;
using System.Collections.Generic;
using System.Linq;

enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}

class Order
{
    private List<(string Name, decimal Price)> items;
    private decimal discountPercentage;
    
    // Read-only properties
    public int OrderId { get; }
    public string Customer { get; }
    public OrderStatus Status { get; private set; }
    
    // Computed properties
    public int ItemCount => items.Count;
    public decimal ItemTotal => items.Sum(i => i.Price);
    public decimal DiscountAmount => ItemTotal * (discountPercentage / 100);
    public decimal ShippingCost => ItemTotal > 100 ? 0 : 10;
    public decimal FinalTotal => ItemTotal - DiscountAmount + ShippingCost;
    public bool CanModify => Status != OrderStatus.Shipped && 
                             Status != OrderStatus.Delivered && 
                             Status != OrderStatus.Cancelled;
    
    // Constructor with validation
    public Order(int orderId, string customer)
    {
        if (orderId <= 0)
            throw new ArgumentException("Order ID must be positive");
        
        if (string.IsNullOrWhiteSpace(customer))
            throw new ArgumentException("Customer name is required");
        
        OrderId = orderId;
        Customer = customer;
        Status = OrderStatus.Pending;
        items = new List<(string, decimal)>();
        discountPercentage = 0;
    }
    
    // Methods with invariant checks
    public void AddItem(string name, decimal price)
    {
        if (!CanModify)
            throw new InvalidOperationException("Cannot add items - order already shipped");
        
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Item name is required");
        
        if (price <= 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Item price must be positive");
        
        if (items.Count >= 50)
            throw new InvalidOperationException("Cannot add item - order limit is 50 items");
        
        items.Add((name, price));
        Console.WriteLine($"✓ Added {name} (${price:F2})");
    }
    
    public void RemoveItem(string name)
    {
        if (!CanModify)
            throw new InvalidOperationException("Cannot remove items - order already shipped");
        
        if (items.Count <= 1)
            throw new InvalidOperationException("Cannot remove item - order must have at least 1 item");
        
        var item = items.FirstOrDefault(i => i.Name == name);
        if (item == default)
            throw new InvalidOperationException($"Item '{name}' not found");
        
        items.Remove(item);
        Console.WriteLine($"✓ Removed {name}");
    }
    
    public void ApplyDiscount(decimal percentage)
    {
        if (!CanModify)
            throw new InvalidOperationException("Cannot apply discount - order already shipped");
        
        if (percentage < 0 || percentage > 50)
            throw new ArgumentOutOfRangeException(
                nameof(percentage),
                $"Discount must be between 0 and 50% (got: {percentage})"
            );
        
        discountPercentage = percentage;
        Console.WriteLine($"✓ Discount applied: ${DiscountAmount:F2}");
    }
    
    public void UpdateStatus(OrderStatus newStatus)
    {
        var validTransitions = new Dictionary<OrderStatus, List<OrderStatus>>
        {
            { OrderStatus.Pending, new List<OrderStatus> { OrderStatus.Processing, OrderStatus.Cancelled } },
            { OrderStatus.Processing, new List<OrderStatus> { OrderStatus.Shipped, OrderStatus.Cancelled } },
            { OrderStatus.Shipped, new List<OrderStatus> { OrderStatus.Delivered } },
            { OrderStatus.Delivered, new List<OrderStatus>() },
            { OrderStatus.Cancelled, new List<OrderStatus>() }
        };
        
        if (!validTransitions[Status].Contains(newStatus))
        {
            throw new InvalidOperationException(
                $"Cannot transition from {Status} to {newStatus}"
            );
        }
        
        Status = newStatus;
        Console.WriteLine($"✓ Status: {Status}");
    }
    
    public void Cancel()
    {
        if (Status == OrderStatus.Shipped || Status == OrderStatus.Delivered)
            throw new InvalidOperationException("Cannot cancel - order already shipped");
        
        if (Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Order already cancelled");
        
        Status = OrderStatus.Cancelled;
        Console.WriteLine("✓ Order cancelled");
    }
    
    public void DisplaySummary()
    {
        Console.WriteLine($"\nOrder Summary:");
        Console.WriteLine($"Order ID: {OrderId}");
        Console.WriteLine($"Customer: {Customer}");
        Console.WriteLine($"Status: {Status}");
        Console.WriteLine($"Items: {ItemCount}");
        Console.WriteLine($"Item Total: ${ItemTotal:F2}");
        Console.WriteLine($"Shipping: {(ShippingCost == 0 ? "FREE (order > $100)" : $"${ShippingCost:F2}")}");
        Console.WriteLine($"Discount: ${DiscountAmount:F2}");
        Console.WriteLine($"Final Total: ${FinalTotal:F2}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Creating order #1001 for Alice...\n");
        
        Order order = new Order(1001, "Alice");
        
        Console.WriteLine("Adding items:");
        order.AddItem("Laptop", 999.99m);
        order.AddItem("Mouse", 29.99m);
        order.AddItem("Keyboard", 79.99m);
        
        order.DisplaySummary();
        
        Console.WriteLine("\nApplying 10% discount:");
        order.ApplyDiscount(10);
        Console.WriteLine($"Final Total: ${order.FinalTotal:F2}");
        
        Console.WriteLine("\nProcessing order:");
        order.UpdateStatus(OrderStatus.Processing);
        order.UpdateStatus(OrderStatus.Shipped);
        order.UpdateStatus(OrderStatus.Delivered);
        
        Console.WriteLine("\nTesting invariants:\n");
        
        Console.WriteLine("Item limits:");
        try
        {
            order.AddItem("Extra Item", 50.00m);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        Console.WriteLine("\nDiscount validation:");
        try
        {
            order.ApplyDiscount(75);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        try
        {
            order.ApplyDiscount(10);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        Console.WriteLine("\nStatus transitions:");
        try
        {
            order.UpdateStatus(OrderStatus.Pending);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        try
        {
            order.Cancel();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        Console.WriteLine("\nPrice validation:");
        Order order2 = new Order(1002, "Bob");
        order2.AddItem("Test", 10.00m);
        
        try
        {
            order2.AddItem("Bad", -10.00m);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        try
        {
            order2.AddItem("Bad", 0);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        Console.WriteLine("\nOrder integrity maintained:");
        Console.WriteLine($"Order #{order.OrderId} remains in valid state: {order.Status}, {order.ItemCount} items, ${order.FinalTotal:F2}");
    }
}