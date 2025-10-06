using System.IO;
using Xunit;

namespace Part2.Ch09.Tests
{
    public class ExceptionHandlingTests
    {
        private const string SolutionsBasePath = "../../../solutions/exercises/09-exception-handling";

        [Fact]
        public void Exercise01_TryCatchBasics_SolutionExists()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "01-try-catch-basics/solution.cs");
            Assert.True(File.Exists(solutionPath), $"Solution file not found: {solutionPath}");
        }

        [Fact]
        public void Exercise01_TryCatchBasics_ContainsSafeParseInt()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "01-try-catch-basics/solution.cs");
            string content = File.ReadAllText(solutionPath);

            Assert.Contains("SafeParseInt", content);
            Assert.Contains("int?", content);
        }

        [Fact]
        public void Exercise01_TryCatchBasics_ContainsSafeDivide()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "01-try-catch-basics/solution.cs");
            string content = File.ReadAllText(solutionPath);

            Assert.Contains("SafeDivide", content);
            Assert.Contains("DivideByZeroException", content);
        }

        [Fact]
        public void Exercise01_TryCatchBasics_ContainsTryCatchBlocks()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "01-try-catch-basics/solution.cs");
            string content = File.ReadAllText(solutionPath);

            Assert.Contains("try", content);
            Assert.Contains("catch", content);
            Assert.Contains("FormatException", content);
            Assert.Contains("OverflowException", content);
        }

        [Fact]
        public void Exercise02_CustomExceptions_SolutionExists()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "02-custom-exceptions/solution.cs");
            Assert.True(File.Exists(solutionPath), $"Solution file not found: {solutionPath}");
        }

        [Fact]
        public void Exercise02_CustomExceptions_ContainsInsufficientFundsException()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "02-custom-exceptions/solution.cs");
            string content = File.ReadAllText(solutionPath);

            Assert.Contains("class InsufficientFundsException", content);
            Assert.Contains(": Exception", content);
            Assert.Contains("Balance", content);
            Assert.Contains("RequestedAmount", content);
        }

        [Fact]
        public void Exercise02_CustomExceptions_ContainsInvalidAmountException()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "02-custom-exceptions/solution.cs");
            string content = File.ReadAllText(solutionPath);

            Assert.Contains("class InvalidAmountException", content);
            Assert.Contains("Amount", content);
        }

        [Fact]
        public void Exercise02_CustomExceptions_ContainsAccountLockedException()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "02-custom-exceptions/solution.cs");
            string content = File.ReadAllText(solutionPath);

            Assert.Contains("class AccountLockedException", content);
            Assert.Contains("Reason", content);
        }

        [Fact]
        public void Exercise02_CustomExceptions_ContainsBankAccountClass()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "02-custom-exceptions/solution.cs");
            string content = File.ReadAllText(solutionPath);

            Assert.Contains("class BankAccount", content);
            Assert.Contains("Deposit", content);
            Assert.Contains("Withdraw", content);
            Assert.Contains("Lock", content);
            Assert.Contains("Unlock", content);
        }

        [Fact]
        public void Exercise02_CustomExceptions_ThrowsCustomExceptions()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "02-custom-exceptions/solution.cs");
            string content = File.ReadAllText(solutionPath);

            Assert.Contains("throw new InsufficientFundsException", content);
            Assert.Contains("throw new InvalidAmountException", content);
            Assert.Contains("throw new AccountLockedException", content);
        }

        [Fact]
        public void Exercise03_FinallyCleanup_SolutionExists()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "03-finally-cleanup/solution.cs");
            Assert.True(File.Exists(solutionPath), $"Solution file not found: {solutionPath}");
        }

        [Fact]
        public void Exercise03_FinallyCleanup_ContainsFileLogger()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "03-finally-cleanup/solution.cs");
            string content = File.ReadAllText(solutionPath);

            Assert.Contains("class FileLogger", content);
            Assert.Contains("LogMessage", content);
        }

        [Fact]
        public void Exercise03_FinallyCleanup_UsesFinallyBlock()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "03-finally-cleanup/solution.cs");
            string content = File.ReadAllText(solutionPath);

            Assert.Contains("finally", content);
            Assert.Contains("Close()", content);
        }

        [Fact]
        public void Exercise03_FinallyCleanup_ContainsSafeFileLogger()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "03-finally-cleanup/solution.cs");
            string content = File.ReadAllText(solutionPath);

            Assert.Contains("class SafeFileLogger", content);
            Assert.Contains("using", content);
        }

        [Fact]
        public void Exercise03_FinallyCleanup_ContainsLogFileClass()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "03-finally-cleanup/solution.cs");
            string content = File.ReadAllText(solutionPath);

            Assert.Contains("class LogFile", content);
            Assert.Contains("IDisposable", content);
            Assert.Contains("Dispose()", content);
        }

        [Fact]
        public void Exercise03_FinallyCleanup_HandlesIOExceptions()
        {
            string solutionPath = Path.Combine(SolutionsBasePath, "03-finally-cleanup/solution.cs");
            string content = File.ReadAllText(solutionPath);

            Assert.Contains("IOException", content);
            Assert.Contains("UnauthorizedAccessException", content);
        }

        [Fact]
        public void AllExercises_UseProperNamespaces()
        {
            string[] exercises = { "01-try-catch-basics", "02-custom-exceptions", "03-finally-cleanup" };
            
            foreach (var exercise in exercises)
            {
                string solutionPath = Path.Combine(SolutionsBasePath, $"{exercise}/solution.cs");
                string content = File.ReadAllText(solutionPath);
                
                Assert.Contains("namespace Part2.Ch09", content);
            }
        }

        [Fact]
        public void AllExercises_ContainMainMethod()
        {
            string[] exercises = { "01-try-catch-basics", "02-custom-exceptions", "03-finally-cleanup" };
            
            foreach (var exercise in exercises)
            {
                string solutionPath = Path.Combine(SolutionsBasePath, $"{exercise}/solution.cs");
                string content = File.ReadAllText(solutionPath);
                
                Assert.Contains("static void Main", content);
            }
        }
    }
}