using System;

class Ticket
{
    // Private fields
    private string eventName;
    private decimal price;
    private int availableSeats;
    
    // Properties with validation
    public string EventName
    {
        get { return eventName; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Event name is required");
            
            if (value.Length < 3 || value.Length > 100)
                throw new ArgumentException("Event name must be 3-100 characters");
            
            eventName = value;
        }
    }
    
    public decimal Price
    {
        get { return price; }
        private set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Price must be positive");
            
            price = value;
        }
    }
    
    public int AvailableSeats
    {
        get { return availableSeats; }
        private set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Seats cannot be negative");
            
            availableSeats = value;
        }
    }
    
    // Constructor with validation
    public Ticket(string eventName, decimal price, int availableSeats)
    {
        EventName = eventName;
        Price = price;
        AvailableSeats = availableSeats;
    }
    
    // Methods with comprehensive guard clauses
    public void BookSeats(int count)
    {
        // Guard 1: Check parameter validity
        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(count),
                $"Seat count must be positive (got: {count})"
            );
        }
        
        // Guard 2: Check state precondition
        if (count > AvailableSeats)
        {
            throw new InvalidOperationException(
                $"Cannot book {count} seats - only {AvailableSeats} available"
            );
        }
        
        // Main logic
        AvailableSeats -= count;
        Console.WriteLine($"✓ Booked {count} seats");
    }
    
    public void CancelSeats(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(count),
                $"Seat count must be positive (got: {count})"
            );
        }
        
        AvailableSeats += count;
        Console.WriteLine($"✓ Cancelled {count} seats");
    }
    
    public void ApplyDiscount(decimal percentage)
    {
        if (percentage < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(percentage),
                $"Discount percentage must be between 0 and 100 (got: {percentage})"
            );
        }
        
        if (percentage > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(percentage),
                $"Discount percentage must be between 0 and 100 (got: {percentage})"
            );
        }
        
        Price = Price * (1 - percentage / 100);
        Console.WriteLine($"✓ Applied {percentage}% discount");
    }
    
    public void ChangePrice(decimal newPrice)
    {
        if (newPrice <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(newPrice),
                $"Price must be positive (got: {newPrice})"
            );
        }
        
        Price = newPrice;
        Console.WriteLine($"✓ Price changed to ${newPrice:F2}");
    }
    
    public void DisplayStatus()
    {
        Console.WriteLine($"Event: {EventName}");
        Console.WriteLine($"Price: ${Price:F2}");
        Console.WriteLine($"Available Seats: {AvailableSeats}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Creating ticket for 'Tech Conference 2024'...");
        Ticket ticket = new Ticket("Tech Conference 2024", 50.00m, 100);
        ticket.DisplayStatus();
        
        Console.WriteLine("\nValid operations:");
        ticket.BookSeats(25);
        ticket.ApplyDiscount(10);
        Console.WriteLine("Current state: ");
        ticket.DisplayStatus();
        
        Console.WriteLine("\nTesting guard clauses:\n");
        
        Console.WriteLine("Booking validation:");
        try
        {
            ticket.BookSeats(-5);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        try
        {
            ticket.BookSeats(100);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        Console.WriteLine("\nDiscount validation:");
        try
        {
            ticket.ApplyDiscount(150);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        try
        {
            ticket.ApplyDiscount(-10);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        Console.WriteLine("\nPrice validation:");
        try
        {
            ticket.ChangePrice(-20);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        try
        {
            ticket.ChangePrice(0);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        Console.WriteLine("\nEvent name validation:");
        try
        {
            var bad1 = new Ticket("", 50, 100);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        try
        {
            var bad2 = new Ticket("AB", 50, 100);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        Console.WriteLine("\nFinal state (protected): ");
        ticket.DisplayStatus();
    }
}