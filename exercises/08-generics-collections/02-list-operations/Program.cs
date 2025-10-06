using System;
using System.Collections.Generic;

class StudentRoster
{
    // TODO: Add private List<string> field for students
    
    
    // TODO: Add Count property that returns students.Count
    
    
    // TODO: Add constructor that initializes the list
    
    
    // TODO: Part 1 - Implement AddStudent method
    // Hint: Check for null/empty, check for duplicates, then add
    
    
    // TODO: Part 1 - Implement RemoveStudent method
    // Hint: Use students.Remove(name)
    
    
    // TODO: Part 1 - Implement DisplayStudents method
    // Hint: Loop through with index, display numbered list
    
    
    // TODO: Part 2 - Implement Contains method
    
    
    // TODO: Part 2 - Implement IndexOf method
    
    
    // TODO: Part 2 - Implement GetStudent method
    // Hint: Check index bounds first
    
    
    // TODO: Part 2 - Implement IsEmpty method
    
    
    // TODO: Part 3 - Implement InsertStudent method
    // Hint: Check index bounds and duplicates
    
    
    // TODO: Part 3 - Implement Clear method
    
    
    // TODO: Part 3 - Implement GetStudentsByLetter method
    // Hint: Create new list, loop and check StartsWith
    
    
    // TODO: Part 3 - Implement SortStudents method
    // Hint: Create copy of list, call Sort(), return it
    
    
    // TODO: Part 3 - Implement ReverseOrder method
    // Hint: Create copy, call Reverse(), return it
    
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Student Roster Manager ===\n");
        
        StudentRoster roster = new StudentRoster();
        
        // TODO: Test adding students
        Console.WriteLine("Adding students:");
        
        
        // TODO: Display roster
        
        
        // TODO: Test searching (Contains, IndexOf)
        Console.WriteLine("\nSearching:");
        
        
        // TODO: Test removing a student
        
        
        // TODO: Display roster again
        
        
        // TODO: Test inserting at position
        
        
        // TODO: Display roster
        
        
        // TODO: Test GetStudentsByLetter
        
        
        // TODO: Test sorting
        Console.WriteLine("\nSorted roster:");
        
        
        // TODO: Test reversing
        Console.WriteLine("\nReversed roster:");
        
        
        // TODO: Test clearing
        
    }
}