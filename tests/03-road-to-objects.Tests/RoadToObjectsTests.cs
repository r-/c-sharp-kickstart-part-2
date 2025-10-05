using System;
using System.IO;
using Xunit;

namespace RoadToObjectsExercises.Tests
{
    public class RoadToObjectsTests
    {
        private readonly string _basePath;

        public RoadToObjectsTests()
        {
            // Find the project root by looking for the exercises folder
            string currentDir = Directory.GetCurrentDirectory();
            string searchDir = currentDir;
            
            // Navigate up until we find the project root (contains exercises and solutions folders)
            while (searchDir != null && !Directory.Exists(Path.Combine(searchDir, "exercises")))
            {
                string? parentDir = Directory.GetParent(searchDir)?.FullName;
                if (parentDir == null || parentDir == searchDir)
                {
                    break; // Reached root
                }
                searchDir = parentDir;
            }
            
            _basePath = searchDir ?? throw new DirectoryNotFoundException("Could not find project root containing exercises folder");
        }

        // ===== Exercise 01: Make Your First Class =====
        
        [Fact]
        public void MakeAClass_Exercise_ShouldExist()
        {
            // Arrange
            string programPath = Path.Combine(_basePath, "exercises", "03-road-to-objects", "01-make-a-class", "Program.cs");
            
            // Assert
            Assert.True(File.Exists(programPath), $"Exercise file should exist at {programPath}");
        }

        [Fact]
        public void MakeAClass_Exercise_ShouldContainRequiredElements()
        {
            // Arrange
            string programPath = Path.Combine(_basePath, "exercises", "03-road-to-objects", "01-make-a-class", "Program.cs");
            
            // Act
            string content = File.ReadAllText(programPath);
            
            // Assert
            Assert.Contains("using System", content);
            Assert.Contains("class Pet", content);
            Assert.Contains("TODO", content);
        }

        [Fact]
        public void MakeAClass_Solution_ShouldExist()
        {
            // Arrange
            string solutionPath = Path.Combine(_basePath, "solutions", "exercises", "03-road-to-objects", "01-make-a-class", "solution.cs");
            
            // Assert
            Assert.True(File.Exists(solutionPath), $"Solution file should exist at {solutionPath}");
        }

        [Fact]
        public void MakeAClass_Solution_ShouldContainPetClass()
        {
            // Arrange
            string solutionPath = Path.Combine(_basePath, "solutions", "exercises", "03-road-to-objects", "01-make-a-class", "solution.cs");
            
            // Act
            string content = File.ReadAllText(solutionPath);
            
            // Assert
            Assert.Contains("class Pet", content);
            Assert.Contains("public string Name", content);
            Assert.Contains("public int Happiness", content);
            Assert.Contains("public void Play", content);
            Assert.Contains("public void Feed", content);
            Assert.Contains("public void ShowStatus", content);
        }

        // ===== Exercise 02: Blueprint Thinking =====
        
        [Fact]
        public void BlueprintThinking_Exercise_ShouldExist()
        {
            // Arrange
            string readmePath = Path.Combine(_basePath, "exercises", "03-road-to-objects", "02-blueprint-thinking", "README.md");
            
            // Assert
            Assert.True(File.Exists(readmePath), $"Exercise README should exist at {readmePath}");
        }

        [Fact]
        public void BlueprintThinking_Exercise_ShouldBeConceptual()
        {
            // Arrange
            string readmePath = Path.Combine(_basePath, "exercises", "03-road-to-objects", "02-blueprint-thinking", "README.md");
            
            // Act
            string content = File.ReadAllText(readmePath);
            
            // Assert - Should be conceptual exercise
            Assert.Contains("concept", content.ToLower());
            Assert.Contains("class", content);
            Assert.Contains("object", content);
        }

        [Fact]
        public void BlueprintThinking_ShouldNotHaveCodeFiles()
        {
            // Arrange
            string exercisePath = Path.Combine(_basePath, "exercises", "03-road-to-objects", "02-blueprint-thinking");
            
            // Act - Check for .cs files
            string[] csFiles = Directory.Exists(exercisePath) 
                ? Directory.GetFiles(exercisePath, "*.cs") 
                : Array.Empty<string>();
            
            // Assert - Conceptual exercise shouldn't have code files
            Assert.Empty(csFiles);
        }

        // ===== Exercise 03: Constructor Practice =====
        
        [Fact]
        public void ConstructorPractice_Exercise_ShouldExist()
        {
            // Arrange
            string programPath = Path.Combine(_basePath, "exercises", "03-road-to-objects", "03-constructor-practice", "Program.cs");
            
            // Assert
            Assert.True(File.Exists(programPath), $"Exercise file should exist at {programPath}");
        }

        [Fact]
        public void ConstructorPractice_Exercise_ShouldContainRequiredElements()
        {
            // Arrange
            string programPath = Path.Combine(_basePath, "exercises", "03-road-to-objects", "03-constructor-practice", "Program.cs");
            
            // Act
            string content = File.ReadAllText(programPath);
            
            // Assert
            Assert.Contains("using System", content);
            Assert.Contains("class Student", content);
            Assert.Contains("TODO", content);
        }

        [Fact]
        public void ConstructorPractice_Solution_ShouldExist()
        {
            // Arrange
            string solutionPath = Path.Combine(_basePath, "solutions", "exercises", "03-road-to-objects", "03-constructor-practice", "solution.cs");
            
            // Assert
            Assert.True(File.Exists(solutionPath), $"Solution file should exist at {solutionPath}");
        }

        [Fact]
        public void ConstructorPractice_Solution_ShouldContainMultipleConstructors()
        {
            // Arrange
            string solutionPath = Path.Combine(_basePath, "solutions", "exercises", "03-road-to-objects", "03-constructor-practice", "solution.cs");
            
            // Act
            string content = File.ReadAllText(solutionPath);
            
            // Assert
            Assert.Contains("class Student", content);
            Assert.Contains("public string Name", content);
            Assert.Contains("public int Age", content);
            Assert.Contains("public string Grade", content);
            
            // Should have multiple constructors (look for "public Student")
            int constructorCount = 0;
            int index = 0;
            while ((index = content.IndexOf("public Student(", index)) != -1)
            {
                constructorCount++;
                index += "public Student(".Length;
            }
            Assert.True(constructorCount >= 2, "Should have at least 2 constructors");
        }

        // ===== Project Structure Tests =====
        
        [Theory]
        [InlineData("01-make-a-class")]
        [InlineData("02-blueprint-thinking")]
        [InlineData("03-constructor-practice")]
        public void Exercise_ShouldHaveReadme(string exerciseName)
        {
            // Arrange
            string readmePath = Path.Combine(_basePath, "exercises", "03-road-to-objects", exerciseName, "README.md");
            
            // Assert
            Assert.True(File.Exists(readmePath), $"README.md should exist at {readmePath}");
        }

        [Fact]
        public void Project_ShouldExist()
        {
            // Arrange
            string projectReadme = Path.Combine(_basePath, "projects", "03-road-to-objects", "README.md");
            
            // Assert
            Assert.True(File.Exists(projectReadme), $"Project README should exist at {projectReadme}");
        }

        [Fact]
        public void Project_ShouldNotHaveSolution()
        {
            // Arrange
            string solutionPath = Path.Combine(_basePath, "solutions", "projects", "03-road-to-objects");
            
            // Assert - Projects should NOT have solutions
            Assert.False(Directory.Exists(solutionPath), "Projects should not have solution directories");
        }

        [Fact]
        public void Project_ShouldEmphasizeCreativity()
        {
            // Arrange
            string projectReadme = Path.Combine(_basePath, "projects", "03-road-to-objects", "README.md");
            
            // Act
            string content = File.ReadAllText(projectReadme);
            
            // Assert - Should emphasize student ownership
            Assert.Contains("YOUR", content);
            Assert.Contains("creative", content.ToLower());
        }
    }
}