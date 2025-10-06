using System;
using System.Collections.Generic;

namespace Part2.Ch07.Ex03
{
    // TODO: Define IDrawable interface
    // - Property: Sprite (string)
    // - Method: Draw()
    
    
    // TODO: Define IMovable interface
    // - Properties: X (int), Y (int)
    // - Method: Move(int deltaX, int deltaY)
    
    
    // TODO: Define IDamageable interface
    // - Properties: Health (int), MaxHealth (int)
    // - Method: TakeDamage(int amount)
    // - Method: IsAlive() returning bool
    
    
    // TODO: Define IInteractable interface
    // - Property: InteractionText (string)
    // - Method: Interact()
    
    
    // TODO: Create Player class implementing ALL four interfaces
    // - Health starts at MaxHealth (100)
    // - Sprite: "🧑"
    // - InteractionText: "Press E to open inventory"
    
    
    // TODO: Create Enemy class implementing IDrawable, IMovable, IDamageable
    // - Health starts at MaxHealth (50)
    // - Sprite: "👾"
    
    
    // TODO: Create Chest class implementing IDrawable, IInteractable
    // - Sprite: "📦"
    // - InteractionText: "Press E to open chest"
    
    
    // TODO: Create Wall class implementing only IDrawable
    // - Sprite: "🧱"
    
    
    class Program
    {
        // TODO: Create RenderEntity method accepting IDrawable parameter
        
        
        // TODO: Create MoveEntity method accepting IMovable parameter
        
        
        // TODO: Create AttackEntity method accepting IDamageable parameter
        
        
        // TODO: Create UseEntity method accepting IInteractable parameter
        
        
        static void Main(string[] args)
        {
            Console.WriteLine("Game Entity System");
            Console.WriteLine("==================\n");
            
            // TODO: Create one of each entity type
            
            
            // TODO: Test rendering all entities
            
            
            // TODO: Test moving movable entities
            
            
            // TODO: Test attacking damageable entities
            
            
            // TODO: Test interacting with interactable entities
            
        }
    }
}