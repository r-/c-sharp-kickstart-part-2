using System;
using System.Collections.Generic;
using System.Linq;

class StudentRoster
{
    private List<string> students;
    
    public int Count => students.Count;
    
    public StudentRoster()
    {
        students = new List<string>();
    }
    
    // Part 1: Basic List Operations
    
    public void AddStudent(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be empty");
            return;
        }
        
        if (students.Contains(name))
        {
            Console.WriteLine($"✗ {name} already exists");
            return;
        }
        
        students.Add(name);
        Console.WriteLine($"✓ Added: {name}");
    }
    
    public void RemoveStudent(string name)
    {
        if (students.Remove(name))
        {
            Console.WriteLine($"✓ Removed {name}");
        }
        else
        {
            Console.WriteLine($"✗ {name} not found");
        }
    }
    
    public void DisplayStudents()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("Roster is empty");
            return;
        }
        
        Console.WriteLine($"\nCurrent roster ({students.Count} students):");
        for (int i = 0; i < students.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {students[i]}");
        }
    }
    
    // Part 2: Search and Query Operations
    
    public bool Contains(string name)
    {
        return students.Contains(name);
    }
    
    public int IndexOf(string name)
    {
        return students.IndexOf(name);
    }
    
    public string GetStudent(int index)
    {
        if (index < 0 || index >= students.Count)
        {
            Console.WriteLine("Invalid index");
            return null;
        }
        return students[index];
    }
    
    public bool IsEmpty()
    {
        return students.Count == 0;
    }
    
    // Part 3: Advanced List Operations
    
    public void InsertStudent(int index, string name)
    {
        if (index < 0 || index > students.Count)
        {
            Console.WriteLine("Invalid insertion index");
            return;
        }
        
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be empty");
            return;
        }
        
        if (students.Contains(name))
        {
            Console.WriteLine($"✗ {name} already exists");
            return;
        }
        
        students.Insert(index, name);
        Console.WriteLine($"✓ Inserted {name} at index {index}");
    }
    
    public void Clear()
    {
        students.Clear();
        Console.WriteLine("✓ Roster cleared");
    }
    
    public List<string> GetStudentsByLetter(char letter)
    {
        List<string> result = new List<string>();
        
        foreach (string name in students)
        {
            if (name.StartsWith(letter.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                result.Add(name);
            }
        }
        
        return result;
    }
    
    public List<string> SortStudents()
    {
        List<string> sorted = new List<string>(students);
        sorted.Sort();
        return sorted;
    }
    
    public List<string> ReverseOrder()
    {
        List<string> reversed = new List<string>(students);
        reversed.Reverse();
        return reversed;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Student Roster Manager ===\n");
        
        StudentRoster roster = new StudentRoster();
        
        // Test adding students
        Console.WriteLine("Adding students:");
        roster.AddStudent("Alice");
        roster.AddStudent("Bob");
        roster.AddStudent("Carol");
        roster.AddStudent("David");
        roster.AddStudent("Alice");  // Duplicate
        
        // Display roster
        roster.DisplayStudents();
        
        // Test searching
        Console.WriteLine("\nSearching:");
        Console.WriteLine($"Contains 'Bob': {roster.Contains("Bob")}");
        Console.WriteLine($"Contains 'Eve': {roster.Contains("Eve")}");
        Console.WriteLine($"Index of 'Carol': {roster.IndexOf("Carol")}");
        
        // Test removing a student
        Console.WriteLine($"\nRemoving 'Bob':");
        roster.RemoveStudent("Bob");
        
        // Display roster again
        roster.DisplayStudents();
        
        // Test inserting at position
        Console.WriteLine($"\nInserting 'Emma' at position 1:");
        roster.InsertStudent(1, "Emma");
        
        // Display roster
        roster.DisplayStudents();
        
        // Test GetStudentsByLetter
        Console.WriteLine($"\nStudents starting with 'A':");
        List<string> aStudents = roster.GetStudentsByLetter('A');
        foreach (string name in aStudents)
        {
            Console.WriteLine($"- {name}");
        }
        
        // Test sorting
        Console.WriteLine("\nSorted roster:");
        List<string> sorted = roster.SortStudents();
        for (int i = 0; i < sorted.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {sorted[i]}");
        }
        
        // Test reversing
        Console.WriteLine("\nReversed roster:");
        List<string> reversed = roster.ReverseOrder();
        for (int i = 0; i < reversed.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {reversed[i]}");
        }
        
        // Test clearing
        Console.WriteLine("\nClearing roster...");
        roster.Clear();
        Console.WriteLine($"Is empty: {roster.IsEmpty()}");
    }
}