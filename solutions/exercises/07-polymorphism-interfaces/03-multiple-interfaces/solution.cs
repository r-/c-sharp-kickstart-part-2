using System;
using System.Collections.Generic;

namespace Part2.Ch07.Ex03.Solution
{
    interface IDrawable
    {
        string Sprite { get; }
        void Draw();
    }
    
    interface IMovable
    {
        int X { get; set; }
        int Y { get; set; }
        void Move(int deltaX, int deltaY);
    }
    
    interface IDamageable
    {
        int Health { get; }
        int MaxHealth { get; }
        void TakeDamage(int amount);
        bool IsAlive();
    }
    
    interface IInteractable
    {
        string InteractionText { get; }
        void Interact();
    }
    
    class Player : IDrawable, IMovable, IDamageable, IInteractable
    {
        public string Sprite => "🧑";
        
        public int X { get; set; }
        public int Y { get; set; }
        
        private int health;
        public int Health 
        { 
            get => health;
            private set => health = Math.Max(0, value);
        }
        public int MaxHealth { get; } = 100;
        
        public string InteractionText => "Press E to open inventory";
        
        public Player(int x, int y)
        {
            X = x;
            Y = y;
            Health = MaxHealth;
        }
        
        public void Draw()
        {
            Console.WriteLine($"{Sprite} Player at ({X}, {Y})");
        }
        
        public void Move(int deltaX, int deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }
        
        public void TakeDamage(int amount)
        {
            Health -= amount;
            Console.WriteLine($"Player took {amount} damage. Health: {Health}/{MaxHealth}");
        }
        
        public bool IsAlive() => Health > 0;
        
        public void Interact()
        {
            Console.WriteLine(InteractionText);
        }
    }
    
    class Enemy : IDrawable, IMovable, IDamageable
    {
        public string Sprite => "👾";
        
        public int X { get; set; }
        public int Y { get; set; }
        
        private int health;
        public int Health 
        { 
            get => health;
            private set => health = Math.Max(0, value);
        }
        public int MaxHealth { get; } = 50;
        
        public Enemy(int x, int y)
        {
            X = x;
            Y = y;
            Health = MaxHealth;
        }
        
        public void Draw()
        {
            Console.WriteLine($"{Sprite} Enemy at ({X}, {Y})");
        }
        
        public void Move(int deltaX, int deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }
        
        public void TakeDamage(int amount)
        {
            Health -= amount;
            Console.WriteLine($"Enemy took {amount} damage. Health: {Health}/{MaxHealth}");
        }
        
        public bool IsAlive() => Health > 0;
    }
    
    class Chest : IDrawable, IInteractable
    {
        public string Sprite => "📦";
        public int X { get; set; }
        public int Y { get; set; }
        
        public string InteractionText => "Press E to open chest";
        
        public Chest(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public void Draw()
        {
            Console.WriteLine($"{Sprite} Chest at ({X}, {Y})");
        }
        
        public void Interact()
        {
            Console.WriteLine(InteractionText);
        }
    }
    
    class Wall : IDrawable
    {
        public string Sprite => "🧱";
        public int X { get; set; }
        public int Y { get; set; }
        
        public Wall(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public void Draw()
        {
            Console.WriteLine($"{Sprite} Wall at ({X}, {Y})");
        }
    }
    
    class Program
    {
        static void RenderEntity(IDrawable entity)
        {
            entity.Draw();
        }
        
        static void MoveEntity(IMovable entity, int dx, int dy)
        {
            entity.Move(dx, dy);
            Console.WriteLine($"{entity.GetType().Name} moved to ({entity.X}, {entity.Y})");
        }
        
        static void AttackEntity(IDamageable entity, int damage)
        {
            entity.TakeDamage(damage);
            if (!entity.IsAlive())
            {
                Console.WriteLine("Game Over: Enemy defeated!");
            }
        }
        
        static void UseEntity(IInteractable entity)
        {
            entity.Interact();
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Game Entity System");
            Console.WriteLine("==================\n");
            
            Console.WriteLine("Creating entities:");
            Player player = new Player(5, 5);
            Console.WriteLine($"✓ Player created at ({player.X}, {player.Y})");
            
            Enemy enemy = new Enemy(10, 8);
            Console.WriteLine($"✓ Enemy created at ({enemy.X}, {enemy.Y})");
            
            Chest chest = new Chest(3, 3);
            Console.WriteLine($"✓ Chest created at ({chest.X}, {chest.Y})");
            
            Wall wall = new Wall(7, 7);
            Console.WriteLine($"✓ Wall created at ({wall.X}, {wall.Y})");
            
            Console.WriteLine("\nRendering all drawable entities:");
            List<IDrawable> drawables = new List<IDrawable> { player, enemy, chest, wall };
            foreach (var drawable in drawables)
            {
                RenderEntity(drawable);
            }
            
            Console.WriteLine("\nMoving movable entities:");
            MoveEntity(player, 1, 0);
            MoveEntity(enemy, 1, 1);
            
            Console.WriteLine("\nAttacking damageable entities:");
            AttackEntity(player, 10);
            AttackEntity(enemy, 25);
            
            Console.WriteLine("\nInteracting with interactive entities:");
            UseEntity(player);
            UseEntity(chest);
        }
    }
}