// Exercise 03-03: Constructor Practice - SOLUTION
// This is a reference solution for teachers

using System;

class Student
{
    public string Name;
    public int Age;
    public string Grade;

    // Default constructor
    public Student()
    {
        Name = "Unknown";
        Age = 16;
        Grade = "Not assigned";
    }

    // Constructor with name and age
    public Student(string name, int age)
    {
        Name = name;
        Age = age;
        Grade = "Not assigned";
    }

    // Constructor with all three parameters
    public Student(string name, int age, string grade)
    {
        Name = name;
        Age = age;
        Grade = grade;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Student: {Name}, Age: {Age}, Grade: {Grade}");
    }
}

class Program
{
    static void Main()
    {
        // Create student using default constructor
        Student student1 = new Student();
        
        // Create student using name + age constructor
        Student student2 = new Student("Alex", 17);
        
        // Create student using full constructor
        Student student3 = new Student("Riley", 18, "A");
        
        // Display all students
        student1.DisplayInfo();
        student2.DisplayInfo();
        student3.DisplayInfo();
    }
}