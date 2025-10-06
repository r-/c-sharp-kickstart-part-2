using System;

namespace Part2.Ch06.Ex01
{
    // TODO: Create Publication base class here
    // Should have: Title, Author, Year properties
    // Should have: Constructor and DisplayInfo() method
    
    
    // TODO: Create Book derived class here
    // Should inherit from Publication
    // Should add: PageCount property
    // Should have: Constructor that calls base constructor
    
    
    // TODO: Create Magazine derived class here
    // Should inherit from Publication
    // Should add: IssueNumber property
    // Should have: Constructor that calls base constructor
    
    
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Library System ===");
            Console.WriteLine();
            
            // TODO: Create a Book instance
            // Example: new Book("The C# Programming Language", "Anders Hejlsberg", 2010, 784)
            
            
            // TODO: Display the book information
            
            
            Console.WriteLine();
            
            // TODO: Create a Magazine instance
            // Example: new Magazine("WIRED Magazine", "Various", 2024, 12)
            
            
            // TODO: Display the magazine information
            
            
            Console.WriteLine();
            
            // TODO: Demonstrate that both can access base class properties
            // Example: Console.WriteLine($"Book title: {book.Title}");
        }
    }
}