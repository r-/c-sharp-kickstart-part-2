using System;
using System.Collections.Generic;
using System.IO;

namespace Part2.Ch10.Project
{
    // ❌ THIS CODE VIOLATES ALL SOLID PRINCIPLES!
    // Your mission: Refactor this into a well-designed system
    
    class Library
    {
        private List<string> books = new List<string>();
        private Dictionary<string, string> borrowedBooks = new Dictionary<string, string>();
        
        // VIOLATES SRP - This class does EVERYTHING:
        // - Data storage
        // - Validation
        // - File I/O
        // - Display/UI
        // - Business logic
        
        public void AddBook(string title)
        {
            // Validation mixed with everything else
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("❌ Error: Title cannot be empty");
                return;
            }
            
            // Business logic
            books.Add(title);
            
            // File I/O (tightly coupled to this class)
            File.AppendAllText("books.txt", title + "\n");
            
            // UI/Display
            Console.WriteLine($"✓ Added: {title}");
        }
        
        // VIOLATES OCP - Must modify this method to add new book types
        public void DisplayBooks()
        {
            Console.WriteLine("\n=== Library Books ===");
            foreach (var book in books)
            {
                // Hard-coded logic for each type
                if (book.StartsWith("Fiction:"))
                    Console.WriteLine($"📖 {book}");
                else if (book.StartsWith("NonFiction:"))
                    Console.WriteLine($"📚 {book}");
                else if (book.StartsWith("Technical:"))
                    Console.WriteLine($"💻 {book}");
                // Must add another if-else for every new book type!
                // This violates the Open/Closed Principle
                else
                    Console.WriteLine($"📄 {book}");
            }
        }
        
        // VIOLATES DIP - Tightly coupled to file system
        // High-level library logic depends on low-level file operations
        public void BorrowBook(string title, string borrower)
        {
            if (!books.Contains(title))
            {
                Console.WriteLine("❌ Book not available");
                return;
            }
            
            // Business logic mixed with storage
            borrowedBooks[title] = borrower;
            books.Remove(title);
            
            // Hard-coded file storage - can't swap for database or memory
            File.AppendAllText("borrowed.txt", $"{borrower}:{title}\n");
            
            // Display mixed in
            Console.WriteLine($"✓ {borrower} borrowed {title}");
        }
        
        public void ReturnBook(string title)
        {
            if (!borrowedBooks.ContainsKey(title))
            {
                Console.WriteLine("❌ Book was not borrowed");
                return;
            }
            
            string borrower = borrowedBooks[title];
            borrowedBooks.Remove(title);
            books.Add(title);
            
            Console.WriteLine($"✓ {borrower} returned {title}");
        }
        
        public void ShowBorrowedBooks()
        {
            Console.WriteLine("\n=== Borrowed Books ===");
            if (borrowedBooks.Count == 0)
            {
                Console.WriteLine("No books currently borrowed");
                return;
            }
            
            foreach (var entry in borrowedBooks)
            {
                Console.WriteLine($"📕 {entry.Key} → {entry.Value}");
            }
        }
        
        public void SearchBooks(string searchTerm)
        {
            Console.WriteLine($"\n=== Search Results for '{searchTerm}' ===");
            bool found = false;
            
            foreach (var book in books)
            {
                if (book.ToLower().Contains(searchTerm.ToLower()))
                {
                    Console.WriteLine($"📖 {book}");
                    found = true;
                }
            }
            
            if (!found)
            {
                Console.WriteLine("No books found");
            }
        }
    }
    
    // TODO: Refactor the Library class using SOLID principles
    // 
    // Create these classes:
    // 
    // DATA LAYER:
    // - Book class (data only)
    // - Different book types (FictionBook, NonFictionBook, etc.) if using OCP
    // 
    // REPOSITORY LAYER:
    // - IBookRepository interface
    // - FileBookRepository (file-based storage)
    // - MemoryBookRepository (in-memory storage for testing)
    // 
    // SERVICE LAYER:
    // - BookValidator (validation logic)
    // - LibraryService (business logic)
    // - Uses IBookRepository and ILogger via DI
    // 
    // PRESENTATION LAYER:
    // - LibraryUI (console display)
    // - Uses LibraryService
    // 
    // SUPPORTING:
    // - ILogger interface
    // - FileLogger, ConsoleLogger implementations
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== MESSY Library System (Before Refactoring) ===\n");
            
            // Current messy implementation
            Library library = new Library();
            
            // Add some books
            library.AddBook("Fiction: The Great Gatsby");
            library.AddBook("Fiction: 1984");
            library.AddBook("NonFiction: Sapiens");
            library.AddBook("Technical: Clean Code");
            
            // Display books
            library.DisplayBooks();
            
            // Borrow a book
            Console.WriteLine();
            library.BorrowBook("Technical: Clean Code", "Alice");
            
            // Show borrowed books
            library.ShowBorrowedBooks();
            
            // Search for books
            library.SearchBooks("Fiction");
            
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("\n❌ PROBLEMS WITH THIS DESIGN:");
            Console.WriteLine("1. Library class does EVERYTHING (violates SRP)");
            Console.WriteLine("2. Can't add new book types without modifying DisplayBooks (violates OCP)");
            Console.WriteLine("3. Tightly coupled to file storage (violates DIP)");
            Console.WriteLine("4. Hard to test (can't mock dependencies)");
            Console.WriteLine("5. Hard to maintain (changes affect multiple concerns)");
            
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("\n✅ YOUR TASK:");
            Console.WriteLine("Refactor this code following SOLID principles!");
            Console.WriteLine("\n1. Create separate classes for each responsibility (SRP)");
            Console.WriteLine("2. Use interfaces for extensibility (OCP, DIP)");
            Console.WriteLine("3. Inject dependencies via constructors (DIP)");
            Console.WriteLine("4. Build a clean, layered architecture");
            Console.WriteLine("5. Make it easy to test and extend");
            
            Console.WriteLine("\n" + new string('=', 60));
            
            // TODO: YOUR REFACTORED IMPLEMENTATION GOES HERE
            // 
            // Example of how it should work:
            // 
            // // Create dependencies
            // IBookRepository repository = new FileBookRepository();
            // ILogger logger = new ConsoleLogger();
            // 
            // // Inject dependencies
            // LibraryService service = new LibraryService(repository, logger);
            // LibraryUI ui = new LibraryUI(service);
            // 
            // // Use the system
            // service.AddBook(new Book("The Great Gatsby", "F. Scott Fitzgerald", BookType.Fiction));
            // ui.DisplayBooks();
            // 
            // // Easy to swap implementations!
            // IBookRepository testRepo = new MemoryBookRepository();
            // LibraryService testService = new LibraryService(testRepo, logger);
            // // No changes to service code needed!
            
            Console.WriteLine("\n=== YOUR REFACTORED SOLUTION ===");
            Console.WriteLine("TODO: Implement your SOLID-compliant library system here");
        }
    }
}