using System;

class Temperature
{
    // Private fields
    private double celsius;
    private string locationName;
    
    // Validated Celsius property
    public double Celsius
    {
        get { return celsius; }
        set
        {
            if (value < -273.15)
            {
                Console.WriteLine("Error: Temperature cannot be below -273.15°C (absolute zero)");
                return;
            }
            if (value > 100)
            {
                Console.WriteLine("Error: Temperature cannot exceed 100°C");
                return;
            }
            celsius = value;
        }
    }
    
    // Validated LocationName property
    public string LocationName
    {
        get { return locationName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Error: Location name is required");
                return;
            }
            if (value.Length < 3)
            {
                Console.WriteLine("Error: Location name must be at least 3 characters");
                return;
            }
            if (value.Length > 50)
            {
                Console.WriteLine("Error: Location name cannot exceed 50 characters");
                return;
            }
            locationName = value;
        }
    }
    
    // Read-only Timestamp property
    public DateTime Timestamp { get; init; }
    
    // Computed properties
    public double Fahrenheit => celsius * 9.0 / 5.0 + 32.0;
    public double Kelvin => celsius + 273.15;
    
    // Constructor
    public Temperature(string location, double celsius)
    {
        LocationName = location;  // Uses property setter with validation
        Celsius = celsius;        // Uses property setter with validation
        Timestamp = DateTime.Now;
    }
    
    // Display temperature information
    public void DisplayInfo()
    {
        Console.WriteLine($"Location: {LocationName}");
        Console.WriteLine($"Celsius: {Celsius:F2}°C");
        Console.WriteLine($"Fahrenheit: {Fahrenheit:F2}°F");
        Console.WriteLine($"Kelvin: {Kelvin:F2}K");
        Console.WriteLine($"Recorded: {Timestamp:yyyy-MM-dd HH:mm:ss}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Creating temperature readings...\n");
        
        // Create valid temperature reading
        Temperature temp = new Temperature("Stockholm", 22.0);
        temp.DisplayInfo();
        
        // Test validation
        Console.WriteLine("\nTesting validation...");
        temp.Celsius = -300;     // Below absolute zero
        temp.Celsius = 150;      // Too hot
        temp.LocationName = "AB"; // Too short
        temp.LocationName = new string('X', 60); // Too long
        
        Console.WriteLine($"\nStockholm temperature is still {temp.Celsius:F2}°C (valid state maintained)");
    }
}