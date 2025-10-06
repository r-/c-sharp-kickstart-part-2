using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Part2.Ch10.Ex01
{
    // ✅ FOLLOWS SRP - Each class has ONE responsibility
    
    // Data class - ONLY holds student data
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
    
    // Calculator class - ONLY performs calculations
    class GradeCalculator
    {
        public double CalculateAverage(List<int> grades)
        {
            if (grades.Count == 0) return 0;
            return grades.Average();
        }
        
        public string GetLetterGrade(double average)
        {
            if (average >= 90) return "A";
            if (average >= 80) return "B";
            if (average >= 70) return "C";
            if (average >= 60) return "D";
            return "F";
        }
        
        public double CalculateGPA(double average)
        {
            // Simplified GPA calculation on 4.0 scale
            if (average >= 90) return 4.0;
            if (average >= 80) return 3.0;
            if (average >= 70) return 2.0;
            if (average >= 60) return 1.0;
            return 0.0;
        }
    }
    
    // Repository class - ONLY handles file I/O
    class StudentRepository
    {
        public void Save(Student student, string filename)
        {
            try
            {
                List<string> lines = new List<string>
                {
                    student.Name,
                    string.Join(",", student.Grades)
                };
                File.WriteAllLines(filename, lines);
                Console.WriteLine($"✓ Student data saved to {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error saving file: {ex.Message}");
            }
        }
        
        public Student Load(string filename)
        {
            try
            {
                string[] lines = File.ReadAllLines(filename);
                Student student = new Student(lines[0]);
                
                if (lines.Length > 1 && !string.IsNullOrEmpty(lines[1]))
                {
                    string[] gradeStrings = lines[1].Split(',');
                    foreach (string gradeStr in gradeStrings)
                    {
                        if (int.TryParse(gradeStr, out int grade))
                        {
                            student.AddGrade(grade);
                        }
                    }
                }
                
                return student;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error loading file: {ex.Message}");
                return null;
            }
        }
    }
    
    // UI class - ONLY handles display
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
        
        public void DisplaySRPBenefits()
        {
            Console.WriteLine("\n=== Demonstrating SRP Benefits ===");
            Console.WriteLine("✓ Student class only manages data");
            Console.WriteLine("✓ GradeCalculator only computes grades");
            Console.WriteLine("✓ StudentRepository only handles file I/O");
            Console.WriteLine("✓ StudentUI only handles display");
            Console.WriteLine("\nEasy to test: Each class can be tested independently");
            Console.WriteLine("Easy to change: Changing display doesn't affect calculations");
            Console.WriteLine("Easy to reuse: Can use GradeCalculator in other contexts");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Student Management System (Refactored) ===\n");
            
            // Create instances of our SRP-compliant classes
            Student alice = new Student("Alice Johnson");
            alice.AddGrade(85);
            alice.AddGrade(92);
            alice.AddGrade(78);
            alice.AddGrade(95);
            alice.AddGrade(88);
            
            GradeCalculator calculator = new GradeCalculator();
            StudentUI ui = new StudentUI();
            StudentRepository repo = new StudentRepository();
            
            // Display the report
            ui.DisplayReport(alice, calculator);
            
            // Save to file
            Console.WriteLine();
            repo.Save(alice, "alice_johnson.txt");
            
            // Show SRP benefits
            ui.DisplaySRPBenefits();
            
            // Demonstrate easy testing
            Console.WriteLine("\n=== Testing Individual Components ===");
            
            // Test calculator independently
            List<int> testGrades = new List<int> { 90, 85, 95 };
            double testAvg = calculator.CalculateAverage(testGrades);
            Console.WriteLine($"Calculator test: Average of {{90, 85, 95}} = {testAvg:F1}");
            Console.WriteLine($"Letter grade: {calculator.GetLetterGrade(testAvg)}");
            
            // Test student independently
            Student bob = new Student("Bob Smith");
            bob.AddGrade(75);
            bob.AddGrade(80);
            Console.WriteLine($"\nStudent test: {bob.Name} has {bob.Grades.Count} grades");
            
            Console.WriteLine("\n✅ All components work independently and together!");
        }
    }
}