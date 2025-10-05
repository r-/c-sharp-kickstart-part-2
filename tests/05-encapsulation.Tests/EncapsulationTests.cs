
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EncapsulationTests;

// Exercise 01: Encapsulate This - ShoppingCart class
public class ShoppingCartTests
{
    [Fact]
    public void ShoppingCart_Constructor_InitializesCorrectly()
    {
        var cart = new ShoppingCart("Alice", 10);

        Assert.Equal("Alice", cart.CustomerName);
        Assert.Equal(10, cart.MaxItems);
        Assert.Equal(0, cart.ItemCount);
        Assert.Equal(0m, cart.TotalPrice);
        Assert.False(cart.IsFull);
    }

    [Fact]
    public void ShoppingCart_EmptyName_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new ShoppingCart("", 10));
    }

    [Fact]
    public void ShoppingCart_ZeroMaxItems_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new ShoppingCart("Alice", 0));
    }

    [Fact]
    public void ShoppingCart_AddItem_IncreasesCountAndTotal()
    {
        var cart = new ShoppingCart("Alice", 10);
        cart.AddItem("Laptop", 999.99m);

        Assert.Equal(1, cart.ItemCount);
        Assert.Equal(999.99m, cart.TotalPrice);
    }

    [Fact]
    public void ShoppingCart_AddMultipleItems_CalculatesCorrectly()
    {
        var cart = new ShoppingCart("Bob", 10);
        cart.AddItem("Mouse", 29.99m);
        cart.AddItem("Keyboard", 79.99m);

        Assert.Equal(2, cart.ItemCount);
        Assert.Equal(109.98m, cart.TotalPrice);
    }

    [Fact]
    public void ShoppingCart_AddToFullCart_ThrowsException()
    {
        var cart = new ShoppingCart("Charlie", 2);
        cart.AddItem("Item1", 10m);
        cart.AddItem("Item2", 20m);

        Assert.Throws<InvalidOperationException>(() => cart.AddItem("Item3", 30m));
    }

    [Fact]
    public void ShoppingCart_AddEmptyItemName_ThrowsException()
    {
        var cart = new ShoppingCart("Diana", 10);

        Assert.Throws<ArgumentException>(() => cart.AddItem("", 50m));
    }

    [Fact]
    public void ShoppingCart_AddNegativePrice_ThrowsException()
    {
        var cart = new ShoppingCart("Eve", 10);

        Assert.Throws<ArgumentOutOfRangeException>(() => cart.AddItem("Item", -10m));
    }

    [Fact]
    public void ShoppingCart_RemoveItem_DecreasesCountAndTotal()
    {
        var cart = new ShoppingCart("Frank", 10);
        cart.AddItem("Laptop", 999.99m);
        cart.AddItem("Mouse", 29.99m);
        cart.RemoveItem("Mouse");

        Assert.Equal(1, cart.ItemCount);
        Assert.Equal(999.99m, cart.TotalPrice);
    }

    [Fact]
    public void ShoppingCart_RemoveNonexistentItem_ThrowsException()
    {
        var cart = new ShoppingCart("Grace", 10);
        cart.AddItem("Item", 10m);

        Assert.Throws<InvalidOperationException>(() => cart.RemoveItem("NonExistent"));
    }

    [Fact]
    public void ShoppingCart_Clear_RemovesAllItems()
    {
        var cart = new ShoppingCart("Henry", 10);
        cart.AddItem("Item1", 10m);
        cart.AddItem("Item2", 20m);
        cart.Clear();

        Assert.Equal(0, cart.ItemCount);
        Assert.Equal(0m, cart.TotalPrice);
    }

    [Fact]
    public void ShoppingCart_IsFull_ReflectsCorrectly()
    {
        var cart = new ShoppingCart("Ivy", 2);
        Assert.False(cart.IsFull);

        cart.AddItem("Item1", 10m);
        Assert.False(cart.IsFull);

        cart.AddItem("Item2", 20m);
        Assert.True(cart.IsFull);
    }
}

// Exercise 02: Guard Clauses - Ticket class
public class TicketTests
{
    [Fact]
    public void Ticket_ValidConstruction_Succeeds()
    {
        var ticket = new Ticket("Tech Conference 2024", 50m, 100);

        Assert.Equal("Tech Conference 2024", ticket.EventName);
        Assert.Equal(50m, ticket.Price);
        Assert.Equal(100, ticket.AvailableSeats);
    }

    [Fact]
    public void Ticket_EmptyEventName_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Ticket("", 50m, 100));
    }

    [Fact]
    public void Ticket_EventNameTooShort_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Ticket("AB", 50m, 100));
    }

    [Fact]
    public void Ticket_EventNameTooLong_ThrowsException()
    {
        var longName = new string('X', 101);
        Assert.Throws<ArgumentException>(() => new Ticket(longName, 50m, 100));
    }

    [Fact]
    public void Ticket_ZeroPrice_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Ticket("Event", 0m, 100));
    }

    [Fact]
    public void Ticket_NegativePrice_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Ticket("Event", -10m, 100));
    }

    [Fact]
    public void Ticket_NegativeSeats_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Ticket("Event", 50m, -1));
    }

    [Fact]
    public void Ticket_BookSeats_ReducesAvailability()
    {
        var ticket = new Ticket("Concert", 75m, 100);
        ticket.BookSeats(25);

        Assert.Equal(75, ticket.AvailableSeats);
    }

    [Fact]
    public void Ticket_BookZeroSeats_ThrowsException()
    {
        var ticket = new Ticket("Event", 50m, 100);

        Assert.Throws<ArgumentOutOfRangeException>(() => ticket.BookSeats(0));
    }

    [Fact]
    public void Ticket_BookNegativeSeats_ThrowsException()
    {
        var ticket = new Ticket("Event", 50m, 100);

        Assert.Throws<ArgumentOutOfRangeException>(() => ticket.BookSeats(-5));
    }

    [Fact]
    public void Ticket_BookMoreThanAvailable_ThrowsException()
    {
        var ticket = new Ticket("Event", 50m, 50);

        Assert.Throws<InvalidOperationException>(() => ticket.BookSeats(51));
    }

    [Fact]
    public void Ticket_CancelSeats_IncreasesAvailability()
    {
        var ticket = new Ticket("Event", 50m, 100);
        ticket.BookSeats(30);
        ticket.CancelSeats(10);

        Assert.Equal(80, ticket.AvailableSeats);
    }

    [Fact]
    public void Ticket_ApplyValidDiscount_ReducesPrice()
    {
        var ticket = new Ticket("Event", 100m, 100);
        ticket.ApplyDiscount(10);

        Assert.Equal(90m, ticket.Price);
    }

    [Fact]
    public void Ticket_ApplyNegativeDiscount_ThrowsException()
    {
        var ticket = new Ticket("Event", 100m, 100);

        Assert.Throws<ArgumentOutOfRangeException>(() => ticket.ApplyDiscount(-10));
    }

    [Fact]
    public void Ticket_ApplyOverHundredDiscount_ThrowsException()
    {
        var ticket = new Ticket("Event", 100m, 100);

        Assert.Throws<ArgumentOutOfRangeException>(() => ticket.ApplyDiscount(150));
    }

    [Fact]
    public void Ticket_ChangePrice_UpdatesCorrectly()
    {
        var ticket = new Ticket("Event", 50m, 100);
        ticket.ChangePrice(75m);

        Assert.Equal(75m, ticket.Price);
    }

    [Fact]
    public void Ticket_ChangePriceToZero_ThrowsException()
    {
        var ticket = new Ticket("Event", 50m, 100);

        Assert.Throws<ArgumentOutOfRangeException>(() => ticket.ChangePrice(0m));
    }
}

// Exercise 03: Readonly & Computed - Person class
public class PersonTests
{
    [Fact]
    public void Person_ValidConstruction_Succeeds()
    {
        var birthDate = new DateTime(1995, 5, 15);
        var person = new Person(1001, "Alice", "Johnson", birthDate, "123-45-6789");

        Assert.Equal(1001, person.Id);
        Assert.Equal("Alice", person.FirstName);
        Assert.Equal("Johnson", person.LastName);
        Assert.Equal(birthDate, person.BirthDate);
        Assert.Equal("123-45-6789", person.SocialSecurity);
    }

    [Fact]
    public void Person_NegativeId_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Person(-1, "Test", "User", DateTime.Now.AddYears(-20), "000-00-0000"));
    }

    [Fact]
    public void Person_FutureBirthDate_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Person(1, "Test", "User", DateTime.Now.AddYears(1), "000-00-0000"));
    }

    [Fact]
    public void Person_FullName_CombinesNames()
    {
        var person = new Person(1, "Alice", "Johnson", DateTime.Now.AddYears(-25), "123-45-6789");

        Assert.Equal("Alice Johnson", person.FullName);
    }

    [Fact]
    public void Person_Age_CalculatesCorrectly()
    {
        var birthDate = DateTime.Now.AddYears(-25);
        var person = new Person(1, "Bob", "Smith", birthDate, "987-65-4321");

        Assert.Equal(25, person.Age);
    }

    [Fact]
    public void Person_IsAdult_ReflectsAge()
    {
        var adult = new Person(1, "Adult", "User", DateTime.Now.AddYears(-20), "111-11-1111");
        var minor = new Person(2, "Minor", "User", DateTime.Now.AddYears(-15), "222-22-2222");

        Assert.True(adult.IsAdult);
        Assert.False(minor.IsAdult);
    }

    [Fact]
    public void Person_NextBirthday_CalculatesCorrectly()
    {
        var birthDate = new DateTime(1995, 6, 15);
        var person = new Person(1, "Charlie", "Brown", birthDate, "333-33-3333");

        var nextBirthday = person.NextBirthday;
        Assert.Equal(6, nextBirthday.Month);
        Assert.Equal(15, nextBirthday.Day);
        Assert.True(nextBirthday >= DateTime.Today);
    }

    [Fact]
    public void Person_ChangeableName_Updates()
    {
        var person = new Person(1, "Diana", "Prince", DateTime.Now.AddYears(-30), "444-44-4444");
        person.FirstName = "Wonder";
        person.LastName = "Woman";

        Assert.Equal("Wonder", person.FirstName);
        Assert.Equal("Woman", person.LastName);
        Assert.Equal("Wonder Woman", person.FullName);
    }

    [Fact]
    public void Person_EmptyFirstName_ThrowsException()
    {
        var person = new Person(1, "Test", "User", DateTime.Now.AddYears(-20), "555-55-5555");

        Assert.Throws<ArgumentException>(() => person.FirstName = "");
    }

    [Fact]
    public void Person_CreatedAt_IsSet()
    {
        var person = new Person(1, "Eve", "Adams", DateTime.Now.AddYears(-25), "666-66-6666");

        Assert.NotEqual(default(DateTime), person.CreatedAt);
        Assert.True(person.CreatedAt <= DateTime.Now);
    }
}

// Exercise 04: Invariants Lab - Order class
public class OrderTests
{
    [Fact]
    public void Order_ValidConstruction_Succeeds()
    {
        var order = new Order(1001, "Alice");

        Assert.Equal(1001, order.OrderId);
        Assert.Equal("Alice", order.Customer);
        Assert.Equal(OrderStatus.Pending, order.Status);
        Assert.Equal(0, order.ItemCount);
        Assert.True(order.CanModify);
    }

    [Fact]
    public void Order_ZeroId_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Order(0, "Customer"));
    }

    [Fact]
    public void Order_EmptyCustomer_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Order(1, ""));
    }

    [Fact]
    public void Order_AddItem_IncreasesTotal()
    {
        var order = new Order(1, "Bob");
        order.AddItem("Laptop", 999.99m);

        Assert.Equal(1, order.ItemCount);
        Assert.Equal(999.99m, order.ItemTotal);
    }

    [Fact]
    public void Order_ShippingCost_FreeOver100()
    {
        var order = new Order(1, "Charlie");
        order.AddItem("Item", 150m);

        Assert.Equal(0m, order.ShippingCost);
    }

    [Fact]
    public void Order_ShippingCost_TenUnder100()
    {
        var order = new Order(1, "Diana");
        order.AddItem("Item", 50m);

        Assert.Equal(10m, order.ShippingCost);
    }

    [Fact]
    public void Order_ApplyDiscount_ReducesTotal()
    {
        var order = new Order(1, "Eve");
        order.AddItem("Item", 100m);
        order.ApplyDiscount(10);

        Assert.Equal(10m, order.DiscountAmount);
        Assert.Equal(90m, order.FinalTotal);
    }

    [Fact]
    public void Order_DiscountOver50_ThrowsException()
    {
        var order = new Order(1, "Frank");
        order.AddItem("Item", 100m);

        Assert.Throws<ArgumentOutOfRangeException>(() => order.ApplyDiscount(75));
    }

    [Fact]
    public void Order_ModifyShippedOrder_ThrowsException()
    {
        var order = new Order(1, "Grace");
        order.AddItem("Item", 50m);
        order.UpdateStatus(OrderStatus.Processing);
        order.UpdateStatus(OrderStatus.Shipped);

        Assert.Throws<InvalidOperationException>(() => order.AddItem("Another", 30m));
    }

    [Fact]
    public void Order_RemoveLastItem_ThrowsException()
    {
        var order = new Order(1, "Henry");
        order.AddItem("OnlyItem", 50m);

        Assert.Throws<InvalidOperationException>(() => order.RemoveItem("OnlyItem"));
    }

    [Fact]
    public void Order_ValidStatusTransition_Succeeds()
    {
        var order = new Order(1, "Ivy");
        order.AddItem("Item", 50m);

        order.UpdateStatus(OrderStatus.Processing);
        Assert.Equal(OrderStatus.Processing, order.Status);

        order.UpdateStatus(OrderStatus.Shipped);
        Assert.Equal(OrderStatus.Shipped, order.Status);

        order.UpdateStatus(OrderStatus.Delivered);
        Assert.Equal(OrderStatus.Delivered, order.Status);
    }

    [Fact]
    public void Order_InvalidStatusTransition_ThrowsException()
    {
        var order = new Order(1, "Jack");
        order.AddItem("Item", 50m);
        order.UpdateStatus(OrderStatus.Processing);
        order.UpdateStatus(OrderStatus.Shipped);

        Assert.Throws<InvalidOperationException>(() => order.UpdateStatus(OrderStatus.Pending));
    }

    [Fact]
    public void Order_CancelPending_Succeeds()
    {
        var order = new Order(1, "Kate");
        order.AddItem("Item", 50m);
        order.Cancel();

        Assert.Equal(OrderStatus.Cancelled, order.Status);
    }

    [Fact]
    public void Order_CancelShipped_ThrowsException()
    {
        var order = new Order(1, "Leo");
        order.AddItem("Item", 50m);
        order.UpdateStatus(OrderStatus.Processing);
        order.UpdateStatus(OrderStatus.Shipped);

        Assert.Throws<InvalidOperationException>(() => order.Cancel());
    }

    [Fact]
    public void Order_CanModify_ReflectsStatus()
    {
        var order = new Order(1, "Mia");
        order.AddItem("Item", 50m);

        Assert.True(order.CanModify);

        order.UpdateStatus(OrderStatus.Processing);
        Assert.True(order.CanModify);

        order.UpdateStatus(OrderStatus.Shipped);
        Assert.False(order.CanModify);
    }

    [Fact]
    public void Order_FinalTotal_CalculatesCorrectly()
    {
        var order = new Order(1, "Noah");
        order.AddItem("Item1", 50m);
        order.AddItem("Item2", 30m);
        order.ApplyDiscount(10);

        // ItemTotal = 80, Discount = 8, Shipping = 10
        // FinalTotal = 80 - 8 + 10 = 82
        Assert.Equal(82m, order.FinalTotal);
    }
}

// Include solution classes for testing
// (Copy from solution files - abbreviated here for space)

class ShoppingCart
{
    private string customerName;
    private List<(string Name, decimal Price)> items;
    private int maxItems;
    
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
    
    public int ItemCount => items.Count;
    public decimal TotalPrice => items.Sum(i => i.Price);
    public bool IsFull => items.Count >= maxItems;
    
    public ShoppingCart(string customerName, int maxItems = 10)
    {
        if (maxItems <= 0)
            throw new ArgumentException("Max items must be positive");
        
        CustomerName = customerName;
        this.maxItems = maxItems;
        items = new List<(string, decimal)>();
    }
    
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

class Ticket
{
    private string eventName;
    private decimal price;
    private int availableSeats;
    
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
    
    public Ticket(string eventName, decimal price, int availableSeats)
    {
        EventName = eventName;
        Price = price;
        AvailableSeats = availableSeats;
    }
    
    public void BookSeats(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(count),
                $"Seat count must be positive (got: {count})"
            );
        }
        
        if (count > AvailableSeats)
        {
            throw new InvalidOperationException(
                $"Cannot book {count} seats - only {AvailableSeats} available"
            );
        }
        
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

class Person
{
    private string firstName;
    private string lastName;
    
    public int Id { get; }
    public DateTime BirthDate { get; }
    public DateTime CreatedAt { get; }
    
    public string SocialSecurity { get; init; }
    
    public string FirstName
    {
        get { return firstName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("First name is required");
            firstName = value;
        }
    }
    
    public string LastName
    {
        get { return lastName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Last name is required");
            lastName = value;
        }
    }
    
    public int Age
    {
        get
        {
            var today = DateTime.Today;
            int age = today.Year - BirthDate.Year;
            if (BirthDate.Date > today.AddYears(-age))
                age--;
            return age;
        }
    }
    
    public bool IsAdult => Age >= 18;
    
    public string FullName => $"{FirstName} {LastName}";
    
    public int YearsRegistered
    {
        get
        {
            var span = DateTime.Now - CreatedAt;
            return (int)(span.TotalDays / 365.25);
        }
    }
    
    public DateTime NextBirthday
    {
        get
        {
            var today = DateTime.Today;
            var next = new DateTime(today.Year, BirthDate.Month, BirthDate.Day);
            if (next < today)
                next = next.AddYears(1);
            return next;
        }
    }
    
    public int DaysUntilBirthday
    {
        get
        {
            return (NextBirthday - DateTime.Today).Days;
        }
    }
    
    public Person(int id, string firstName, string lastName, DateTime birthDate, string ssn)
    {
        if (id <= 0)
            throw new ArgumentException("ID must be positive");
        
        if (birthDate > DateTime.Now)
            throw new ArgumentException("Birth date cannot be in future");
        
        Id = id;
        BirthDate = birthDate;
        FirstName = firstName;
        LastName = lastName;
        SocialSecurity = ssn;
        CreatedAt = DateTime.Now;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Name: {FullName}");
        Console.WriteLine($"Social Security: {SocialSecurity}");
        Console.WriteLine($"Birth Date: {BirthDate:yyyy-MM-dd}");
        Console.WriteLine($"Age: {Age} years");
        Console.WriteLine($"Status: {(IsAdult ? "Adult" : "Minor")}");
        Console.WriteLine($"Created: {CreatedAt:yyyy-MM-dd}");
        Console.WriteLine($"Years Registered: {YearsRegistered}");
        Console.WriteLine($"Next Birthday: {NextBirthday:yyyy-MM-dd} (in {DaysUntilBirthday} days)");
    }
}

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
    
    public int OrderId { get; }
    public string Customer { get; }
    public OrderStatus Status { get; private set; }
    
    public int ItemCount => items.Count;
    public decimal ItemTotal => items.Sum(i => i.Price);
    public decimal DiscountAmount => ItemTotal * (discountPercentage / 100);
    public decimal ShippingCost => ItemTotal >= 100 ? 0 : 10;
    public decimal FinalTotal => ItemTotal - DiscountAmount + ShippingCost;
    public bool CanModify => Status != OrderStatus.Shipped && 
                             Status != OrderStatus.Delivered && 
                             Status != OrderStatus.Cancelled;
    
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