# Exercise 10-01: Single Responsibility Principle

## Goal

Master the Single Responsibility Principle by refactoring a messy "god class" into focused, single-purpose classes. You'll learn to identify different responsibilities and separate them cleanly.

## Background

The Single Responsibility Principle states that a class should have only one reason to change. When classes do too many things, they become hard to understand, test, and maintain. This exercise teaches you to recognize multiple responsibilities and separate them properly.

## Instructions

You will refactor a poorly designed `StudentManager` class that violates SRP.

### Part 1: Analyze the Problem

Study the provided `StudentManager` class. It currently handles:
- Student data (name, grades)
- Grade calculation and validation
- File I/O for saving/loading
- Console UI for displaying results

This is a violation of SRP—one class with four different responsibilities!

### Part 2: Create Focused Classes

Refactor into these separate classes:

1. **Student** - Just holds student data (name, grades list)
2. **GradeCalculator** - Calculates average, letter grade, GPA
3. **StudentRepository** - Handles saving/loading from files
4. **StudentUI** - Displays student information to console

### Part 3: Update Main Program

Modify `Main()` to use your new classes working together:
- Create a `Student` object
- Use `GradeCalculator` to compute average
- Use `StudentUI` to display the results
- Use `StudentRepository` to save the student

## Requirements

Your solution must:
1. Create four separate classes, each with a single responsibility
2. Remove all multi-purpose code from the original class
3. Each class should have one clear reason to change
4. Classes should work together through clean interfaces
5. Demonstrate the refactored code working correctly
6. Show how this makes testing and changes easier

## Expected Output

```
=== Student Management System ===

Student: Alice Johnson
Grades: 85, 92, 78, 95, 88

--- Grade Report ---
Average: 87.6
Letter Grade: B
GPA: 3.3

Student data saved to file.

Demonstrating SRP benefits:
✓ Student class only manages data
✓ GradeCalculator only computes grades
✓ StudentRepository only handles file I/O
✓ StudentUI only handles display

Easy to test: Each class can be tested independently
Easy to change: Changing display doesn't affect calculations
Easy to reuse: Can use GradeCalculator in other contexts
```

## Starter Code

```csharp
// ❌ This violates SRP - DO NOT use this design!
// Your job is to refactor it into focused classes

class StudentManager
{
    public string Name { get; set; }
    public List<int> Grades { get; set; }
    
    public StudentManager(string name)
    {
        Name = name;
        Grades = new List<int>();
    }
    
    // Responsibility 1: Manage student data
    public void AddGrade(int grade)
    {
        if (grade < 0 || grade > 100)
        {
            Console.WriteLine("Invalid grade!");
            return;
        }
        Grades.Add(grade);
    }
    
    // Responsibility 2: Calculate grades
    public double CalculateAverage()
    {
        if (Grades.Count == 0) return 0;
        double sum = 0;
        foreach (var grade in Grades)
        {
            sum += grade;
        }
        return sum / Grades.Count;
    }
    
    public string GetLetterGrade()
    {
        double avg = CalculateAverage();
        if (avg >= 90) return "A";
        if (avg >= 80) return "B";
        if (avg >= 70) return "C";
        if (avg >= 60) return "D";
        return "F";
    }
    
    // Responsibility 3: Display to console
    public void DisplayReport()
    {
        Console.WriteLine($"\n--- Grade Report ---");
        Console.WriteLine($"Student: {Name}");
        Console.Write("Grades: ");
        Console.WriteLine(string.Join(", ", Grades));
        Console.WriteLine($"Average: {CalculateAverage():F1}");
        Console.WriteLine($"Letter Grade: {GetLetterGrade()}");
    }
    
    // Responsibility 4: Save/load from file
    public void SaveToFile()
    {
        string filename = $"{Name.Replace(" ", "_")}.txt";
        List<string> lines = new List<string>();
        lines.Add(Name);
        lines.Add(string.Join(",", Grades));
        File.WriteAllLines(filename, lines);
        Console.WriteLine($"Saved to {filename}");
    }
    
    public static StudentManager LoadFromFile(string filename)
    {
        string[] lines = File.ReadAllLines(filename);
        StudentManager student = new StudentManager(lines[0]);
        string[] gradeStrings = lines[1].Split(',');
        foreach (string gradeStr in gradeStrings)
        {
            student.AddGrade(int.Parse(gradeStr));
        }
        return student;
    }
}
```

## Hints

**Student class (data only):**
```csharp
class Student
{
    public string Name { get; set; }
    public List<int> Grades { get; private set; }
    
    public Student(string name)
    {
        Name = name;
        Grades = new List<int>();
    }
    
    public void AddGrade(int grade)
    {
        if (grade >= 0 && grade <= 100)
        {
            Grades.Add(grade);
        }
    }
}
```

**GradeCalculator class (calculations only):**
```csharp
class GradeCalculator
{
    public double CalculateAverage(List<int> grades)
    {
        if (grades.Count == 0) return 0;
        return grades.Average();  // Or use manual loop
    }
    
    public string GetLetterGrade(double average)
    {
        // Convert average to letter grade
    }
    
    public double CalculateGPA(double average)
    {
        // Convert average to 4.0 scale
        return average / 25.0;  // Simplified conversion
    }
}
```

**StudentRepository class (file I/O only):**
```csharp
class StudentRepository
{
    public void Save(Student student, string filename)
    {
        // Write student data to file
    }
    
    public Student Load(string filename)
    {
        // Read student data from file
        return null;  // Replace with actual load logic
    }
}
```

**StudentUI class (display only):**
```csharp
class StudentUI
{
    public void DisplayReport(Student student, GradeCalculator calculator)
    {
        Console.WriteLine($"\nStudent: {student.Name}");
        Console.Write("Grades: ");
        Console.WriteLine(string.Join(", ", student.Grades));
        
        double avg = calculator.CalculateAverage(student.Grades);
        Console.WriteLine($"\n--- Grade Report ---");
        Console.WriteLine($"Average: {avg:F1}");
        Console.WriteLine($"Letter Grade: {calculator.GetLetterGrade(avg)}");
        Console.WriteLine($"GPA: {calculator.CalculateGPA(avg):F1}");
    }
}
```

## Common Mistakes to Avoid

- ❌ Putting multiple responsibilities in one class
- ❌ Making classes depend on concrete implementations instead of passing data
- ❌ Creating classes that are too small (one method per class is overkill)
- ❌ Mixing business logic with UI or file I/O

## Bonus Challenge

Enhance your refactored design:

1. **Add validation class** - Create `GradeValidator` to validate grade inputs
2. **Multiple file formats** - Create `IStudentRepository` interface with JSON and CSV implementations
3. **Different UI** - Create `HTMLStudentUI` that generates HTML reports
4. **Student comparison** - Add `StudentComparator` for ranking students
5. **Grade statistics** - Create `GradeStatistics` for class-wide metrics

Example bonus structure:
```csharp
interface IStudentRepository
{
    void Save(Student student, string filename);
    Student Load(string filename);
}

class JsonStudentRepository : IStudentRepository
{
    // JSON format implementation
}

class CsvStudentRepository : IStudentRepository
{
    // CSV format implementation
}

// Easy to swap implementations!
IStudentRepository repo = new JsonStudentRepository();
// or
IStudentRepository repo = new CsvStudentRepository();
```

## What You're Learning

- How to identify multiple responsibilities in a class
- Breaking down complex classes into focused components
- Making code easier to test by separating concerns
- Building systems where each class has one reason to change
- The foundation for all other SOLID principles

## Reflection Questions

After completing this exercise, consider:

1. Why is testing easier with separated classes?
2. If you need to change how grades are displayed, which class changes?
3. If you need to add XML file support, which class changes?
4. How does SRP make your code more maintainable?
5. What are the signs that a class is doing too much?

---

**Time Estimate:** 40 minutes  
**Difficulty:** Medium  
**Type:** Design Principle - SRP

**Next:** After completing this exercise, move to [Exercise 10-02: Open/Closed Principle](../02-open-closed/) to learn about extensible design.