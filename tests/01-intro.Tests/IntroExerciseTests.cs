using System;
using System.IO;
using Xunit;

namespace IntroExercises.Tests
{
    public class IntroExerciseTests
    {
        private readonly string _basePath;

        public IntroExerciseTests()
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

        // ===== Exercise 01: Variables =====
        
        [Fact]
        public void Variables_Exercise_ShouldExist()
        {
            // Arrange
            string programPath = Path.Combine(_basePath, "exercises", "01-intro", "01-variables", "Program.cs");
            
            // Assert
            Assert.True(File.Exists(programPath), $"Exercise file should exist at {programPath}");
        }

        [Fact]
        public void Variables_Exercise_ShouldContainRequiredElements()
        {
            // Arrange
            string programPath = Path.Combine(_basePath, "exercises", "01-intro", "01-variables", "Program.cs");
            
            // Act
            string content = File.ReadAllText(programPath);
            
            // Assert
            Assert.Contains("using System", content);
            Assert.Contains("Console.WriteLine", content);
            Assert.Contains("TODO", content);
        }

        [Fact]
        public void Variables_Solution_ShouldExist()
        {
            // Arrange
            string solutionPath = Path.Combine(_basePath, "solutions", "exercises", "01-intro", "01-variables", "solution.cs");
            
            // Assert
            Assert.True(File.Exists(solutionPath), $"Solution file should exist at {solutionPath}");
        }

        [Fact]
        public void Variables_Solution_ShouldContainVariableTypes()
        {
            // Arrange
            string solutionPath = Path.Combine(_basePath, "solutions", "exercises", "01-intro", "01-variables", "solution.cs");
            
            // Act
            string content = File.ReadAllText(solutionPath);
            
            // Assert
            Assert.Contains("int", content);
            Assert.Contains("string", content);
            Assert.Contains("double", content);
            Assert.Contains("bool", content);
        }

        // ===== Exercise 02: If Statements =====
        
        [Fact]
        public void IfStatements_Exercise_ShouldExist()
        {
            // Arrange
            string programPath = Path.Combine(_basePath, "exercises", "01-intro", "02-if-statements", "Program.cs");
            
            // Assert
            Assert.True(File.Exists(programPath), $"Exercise file should exist at {programPath}");
        }

        [Fact]
        public void IfStatements_Exercise_ShouldContainRequiredElements()
        {
            // Arrange
            string programPath = Path.Combine(_basePath, "exercises", "01-intro", "02-if-statements", "Program.cs");
            
            // Act
            string content = File.ReadAllText(programPath);
            
            // Assert
            Assert.Contains("using System", content);
            Assert.Contains("static void Main", content);
            Assert.Contains("TODO", content);
        }

        [Fact]
        public void IfStatements_Solution_ShouldExist()
        {
            // Arrange
            string solutionPath = Path.Combine(_basePath, "solutions", "exercises", "01-intro", "02-if-statements", "solution.cs");
            
            // Assert
            Assert.True(File.Exists(solutionPath), $"Solution file should exist at {solutionPath}");
        }

        [Fact]
        public void IfStatements_Solution_ShouldContainConditionalLogic()
        {
            // Arrange
            string solutionPath = Path.Combine(_basePath, "solutions", "exercises", "01-intro", "02-if-statements", "solution.cs");
            
            // Act
            string content = File.ReadAllText(solutionPath);
            
            // Assert
            Assert.Contains("if", content);
            Assert.Contains("else", content);
            Assert.Contains("int.Parse", content);
        }

        // ===== Exercise 03: Loops =====
        
        [Fact]
        public void Loops_Exercise_ShouldExist()
        {
            // Arrange
            string programPath = Path.Combine(_basePath, "exercises", "01-intro", "03-loops", "Program.cs");
            
            // Assert
            Assert.True(File.Exists(programPath), $"Exercise file should exist at {programPath}");
        }

        [Fact]
        public void Loops_Exercise_ShouldContainRequiredElements()
        {
            // Arrange
            string programPath = Path.Combine(_basePath, "exercises", "01-intro", "03-loops", "Program.cs");
            
            // Act
            string content = File.ReadAllText(programPath);
            
            // Assert
            Assert.Contains("using System", content);
            Assert.Contains("TODO", content);
        }

        [Fact]
        public void Loops_Solution_ShouldExist()
        {
            // Arrange
            string solutionPath = Path.Combine(_basePath, "solutions", "exercises", "01-intro", "03-loops", "solution.cs");
            
            // Assert
            Assert.True(File.Exists(solutionPath), $"Solution file should exist at {solutionPath}");
        }

        [Fact]
        public void Loops_Solution_ShouldContainLoopStructures()
        {
            // Arrange
            string solutionPath = Path.Combine(_basePath, "solutions", "exercises", "01-intro", "03-loops", "solution.cs");
            
            // Act
            string content = File.ReadAllText(solutionPath);
            
            // Assert
            Assert.Contains("for", content);
            Assert.Contains("while", content);
        }

        // ===== Exercise 04: Methods =====
        
        [Fact]
        public void Methods_Exercise_ShouldExist()
        {
            // Arrange
            string programPath = Path.Combine(_basePath, "exercises", "01-intro", "04-methods", "Program.cs");
            
            // Assert
            Assert.True(File.Exists(programPath), $"Exercise file should exist at {programPath}");
        }

        [Fact]
        public void Methods_Exercise_ShouldContainRequiredElements()
        {
            // Arrange
            string programPath = Path.Combine(_basePath, "exercises", "01-intro", "04-methods", "Program.cs");
            
            // Act
            string content = File.ReadAllText(programPath);
            
            // Assert
            Assert.Contains("using System", content);
            Assert.Contains("static void Main", content);
            Assert.Contains("TODO", content);
        }

        [Fact]
        public void Methods_Solution_ShouldExist()
        {
            // Arrange
            string solutionPath = Path.Combine(_basePath, "solutions", "exercises", "01-intro", "04-methods", "solution.cs");
            
            // Assert
            Assert.True(File.Exists(solutionPath), $"Solution file should exist at {solutionPath}");
        }

        [Fact]
        public void Methods_Solution_ShouldContainRequiredMethods()
        {
            // Arrange
            string solutionPath = Path.Combine(_basePath, "solutions", "exercises", "01-intro", "04-methods", "solution.cs");
            
            // Act
            string content = File.ReadAllText(solutionPath);
            
            // Assert
            Assert.Contains("static void Greet", content);
            Assert.Contains("static int Add", content);
            Assert.Contains("static bool IsEven", content);
            Assert.Contains("static void PrintSquare", content);
        }

        // ===== Project Structure Tests =====
        
        [Theory]
        [InlineData("01-variables")]
        [InlineData("02-if-statements")]
        [InlineData("03-loops")]
        [InlineData("04-methods")]
        public void Exercise_ShouldHaveReadme(string exerciseName)
        {
            // Arrange
            string readmePath = Path.Combine(_basePath, "exercises", "01-intro", exerciseName, "README.md");
            
            // Assert
            Assert.True(File.Exists(readmePath), $"README.md should exist at {readmePath}");
        }
    }
}