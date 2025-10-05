# Mini Project: Build YOUR Student Registry

## Project Overview

Create YOUR own student management system with bulletproof encapsulation and comprehensive invariant enforcement! This is YOUR chance to build a production-quality system that cannot be broken by invalid data. While there are minimum requirements, you're encouraged to add YOUR creative features and design YOUR registry YOUR way.

## Why This Project?

Real student information systems handle critical data that must remain accurate and consistent. This project teaches you enterprise-level defensive programming patterns used in production database systems, CRM software, and educational platforms.

## Learning Goals

- Enforce multiple invariants simultaneously
- Use guard clauses throughout your codebase
- Design read-only and computed properties appropriately
- Prevent all invalid states at every level
- Handle complex business rules with confidence
- Build classes that cannot be misused

## Minimum Requirements

YOUR student registry MUST include these core features:

### 1. Student Class

Create a `Student` class with:
- Student ID (read-only, set once, validated: must be positive)
- Name (changeable, validated: not empty, 2-50 characters)
- Grade (changeable, validated: 0-100 range)
- Enrollment date (read-only, set in constructor)
- Methods: `UpdateGrade` with comprehensive validation
- Computed property: `IsPassing` (grade >= 50)

### 2. StudentRegistry Class

Create a `StudentRegistry` class with:
- Private list of students
- `AddStudent` method (reject null, duplicate IDs)
- `FindStudent` by ID
- `GetPassingStudents` returns list
- `GetFailingStudents` returns list
- `DisplayAllStudents` with formatted output

### 3. Invariants to Enforce

Your system must maintain these rules at all times:
- No duplicate student IDs in registry
- All students have valid names (2-50 chars)
- All grades are 0-100
- Cannot add null students
- Student ID never changes after creation
- Enrollment date never changes
- Cannot have negative student count

### 4. Validation Requirements

Every operation must validate:
- Constructor parameters
- Method inputs
- State preconditions
- Business rules
- Data consistency

### 5. User Interaction

Your program should:
- Create multiple students
- Add them to registry
- Attempt invalid operations
- Show they're rejected with clear errors
- Update student grades
- Display formatted student lists
- Calculate statistics (passing rate, average grade)

## Expected Output Example

```
Creating Student Registry...

Adding students:
✓ Student 1001 (Alice) added successfully
✓ Student 1002 (Bob) added successfully
✓ Student 1003 (Charlie) added successfully

Attempting invalid operations:
Error: Student ID must be positive
Error: Duplicate student ID: 1001
Error: Name must be 2-50 characters
Error: Grade must be between 0 and 100
Error: Cannot add null student

Student Registry:
1001: Alice Johnson - Grade: 85 (Passing) ✓
1002: Bob Smith - Grade: 45 (Not Passing) ✗
1003: Charlie Brown - Grade: 92 (Passing) ✓

Statistics:
Total Students: 3
Passing: 2 (66.7%)
Failing: 1 (33.3%)
Average Grade: 74.0

Testing grade updates:
✓ Alice's grade updated from 85 to 92
Error: Grade must be between 0 and 100 (got: 150)
Alice's grade remains 92 (protected)

Final Registry State:
All invariants maintained: 3 students, all valid
```

## Implementation Steps

1. **Design Student Class** - Plan all properties and invariants
2. **Add Comprehensive Validation** - Constructor and all setters
3. **Create Registry Class** - Manage student collection
4. **Implement Guard Clauses** - Every method validates inputs
5. **Add Computed Properties** - Derive values from data
6. **Test All Edge Cases** - Verify every invariant
7. **Polish and Document** - Clean code, clear messages

## Hints

### Student Class Structure

```csharp
class Student
{
    private string name;
    private int grade;
    
    // Read-only properties
    public int Id { get; }
    public DateTime EnrollmentDate { get; }
    
    // Validated property
    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name is required");
            
            if (value.Length < 2 || value.Length > 50)
                throw new ArgumentException("Name must be 2-50 characters");
            
            name = value;
        }
    }
    
    // Validated property with private setter
    public int Grade
    {
        get { return grade; }
        private set
        {
            if (value < 0 || value > 100)
                throw new ArgumentOutOfRangeException("Grade must be 0-100");
            
            grade = value;
        }
    }
    
    // Computed property
    public bool IsPassing => Grade >= 50;
    
    // Constructor with validation
    public Student(int id, string name, int initialGrade)
    {
        if (id <= 0)
            throw new ArgumentException("ID must be positive");
        
        Id = id;
        Name = name;  // Uses property validation
        Grade = initialGrade;
        EnrollmentDate = DateTime.Now;
    }
    
    // Method with guard clauses
    public void UpdateGrade(int newGrade)
    {
        Grade = newGrade;  // Uses property validation
    }
}
```

### StudentRegistry Class Structure

```csharp
class StudentRegistry
{
    private List<Student> students;
    
    public int StudentCount => students.Count;
    public int PassingCount => students.Count(s => s.IsPassing);
    public int FailingCount => students.Count(s => !s.IsPassing);
    public double AverageGrade => students.Average(s => s.Grade);
    
    public StudentRegistry()
    {
        students = new List<Student>();
    }
    
    public void AddStudent(Student student)
    {
        // Guard clause: null check
        if (student == null)
            throw new ArgumentNullException(nameof(student));
        
        // Guard clause: duplicate check
        if (students.Any(s => s.Id == student.Id))
            throw new InvalidOperationException($"Duplicate student ID: {student.Id}");
        
        students.Add(student);
    }
    
    public Student FindStudent(int id)
    {
        return students.FirstOrDefault(s => s.Id == id);
    }
    
    public List<Student> GetPassingStudents()
    {
        return students.Where(s => s.IsPassing).ToList();
    }
    
    public List<Student> GetFailingStudents()
    {
        return students.Where(s => !s.IsPassing).ToList();
    }
}
```

### Validation Pattern

```csharp
// In every method, check everything before doing anything
public void SomeMethod(int param)
{
    // Guard 1: Check parameter
    if (param <= 0)
        throw new ArgumentException("Param must be positive");
    
    // Guard 2: Check state
    if (someCondition)
        throw new InvalidOperationException("Invalid state");
    
    // Main logic - now safe to proceed
    DoTheWork();
}
```

## Make It YOUR Own!

After meeting minimum requirements, add YOUR creative features:

### Feature Ideas

- **Grade History** - Track all grade changes with dates
- **Attendance Tracking** - Add attendance percentage
- **Course Enrollment** - Multiple courses per student
- **GPA Calculation** - Weighted average across courses
- **Honor Roll** - Automatic list of students with grade >= 90
- **Academic Probation** - Alert for students with grade < 60
- **Grade Categories** - A, B, C, D, F classification
- **Bulk Operations** - Update multiple students at once
- **Search Features** - Find by name, grade range, passing status
- **Statistics Dashboard** - Comprehensive analytics
- **Export Functions** - Generate reports or CSV files
- **Student Notes** - Add comments or annotations
- **Parent Contacts** - Store emergency contact info
- **Prerequisites** - Course dependencies and requirements
- **Graduation Tracking** - Credits earned toward graduation

### Design Decisions YOU Make

Think about:
- What additional student information should you track?
- How should grade updates be logged?
- Should there be a maximum number of students?
- How can you make the display more informative?
- What statistics would be most useful?
- Should students be sortable by different criteria?

**Remember**: There's no single "correct" registry. YOUR creativity and problem-solving approach matter!

## Self-Assessment Checklist

Test your registry thoroughly:

### Core Functionality
- [ ] Student class enforces all invariants
- [ ] Constructor validates all parameters
- [ ] Guard clauses in every method
- [ ] Read-only properties used appropriately
- [ ] Computed properties for derived values
- [ ] Registry prevents duplicate IDs
- [ ] Cannot add invalid students
- [ ] All fields are private
- [ ] Clear, specific error messages
- [ ] Program runs without crashes

### Invariant Enforcement
- [ ] Student ID always positive
- [ ] Names always 2-50 characters
- [ ] Grades always 0-100
- [ ] No null students in registry
- [ ] No duplicate IDs
- [ ] Enrollment date never changes
- [ ] All invariants survive error conditions

### Code Quality
- [ ] Clean guard clause patterns
- [ ] Appropriate properties (auto vs custom)
- [ ] Good method organization
- [ ] Descriptive variable names
- [ ] Comments explain complex logic
- [ ] No code duplication

### Edge Cases
- [ ] Handles boundary values (0, 100, etc.)
- [ ] Handles empty registry
- [ ] Handles single student
- [ ] Handles large numbers of students
- [ ] Validates string lengths correctly
- [ ] Null checks everywhere needed

## What Makes a Great Project?

Your project will be evaluated on:

- **Encapsulation** (40%): All invariants enforced, no invalid states possible
- **Code Quality** (30%): Clean guard clauses, appropriate properties, clear naming
- **Functionality** (20%): Works reliably, handles all edge cases
- **Creativity** (10%): YOUR unique features and enhancements

## Common Pitfalls to Avoid

**Problem**: Forgetting to validate in constructor
```csharp
// ❌ Wrong - invalid object can be created
public Student(int id, string name, int grade)
{
    Id = id;  // Could be negative!
    Name = name;  // Could be empty!
    Grade = grade;  // Could be 200!
}

// ✅ Correct - validate everything
public Student(int id, string name, int grade)
{
    if (id <= 0)
        throw new ArgumentException("ID must be positive");
    Id = id;
    Name = name;  // Uses property validation
    Grade = grade;  // Uses property validation
}
```

**Problem**: Not checking for duplicates
```csharp
// ❌ Wrong - allows duplicate IDs
public void AddStudent(Student student)
{
    students.Add(student);
}

// ✅ Correct - prevents duplicates
public void AddStudent(Student student)
{
    if (student == null)
        throw new ArgumentNullException();
    if (students.Any(s => s.Id == student.Id))
        throw new InvalidOperationException("Duplicate ID");
    students.Add(student);
}
```

**Problem**: Exposing mutable collection
```csharp
// ❌ Wrong - caller can modify internal list
public List<Student> GetStudents()
{
    return students;  // Danger!
}

// ✅ Correct - return read-only or copy
public IReadOnlyList<Student> GetStudents()
{
    return students.AsReadOnly();
}
```

## Showcase YOUR Work!

When you're done:
- Demonstrate YOUR registry system
- Explain YOUR invariant choices
- Show how YOUR system prevents invalid states
- Share unique features YOU added
- Discuss challenges YOU overcame
- Present YOUR statistics dashboard

**This is YOUR project** - make it bulletproof and professional!

## Resources

- Review Chapter 5 concepts for encapsulation patterns
- Look at exercises 01-04 for invariant examples
- Remember: invariants + guard clauses = unbreakable code

## Extension Ideas for Advanced Students

**Level 1: Enhanced Student**
- Add student email with validation
- Track student status (active, graduated, withdrawn)
- Add age calculation from birth date
- Multiple phone numbers

**Level 2: Advanced Registry**
- Sort students by different criteria
- Filter by multiple conditions
- Search with partial name matching
- Pagination for large lists

**Level 3: Course Management**
- Course class with prerequisites
- Student enrollment in courses
- Grade per course
- GPA calculation

**Level 4: Reporting System**
- Generate student transcripts
- Create grade distribution charts (text-based)
- Export to CSV or JSON
- Import from file with validation

**Level 5: Complete School System**
- Teacher class
- Classroom assignments
- Schedule management
- Attendance tracking
- Parent portal features

---

**Good luck!** Remember: every invariant you enforce is a bug you prevent. Make YOUR student registry impossible to break!