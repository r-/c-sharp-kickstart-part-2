# Tests

This folder contains automated tests for the C# Kickstart Part 2 course exercises.

## Purpose

The tests serve two main purposes:

1. **For Students**: Verify that your exercise solutions meet the basic requirements
2. **For Teachers**: Validate that reference solutions are correct and complete

## What Gets Tested

- **Exercises Only**: Only exercises have tests. Projects are open-ended and encourage creativity, so they don't have automated tests.
- **File Structure**: Tests verify that required files exist in the correct locations
- **Required Elements**: Tests check for required keywords, method signatures, and code patterns
- **Content Validation**: Tests ensure exercise and solution files contain necessary components

## Running Tests

### Running All Tests

From the project root directory:

```bash
dotnet test
```

### Running Tests for a Specific Chapter

```bash
dotnet test tests/01-intro.Tests
```

### Running a Specific Test

```bash
dotnet test --filter "Variables_Exercise_ShouldExist"
```

## Understanding Test Output

When you run tests, you'll see output like:

```
Passed!  - Failed:     0, Passed:    15, Skipped:     0, Total:    15
```

- **Passed**: Your code meets the requirements ✓
- **Failed**: Something is missing or incorrect - read the error message
- **Skipped**: Tests that were intentionally not run

### Example Test Failure

If a test fails, you'll see a message explaining what's wrong:

```
Assert.True() Failure
Expected: True
Actual:   False
Message: Exercise file should exist at C:\Code\exercises\01-intro\01-variables\Program.cs
```

This tells you exactly what the test expected and where to fix it.

## Test Structure

Each chapter has its own test project following xUnit conventions:

```
tests/
├── 01-intro.Tests/
│   ├── 01-intro.Tests.csproj       # Test project configuration
│   └── IntroExerciseTests.cs       # Test cases for Chapter 1
└── README.md                        # This file
```

## Why No Tests for Projects?

Projects are designed to foster creativity and personal expression. Unlike exercises with fixed solutions, projects encourage you to:

- Implement YOUR unique ideas
- Solve problems YOUR way  
- Extend beyond minimum requirements

Automated tests would restrict this creative freedom by enforcing specific implementations.

## For Students: Test-Driven Learning

You can use tests as a learning tool:

1. **Read** the exercise requirements in README.md
2. **Run tests** before coding to see what's expected
3. **Write code** to meet the requirements
4. **Run tests again** to verify your work
5. **Iterate** until tests pass

**Important**: Passing tests means you meet basic requirements, but you should also:
- Test your code manually with various inputs
- Verify correct output format
- Check edge cases and error handling

## For Teachers: Solution Validation

Run tests regularly to ensure reference solutions in `/solutions/exercises/` remain correct:

```bash
# Test all solutions
dotnet test

# Test specific chapter
dotnet test tests/01-intro.Tests
```

Tests verify that solution files:
- Exist in the correct location
- Contain required methods and keywords
- Follow course standards

## FAQ

**Q: My code works when I run it, but tests fail. Why?**  
A: Tests check for specific elements (method names, keywords, file structure). Ensure you're following the exercise requirements exactly.

**Q: Can I modify the tests?**  
A: Tests are part of the course infrastructure. If you believe a test is incorrect, report it to your instructor.

**Q: Do I need to pass all tests to complete an exercise?**  
A: Tests verify basic requirements. Your instructor may have additional criteria—always check grading guidelines.

**Q: How do I know what a failing test expects?**  
A: Read the test error message carefully. It shows what was expected vs. what was found. You can also look at the test source code for details.