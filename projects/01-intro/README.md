# Mini Project: Build Your Own Calculator

## Project Overview

Create YOUR own console calculator that performs arithmetic operations with error handling. This is YOUR project - while there are minimum requirements, you're encouraged to add your own features, style, and improvements to make it unique!

## Why This Project?

This project lets you:
- Combine all Chapter 1 concepts into something real
- Express your creativity in how you solve problems
- Build something you can demonstrate and be proud of
- Practice making design decisions on your own

## Learning Goals

- Apply variables, conditionals, loops, and methods in a complete program
- Implement error handling and input validation
- Make your own design decisions about features and user experience
- Practice problem-solving without a fixed solution to follow

## Minimum Requirements

Your calculator MUST include these core features:

### 1. Basic Operations
- Addition (+)
- Subtraction (-)
- Multiplication (*)
- Division (/)

### 2. User Interaction
- Clear menu showing available operations
- Prompt for two numbers
- Ask which operation to perform
- Display the result clearly
- Allow multiple calculations in one session
- Way to exit the program

### 3. Error Handling
- Handle invalid number input (letters instead of numbers)
- Handle division by zero
- Handle invalid operation choices
- Display helpful error messages

### 4. Code Structure
- Use methods to organize your code
- At minimum:
  - Methods for each operation
  - Method to display menu
  - Method to get valid user input

## Expected Output

Example session:

```
=== Simple Calculator ===
Operations:
  + : Addition
  - : Subtraction
  * : Multiplication
  / : Division
  Q : Quit

Enter first number: 10
Enter second number: 5
Enter operation (+, -, *, /, Q): +
Result: 10 + 5 = 15

Continue? (Y/N): Y

Enter first number: 20
Enter second number: 0
Enter operation (+, -, *, /, Q): /
Error: Division by zero is not allowed.

Continue? (Y/N): Y

Enter first number: abc
Error: Please enter a valid number.

Enter first number: 15
Enter second number: 3
Enter operation (+, -, *, /, Q): *
Result: 15 * 3 = 45

Continue? (Y/N): N
Thank you for using the calculator!
```

## Implementation Steps

1. **Start with structure** - Create the main program loop
2. **Add menu** - Display operation choices
3. **Get input** - Create methods to safely read numbers
4. **Add operations** - Implement calculation methods
5. **Error handling** - Add try/catch blocks
6. **Polish** - Add clear messages and formatting

## Hints

### Input Validation
```csharp
try
{
    double number = double.Parse(Console.ReadLine());
    return number;
}
catch (FormatException)
{
    Console.WriteLine("Error: Please enter a valid number.");
}
```

### Division by Zero
```csharp
if (b == 0)
{
    throw new DivideByZeroException();
}
return a / b;
```

### Main Loop Structure
```csharp
bool continueCalculating = true;
while (continueCalculating)
{
    // Calculator logic here
    
    Console.Write("Continue? (Y/N): ");
    string response = Console.ReadLine().ToUpper();
    continueCalculating = (response == "Y");
}
```

## Make It Your Own!

After meeting the minimum requirements, add YOUR OWN features! Here are ideas to spark creativity:

### Feature Ideas
- 🎨 **Visual Polish**: Colors, ASCII art, better formatting
- 📊 **Calculation History**: Store and review previous calculations
- 🔧 **More Operations**: Square root, power, modulo, percentage
- 💾 **Save Results**: Export calculations to a file
- 🎯 **Scientific Mode**: Trigonometry, logarithms
- 🎲 **Random Number Generator**: For quick calculations
- ⏱️ **Speed Test Mode**: Time-based math challenges
- 📈 **Statistics**: Average, sum of multiple numbers
- 🌍 **Unit Converter**: Temperature, distance, weight
- 🎨 **Themes**: Let users choose color schemes

### Design Decisions You Make

Think about:
- How do you want the menu to look?
- What makes error messages helpful vs confusing?
- Should operations be case-sensitive?
- How do you make the calculator feel polished?
- What would make YOUR calculator stand out?

**Remember**: There's no single "correct" calculator. Your creativity and problem-solving approach are what matter!

## Self-Assessment Checklist

Test your calculator thoroughly:

### Core Functionality
- [ ] All four basic operations work correctly
- [ ] Calculator doesn't crash on bad input
- [ ] User can perform multiple calculations
- [ ] User can exit cleanly
- [ ] Error messages are clear and helpful

### Code Quality
- [ ] Code is organized into logical methods
- [ ] Variable names are clear and descriptive
- [ ] Comments explain complex logic
- [ ] No unnecessary code repetition

### Your Additions
- [ ] Added at least one unique feature
- [ ] Tested your custom features thoroughly
- [ ] Code is clean and understandable

## What Makes a Great Project?

Your project will be evaluated on:

- **Meets Requirements** (40%): All core features work correctly
- **Error Handling** (20%): Handles problems gracefully
- **Code Quality** (20%): Clean, organized, readable code
- **Creativity** (10%): Your unique features and improvements
- **User Experience** (10%): Easy to use and understand

## Showcase Your Work!

When you're done:
- Be prepared to demonstrate your calculator
- Explain your design decisions
- Share what unique features you added
- Discuss challenges you overcame

**This is YOUR project** - make it something you're proud to show off!

## Resources

- [C# Exception Handling](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions/)
- [Console Class Documentation](https://docs.microsoft.com/en-us/dotnet/api/system.console)
- Review Chapter 1 concepts

---

**Good luck!** Remember to test thoroughly and handle all possible user inputs gracefully.