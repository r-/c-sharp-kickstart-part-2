# C# Kickstart Part 2 - Object-Oriented Programming

Welcome to **C# Kickstart Part 2**! This course teaches object-oriented programming fundamentals in C# through hands-on exercises and projects.

## Course Overview

This course builds on C# fundamentals and guides you through:

- **Chapter 1**: Introduction and Review - Refresher on C# basics
- **Chapter 2**: Programming Paradigms - Understanding different programming approaches
- **Chapter 3**: The Road to Objects - Creating your first classes
- **Chapter 4**: Classes and Properties - Safe data access with validation
- **Chapter 5**: Encapsulation - Writing bulletproof code with invariants

## Learning Objectives

By completing this course, you will:

- Master class design and object creation
- Understand encapsulation and information hiding
- Implement properties with validation
- Apply defensive programming patterns
- Build robust, maintainable code

## 🗂️ Repository Structure

```
c-sharp-kickstart-part-2/
├── book/              # Course chapters and learning materials
├── exercises/         # Hands-on practice exercises
├── projects/          # Mini projects to apply your knowledge
├── solutions/         # Reference solutions for exercises
├── tests/             # Automated test suites
└── docs/              # Additional documentation
```

##  Getting Started

### Prerequisites

- **.NET 8.0 SDK** or later installed
- A code editor (**Visual Studio**, **VS Code**, or **Rider**)
- Basic C# knowledge (variables, loops, methods)

### Installation

1. **Clone this repository**:
   ```bash
   git clone https://github.com/r-/c-sharp-kickstart-part-2.git
   cd c-sharp-kickstart-part-2
   ```

2. **Verify installation**:
   ```bash
   dotnet --version
   ```

3. **Build the solution**:
   ```bash
   dotnet build
   ```

4. **Run tests** (optional):
   ```bash
   dotnet test
   ```

## 📖 How to Use This Course

1. **Read the chapter** in the [`book/chapters/`](./book/chapters/) directory
2. **Complete exercises** in the [`exercises/`](./exercises/) directory
3. **Build the mini project** to apply what you learned
4. **Check solutions** in [`solutions/`](./solutions/) if you get stuck
5. **Run tests** to verify your solutions

### Recommended Learning Path

Start with Chapter 1 and work sequentially through the chapters. Each chapter builds on previous concepts.

## Exercises

Each chapter includes progressive exercises:

- **Exercise 01-XX**: Introduction exercises
- **Exercise 02-XX**: Paradigms exploration
- **Exercise 03-XX**: Class creation practice
- **Exercise 04-XX**: Properties and validation
- **Exercise 05-XX**: Encapsulation mastery

##  Mini Projects

Apply your knowledge with comprehensive projects:

- **Chapter 3 Project**: Build YOUR first object-oriented program
- **Chapter 4 Project**: Create a validated bank account system
- **Chapter 5 Project**: Design a student registry with complete encapsulation

## 🧪 Testing Your Code

Run tests for specific exercises:

```bash
# Run all tests
dotnet test

# Run tests for a specific chapter
dotnet test tests/03-road-to-objects.Tests/

# Run a specific test
dotnet test --filter "DisplayName~MethodName"
```

## Using GitHub

Learn how to save and sync your work across computers:

- See [`docs/using-github.md`](./docs/using-github.md) for detailed instructions
- Fork this repository to your GitHub account
- Push your solutions to track your progress
- Access your work from anywhere

## Contributing

Found a typo or have a suggestion? Contributions are welcome!

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/improvement`)
3. Commit your changes (`git commit -m 'Add some improvement'`)
4. Push to the branch (`git push origin feature/improvement`)
5. Open a Pull Request

## License

This course material is provided for educational purposes.

## Getting Help

- Review the chapter materials thoroughly
- Check the [`solutions/`](./solutions/) directory for reference
- Try the exercises multiple times
- Experiment and learn from mistakes!

## Acknowledgments

Created for students learning object-oriented programming in C#.

---

**Ready to start?** Begin with [Chapter 1: Introduction](./book/chapters/01-intro.md)!
