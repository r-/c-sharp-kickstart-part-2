# Exercise 01-02: If Statements

## Goal

Master conditional logic by creating a program that makes decisions based on user input. You'll practice using if-else chains and comparison operators.

## Background

If statements allow programs to make decisions based on conditions. They help programs respond differently to different situations.

## Instructions

Create a program that asks the user for their age and provides age-appropriate messages.

## Requirements

Your program should:

1. Prompt the user to enter their age
2. Read the input using `Console.ReadLine()`
3. Convert the input to an integer using `int.Parse()`
4. Use if-else statements to display different messages based on age:
   - Under 13: "You are a child"
   - 13-17: "You are a teenager"
   - 18-64: "You are an adult"
   - 65 and over: "You are a senior"

## Expected Output

Example interaction:

```
Enter your age: 15
You are a teenager
```

Another example:

```
Enter your age: 25
You are an adult
```

## Hints

- Use `Console.ReadLine()` to get user input as a string
- Convert to integer: `int age = int.Parse(Console.ReadLine());`
- Chain conditions using `else if`
- Use comparison operators: `<`, `<=`, `>=`, `>`

## Bonus Challenge

Add input validation to check if the age is reasonable (e.g., between 0 and 120). Display an error message for invalid ages.