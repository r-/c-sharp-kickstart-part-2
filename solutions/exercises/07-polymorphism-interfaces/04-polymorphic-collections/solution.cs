using System;
using System.Collections.Generic;
using System.Linq;

namespace Part2.Ch07.Ex04.Solution
{
    interface IShape
    {
        string Name { get; }
        string Color { get; set; }
        double GetArea();
        double GetPerimeter();
        void Draw();
    }
    
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
    
    class Rectangle : IShape
    {
        public string Name => "Rectangle";
        public string Color { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        
        public double GetArea()
        {
            return Width * Height;
        }
        
        public double GetPerimeter()
        {
            return 2 * (Width + Height);
        }
        
        public void Draw()
        {
            Console.WriteLine($"⬜ {Color} {Name} ({Width:F2}×{Height:F2}) - Area: {GetArea():F2}, Perimeter: {GetPerimeter():F2}");
        }
    }
    
    class Triangle : IShape
    {
        public string Name => "Triangle";
        public string Color { get; set; }
        public double Base { get; set; }
        public double Height { get; set; }
        public double SideA { get; set; }
        public double SideB { get; set; }
        
        public double GetArea()
        {
            return 0.5 * Base * Height;
        }
        
        public double GetPerimeter()
        {
            return Base + SideA + SideB;
        }
        
        public void Draw()
        {
            Console.WriteLine($"🔺 {Color} {Name} (base={Base:F2}) - Area: {GetArea():F2}, Perimeter: {GetPerimeter():F2}");
        }
    }
    
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
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Shape Drawing Application");
            Console.WriteLine("=========================\n");
            
            ShapeManager manager = new ShapeManager();
            bool running = true;
            
            while (running)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add Circle");
                Console.WriteLine("2. Add Rectangle");
                Console.WriteLine("3. Add Triangle");
                Console.WriteLine("4. Display All Shapes");
                Console.WriteLine("5. Show Total Area");
                Console.WriteLine("6. Show Average Area");
                Console.WriteLine("7. Find Largest Shape");
                Console.WriteLine("8. Filter by Color");
                Console.WriteLine("9. Exit");
                
                Console.Write("\nChoice: ");
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter radius: ");
                        if (double.TryParse(Console.ReadLine(), out double radius))
                        {
                            Console.Write("Enter color: ");
                            string color = Console.ReadLine();
                            
                            manager.AddShape(new Circle 
                            { 
                                Radius = radius, 
                                Color = color 
                            });
                        }
                        else
                        {
                            Console.WriteLine("Invalid radius!");
                        }
                        break;
                        
                    case "2":
                        Console.Write("Enter width: ");
                        if (double.TryParse(Console.ReadLine(), out double width))
                        {
                            Console.Write("Enter height: ");
                            if (double.TryParse(Console.ReadLine(), out double height))
                            {
                                Console.Write("Enter color: ");
                                string color = Console.ReadLine();
                                
                                manager.AddShape(new Rectangle 
                                { 
                                    Width = width,
                                    Height = height,
                                    Color = color 
                                });
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid dimensions!");
                        }
                        break;
                        
                    case "3":
                        Console.Write("Enter base: ");
                        if (double.TryParse(Console.ReadLine(), out double triangleBase))
                        {
                            Console.Write("Enter height: ");
                            if (double.TryParse(Console.ReadLine(), out double triangleHeight))
                            {
                                Console.Write("Enter sideA: ");
                                if (double.TryParse(Console.ReadLine(), out double sideA))
                                {
                                    Console.Write("Enter sideB: ");
                                    if (double.TryParse(Console.ReadLine(), out double sideB))
                                    {
                                        Console.Write("Enter color: ");
                                        string color = Console.ReadLine();
                                        
                                        manager.AddShape(new Triangle 
                                        { 
                                            Base = triangleBase,
                                            Height = triangleHeight,
                                            SideA = sideA,
                                            SideB = sideB,
                                            Color = color 
                                        });
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid dimensions!");
                        }
                        break;
                        
                    case "4":
                        manager.DrawAll();
                        break;
                        
                    case "5":
                        Console.WriteLine($"\nTotal area of all shapes: {manager.GetTotalArea():F2}");
                        break;
                        
                    case "6":
                        Console.WriteLine($"\nAverage area: {manager.GetAverageArea():F2}");
                        break;
                        
                    case "7":
                        IShape largest = manager.GetLargestShape();
                        if (largest != null)
                        {
                            Console.WriteLine($"\nLargest shape: {largest.Color} {largest.Name} with area {largest.GetArea():F2}");
                        }
                        else
                        {
                            Console.WriteLine("No shapes to compare!");
                        }
                        break;
                        
                    case "8":
                        Console.Write("Enter color to filter: ");
                        string filterColor = Console.ReadLine();
                        List<IShape> filtered = manager.GetShapesByColor(filterColor);
                        
                        if (filtered.Count > 0)
                        {
                            Console.WriteLine($"\nShapes with color {filterColor}:");
                            foreach (var shape in filtered)
                            {
                                Console.Write("- ");
                                shape.Draw();
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No shapes found with color {filterColor}");
                        }
                        break;
                        
                    case "9":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;
                        
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}