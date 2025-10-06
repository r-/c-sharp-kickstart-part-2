using Xunit;
using System.IO;

namespace Part2.Ch07.Tests
{
    public class PolymorphismInterfacesTests
    {
        private const string ExercisesPath = "../../../exercises/07-polymorphism-interfaces";
        private const string SolutionsPath = "../../../solutions/exercises/07-polymorphism-interfaces";
        
        [Fact]
        public void Exercise01_FilesExist()
        {
            Assert.True(File.Exists($"{ExercisesPath}/01-abstract-classes/README.md"), 
                "Exercise 01 README should exist");
            Assert.True(File.Exists($"{ExercisesPath}/01-abstract-classes/Program.cs"), 
                "Exercise 01 Program.cs should exist");
        }
        
        [Fact]
        public void Exercise01_Solution_Exists()
        {
            Assert.True(File.Exists($"{SolutionsPath}/01-abstract-classes/solution.cs"),
                "Exercise 01 solution should exist");
        }
        
        [Fact]
        public void Exercise01_Solution_HasAbstractClass()
        {
            string solution = File.ReadAllText($"{SolutionsPath}/01-abstract-classes/solution.cs");
            Assert.Contains("abstract class Employee", solution);
            Assert.Contains("public abstract decimal CalculatePay()", solution);
        }
        
        [Fact]
        public void Exercise01_Solution_HasDerivedClasses()
        {
            string solution = File.ReadAllText($"{SolutionsPath}/01-abstract-classes/solution.cs");
            Assert.Contains("class HourlyEmployee : Employee", solution);
            Assert.Contains("class SalariedEmployee : Employee", solution);
            Assert.Contains("class CommissionEmployee : Employee", solution);
        }
        
        [Fact]
        public void Exercise01_Solution_HasOverrides()
        {
            string solution = File.ReadAllText($"{SolutionsPath}/01-abstract-classes/solution.cs");
            Assert.Contains("public override decimal CalculatePay()", solution);
        }
        
        [Fact]
        public void Exercise02_FilesExist()
        {
            Assert.True(File.Exists($"{ExercisesPath}/02-interfaces/README.md"),
                "Exercise 02 README should exist");
            Assert.True(File.Exists($"{ExercisesPath}/02-interfaces/Program.cs"),
                "Exercise 02 Program.cs should exist");
        }
        
        [Fact]
        public void Exercise02_Solution_Exists()
        {
            Assert.True(File.Exists($"{SolutionsPath}/02-interfaces/solution.cs"),
                "Exercise 02 solution should exist");
        }
        
        [Fact]
        public void Exercise02_Solution_HasInterface()
        {
            string solution = File.ReadAllText($"{SolutionsPath}/02-interfaces/solution.cs");
            Assert.Contains("interface IDataStore", solution);
        }
        
        [Fact]
        public void Exercise02_Solution_HasImplementations()
        {
            string solution = File.ReadAllText($"{SolutionsPath}/02-interfaces/solution.cs");
            Assert.Contains("class MemoryStore : IDataStore", solution);
            Assert.Contains("class FileStore : IDataStore", solution);
            Assert.Contains("class DatabaseStore : IDataStore", solution);
        }
        
        [Fact]
        public void Exercise02_Solution_HasPolymorphicMethod()
        {
            string solution = File.ReadAllText($"{SolutionsPath}/02-interfaces/solution.cs");
            Assert.Contains("void TestStorage(IDataStore storage)", solution);
        }
        
        [Fact]
        public void Exercise03_FilesExist()
        {
            Assert.True(File.Exists($"{ExercisesPath}/03-multiple-interfaces/README.md"),
                "Exercise 03 README should exist");
            Assert.True(File.Exists($"{ExercisesPath}/03-multiple-interfaces/Program.cs"),
                "Exercise 03 Program.cs should exist");
        }
        
        [Fact]
        public void Exercise03_Solution_Exists()
        {
            Assert.True(File.Exists($"{SolutionsPath}/03-multiple-interfaces/solution.cs"),
                "Exercise 03 solution should exist");
        }
        
        [Fact]
        public void Exercise03_Solution_HasMultipleInterfaces()
        {
            string solution = File.ReadAllText($"{SolutionsPath}/03-multiple-interfaces/solution.cs");
            Assert.Contains("interface IDrawable", solution);
            Assert.Contains("interface IMovable", solution);
            Assert.Contains("interface IDamageable", solution);
            Assert.Contains("interface IInteractable", solution);
        }
        
        [Fact]
        public void Exercise03_Solution_HasMultipleImplementations()
        {
            string solution = File.ReadAllText($"{SolutionsPath}/03-multiple-interfaces/solution.cs");
            Assert.Contains("class Player : IDrawable, IMovable, IDamageable, IInteractable", solution);
            Assert.Contains("class Enemy : IDrawable, IMovable, IDamageable", solution);
            Assert.Contains("class Chest : IDrawable, IInteractable", solution);
            Assert.Contains("class Wall : IDrawable", solution);
        }
        
        [Fact]
        public void Exercise04_FilesExist()
        {
            Assert.True(File.Exists($"{ExercisesPath}/04-polymorphic-collections/README.md"),
                "Exercise 04 README should exist");
            Assert.True(File.Exists($"{ExercisesPath}/04-polymorphic-collections/Program.cs"),
                "Exercise 04 Program.cs should exist");
        }
        
        [Fact]
        public void Exercise04_Solution_Exists()
        {
            Assert.True(File.Exists($"{SolutionsPath}/04-polymorphic-collections/solution.cs"),
                "Exercise 04 solution should exist");
        }
        
        [Fact]
        public void Exercise04_Solution_HasShapeInterface()
        {
            string solution = File.ReadAllText($"{SolutionsPath}/04-polymorphic-collections/solution.cs");
            Assert.Contains("interface IShape", solution);
            Assert.Contains("double GetArea()", solution);
            Assert.Contains("double GetPerimeter()", solution);
        }
        
        [Fact]
        public void Exercise04_Solution_HasShapeImplementations()
        {
            string solution = File.ReadAllText($"{SolutionsPath}/04-polymorphic-collections/solution.cs");
            Assert.Contains("class Circle : IShape", solution);
            Assert.Contains("class Rectangle : IShape", solution);
            Assert.Contains("class Triangle : IShape", solution);
        }
        
        [Fact]
        public void Exercise04_Solution_HasShapeManager()
        {
            string solution = File.ReadAllText($"{SolutionsPath}/04-polymorphic-collections/solution.cs");
            Assert.Contains("class ShapeManager", solution);
            Assert.Contains("List<IShape>", solution);
        }
        
        [Fact]
        public void Exercise04_Solution_HasManagerMethods()
        {
            string solution = File.ReadAllText($"{SolutionsPath}/04-polymorphic-collections/solution.cs");
            Assert.Contains("void AddShape", solution);
            Assert.Contains("void DrawAll", solution);
            Assert.Contains("double GetTotalArea", solution);
            Assert.Contains("IShape GetLargestShape", solution);
        }
    }
}