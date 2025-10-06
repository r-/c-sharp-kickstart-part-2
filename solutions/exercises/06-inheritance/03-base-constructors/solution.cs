using System;
using System.Collections.Generic;

namespace Part2.Ch06.Ex03.Solution
{
    // Base Vehicle class with validation
    class Vehicle
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal DailyRate { get; protected set; }
        
        public Vehicle(string make, string model, int year, decimal dailyRate)
        {
            if (string.IsNullOrWhiteSpace(make))
                throw new ArgumentException("Make is required");
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model is required");
            if (year < 1900 || year > DateTime.Now.Year)
                throw new ArgumentException($"Year must be between 1900 and {DateTime.Now.Year}");
            if (dailyRate <= 0)
                throw new ArgumentException("Daily rate must be positive");
            
            Make = make;
            Model = model;
            Year = year;
            DailyRate = dailyRate;
        }
        
        public virtual string GetRentalInfo()
        {
            return $"{Year} {Make} {Model} - ${DailyRate:F2}/day";
        }
    }
    
    // Car derived class
    class Car : Vehicle
    {
        public int NumberOfDoors { get; set; }
        
        public Car(string make, string model, int year, decimal dailyRate, int doors)
            : base(make, model, year, dailyRate)
        {
            if (doors != 2 && doors != 4 && doors != 5)
                throw new ArgumentException("Car must have 2, 4, or 5 doors");
            
            NumberOfDoors = doors;
        }
        
        public override string GetRentalInfo()
        {
            return $"{base.GetRentalInfo()} - {NumberOfDoors} doors";
        }
    }
    
    // Truck derived class
    class Truck : Vehicle
    {
        public int PayloadCapacity { get; set; }
        
        public Truck(string make, string model, int year, decimal dailyRate, int capacity)
            : base(make, model, year, dailyRate)
        {
            if (capacity <= 0)
                throw new ArgumentException("Payload capacity must be positive");
            
            PayloadCapacity = capacity;
        }
        
        public override string GetRentalInfo()
        {
            return $"{base.GetRentalInfo()} - {PayloadCapacity} kg capacity";
        }
    }
    
    // Motorcycle derived class with two constructors
    class Motorcycle : Vehicle
    {
        public int EngineSize { get; set; }
        
        // Full constructor
        public Motorcycle(string make, string model, int year, decimal dailyRate, int engineSize)
            : base(make, model, year, dailyRate)
        {
            if (engineSize < 50 || engineSize > 2000)
                throw new ArgumentException("Engine size must be between 50 and 2000 cc");
            
            EngineSize = engineSize;
        }
        
        // Simplified constructor with defaults
        public Motorcycle(string make, string model, int year)
            : this(make, model, year, 50.00m, 500)
        {
        }
        
        public override string GetRentalInfo()
        {
            return $"{base.GetRentalInfo()} - {EngineSize} cc";
        }
    }
    
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Vehicle Rental System ===");
            Console.WriteLine();
            Console.WriteLine("Creating valid vehicles...");
            
            List<Vehicle> vehicles = new List<Vehicle>();
            
            // Create valid vehicles
            try
            {
                Car car = new Car("Volvo", "V90", 2023, 85.00m, 4);
                Console.WriteLine($"✓ Car created: {car.GetRentalInfo()}");
                vehicles.Add(car);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            try
            {
                Truck truck = new Truck("Ford", "F-150", 2022, 120.00m, 1000);
                Console.WriteLine($"✓ Truck created: {truck.GetRentalInfo()}");
                vehicles.Add(truck);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            try
            {
                Motorcycle bike = new Motorcycle("Harley Davidson", "Street", 2024, 65.00m, 883);
                Console.WriteLine($"✓ Motorcycle created: {bike.GetRentalInfo()}");
                vehicles.Add(bike);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            Console.WriteLine();
            Console.WriteLine("Testing invalid data...");
            
            // Test invalid year
            try
            {
                Car badCar = new Car("Toyota", "Camry", 2050, 75.00m, 4);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Error creating car: {ex.Message}");
            }
            
            // Test invalid payload
            try
            {
                Truck badTruck = new Truck("GMC", "Sierra", 2023, 100.00m, -500);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Error creating truck: {ex.Message}");
            }
            
            // Test invalid engine size
            try
            {
                Motorcycle badBike = new Motorcycle("Yamaha", "YZF", 2024, 70.00m, 3000);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Error creating motorcycle: {ex.Message}");
            }
            
            Console.WriteLine();
            Console.WriteLine("Using simplified constructor...");
            
            try
            {
                Motorcycle simpleBike = new Motorcycle("Honda", "CB", 2024);
                Console.WriteLine($"✓ Motorcycle created with defaults: {simpleBike.GetRentalInfo()}");
                vehicles.Add(simpleBike);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            Console.WriteLine();
            Console.WriteLine("All vehicles:");
            foreach (Vehicle vehicle in vehicles)
            {
                Console.WriteLine($"- {vehicle.GetRentalInfo()}");
            }
        }
    }
}