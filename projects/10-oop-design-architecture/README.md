# Mini Project: Library Management System Refactor

## Project Overview

Transform a messy, poorly-designed library system into YOUR well-architected masterpiece! This is YOUR project to own—take a tightly-coupled, violation-riddled codebase and refactor it using all the SOLID principles you've learned. You'll create a flexible, maintainable system that demonstrates professional software design.

You'll combine everything from Chapter 10: Single Responsibility Principle, Open/Closed Principle, Dependency Inversion, composition over inheritance, and clean architectural patterns to build a library system that's easy to test, extend, and maintain.

## Why This Project?

Refactoring messy code is a critical skill every developer needs. In the real world, you'll rarely start with a clean slate—you'll inherit codebases that need improvement. This project teaches you to:
- Identify design violations systematically
- Apply SOLID principles to improve existing code
- Separate concerns into focused components
- Build systems that are easy to extend
- Create testable, maintainable architectures

These are the same patterns used in professional software development to keep large codebases manageable and flexible.

## Learning Goals

- Apply all SOLID principles in one cohesive system
- Identify and fix violations of each principle
- Design clean, layered architecture
- Use dependency injection effectively
- Build extensible systems using interfaces
- Create focused, single-purpose classes
- Write code that's easy to test and maintain

## The Messy Code

YOU'VE been given this poorly-designed library system to refactor:

```csharp
// ❌ VIOLATES ALL SOLID PRINCIPLES!
class Library
{
    private List<string> books = new List<string>();
    private Dictionary<string, string> borrowedBooks = new Dictionary<string, string>();
    
    // Violates SRP - does EVERYTHING
    public void AddBook(string title)
    {
        // Validation
        if (string.IsNullOrEmpty(title))
        {
            Console.WriteLine("Error: Title cannot be empty");
            return;
        }
        
        // Business logic
        books.Add(title);
        
        // File I/O
        File.AppendAllText("books.txt", title + "\n");
        
        // UI
        Console.WriteLine($"✓ Added: {title}");
    }
    
    // Violates OCP - must modify to add new book types
    public void DisplayBooks()
    {
        Console.WriteLine("=== Library Books ===");
        foreach (var book in books)
        {
            if (book.StartsWith("Fiction:"))
                Console.WriteLine($"📖 {book}");
            else if (book.StartsWith("NonFiction:"))
                Console.WriteLine($"📚 {book}");
            // Must modify this method for every new type!
        }
    }
    
    // Violates DIP - tightly coupled to specific implementations
    public void BorrowBook(string title, string borrower)
    {
        if (!books.Contains(title))
        {
            Console.WriteLine("Book not available");
            return;
        }
        
        borrowedBooks[title] = borrower;
        books.Remove(title);
        
        // Hard-coded file storage
        File.AppendAllText("borrowed.txt", $"{borrower}:{title}\n");
        
        Console.WriteLine($"{borrower} borrowed {title}");
    }
}
```

YOUR mission: Fix all these violations!

## Minimum Requirements

YOUR refactored library system MUST include:

### 1. Apply Single Responsibility Principle

Create separate classes for different responsibilities:
- `Book` class - Represents book data only
- `BookValidator` - Validates book data
- `LibraryService` - Core business logic
- `IBookRepository` interface - Storage abstraction
- `FileBookRepository` - File-based storage implementation
- `LibraryUI` - Console interface/display

**Each class has ONE reason to change!**

### 2. Apply Open/Closed Principle

Design for extension without modification:
- Create `Book` base class or `IBook` interface
- Implement different book types (`FictionBook`, `NonFictionBook`, `ReferenceBook`)
- Add new book types WITHOUT modifying existing code
- Use polymorphism for book-specific behavior

### 3. Apply Dependency Inversion Principle

Depend on abstractions, not concrete classes:
- Create `IBookRepository` interface for storage
- Create `ILogger` interface for logging
- Inject dependencies through constructors
- Demonstrate swapping implementations easily

### 4. Demonstrate Composition Over Inheritance

Where appropriate:
- Use composition to add capabilities
- Avoid deep inheritance hierarchies
- Show flexibility of composed designs

### 5. Build Clean Architecture

Organize YOUR system into layers:
- **Data Layer**: `Book`, `BookData` classes
- **Repository Layer**: `IBookRepository`, implementations
- **Service Layer**: `LibraryService` (business logic)
- **Presentation Layer**: `LibraryUI` (console interface)

### 6. Make It Testable

Design to enable testing:
- Create mock implementations of interfaces
- Demonstrate testing with mocks
- Show how SOLID makes testing easy

## Expected Output

```
=== Library Management System ===

[Using FileRepository and ConsoleLogger]

--- Adding Books ---
✓ Added: The Great Gatsby (Fiction)
✓ Added: Clean Code (Technical)
✓ Added: 1984 (Fiction)

--- Available Books ---
📖 Fiction: The Great Gatsby
💻 Technical: Clean Code
📖 Fiction: 1984

--- Borrowing Book ---
✓ Alice borrowed "Clean Code"
[Logged to file: Alice borrowed Clean Code at 2025-01-06 15:30]

--- Current Inventory ---
📖 Fiction: The Great Gatsby
📖 Fiction: 1984

Borrowed Books:
  Clean Code → Alice

--- Switching to In-Memory Storage ---
[Using MemoryRepository and ConsoleLogger]

✓ Testing mode activated
✓ Added test books
✓ All operations logged to console only

=== SOLID Principles Demonstrated ===

✓ SRP: Each class has ONE responsibility
  - Book: Data only
  - BookValidator: Validation only
  - LibraryService: Business logic only
  - LibraryUI: Display only
  - FileBookRepository: Storage only

✓ OCP: Added ReferenceBook without modifying existing code
  - New book type: Magazine
  - New book type: EBook
  - System extended, not modified!

✓ DIP: High-level code depends on interfaces
  - Swapped FileRepository → MemoryRepository
  - Swapped FileLogger → ConsoleLogger
  - No changes to LibraryService!

✓ Clean Architecture: Clear layer separation
  - Data → Repository → Service → UI
  - Each layer independent
  - Easy to test each layer

=== Benefits ===
✓ Easy to add new book types
✓ Easy to swap storage (file, database, memory)
✓ Easy to test with mock repositories
✓ Easy to add new features
✓ Maintainable and flexible
```

## Implementation Steps

1. **Analyze the messy code**
   - Identify all SOLID violations
   - List all responsibilities
   - Identify tight coupling
   - Plan the refactoring

2. **Create data models**
   - Design `Book` class or interface
   - Create book type classes
   - Add necessary properties
   - Keep them simple (data only)

3. **Define interfaces**
   - `IBookRepository` for storage
   - `ILogger` for logging
   - `IBook` for polymorphic books (optional)
   - Think about abstractions

4. **Build repository layer**
   - Implement `FileBookRepository`
   - Implement `MemoryBookRepository` (for testing)
   - Keep storage logic isolated
   - Handle errors gracefully

5. **Create service layer**
   - Build `LibraryService` with business logic
   - Inject dependencies via constructor
   - Validate through `BookValidator`
   - Keep UI code separate

6. **Build presentation layer**
   - Create `LibraryUI` for display
   - Use `LibraryService` for operations
   - Format output nicely
   - Handle user interaction

7. **Demonstrate flexibility**
   - Show swapping implementations
   - Add new book type
   - Show testing with mocks
   - Prove principles work

## Hints

**Book class (SRP - data only):**
```csharp
class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public BookType Type { get; set; }
    
    public Book(string title, string author, string isbn, BookType type)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        Type = type;
    }
}

enum BookType
{
    Fiction,
    NonFiction,
    Reference,
    Technical
}
```

**Repository interface (DIP):**
```csharp
interface IBookRepository
{
    void Add(Book book);
    void Remove(Book book);
    Book FindByTitle(string title);
    List<Book> GetAllAvailable();
    void SaveBorrowed(string bookTitle, string borrower);
}
```

**File repository implementation:**
```csharp
class FileBookRepository : IBookRepository
{
    private List<Book> books = new List<Book>();
    
    public void Add(Book book)
    {
        books.Add(book);
        // Save to file
        File.AppendAllText("books.txt", 
            $"{book.Title}|{book.Author}|{book.ISBN}|{book.Type}\n");
    }
    
    public Book FindByTitle(string title)
    {
        return books.Find(b => b.Title == title);
    }
    
    // ... other methods
}
```

**Memory repository (for testing):**
```csharp
class MemoryBookRepository : IBookRepository
{
    private List<Book> books = new List<Book>();
    private Dictionary<string, string> borrowed = new Dictionary<string, string>();
    
    public void Add(Book book)
    {
        books.Add(book);
        // No file I/O - just memory
    }
    
    // ... other methods (no file operations)
}
```

**Service layer with DI:**
```csharp
class LibraryService
{
    private IBookRepository repository;
    private ILogger logger;
    
    // Dependency Injection via constructor
    public LibraryService(IBookRepository repo, ILogger log)
    {
        repository = repo;
        logger = log;
    }
    
    public void AddBook(Book book)
    {
        // Validation
        if (string.IsNullOrEmpty(book.Title))
        {
            throw new ArgumentException("Title cannot be empty");
        }
        
        // Business logic
        repository.Add(book);
        logger.Log($"Added book: {book.Title}");
    }
    
    public void BorrowBook(string title, string borrower)
    {
        Book book = repository.FindByTitle(title);
        if (book == null)
        {
            throw new InvalidOperationException("Book not found");
        }
        
        repository.SaveBorrowed(title, borrower);
        repository.Remove(book);
        logger.Log($"{borrower} borrowed {title}");
    }
}
```

**UI layer:**
```csharp
class LibraryUI
{
    private LibraryService service;
    
    public LibraryUI(LibraryService svc)
    {
        service = svc;
    }
    
    public void DisplayBooks(List<Book> books)
    {
        Console.WriteLine("\n=== Available Books ===");
        foreach (var book in books)
        {
            string icon = book.Type == BookType.Fiction ? "📖" : "📚";
            Console.WriteLine($"{icon} {book.Title} by {book.Author}");
        }
    }
    
    public void DisplayBorrowSuccess(string title, string borrower)
    {
        Console.WriteLine($"✓ {borrower} borrowed \"{title}\"");
    }
}
```

**Main program demonstrating flexibility:**
```csharp
// Production configuration
var fileRepo = new FileBookRepository();
var fileLogger = new FileLogger();
var service = new LibraryService(fileRepo, fileLogger);
var ui = new LibraryUI(service);

// Add books
service.AddBook(new Book("1984", "Orwell", "123", BookType.Fiction));
ui.DisplayBooks(service.GetAvailableBooks());

// Easy to switch to testing configuration!
var memoryRepo = new MemoryBookRepository();
var consoleLogger = new ConsoleLogger();
var testService = new LibraryService(memoryRepo, consoleLogger);
// No changes to service code needed!
```

## Make It YOUR Own!

Add YOUR creative features:

### Core Features
- **Search functionality** - Find books by title, author, ISBN
- **Due dates** - Track when books should be returned
- **Late fees** - Calculate fees for overdue books
- **Reservation system** - Reserve books that are borrowed
- **Multiple copies** - Track quantity of each book
- **User accounts** - Track borrowing history per user
- **Categories** - Browse books by category/genre
- **Ratings** - Users can rate books
- **Reviews** - Users can review books

### Advanced Architecture
- **Event system** - Publish events when books borrowed/returned
- **Command pattern** - Undo/redo operations
- **Factory pattern** - Create different book types
- **Observer pattern** - Notify when favorite books available
- **Strategy pattern** - Different late fee calculations
- **Repository pattern variations** - Database, API, cloud storage
- **Unit of Work** - Transactional operations
- **CQRS pattern** - Separate read/write operations

### Storage Options
- **JSON repository** - Store data in JSON format
- **CSV repository** - Import/export CSV files
- **XML repository** - XML-based storage
- **Database repository** - SQLite or SQL Server
- **Cloud repository** - Save to cloud storage
- **Hybrid storage** - Metadata in one place, files in another

### Book Types (OCP)
- **EBook** - Digital books with file size, format
- **AudioBook** - Books with duration, narrator
- **Magazine** - Periodicals with issue number
- **Reference** - Cannot be borrowed, in-library only
- **RareBook** - Special handling, limited access
- **ChildrenBook** - Age rating, illustrations count

### User Experience
- **Interactive menu** - Full menu-driven interface
- **Batch operations** - Add multiple books from file
- **Export reports** - Generate inventory reports
- **Statistics** - Most borrowed books, popular genres
- **Recommendations** - Suggest books based on history
- **Import catalog** - Load books from online catalogs

### Validation & Business Rules
- **Borrowing limits** - Max books per user
- **Membership tiers** - Different privileges
- **ISBN validation** - Validate ISBN format
- **Duplicate detection** - Prevent duplicate books
- **Inventory rules** - Min/max copies per title

## Self-Assessment Checklist

### SOLID Principles
- [ ] SRP: Each class has one responsibility
- [ ] OCP: Can add book types without modifying existing code
- [ ] LSP: Derived book types work as base type
- [ ] ISP: Focused interfaces (if applicable)
- [ ] DIP: Service depends on interfaces, not concrete classes

### Architecture
- [ ] Clear layer separation (Data, Repository, Service, UI)
- [ ] Dependencies flow in correct direction
- [ ] High-level code doesn't depend on low-level details
- [ ] Easy to swap implementations
- [ ] Testable design with mock support

### Code Quality
- [ ] Well-organized class structure
- [ ] Good naming conventions
- [ ] Proper error handling
- [ ] Clean, readable code
- [ ] Comments where needed

### Functionality
- [ ] Can add books
- [ ] Can borrow books
- [ ] Can return books (if implemented)
- [ ] Can search/list books
- [ ] Data persists (if using files)

## What Makes a Great Project?

**SOLID Principles Application (40%)**
- All five principles correctly applied
- Clear demonstration of each principle
- Violations eliminated from original code
- Well-designed abstractions

**Architecture (30%)**
- Clean layer separation
- Proper dependency flow
- Interfaces used appropriately
- Easy to extend and maintain

**Code Quality (20%)**
- Well-organized and readable
- Good separation of concerns
- Proper error handling
- Follows C# conventions

**Functionality (10%)**
- Core features work correctly
- Good user experience
- Robust and reliable
- Creative extensions

## Testing YOUR Design

Demonstrate that YOUR design follows SOLID:

**Test SRP:**
- Change display format → Only UI class changes
- Change storage → Only repository changes
- Change validation → Only validator changes

**Test OCP:**
- Add new book type → No existing code modified
- Add new repository → No service code modified

**Test DIP:**
- Swap FileRepository for MemoryRepository → Works seamlessly
- Swap FileLogger for ConsoleLogger → No service changes needed

**Test Composition:**
- Add new capabilities without deep inheritance
- Show flexibility of composition

## Reflection Questions

After building YOUR library system, consider:

1. Which SOLID violation was hardest to fix? Why?
2. How does SRP make YOUR code easier to test?
3. What would YOU need to change to add a DatabaseRepository?
4. How does DIP make YOUR system more flexible?
5. Where did composition work better than inheritance?
6. How would YOUR architecture handle 100,000 books?
7. What design patterns emerged naturally?
8. How easy is it to add a new feature now?

## Connection to Real-World Systems

YOUR library system uses the same patterns as:
- **E-commerce platforms** - Products, orders, inventory
- **Banking systems** - Accounts, transactions, storage
- **Healthcare systems** - Patients, records, compliance
- **Social media** - Posts, users, notifications
- **Enterprise applications** - Clean architecture everywhere

The SOLID principles YOU applied here scale to systems with millions of users!

---

**Time Estimate:** 3-4 hours  
**Difficulty:** Medium-Hard  
**Type:** Comprehensive SOLID Refactoring Project

This project demonstrates YOUR mastery of object-oriented design. Build it well, and YOU'LL have proven YOU can design professional, maintainable software systems. This is the culmination of everything YOU've learned about OOP in C#!