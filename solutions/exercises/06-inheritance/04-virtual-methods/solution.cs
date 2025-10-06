using System;
using System.Collections.Generic;

namespace Part2.Ch06.Ex04.Solution
{
    // Base Shape class with virtual methods
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
            return 0.0;
        }
        
        public virtual double CalculatePerimeter()
        {
            return 0.0;
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
    
    // Rectangle derived class
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
    
    // Circle derived class
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
    
    // Triangle derived class
    class Triangle : Shape
    {
        public double Base { get; set; }
        public double Height { get; set; }
        
        public Triangle(string color, double baseLength, double height)
            : base("Triangle", color)
        {
            if (baseLength <= 0 || height <= 0)
                throw new ArgumentException("Dimensions must be positive");
            
            Base = baseLength;
            Height = height;
        }
        
        public override double CalculateArea()
        {
            return (Base * Height) / 2;
        }
        
        public override void Draw()
        {
            Console.WriteLine($"--- {Color} {Name} ---");
            
            int h = (int)Height;
            int b = (int)Base;
            
            for (int i = 0; i < h; i++)
            {
                int spaces = h - i - 1;
                int stars = i * 2 + 1;
                
                for (int j = 0; j < spaces; j++)
                    Console.Write(" ");
                
                for (int j = 0; j < stars; j++)
                {
                    if (j == 0 || j == stars - 1 || i == h - 1)
                        Console.Write("*");
                    else
                        Console.Write(" ");
                }
                
                Console.WriteLine();
            }
        }
    }
    
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Shape Drawing System ===");
            Console.WriteLine();
            Console.WriteLine("Creating shapes...");
            
            // Create shapes of different types
            Shape[] shapes = new Shape[]
            {
                new Rectangle("Blue", 5, 3),
                new Circle("Red", 4),
                new Triangle("Green", 6, 4)
            };
            
            Console.WriteLine($"✓ Rectangle: Blue, 5.0 x 3.0");
            Console.WriteLine($"✓ Circle: Red, radius 4.0");
            Console.WriteLine($"✓ Triangle: Green, base 6.0, height 4.0");
            
            Console.WriteLine();
            Console.WriteLine("Drawing all shapes:");
            Console.WriteLine();
            
            // Use polymorphism to call GetInfo on all shapes
            foreach (Shape shape in shapes)
            {
                shape.GetInfo();
            }
            
            // Calculate statistics
            Console.WriteLine("Shape Statistics:");
            double totalArea = 0;
            foreach (Shape shape in shapes)
            {
                totalArea += shape.CalculateArea();
            }
            
            Console.WriteLine($"Total shapes: {shapes.Length}");
            Console.WriteLine($"Total area: {totalArea:F2} square units");
            Console.WriteLine($"Average area: {totalArea / shapes.Length:F2} square units");
        }
    }
}