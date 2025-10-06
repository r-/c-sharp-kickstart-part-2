using System;
using Xunit;

namespace Part2.Ch10.Tests
{
    // Chapter 10 tests verify that students understand and apply SOLID principles
    // These are structural tests that check for proper class design
    
    public class Chapter10StructureTests
    {
        [Fact]
        public void Exercise01_HasRequiredFiles()
        {
            // Verify the exercise folder structure exists
            string exercisePath = @"exercises\10-oop-design-architecture\01-single-responsibility";
            Assert.True(System.IO.Directory.Exists(exercisePath), 
                "Exercise 01 directory should exist");
            
            string readmePath = System.IO.Path.Combine(exercisePath, "README.md");
            Assert.True(System.IO.File.Exists(readmePath), 
                "Exercise 01 should have README.md");
            
            string programPath = System.IO.Path.Combine(exercisePath, "Program.cs");
            Assert.True(System.IO.File.Exists(programPath), 
                "Exercise 01 should have Program.cs");
        }
        
        [Fact]
        public void Exercise02_HasRequiredFiles()
        {
            string exercisePath = @"exercises\10-oop-design-architecture\02-open-closed";
            Assert.True(System.IO.Directory.Exists(exercisePath), 
                "Exercise 02 directory should exist");
            
            string readmePath = System.IO.Path.Combine(exercisePath, "README.md");
            Assert.True(System.IO.File.Exists(readmePath), 
                "Exercise 02 should have README.md");
        }
        
        [Fact]
        public void Exercise03_HasRequiredFiles()
        {
            string exercisePath = @"exercises\10-oop-design-architecture\03-dependency-inversion";
            Assert.True(System.IO.Directory.Exists(exercisePath), 
                "Exercise 03 directory should exist");
            
            string readmePath = System.IO.Path.Combine(exercisePath, "README.md");
            Assert.True(System.IO.File.Exists(readmePath), 
                "Exercise 03 should have README.md");
        }
        
        [Fact]
        public void MiniProject_HasRequiredFiles()
        {
            string projectPath = @"projects\10-oop-design-architecture";
            Assert.True(System.IO.Directory.Exists(projectPath), 
                "Mini project directory should exist");
            
            string readmePath = System.IO.Path.Combine(projectPath, "README.md");
            Assert.True(System.IO.File.Exists(readmePath), 
                "Mini project should have README.md");
            
            string programPath = System.IO.Path.Combine(projectPath, "Program.cs");
            Assert.True(System.IO.File.Exists(programPath), 
                "Mini project should have Program.cs");
        }
        
        [Fact]
        public void Solutions_ExistForAllExercises()
        {
            string[] solutionPaths = new[]
            {
                @"solutions\exercises\10-oop-design-architecture\01-single-responsibility\solution.cs",
                @"solutions\exercises\10-oop-design-architecture\02-open-closed\solution.cs",
                @"solutions\exercises\10-oop-design-architecture\03-dependency-inversion\solution.cs"
            };
            
            foreach (var path in solutionPaths)
            {
                Assert.True(System.IO.File.Exists(path), 
                    $"Solution file should exist: {path}");
            }
        }
    }
    
    public class Exercise01_SRP_Tests
    {
        [Fact]
        public void README_ContainsKeyTerms()
        {
            string readmePath = @"exercises\10-oop-design-architecture\01-single-responsibility\README.md";
            if (!System.IO.File.Exists(readmePath)) return;
            
            string content = System.IO.File.ReadAllText(readmePath);
            
            Assert.Contains("Single Responsibility", content, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("SRP", content);
            Assert.Contains("one reason to change", content, StringComparison.OrdinalIgnoreCase);
        }
        
        [Fact]
        public void README_HasRequiredSections()
        {
            string readmePath = @"exercises\10-oop-design-architecture\01-single-responsibility\README.md";
            if (!System.IO.File.Exists(readmePath)) return;
            
            string content = System.IO.File.ReadAllText(readmePath);
            
            Assert.Contains("## Goal", content);
            Assert.Contains("## Background", content);
            Assert.Contains("## Instructions", content);
            Assert.Contains("## Requirements", content);
        }
    }
    
    public class Exercise02_OCP_Tests
    {
        [Fact]
        public void README_ContainsKeyTerms()
        {
            string readmePath = @"exercises\10-oop-design-architecture\02-open-closed\README.md";
            if (!System.IO.File.Exists(readmePath)) return;
            
            string content = System.IO.File.ReadAllText(readmePath);
            
            Assert.Contains("Open/Closed", content, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("OCP", content);
            Assert.Contains("open for extension", content, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("closed for modification", content, StringComparison.OrdinalIgnoreCase);
        }
        
        [Fact]
        public void README_MentionsInterfaces()
        {
            string readmePath = @"exercises\10-oop-design-architecture\02-open-closed\README.md";
            if (!System.IO.File.Exists(readmePath)) return;
            
            string content = System.IO.File.ReadAllText(readmePath);
            
            Assert.Contains("interface", content, StringComparison.OrdinalIgnoreCase);
        }
    }
    
    public class Exercise03_DIP_Tests
    {
        [Fact]
        public void README_ContainsKeyTerms()
        {
            string readmePath = @"exercises\10-oop-design-architecture\03-dependency-inversion\README.md";
            if (!System.IO.File.Exists(readmePath)) return;
            
            string content = System.IO.File.ReadAllText(readmePath);
            
            Assert.Contains("Dependency Inversion", content, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("DIP", content);
            Assert.Contains("depend on abstractions", content, StringComparison.OrdinalIgnoreCase);
        }
        
        [Fact]
        public void README_MentionsDependencyInjection()
        {
            string readmePath = @"exercises\10-oop-design-architecture\03-dependency-inversion\README.md";
            if (!System.IO.File.Exists(readmePath)) return;
            
            string content = System.IO.File.ReadAllText(readmePath);
            
            Assert.Contains("injection", content, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("constructor", content, StringComparison.OrdinalIgnoreCase);
        }
    }
    
    public class MiniProject_Tests
    {
        [Fact]
        public void README_MentionsAllSOLIDPrinciples()
        {
            string readmePath = @"projects\10-oop-design-architecture\README.md";
            if (!System.IO.File.Exists(readmePath)) return;
            
            string content = System.IO.File.ReadAllText(readmePath);
            
            Assert.Contains("SOLID", content);
            Assert.Contains("Single Responsibility", content, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("Open/Closed", content, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("Dependency Inversion", content, StringComparison.OrdinalIgnoreCase);
        }
        
        [Fact]
        public void README_HasRefactoringGuidance()
        {
            string readmePath = @"projects\10-oop-design-architecture\README.md";
            if (!System.IO.File.Exists(readmePath)) return;
            
            string content = System.IO.File.ReadAllText(readmePath);
            
            Assert.Contains("refactor", content, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("violate", content, StringComparison.OrdinalIgnoreCase);
        }
        
        [Fact]
        public void README_HasMinimumRequirements()
        {
            string readmePath = @"projects\10-oop-design-architecture\README.md";
            if (!System.IO.File.Exists(readmePath)) return;
            
            string content = System.IO.File.ReadAllText(readmePath);
            
            Assert.Contains("## Minimum Requirements", content);
            Assert.Contains("repository", content, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("interface", content, StringComparison.OrdinalIgnoreCase);
        }
    }
    
    public class Chapter10_ContentQuality_Tests
    {
        [Fact]
        public void Chapter10_MainFile_Exists()
        {
            string chapterPath = @"book\chapters\10-oop-design-architecture.md";
            Assert.True(System.IO.File.Exists(chapterPath), 
                "Chapter 10 main file should exist");
        }
        
        [Fact]
        public void Chapter10_CoverSOLIDPrinciples()
        {
            string chapterPath = @"book\chapters\10-oop-design-architecture.md";
            if (!System.IO.File.Exists(chapterPath)) return;
            
            string content = System.IO.File.ReadAllText(chapterPath);
            
            Assert.Contains("SOLID", content);
            Assert.Contains("Single Responsibility Principle", content);
            Assert.Contains("Open/Closed Principle", content);
            Assert.Contains("Dependency Inversion Principle", content);
        }
        
        [Fact]
        public void Chapter10_MentionsArchitecture()
        {
            string chapterPath = @"book\chapters\10-oop-design-architecture.md";
            if (!System.IO.File.Exists(chapterPath)) return;
            
            string content = System.IO.File.ReadAllText(chapterPath);
            
            Assert.Contains("architecture", content, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("MVC", content);
        }
        
        [Fact]
        public void Chapter10_HasLearningObjectives()
        {
            string chapterPath = @"book\chapters\10-oop-design-architecture.md";
            if (!System.IO.File.Exists(chapterPath)) return;
            
            string content = System.IO.File.ReadAllText(chapterPath);
            
            Assert.Contains("## Learning Objectives", content);
        }
        
        [Fact]
        public void Chapter10_HasKeyTerms()
        {
            string chapterPath = @"book\chapters\10-oop-design-architecture.md";
            if (!System.IO.File.Exists(chapterPath)) return;
            
            string content = System.IO.File.ReadAllText(chapterPath);
            
            Assert.Contains("## Key Terms", content);
        }
    }
}