# Exercise 07-04: Polymorphic Collections

## Goal

Master working with collections of interface types. You'll create a shape drawing system that manages different shape types through a common interface, demonstrating the full power of polymorphism.

## Background

One of the most powerful uses of interfaces is storing different types in the same collection. A `List<IShape>` can hold circles, rectangles, triangles—anything implementing `IShape`. This enables elegant, extensible designs.

## Instructions

You will create a shape drawing application with polymorphic collections.

### Part 1: Define the Shape Interface

Create an `IShape` interface with:
- Property: `string Name { get; }`
- Property: `string Color { get; set; }`
- Method: `double GetArea()`
- Method: `double GetPerimeter()`
- Method: `void Draw()`

### Part 2: Implement Shape Classes

Create these shape implementations:

1. **Circle**
   - Property: `double Radius`
   - Area: `π * r²`
   - Perimeter: `2 * π * r`
   - Draw: Display "⭕ Circle"

2. **Rectangle**
   - Properties: `double Width`, `double Height`
   - Area: `width * height`
   - Perimeter: `2 * (width + height)`
   - Draw: Display "⬜ Rectangle"

3. **Triangle**
   - Properties: `double Base`, `double Height`, `double SideA`, `double SideB`
   - Area: `0.5 * base * height`
   - Perimeter: `base + sideA + sideB`
   - Draw: Display "🔺 Triangle"

### Part 3: Create Shape Manager

Build a `ShapeManager` class with:
- Private `List<IShape>` to store shapes
- Method: `void AddShape(IShape shape)`
- Method: `void DrawAll()`
- Method: `double GetTotalArea()`
- Method: `double GetAverageArea()`
- Method: `IShape GetLargestShape()` (by area)
- Method: `List<IShape> GetShapesByColor(string color)`
- Property: `int ShapeCount`

### Part 4: Interactive Menu

Create a menu system that allows users to:
1. Add a new shape (choose type and properties)
2. Display all shapes
3. Show total area
4. Show average area
5. Find largest shape
6. Filter shapes by color
7. Exit

## Requirements

Your solution must:
1. Define `IShape` interface with all specified members
2. Implement all three shape classes
3. Create `ShapeManager` with all methods
4. Use `List<IShape>` to store different shape types together
5. Demonstrate polymorphism through the collection
6. Build interactive menu for user interaction
7. Handle invalid inputs gracefully

## Expected Output

```
Shape Drawing Application
=========================

Menu:
1. Add Circle
2. Add Rectangle
3. Add Triangle
4. Display All Shapes
5. Show Total Area
6. Show Average Area
7. Find Largest Shape
8. Filter by Color
9. Exit

Choice: 1
Enter radius: 5
Enter color: Red
✓ Circle added

Choice: 2
Enter width: 4
Enter height: 6
Enter color: Blue
✓ Rectangle added

Choice: 3
Enter base: 3
Enter height: 4
Enter sideA: 3
Enter sideB: 5
Enter color: Green
✓ Triangle added

Choice: 4

All Shapes:
-----------
1. ⭕ Red Circle (r=5.00) - Area: 78.54, Perimeter: 31.42
2. ⬜ Blue Rectangle (4.00×6.00) - Area: 24.00, Perimeter: 20.00
3. 🔺 Green Triangle (base=3.00) - Area: 6.00, Perimeter: 11.00

Choice: 5
Total area of all shapes: 108.54

Choice: 7
Largest shape: ⭕ Red Circle with area 78.54

Choice: 8
Enter color to filter: Blue
Shapes with color Blue:
- ⬜ Blue Rectangle - Area: 24.00

Choice: 9
Goodbye!
```

## Hints

**IShape interface:**
```csharp
interface IShape
{
    string Name { get; }
    string Color { get; set; }
    double GetArea();
    double GetPerimeter();
    void Draw();
}
```

**Circle implementation:**
```csharp
class Circle : IShape
{
    public string Name => "Circle";
    public string Color { get; set; }
    public double Radius { get; set; }
    
    public double GetArea()
    {
        return Math.PI * Radius * Radius;
    }
    
    public double GetPerimeter()
    {
        return 2 * Math.PI * Radius;
    }
    
    public void Draw()
    {
        Console.WriteLine($"⭕ {Color} {Name} (r={Radius:F2}) - Area: {GetArea():F2}, Perimeter: {GetPerimeter():F2}");
    }
}
```

**ShapeManager:**
```csharp
class ShapeManager
{
    private List<IShape> shapes = new List<IShape>();
    
    public int ShapeCount => shapes.Count;
    
    public void AddShape(IShape shape)
    {
        shapes.Add(shape);
        Console.WriteLine($"✓ {shape.Name} added");
    }
    
    public void DrawAll()
    {
        Console.WriteLine("\nAll Shapes:");
        Console.WriteLine("-----------");
        
        for (int i = 0; i < shapes.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            shapes[i].Draw();
        }
    }
    
    public double GetTotalArea()
    {
        double total = 0;
        foreach (IShape shape in shapes)
        {
            total += shape.GetArea();
        }
        return total;
    }
    
    public double GetAverageArea()
    {
        if (shapes.Count == 0) return 0;
        return GetTotalArea() / shapes.Count;
    }
    
    public IShape GetLargestShape()
    {
        if (shapes.Count == 0) return null;
        
        IShape largest = shapes[0];
        foreach (IShape shape in shapes)
        {
            if (shape.GetArea() > largest.GetArea())
            {
                largest = shape;
            }
        }
        return largest;
    }
    
    public List<IShape> GetShapesByColor(string color)
    {
        List<IShape> filtered = new List<IShape>();
        foreach (IShape shape in shapes)
        {
            if (shape.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
            {
                filtered.Add(shape);
            }
        }
        return filtered;
    }
}
```

**Menu system:**
```csharp
ShapeManager manager = new ShapeManager();
bool running = true;

while (running)
{
    Console.WriteLine("\nMenu:");
    Console.WriteLine("1. Add Circle");
    // ... other options
    Console.WriteLine("9. Exit");
    
    Console.Write("\nChoice: ");
    string choice = Console.ReadLine();
    
    switch (choice)
    {
        case "1":
            Console.Write("Enter radius: ");
            double radius = double.Parse(Console.ReadLine());
            Console.Write("Enter color: ");
            string color = Console.ReadLine();
            
            manager.AddShape(new Circle 
            { 
                Radius = radius, 
                Color = color 
            });
            break;
            
        case "4":
            manager.DrawAll();
            break;
            
        case "9":
            running = false;
            Console.WriteLine("Goodbye!");
            break;
    }
}
```

## Common Mistakes to Avoid

- ❌ Using `List<Circle>` instead of `List<IShape>`—loses polymorphism
- ❌ Forgetting to check for empty list before finding largest
- ❌ Not handling invalid user input
- ❌ Casting to specific types instead of using interface methods

## Bonus Challenge

1. Add `IComparable<IShape>` to enable sorting by area
2. Add method `void SortByArea()` to ShapeManager
3. Add `void RemoveShape(int index)` method
4. Save shapes to a file and load them back
5. Add `ICloneable` interface to allow copying shapes
6. Create a `CompositeShape` that contains other shapes

## What You're Learning

- Working with heterogeneous collections through interfaces
- The power of `List<IInterface>` for polymorphism
- How interfaces enable extensible designs
- LINQ operations on interface collections
- Building flexible APIs using interface types

---

**Congratulations!** After completing this exercise, you've mastered the core concepts of polymorphism and interfaces. Continue to the [Mini Project: Plugin System](../../projects/07-polymorphism-interfaces/) to build a complete system using these principles.