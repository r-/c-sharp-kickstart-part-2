# Exercise 02-02: The Right Tool for the Job

## Goal

Develop the skill to match programming paradigms with appropriate problems. Learn to justify your choices using paradigm characteristics.

## Background

Experienced programmers don't ask "Which paradigm is best?" Instead, they ask "Which paradigm fits this problem?"

Just like a carpenter chooses between a hammer, screwdriver, or saw based on the task, programmers choose paradigms based on the problem's nature. Sometimes the choice is obvious, sometimes multiple approaches work.

This exercise helps you think about when to use each paradigm.

## Instructions

This is an **analysis and reasoning exercise** - no coding required. You'll evaluate different programming problems and decide which paradigm fits best.

### Quick Reference

| Paradigm | What It Means | Example Thinking | When to Use |
|----------|---------------|------------------|-------------|
| **Imperative** | Do steps one after another, changing variables | "Add 10, then print it" | Quick one-off tasks, simple scripts |
| **Procedural** | Group repeated steps into functions | "I'll make a function for this" | Tasks with repeated operations |
| **Object-Oriented** | Combine data and actions into objects | "A Player has health and can attack" | Multiple "things" with their own data |

**Key Decision Hints:**
- Many independent "things" with their own data → **OOP**
- Mostly repeating similar steps → **Procedural**
- One quick sequence of operations → **Imperative**

## Requirements

### Part 1: Paradigm Selection

For each problem below, complete the table:

| Problem | Best Paradigm | Justification (1 sentence) |
|---------|---------------|---------------------------|
| A calculator that adds two numbers | | |
| A drawing program where shapes can move and resize | | |
| A quiz that asks several questions and keeps score | | |
| Converting temperature from Celsius to Fahrenheit | | |
| A school system tracking students, teachers, and classes | | |
| Printing the numbers 1 to 100 | | |

### Part 2: Deeper Analysis

Answer these questions in 2-3 sentences each:

1. **Mixed Paradigms**: Why do many modern programs mix more than one paradigm? Give a specific example.

2. **Unnecessary OOP**: Describe a task where using OOP would be overkill. What would you use instead and why?

3. **Evolution**: How does the complexity of a problem influence which paradigm you should choose?

## Expected Response Format

Create a document with:

```markdown
# Part 1: Paradigm Selection

| Problem | Best Paradigm | Justification |
|---------|---------------|---------------|
| A calculator... | [Your choice] | [Your reasoning] |
| A drawing program... | [Your choice] | [Your reasoning] |
| ...

# Part 2: Deeper Analysis

## 1. Mixed Paradigms
[Your answer]

## 2. Unnecessary OOP
[Your answer]

## 3. Evolution
[Your answer]
```

## Hints

### Choosing the Right Paradigm

**Ask yourself:**
1. "How many independent 'things' does this problem have?"
   - Many things → Consider OOP
   - One or none → Consider Procedural or Imperative

2. "Will the same operation be repeated?"
   - Yes, many times → Consider Procedural (make functions)
   - No, just once → Consider Imperative

3. "Do the 'things' have their own data and behavior?"
   - Yes → Definitely OOP
   - No → Procedural is fine

**Examples:**

**Simple Calculator:**
- Only one task: add two numbers
- No data to manage over time
- **Best choice:** Procedural (one `Add()` function)

**Drawing Program:**
- Multiple shapes (Circle, Square, Triangle)
- Each shape has its own position, size, color
- Each shape can move, resize, draw itself
- **Best choice:** OOP (create `Shape` classes)

**Quiz Program:**
- Could work either way!
- Procedural: Functions for asking questions, checking answers
- OOP: `Question` and `Quiz` classes with scores
- **Best choice:** OOP if tracking many questions, Procedural if simple

## Reflection Questions

1. Can you think of a real program you use (game, app, website) that probably uses multiple paradigms? Which parts use which paradigm?

2. If you were building a simple to-do list app, which paradigm would you choose? Why?

3. Has your thinking about "the best way to code" changed after this chapter? How?

## Bonus Challenge

Pick one of the problems from Part 1 and write **pseudocode** (not actual C# code) showing how you would structure it using your chosen paradigm. For example:

```
// Pseudocode for drawing program (OOP approach)
Class Circle:
  - data: x, y, radius, color
  - methods: move(), resize(), draw()

Class Square:
  - data: x, y, size, color
  - methods: move(), resize(), draw()
```

---

**Time Estimate:** 10-15 minutes  
**Difficulty:** Easy  
**Type:** Analysis & Discussion

This exercise bridges Chapter 2 (understanding paradigms) with Chapter 3 (implementing OOP in C#).