# Exercise 06-04: Virtual Methods

## Goal

Practice designing flexible class hierarchies using virtual methods and polymorphism. You'll learn when to make methods virtual, how to override them effectively, and how polymorphism enables treating different types uniformly.

## Background

Virtual methods are the foundation of polymorphism in C#. By marking a base class method as `virtual`, you allow derived classes to provide their own implementations while still being able to call the base implementation if needed. This creates flexible, extensible designs.

## Instructions

You're building a shape drawing system. Different shapes calculate area and draw differently, but they all share the concept of being shapes with dimensions.

1. Create an abstract shape hierarchy with virtual methods
2. Override methods to provide shape-specific behavior
3. Use polymorphism to treat all shapes uniformly
4. Practice both overriding completely and calling base implementations

## Requirements

1. **Shape base class must have:**
   - Name property (string)
   - Color property (string)
   - Virtual method `CalculateArea()` returning double
   - Virtual method `CalculatePerimeter()` returning double
   - Virtual method `Draw()` that displays the shape
   - Method `GetInfo()` that uses virtual methods

2. **Rectangle derived class must:**
   - Add Width and Height properties
   - Override `CalculateArea()` to return width × height
   - Override `CalculatePerimeter()` to return 2 × (width + height)
   - Override `Draw()` to show ASCII rectangle

3. **Circle derived class must:**
   - Add Radius property
   - Override `CalculateArea()` to return π × r²
   - Override `CalculatePerimeter()` to return 2 × π × r
   - Override `Draw()` to show ASCII circle

4. **Triangle derived class must:**
   - Add Base and Height properties
   - Override `CalculateArea()` to return (base × height) / 2
   - Override `Draw()` to show ASCII triangle
   - Keep default perimeter (return 0 or call base)

5. **In Main() demonstrate:**
   - Create multiple shapes of different types
   - Store them in a Shape array or list
   - Use polymorphism to calculate areas
   - Use polymorphism to draw all shapes
   - Show that different implementations are called

## Expected Output

```
=== Shape Drawing System ===

Creating shapes...
✓ Rectangle: Blue, 5.0 x 3.0
✓ Circle: Red, radius 4.0
✓ Triangle: Green, base 6.0, height 4.0

Drawing all shapes:

--- Blue Rectangle ---
*****
*   *
*   *
*****
Area: 15.00 square units
Perimeter: 16.00 units

--- Red Circle ---
   ***
 *     *
*       *
 *     *
   ***
Area: 50.27 square units
Perimeter: 25.13 units

--- Green Triangle ---
    *
   * *
  *   *
 *     *
*********
Area: 12.00 square units
Perimeter: Not implemented

Shape Statistics:
Total shapes: 3
Total area: 77.27 square units
Average area: 25.76 square units
```

## Hints

**Shape base class:**
```csharp
class Shape
{
    public string Name { get; set; }
    public string Color { get; set; }
    
    public Shape(string name, string color)
    {
        Name = name;
        Color = color;
    }
    
    public virtual double CalculateArea()
    {
        return 0.0;  // Default implementation
    }
    
    public virtual double CalculatePerimeter()
    {
        return 0.0;  // Default implementation
    }
    
    public virtual void Draw()
    {
        Console.WriteLine($"--- {Color} {Name} ---");
        Console.WriteLine("(Generic shape - no specific drawing)");
    }
    
    public void GetInfo()
    {
        Draw();
        Console.WriteLine($"Area: {CalculateArea():F2} square units");
        if (CalculatePerimeter() > 0)
            Console.WriteLine($"Perimeter: {CalculatePerimeter():F2} units");
        else
            Console.WriteLine("Perimeter: Not implemented");
        Console.WriteLine();
    }
}
```

**Rectangle example:**
```csharp
class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public Rectangle(string color, double width, double height)
        : base("Rectangle", color)
    {
        if (width <= 0 || height <= 0)
            throw new ArgumentException("Dimensions must be positive");
        
        Width = width;
        Height = height;
    }
    
    public override double CalculateArea()
    {
        return Width * Height;
    }
    
    public override double CalculatePerimeter()
    {
        return 2 * (Width + Height);
    }
    
    public override void Draw()
    {
        Console.WriteLine($"--- {Color} {Name} ---");
        
        // Simple ASCII rectangle
        int w = (int)Width;
        int h = (int)Height;
        
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                if (i == 0 || i == h - 1 || j == 0 || j == w - 1)
                    Console.Write("*");
                else
                    Console.Write(" ");
            }
            Console.WriteLine();
        }
    }
}
```

**Circle example:**
```csharp
class Circle : Shape
{
    public double Radius { get; set; }
    
    public Circle(string color, double radius)
        : base("Circle", color)
    {
        if (radius <= 0)
            throw new ArgumentException("Radius must be positive");
        
        Radius = radius;
    }
    
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
    
    public override double CalculatePerimeter()
    {
        return 2 * Math.PI * Radius;
    }
    
    public override void Draw()
    {
        Console.WriteLine($"--- {Color} {Name} ---");
        
        // Simple ASCII circle
        int r = (int)Radius;
        for (int i = -r; i <= r; i++)
        {
            for (int j = -r; j <= r; j++)
            {
                double dist = Math.Sqrt(i * i + j * j);
                if (Math.Abs(dist - r) < 0.5)
                    Console.Write("*");
                else
                    Console.Write(" ");
            }
            Console.WriteLine();
        }
    }
}
```

**Using polymorphism:**
```csharp
Shape[] shapes = new Shape[]
{
    new Rectangle("Blue", 5, 3),
    new Circle("Red", 4),
    new Triangle("Green", 6, 4)
};

foreach (Shape shape in shapes)
{
    shape.GetInfo();  // Calls overridden methods!
}

// Calculate total area
double totalArea = 0;
foreach (Shape shape in shapes)
{
    totalArea += shape.CalculateArea();
}
Console.WriteLine($"Total area: {totalArea:F2} square units");
```

## Bonus Challenge

Enhance your shape system:

1. **Add more shapes** - Polygon, Ellipse, Square (derived from Rectangle)
2. **Add scaling** - Virtual method `Scale(double factor)` that adjusts dimensions
3. **Add rotation** - Track rotation angle, affect drawing
4. **Add comparison** - Implement IComparable to sort by area
5. **Add shape validation** - Virtual `Validate()` method checking if dimensions are valid
6. **Add 3D shapes** - Create Shape3D base class with Volume calculation
7. **Add color schemes** - Predefined color constants, validation
8. **Add shape builder** - Factory pattern to create shapes from user input

## What You'll Learn

- When and why to mark methods as `virtual`
- How `override` provides different implementations
- The power of polymorphism in action
- How to design flexible class hierarchies
- When to call base implementations vs. replace completely
- How virtual methods enable extensibility
- The relationship between virtual methods and polymorphism

Remember: Virtual methods say "this behavior can be customized." Override says "here's my custom version." Polymorphism says "I'll call the right version automatically."