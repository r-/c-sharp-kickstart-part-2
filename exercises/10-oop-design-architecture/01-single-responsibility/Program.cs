using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Part2.Ch10.Ex01
{
    // ❌ This class violates SRP - it has FOUR different responsibilities!
    // Your task: Refactor this into separate, focused classes
    
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
                Console.WriteLine("Invalid grade! Must be between 0-100");
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
    
    // TODO: Create these classes following SRP:
    // - Student (data only)
    // - GradeCalculator (calculations only)
    // - StudentRepository (file I/O only)
    // - StudentUI (display only)
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Student Management System ===\n");
            
            // Current implementation (violates SRP)
            Console.WriteLine("❌ Current Design (violates SRP):");
            StudentManager student = new StudentManager("Alice Johnson");
            student.AddGrade(85);
            student.AddGrade(92);
            student.AddGrade(78);
            student.AddGrade(95);
            student.AddGrade(88);
            student.DisplayReport();
            student.SaveToFile();
            
            Console.WriteLine("\n" + new string('=', 50));
            
            // TODO: Your refactored implementation
            Console.WriteLine("\n✅ Refactored Design (follows SRP):");
            Console.WriteLine("TODO: Implement your refactored classes here");
            
            // TODO: Example of how it should work:
            // Student alice = new Student("Alice Johnson");
            // alice.AddGrade(85);
            // alice.AddGrade(92);
            // alice.AddGrade(78);
            // alice.AddGrade(95);
            // alice.AddGrade(88);
            
            // GradeCalculator calculator = new GradeCalculator();
            // StudentUI ui = new StudentUI();
            // StudentRepository repo = new StudentRepository();
            
            // ui.DisplayReport(alice, calculator);
            // repo.Save(alice, "alice_johnson.txt");
            
            Console.WriteLine("\n=== Benefits of SRP ===");
            Console.WriteLine("✓ Student class only manages data");
            Console.WriteLine("✓ GradeCalculator only computes grades");
            Console.WriteLine("✓ StudentRepository only handles file I/O");
            Console.WriteLine("✓ StudentUI only handles display");
            Console.WriteLine("\nEach class has ONE reason to change!");
        }
    }
}