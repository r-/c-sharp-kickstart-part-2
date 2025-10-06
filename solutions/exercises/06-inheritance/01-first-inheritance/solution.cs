using System;

namespace Part2.Ch06.Ex01.Solution
{
    // Base class with shared functionality
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
        
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Publication: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Year: {Year}");
        }
    }
    
    // Book derived class
    class Book : Publication
    {
        public int PageCount { get; set; }
        
        public Book(string title, string author, int year, int pageCount)
            : base(title, author, year)
        {
            PageCount = pageCount;
        }
        
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Pages: {PageCount}");
        }
    }
    
    // Magazine derived class
    class Magazine : Publication
    {
        public int IssueNumber { get; set; }
        
        public Magazine(string title, string author, int year, int issueNumber)
            : base(title, author, year)
        {
            IssueNumber = issueNumber;
        }
        
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Issue: #{IssueNumber}");
        }
    }
    
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Library System ===");
            Console.WriteLine();
            
            // Create a Book instance
            Book book = new Book("The C# Programming Language", "Anders Hejlsberg", 2010, 784);
            book.DisplayInfo();
            
            Console.WriteLine();
            
            // Create a Magazine instance
            Magazine magazine = new Magazine("WIRED Magazine", "Various", 2024, 12);
            magazine.DisplayInfo();
            
            Console.WriteLine();
            
            // Demonstrate that both can access base class properties
            Console.WriteLine($"Book title: {book.Title}");
            Console.WriteLine($"Magazine issue: #{magazine.IssueNumber}");
        }
    }
}