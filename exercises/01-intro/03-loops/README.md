# Exercise 01-03: Loops Practice

## Goal

Build confidence with both `for` and `while` loops by creating a countdown timer. You'll learn when to use each type of loop and how to control iteration.

## Background

Loops allow programs to repeat tasks efficiently. The two most common loop types are `for` loops (when you know how many times to repeat) and `while` loops (when you repeat based on a condition).

## Instructions

Create a program that demonstrates both `for` and `while` loops by creating a countdown timer.

## Requirements

Your program should:

1. Ask the user for a starting number
2. Use a `for` loop to count down from that number to 1
3. Display each number on a new line
4. When the countdown reaches 0, display "Blast off!"
5. Then use a `while` loop to count up from 1 to the original starting number

## Expected Output

Example interaction:

```
Enter starting number: 5
Counting down with FOR loop:
5
4
3
2
1
Blast off!

Counting up with WHILE loop:
1
2
3
4
5
Done!
```

## Hints

- For loop syntax: `for (int i = start; i > 0; i--)`
- While loop syntax: `while (condition) { ... }`
- Use `int.Parse()` to convert user input
- Remember to increment/decrement loop variables correctly

## Bonus Challenge

Add a small delay between numbers using `System.Threading.Thread.Sleep(1000);` to make it look like a real countdown timer (1000 = 1 second).