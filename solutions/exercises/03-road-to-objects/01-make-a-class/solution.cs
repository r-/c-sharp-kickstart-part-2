// Exercise 03-01: Make Your First Class - SOLUTION
// This is a reference solution for teachers

using System;

class Pet
{
    // Fields: What a Pet HAS
    public string Name;
    public int Happiness;
    
    // Methods: What a Pet CAN DO
    public void Play()
    {
        Happiness += 10;
    }
    
    public void Feed()
    {
        Happiness += 15;
    }
    
    public void ShowStatus()
    {
        Console.WriteLine($"{Name}'s happiness: {Happiness}");
    }
}

class Program
{
    static void Main()
    {
        // Create two Pet objects
        Pet pet1 = new Pet();
        Pet pet2 = new Pet();
        
        // Set their names and initial happiness
        pet1.Name = "Fluffy";
        pet1.Happiness = 50;
        
        pet2.Name = "Rex";
        pet2.Happiness = 50;
        
        // Interact with the pets
        pet1.Play();
        pet1.Feed();  // Fluffy: 50 + 10 + 15 = 75
        
        pet2.Play();  // Rex: 50 + 10 = 60
        
        // Display results
        pet1.ShowStatus();
        pet2.ShowStatus();
    }
}