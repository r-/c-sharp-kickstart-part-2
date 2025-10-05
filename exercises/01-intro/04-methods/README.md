# Exercise 01-04: Methods

## Goal

Learn to organize code into reusable methods. You'll create methods that accept parameters, return values, and perform specific tasks, making your code cleaner and more maintainable.

## Background

Methods are reusable blocks of code that perform specific tasks. They help organize code and avoid repetition. Methods can accept parameters (input) and return values (output).

## Instructions

Create a program with several useful methods that perform calculations and display results.

## Requirements

Your program should include the following methods:

1. `Greet(string name)` - Displays a personalized greeting
2. `Add(int a, int b)` - Returns the sum of two numbers
3. `IsEven(int number)` - Returns true if the number is even, false otherwise
4. `PrintSquare(int size)` - Prints a square pattern using asterisks (*)

In `Main()`, call each method to demonstrate it works correctly.

## Expected Output

Example output:

```
Hello, Alex!
5 + 3 = 8
Is 10 even? True
Is 7 even? False

***
***
***
```

## Hints

- Method syntax: `static ReturnType MethodName(parameters) { ... }`
- Use `void` for methods that don't return a value
- Use `return` keyword to send back a value
- Nested loops create 2D patterns

## Bonus Challenge

Create a method `Factorial(int n)` that calculates the factorial of a number (e.g., 5! = 5×4×3×2×1 = 120).