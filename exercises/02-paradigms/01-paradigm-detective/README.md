# Exercise 02-01: Paradigm Detective

## Goal

Learn to recognize and distinguish between imperative, procedural, and object-oriented programming styles by analyzing code snippets.

## Background

Understanding different programming paradigms helps you read existing code and choose the right approach for new problems. Each paradigm has its own "signature" - patterns that make it recognizable.

Before object-oriented programming, code was written in imperative or procedural styles. Recognizing these patterns helps you understand why OOP was invented and when to use each approach.

## Instructions

This is a **concept recognition exercise** - no coding required. You'll analyze code snippets and identify which paradigm each one uses.

### Quick Reference Table

| Paradigm | Core Idea | Key Concept | Typical Pattern |
|----------|-----------|-------------|-----------------|
| **Imperative** | Step-by-step instructions that modify variables | "Do this, then that" | Direct variable assignments and sequences |
| **Procedural** | Group logic into reusable functions | "Make a function for this job" | Functions that are called from main |
| **Object-Oriented** | Bundle data and behavior inside classes/objects | "Model real things" | Classes with properties and methods |

## Requirements

For each code snippet below:

1. Identify which paradigm it uses (Imperative, Procedural, or Object-Oriented)
2. Write one sentence explaining your choice
3. Answer the reflection questions at the end

### Code Snippets to Analyze

**Snippet A:**
```csharp
int total = 0;
total += 10;
total += 5;
Console.WriteLine(total);
```

**Snippet B:**
```csharp
static int Add(int a, int b)
{
    return a + b;
}

static void Main()
{
    int result = Add(5, 3);
    Console.WriteLine(result);
}
```

**Snippet C:**
```csharp
class Counter
{
    public int Value { get; set; }
    
    public void Increment()
    {
        Value++;
    }
}

static void Main()
{
    Counter counter = new Counter();
    counter.Increment();
    Console.WriteLine(counter.Value);
}
```

## Expected Response Format

Create a document (text file or markdown) with your answers:

```markdown
## Snippet A
Paradigm: [Your answer]
Explanation: [One sentence why]

## Snippet B
Paradigm: [Your answer]
Explanation: [One sentence why]

## Snippet C
Paradigm: [Your answer]
Explanation: [One sentence why]

## Analysis Questions
1. [Your answer]
2. [Your answer]

## Reflection
[Your thoughts]
```

## Analysis Questions

1. **Scalability**: Which snippet would scale best to a large program with many similar tasks? Explain why.

2. **Clarity**: Which snippet is easiest to understand at first glance? Why?

## Reflection

Answer these questions in 2-3 sentences each:

1. Why do you think programming evolved from snippet A → B → C?

2. Give one real-world example where you would use each paradigm:
   - Imperative: 
   - Procedural: 
   - Object-Oriented: 

## Hints

- Look for the **organizing principle**: Is code organized by sequence, functions, or objects?
- Check for **data organization**: Is data separate or bundled with behavior?
- Consider **reusability**: Can parts be reused easily?
- Think about **scale**: Which would work better for 1,000 lines of code?

## Bonus Challenge

Find a piece of code in your previous exercises and identify which paradigm it uses. Rewrite it in a different paradigm and compare the two versions.

---

**Time Estimate:** 15 minutes  
**Difficulty:** Easy  
**Type:** Concept Recognition

This exercise prepares you for Chapter 3 where you'll start writing object-oriented code yourself.