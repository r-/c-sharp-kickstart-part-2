# Exercise 01-01: Variables Practice

## Goal

Practice declaring and using different data types in C#. By the end of this exercise, you'll be comfortable working with `int`, `string`, `double`, and `bool` variables.

## Background

Variables are containers for storing data. In C#, you must declare the type of data a variable will hold before using it.

## Instructions

Create a program that declares and uses different types of variables to store information about yourself.

## Requirements

Your program should:

1. Declare an `int` variable for your age
2. Declare a `string` variable for your name
3. Declare a `double` variable for your height in meters (e.g., 1.75)
4. Declare a `bool` variable indicating if you're a student
5. Print all variables using `Console.WriteLine()` with string interpolation

## Expected Output

Your output should look similar to this (with your own information):

```
Name: Alex
Age: 17
Height: 1.75 meters
Student: True
```

## Hints

- Use `$` before the string to enable string interpolation: `$"Age: {age}"`
- Remember that strings use double quotes: `"text"`
- Boolean values are `true` or `false` (lowercase)
- Decimal numbers use `double` type: `double height = 1.75;`

## Bonus Challenge

Calculate and display your birth year using the current year (2025) minus your age.

Example: `Birth year: 2008`