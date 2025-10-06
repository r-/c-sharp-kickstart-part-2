using System;
using System.IO;
using Xunit;

namespace GenericCollectionsTests
{
    public class GenericsCollectionsTests
    {
        private const string SolutionsBasePath = "../../../solutions/exercises/08-generics-collections";
        
        [Fact]
        public void Exercise01_GenericMethods_SolutionExists()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "01-generic-methods/solution.cs");
            Assert.True(File.Exists(solutionPath), $"Solution file not found: {solutionPath}");
        }
        
        [Fact]
        public void Exercise01_GenericMethods_ContainsGenericUtilityClass()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "01-generic-methods/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("class GenericUtility", content);
        }
        
        [Fact]
        public void Exercise01_GenericMethods_ContainsSwapMethod()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "01-generic-methods/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("Swap<T>", content);
            Assert.Contains("ref T", content);
        }
        
        [Fact]
        public void Exercise01_GenericMethods_ContainsGetFirstMethod()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "01-generic-methods/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("GetFirst<T>", content);
            Assert.Contains("List<T>", content);
        }
        
        [Fact]
        public void Exercise01_GenericMethods_ContainsMaxMethodWithConstraint()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "01-generic-methods/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("Max<T>", content);
            Assert.Contains("where T : IComparable<T>", content);
        }
        
        [Fact]
        public void Exercise01_GenericMethods_ContainsCreatePairMethod()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "01-generic-methods/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("CreatePair<T1, T2>", content);
        }
        
        [Fact]
        public void Exercise02_ListOperations_SolutionExists()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "02-list-operations/solution.cs");
            Assert.True(File.Exists(solutionPath), $"Solution file not found: {solutionPath}");
        }
        
        [Fact]
        public void Exercise02_ListOperations_ContainsStudentRosterClass()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "02-list-operations/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("class StudentRoster", content);
        }
        
        [Fact]
        public void Exercise02_ListOperations_UsesListString()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "02-list-operations/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("List<string>", content);
        }
        
        [Fact]
        public void Exercise02_ListOperations_ContainsAddStudentMethod()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "02-list-operations/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("AddStudent", content);
        }
        
        [Fact]
        public void Exercise02_ListOperations_ContainsSortStudentsMethod()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "02-list-operations/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("SortStudents", content);
        }
        
        [Fact]
        public void Exercise02_ListOperations_ContainsGetStudentsByLetterMethod()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "02-list-operations/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("GetStudentsByLetter", content);
        }
        
        [Fact]
        public void Exercise03_DictionaryOperations_SolutionExists()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "03-dictionary-operations/solution.cs");
            Assert.True(File.Exists(solutionPath), $"Solution file not found: {solutionPath}");
        }
        
        [Fact]
        public void Exercise03_DictionaryOperations_ContainsPhoneBookClass()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "03-dictionary-operations/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("class PhoneBook", content);
        }
        
        [Fact]
        public void Exercise03_DictionaryOperations_UsesDictionary()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "03-dictionary-operations/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("Dictionary<string, string>", content);
        }
        
        [Fact]
        public void Exercise03_DictionaryOperations_ContainsAddContactMethod()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "03-dictionary-operations/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("AddContact", content);
        }
        
        [Fact]
        public void Exercise03_DictionaryOperations_ContainsTryGetPhoneNumberMethod()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "03-dictionary-operations/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("TryGetPhoneNumber", content);
            Assert.Contains("out string", content);
        }
        
        [Fact]
        public void Exercise03_DictionaryOperations_ContainsFindContactsByAreaCodeMethod()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "03-dictionary-operations/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("FindContactsByAreaCode", content);
        }
        
        [Fact]
        public void Exercise04_GenericClasses_SolutionExists()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "04-generic-classes/solution.cs");
            Assert.True(File.Exists(solutionPath), $"Solution file not found: {solutionPath}");
        }
        
        [Fact]
        public void Exercise04_GenericClasses_ContainsStackClass()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "04-generic-classes/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("class Stack<T>", content);
        }
        
        [Fact]
        public void Exercise04_GenericClasses_StackContainsPushPopMethods()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "04-generic-classes/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("Push", content);
            Assert.Contains("Pop", content);
            Assert.Contains("Peek", content);
        }
        
        [Fact]
        public void Exercise04_GenericClasses_ContainsPairClass()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "04-generic-classes/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("class Pair<T1, T2>", content);
        }
        
        [Fact]
        public void Exercise04_GenericClasses_PairContainsFirstSecondProperties()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "04-generic-classes/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("First", content);
            Assert.Contains("Second", content);
        }
        
        [Fact]
        public void Exercise04_GenericClasses_ContainsBoxClassWithConstraint()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "04-generic-classes/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("class Box<T>", content);
            Assert.Contains("where T : IComparable<T>", content);
        }
        
        [Fact]
        public void Exercise04_GenericClasses_BoxContainsComparisonMethods()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "04-generic-classes/solution.cs");
            string content = File.ReadAllText(solutionPath);
            
            Assert.Contains("IsGreaterThan", content);
            Assert.Contains("IsLessThan", content);
            Assert.Contains("Max", content);
        }
    }
}