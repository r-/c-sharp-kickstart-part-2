using System;
using System.IO;
using Xunit;

namespace Part2.Ch06.Tests
{
    public class InheritanceExerciseTests
    {
        private readonly string exercisesPath = Path.Combine("..", "..", "..", "..", "exercises", "06-inheritance");
        private readonly string solutionsPath = Path.Combine("..", "..", "..", "..", "solutions", "exercises", "06-inheritance");

        [Fact]
        public void Exercise01_FilesExist()
        {
            var readmePath = Path.Combine(exercisesPath, "01-first-inheritance", "README.md");
            var programPath = Path.Combine(exercisesPath, "01-first-inheritance", "Program.cs");
            var solutionPath = Path.Combine(solutionsPath, "01-first-inheritance", "solution.cs");

            Assert.True(File.Exists(readmePath), "Exercise 01 README.md should exist");
            Assert.True(File.Exists(programPath), "Exercise 01 Program.cs should exist");
            Assert.True(File.Exists(solutionPath), "Exercise 01 solution.cs should exist");
        }

        [Fact]
        public void Exercise01_ContainsRequiredKeywords()
        {
            var solutionPath = Path.Combine(solutionsPath, "01-first-inheritance", "solution.cs");
            var content = File.ReadAllText(solutionPath);

            Assert.Contains("class Publication", content);
            Assert.Contains("class Book", content);
            Assert.Contains("class Magazine", content);
            Assert.Contains(": Publication", content);
            Assert.Contains(":base(", content);
        }

        [Fact]
        public void Exercise02_FilesExist()
        {
            var readmePath = Path.Combine(exercisesPath, "02-method-overriding", "README.md");
            var programPath = Path.Combine(exercisesPath, "02-method-overriding", "Program.cs");
            var solutionPath = Path.Combine(solutionsPath, "02-method-overriding", "solution.cs");

            Assert.True(File.Exists(readmePath), "Exercise 02 README.md should exist");
            Assert.True(File.Exists(programPath), "Exercise 02 Program.cs should exist");
            Assert.True(File.Exists(solutionPath), "Exercise 02 solution.cs should exist");
        }

        [Fact]
        public void Exercise02_ContainsRequiredKeywords()
        {
            var solutionPath = Path.Combine(solutionsPath, "02-method-overriding", "solution.cs");
            var content = File.ReadAllText(solutionPath);

            Assert.Contains("virtual", content);
            Assert.Contains("override", content);
            Assert.Contains("class Payment", content);
            Assert.Contains("ProcessPayment", content);
            Assert.Contains("GetReceipt", content);
        }

        [Fact]
        public void Exercise03_FilesExist()
        {
            var readmePath = Path.Combine(exercisesPath, "03-base-constructors", "README.md");
            var programPath = Path.Combine(exercisesPath, "03-base-constructors", "Program.cs");
            var solutionPath = Path.Combine(solutionsPath, "03-base-constructors", "solution.cs");

            Assert.True(File.Exists(readmePath), "Exercise 03 README.md should exist");
            Assert.True(File.Exists(programPath), "Exercise 03 Program.cs should exist");
            Assert.True(File.Exists(solutionPath), "Exercise 03 solution.cs should exist");
        }

        [Fact]
        public void Exercise03_ContainsRequiredKeywords()
        {
            var solutionPath = Path.Combine(solutionsPath, "03-base-constructors", "solution.cs");
            var content = File.ReadAllText(solutionPath);

            Assert.Contains("class Vehicle", content);
            Assert.Contains("class Car", content);
            Assert.Contains("class Truck", content);
            Assert.Contains("class Motorcycle", content);
            Assert.Contains(":base(", content);
            Assert.Contains("ArgumentException", content);
        }

        [Fact]
        public void Exercise04_FilesExist()
        {
            var readmePath = Path.Combine(exercisesPath, "04-virtual-methods", "README.md");
            var programPath = Path.Combine(exercisesPath, "04-virtual-methods", "Program.cs");
            var solutionPath = Path.Combine(solutionsPath, "04-virtual-methods", "solution.cs");

            Assert.True(File.Exists(readmePath), "Exercise 04 README.md should exist");
            Assert.True(File.Exists(programPath), "Exercise 04 Program.cs should exist");
            Assert.True(File.Exists(solutionPath), "Exercise 04 solution.cs should exist");
        }

        [Fact]
        public void Exercise04_ContainsRequiredKeywords()
        {
            var solutionPath = Path.Combine(solutionsPath, "04-virtual-methods", "solution.cs");
            var content = File.ReadAllText(solutionPath);

            Assert.Contains("class Shape", content);
            Assert.Contains("virtual", content);
            Assert.Contains("override", content);
            Assert.Contains("CalculateArea", content);
            Assert.Contains("Draw", content);
        }

        [Fact]
        public void MiniProject_FilesExist()
        {
            var projectPath = Path.Combine("..", "..", "..", "..", "projects", "06-inheritance");
            var readmePath = Path.Combine(projectPath, "README.md");
            var programPath = Path.Combine(projectPath, "Program.cs");

            Assert.True(File.Exists(readmePath), "Mini Project README.md should exist");
            Assert.True(File.Exists(programPath), "Mini Project Program.cs should exist");
        }

        [Fact]
        public void Chapter06_FilesExist()
        {
            var chapterPath = Path.Combine("..", "..", "..", "..", "book", "chapters", "06-inheritance.md");
            Assert.True(File.Exists(chapterPath), "Chapter 06 markdown file should exist");
        }

        [Fact]
        public void Chapter06_ContainsRequiredSections()
        {
            var chapterPath = Path.Combine("..", "..", "..", "..", "book", "chapters", "06-inheritance.md");
            var content = File.ReadAllText(chapterPath);

            Assert.Contains("Learning Objectives", content);
            Assert.Contains("Prerequisites", content);
            Assert.Contains("Mission Brief", content);
            Assert.Contains("Concept Power-Up", content);
            Assert.Contains("Core Concepts", content);
            Assert.Contains("Try It", content);
            Assert.Contains("Common Mistakes", content);
            Assert.Contains("Hands-On Practice", content);
            Assert.Contains("Why It Matters", content);
            Assert.Contains("Checkpoint", content);
            Assert.Contains("Reflection Questions", content);
            Assert.Contains("Next Chapter Preview", content);
            Assert.Contains("Key Terms", content);
        }

        [Fact]
        public void Chapter06_ContainsInheritanceConcepts()
        {
            var chapterPath = Path.Combine("..", "..", "..", "..", "book", "chapters", "06-inheritance.md");
            var content = File.ReadAllText(chapterPath);

            Assert.Contains("base class", content.ToLower());
            Assert.Contains("derived class", content.ToLower());
            Assert.Contains("virtual", content);
            Assert.Contains("override", content);
            Assert.Contains(":base(", content);
            Assert.Contains("protected", content);
            Assert.Contains("polymorphism", content.ToLower());
        }
    }
}