using System;
using System.Collections.Generic;
using System.Linq;

namespace Part2.Ch07.Project
{
    // TODO: Define IPlugin interface
    // - Properties: Name, Version, Description
    // - Methods: Initialize(), Execute(), Shutdown()
    
    
    // TODO: Create LoggerPlugin implementing IPlugin
    // - Track log messages and count
    // - Support different log levels (Info, Warning, Error)
    // - Display logs with timestamps
    
    
    // TODO: Create CalculatorPlugin implementing IPlugin
    // - Perform basic math operations
    // - Track calculation history
    // - Handle user input for operations
    
    
    // TODO: Create GreeterPlugin implementing IPlugin
    // - Display personalized greetings
    // - Show random motivational quotes
    // - Track greeting count
    
    
    // TODO: Create PluginManager class
    // - Store plugins in List<IPlugin>
    // - Methods: RegisterPlugin, ExecuteAll, ExecuteByName, ListPlugins, UnloadAll
    // - Property: PluginCount
    
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Plugin System v1.0");
            Console.WriteLine("==================\n");
            
            // TODO: Create PluginManager instance
            
            
            // TODO: Register plugins
            
            
            // TODO: Create menu loop
            // 1. List all plugins
            // 2. Execute all plugins
            // 3. Execute specific plugin
            // 4. Plugin information
            // 5. Shutdown system
            
        }
    }
}