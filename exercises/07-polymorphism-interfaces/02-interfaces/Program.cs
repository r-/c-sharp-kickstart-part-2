using System;
using System.Collections.Generic;

namespace Part2.Ch07.Ex02
{
    // TODO: Define the IDataStore interface
    // - Properties: StorageName (string), ItemCount (int)
    // - Methods: Save, Load, Delete, Clear
    
    
    // TODO: Implement MemoryStore class that implements IDataStore
    // - Use Dictionary<string, string> for storage
    // - StorageName: "Memory Storage"
    // - Implement all interface methods
    
    
    // TODO: Implement FileStore class that implements IDataStore
    // - Use Dictionary<string, string> for storage
    // - StorageName: "File Storage"
    // - Add Console output for file operations
    // - Implement all interface methods
    
    
    // TODO: Implement DatabaseStore class that implements IDataStore
    // - Use Dictionary<string, string> for storage
    // - StorageName: "Database Storage"
    // - Add Console output for database operations
    // - Implement all interface methods
    
    
    class Program
    {
        // TODO: Create TestStorage method that accepts IDataStore parameter
        // - Display storage name
        // - Save three items
        // - Load and display one item
        // - Show item count
        // - Delete one item
        // - Show updated count
        
        
        static void Main(string[] args)
        {
            Console.WriteLine("Testing Storage Systems");
            Console.WriteLine("=======================");
            
            // TODO: Create instances of each storage type
            
            
            // TODO: Test each storage type using TestStorage method
            
        }
    }
}