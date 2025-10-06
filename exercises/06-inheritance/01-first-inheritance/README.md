# Exercise 06-01: First Inheritance

## Goal

Create your first class hierarchy with a base class and two derived classes. You'll learn how inheritance eliminates code duplication and allows classes to share common functionality.

## Background

When multiple classes share common properties and methods, inheritance helps you write the shared code once in a base class. Derived classes automatically inherit all the base class functionality and can add their own unique features.

## Instructions

You're building a simple library system. Books and magazines share many properties (title, author, year), but magazines also have an issue number and books have a page count.

1. Create a `Publication` base class with shared properties
2. Create a `Book` derived class that adds book-specific features
3. Create a `Magazine` derived class that adds magazine-specific features
4. Test your inheritance by creating instances and displaying information

## Requirements

1. **Publication base class must have:**
   - Title property (string)
   - Author property (string)
   - Year property (int)
   - Constructor that initializes all three properties
   - Method `DisplayInfo()` that prints publication details

2. **Book derived class must:**
   - Inherit from Publication
   - Add PageCount property (int)
   - Have constructor that accepts title, author, year, and pageCount
   - Call base constructor using `:base()`
   - Override or extend DisplayInfo() to include page count

3. **Magazine derived class must:**
   - Inherit from Publication
   - Add IssueNumber property (int)
   - Have constructor that accepts title, author, year, and issueNumber
   - Call base constructor using `:base()`
   - Override or extend DisplayInfo() to include issue number

4. **In Main() create and display:**
   - At least one Book instance
   - At least one Magazine instance
   - Show that both can use the base class properties

## Expected Output

```
=== Library System ===

Publication: The C# Programming Language
Author: Anders Hejlsberg
Year: 2010
Pages: 784

Publication: WIRED Magazine
Author: Various
Year: 2024
Issue: #12

Book title: The C# Programming Language
Magazine issue: #12
```

## Hints

**Publication base class:**
```csharp
class Publication
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    
    public Publication(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"Publication: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Year: {Year}");
    }
}
```

**Book derived class:**
```csharp
class Book : Publication
{
    public int PageCount { get; set; }
    
    // TODO: Add constructor that calls base constructor
    // TODO: Add method to display book info including pages
}
```

**Calling base constructor:**
```csharp
public Book(string title, string author, int year, int pageCount) 
    : base(title, author, year)
{
    PageCount = pageCount;
}
```

## Bonus Challenge

Add these enhancements to deepen your understanding:

1. **Add validation** - Ensure year is between 1900 and current year, page count and issue number are positive
2. **Add more derived classes** - Create `Newspaper` with a `Section` property, or `EBook` with `FileSize`
3. **Add virtual methods** - Make DisplayInfo() virtual in base class and override it in derived classes
4. **Add computed properties** - Add `Age` property that calculates years since publication
5. **Create a collection** - Store all publications in a List and display them all

## What You'll Learn

- How to create a base class with shared functionality
- How to derive classes using `:` syntax
- How to call base class constructors with `:base()`
- How inheritance eliminates code duplication
- The relationship between base and derived classes

Remember: Inheritance models "is-a" relationships. A Book **is a** Publication. A Magazine **is a** Publication.