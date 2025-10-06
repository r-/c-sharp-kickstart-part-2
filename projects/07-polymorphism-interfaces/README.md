# Mini Project: Build YOUR Plugin System

## Project Overview

Create YOUR own extensible plugin system using interfaces and polymorphism! This is YOUR project to design, implement, and customize. You'll build a system where new functionality can be added without modifying existing code—the essence of professional software architecture.

## Why This Project?

Real-world applications use plugin architectures to enable extensibility:
- Visual Studio Code extensions
- WordPress plugins
- Browser extensions
- Game mods

This project teaches you the Open/Closed Principle: software should be open for extension but closed for modification.

## Learning Goals

- Design with interfaces for maximum flexibility
- Implement plugin discovery and registration
- Use polymorphism to treat plugins uniformly
- Build systems that extend without breaking
- Apply professional architectural patterns
- Practice the SOLID principles

## Minimum Requirements

YOUR plugin system MUST include:

### 1. Core Plugin Interface

Define `IPlugin` interface with:
- `string Name { get; }` - Plugin identifier
- `string Version { get; }` - Version number
- `string Description { get; }` - What the plugin does
- `void Initialize()` - Setup when plugin loads
- `void Execute()` - Main plugin functionality
- `void Shutdown()` - Cleanup before unload

### 2. At Least Three Plugin Types

Implement these concrete plugins:

**LoggerPlugin**
- Writes log messages to console
- Tracks message count
- Shows timestamps
- Supports different log levels (Info, Warning, Error)

**CalculatorPlugin**
- Performs mathematical operations
- Supports basic operations (+, -, *, /)
- Shows calculation history
- Handles invalid inputs gracefully

**GreeterPlugin**
- Displays welcome messages
- Personalizes greetings
- Shows random motivational quotes
- Tracks greeting count

### 3. PluginManager Class

Create a manager that:
- Stores registered plugins in `List<IPlugin>`
- `void RegisterPlugin(IPlugin plugin)` - adds and initializes
- `void ExecuteAll()` - runs all plugins
- `void ExecuteByName(string name)` - runs specific plugin
- `void ListPlugins()` - displays all registered plugins
- `void UnloadAll()` - shutdowns and removes all plugins
- `int PluginCount { get; }` - number of loaded plugins

### 4. User Interaction

Provide a menu allowing users to:
1. List all plugins
2. Execute all plugins
3. Execute specific plugin by name
4. Add new plugin (if implementing dynamic loading)
5. Remove plugin
6. Exit (with proper shutdown)

## Expected Output

```
Plugin System v1.0
==================

Initializing system...

Registering plugins:
✓ Logger v1.0 initialized
  Description: Advanced logging with multiple levels
✓ Calculator v1.0 initialized
  Description: Mathematical operations and history
✓ Greeter v1.0 initialized
  Description: Personalized greetings and quotes

Plugin system ready!

Menu:
1. List all plugins
2. Execute all plugins
3. Execute specific plugin
4. Plugin information
5. Shutdown system

Choice: 1

Registered Plugins (3):
-----------------------
[1] Logger v1.0 - Advanced logging with multiple levels
[2] Calculator v1.0 - Mathematical operations and history
[3] Greeter v1.0 - Personalized greetings and quotes

Choice: 2

Executing all plugins...

[Logger] Plugin executing...
[Logger] Log level: Info
[Logger] Message: System operational
[Logger] Total logs: 1

[Calculator] Plugin executing...
[Calculator] Ready for calculations
[Calculator] 5 + 3 = 8
[Calculator] 10 * 2 = 20
[Calculator] History: 2 calculations

[Greeter] Plugin executing...
[Greeter] Welcome to the plugin system!
[Greeter] Quote: "Code is poetry"
[Greeter] Total greetings: 1

All plugins executed successfully!

Choice: 3

Enter plugin name: Calculator

[Calculator] Plugin executing...
[Calculator] Enter operation (or 'exit'):
> 15 + 7
Result: 22
> 100 / 5
Result: 20
> exit
[Calculator] Session complete. 2 calculations performed.

Choice: 5

Shutting down system...
[Logger] Shutting down. Final log count: 1
[Calculator] Shutting down. Total calculations: 4
[Greeter] Shutting down. Total greetings: 1

System shutdown complete. Goodbye!
```

## Implementation Steps

### Phase 1: Foundation
1. Define `IPlugin` interface
2. Create `PluginManager` class with basic functionality
3. Test with a simple plugin

### Phase 2: Core Plugins
4. Implement `LoggerPlugin` with log levels
5. Implement `CalculatorPlugin` with operations
6. Implement `GreeterPlugin` with quotes

### Phase 3: Management
7. Add plugin registration system
8. Implement execute all/specific functionality
9. Add proper initialization and shutdown

### Phase 4: User Interface
10. Create interactive menu system
11. Add error handling for invalid input
12. Implement graceful shutdown

### Phase 5: Polish
13. Add plugin info display
14. Implement plugin validation
15. Add comprehensive error handling
16. Document YOUR code

## Hints

### IPlugin Interface

```csharp
interface IPlugin
{
    string Name { get; }
    string Version { get; }
    string Description { get; }
    
    void Initialize();
    void Execute();
    void Shutdown();
}
```

### Sample Plugin Implementation

```csharp
class LoggerPlugin : IPlugin
{
    public string Name => "Logger";
    public string Version => "1.0";
    public string Description => "Advanced logging with multiple levels";
    
    private int logCount = 0;
    private List<string> logs = new List<string>();
    
    public void Initialize()
    {
        Console.WriteLine($"[{Name}] Plugin initialized");
        logCount = 0;
        logs.Clear();
    }
    
    public void Execute()
    {
        Console.WriteLine($"\n[{Name}] Plugin executing...");
        
        // Log some messages
        LogMessage("Info", "System operational");
        LogMessage("Warning", "Low memory");
        
        Console.WriteLine($"[{Name}] Total logs: {logCount}");
    }
    
    private void LogMessage(string level, string message)
    {
        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        string logEntry = $"[{timestamp}] [{level}] {message}";
        logs.Add(logEntry);
        logCount++;
        Console.WriteLine($"[{Name}] {logEntry}");
    }
    
    public void Shutdown()
    {
        Console.WriteLine($"[{Name}] Shutting down. Final log count: {logCount}");
        logs.Clear();
    }
}
```

### PluginManager Class

```csharp
class PluginManager
{
    private List<IPlugin> plugins = new List<IPlugin>();
    
    public int PluginCount => plugins.Count;
    
    public void RegisterPlugin(IPlugin plugin)
    {
        if (plugin == null)
        {
            Console.WriteLine("Error: Cannot register null plugin");
            return;
        }
        
        // Check for duplicate names
        if (plugins.Any(p => p.Name == plugin.Name))
        {
            Console.WriteLine($"Error: Plugin '{plugin.Name}' already registered");
            return;
        }
        
        plugin.Initialize();
        plugins.Add(plugin);
        Console.WriteLine($"✓ {plugin.Name} v{plugin.Version} initialized");
        Console.WriteLine($"  Description: {plugin.Description}");
    }
    
    public void ExecuteAll()
    {
        Console.WriteLine("\nExecuting all plugins...\n");
        
        foreach (var plugin in plugins)
        {
            try
            {
                plugin.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing {plugin.Name}: {ex.Message}");
            }
        }
        
        Console.WriteLine("\nAll plugins executed successfully!");
    }
    
    public void ExecuteByName(string name)
    {
        var plugin = plugins.FirstOrDefault(p => 
            p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        
        if (plugin != null)
        {
            try
            {
                plugin.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing {plugin.Name}: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Plugin '{name}' not found");
        }
    }
    
    public void ListPlugins()
    {
        Console.WriteLine($"\nRegistered Plugins ({PluginCount}):");
        Console.WriteLine("-----------------------");
        
        for (int i = 0; i < plugins.Count; i++)
        {
            var plugin = plugins[i];
            Console.WriteLine($"[{i + 1}] {plugin.Name} v{plugin.Version} - {plugin.Description}");
        }
    }
    
    public void UnloadAll()
    {
        Console.WriteLine("\nShutting down system...");
        
        foreach (var plugin in plugins)
        {
            try
            {
                plugin.Shutdown();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error shutting down {plugin.Name}: {ex.Message}");
            }
        }
        
        plugins.Clear();
        Console.WriteLine("System shutdown complete. Goodbye!");
    }
}
```

### Main Menu Loop

```csharp
static void Main(string[] args)
{
    Console.WriteLine("Plugin System v1.0");
    Console.WriteLine("==================\n");
    
    PluginManager manager = new PluginManager();
    
    // Register plugins
    Console.WriteLine("Registering plugins:");
    manager.RegisterPlugin(new LoggerPlugin());
    manager.RegisterPlugin(new CalculatorPlugin());
    manager.RegisterPlugin(new GreeterPlugin());
    
    Console.WriteLine("\nPlugin system ready!\n");
    
    bool running = true;
    while (running)
    {
        Console.WriteLine("\nMenu:");
        Console.WriteLine("1. List all plugins");
        Console.WriteLine("2. Execute all plugins");
        Console.WriteLine("3. Execute specific plugin");
        Console.WriteLine("4. Plugin information");
        Console.WriteLine("5. Shutdown system");
        
        Console.Write("\nChoice: ");
        string choice = Console.ReadLine();
        
        switch (choice)
        {
            case "1":
                manager.ListPlugins();
                break;
                
            case "2":
                manager.ExecuteAll();
                break;
                
            case "3":
                Console.Write("Enter plugin name: ");
                string name = Console.ReadLine();
                manager.ExecuteByName(name);
                break;
                
            case "4":
                manager.ListPlugins();
                break;
                
            case "5":
                manager.UnloadAll();
                running = false;
                break;
                
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}
```

## Make It YOUR Own!

After meeting the requirements, add YOUR creative features:

### Advanced Features
- **Plugin priorities** - Control execution order with priority levels
- **Plugin dependencies** - Plugins can require other plugins
- **Configuration system** - Load plugin settings from JSON/XML files
- **Hot reload** - Add/remove plugins at runtime without restart
- **Plugin marketplace** - Discover and "install" new plugins
- **Event system** - Plugins communicate via publish/subscribe
- **Resource management** - Implement `IDisposable` for cleanup
- **Plugin sandboxing** - Limit what plugins can do
- **Performance monitoring** - Track plugin execution time
- **Plugin categories** - Group plugins by type (Utility, Game, Tool)
- **Command-line arguments** - Load specific plugins at startup
- **Auto-discovery** - Scan directory for plugin assemblies
- **Plugin state persistence** - Save/load plugin state
- **Async plugins** - Support async/await in Execute()
- **Plugin UI** - Plugins can have interactive menus

### Creative Plugin Ideas
- **WeatherPlugin** - Fetch and display weather (simulated)
- **ReminderPlugin** - Set and manage reminders
- **QuizPlugin** - Interactive quiz game
- **MusicPlayerPlugin** - Simulated music player
- **FileManagerPlugin** - Browse and manage files
- **TimerPlugin** - Countdown timer and stopwatch
- **PasswordGeneratorPlugin** - Generate secure passwords
- **UnitConverterPlugin** - Convert between units
- **DiceRollerPlugin** - RPG dice rolling
- **EncryptionPlugin** - Simple text encryption

## Self-Assessment Checklist

- [ ] IPlugin interface clearly defined
- [ ] At least three different plugins implemented
- [ ] PluginManager handles registration correctly
- [ ] Can execute all plugins
- [ ] Can execute specific plugin by name
- [ ] Plugins properly initialized and shutdown
- [ ] Error handling for missing/invalid plugins
- [ ] User-friendly menu system
- [ ] Code follows interface segregation
- [ ] System extensible without modification
- [ ] Clear console output and feedback
- [ ] No crashes on invalid input

## What Makes a Great Project?

**Architecture (35%)**
- Clean interface design
- Proper separation of concerns
- Extensible without modification
- Follows SOLID principles

**Functionality (30%)**
- All required features work
- Plugins demonstrate variety
- Error handling is robust
- Graceful startup and shutdown

**Code Quality (25%)**
- Well-organized and readable
- Consistent naming conventions
- Appropriate use of polymorphism
- Good comments and documentation

**Creativity (10%)**
- YOUR unique plugins
- Innovative features
- Polished user experience
- Goes beyond requirements

## Testing YOUR System

Test these scenarios:
1. ✅ Register multiple plugins successfully
2. ✅ Execute all plugins without errors
3. ✅ Execute specific plugin by name
4. ✅ Handle invalid plugin name gracefully
5. ✅ Proper initialization and shutdown
6. ✅ No duplicate plugin names allowed
7. ✅ Menu navigation works correctly
8. ✅ System handles errors without crashing

## Common Pitfalls to Avoid

- ❌ Not initializing plugins before use
- ❌ Forgetting to call Shutdown() on exit
- ❌ Not handling exceptions in plugin execution
- ❌ Allowing duplicate plugin registration
- ❌ Hard-coding plugin types instead of using interface
- ❌ Missing null checks for plugin references
- ❌ Not providing user feedback for actions
- ❌ Leaving menu loop without cleanup

## Reflection

After completing YOUR project, consider:
1. How does the plugin system demonstrate the Open/Closed Principle?
2. What makes YOUR design extensible?
3. How would you add a new plugin type without modifying existing code?
4. What are the advantages of interface-based design?
5. How could YOU improve the system further?

---

**Congratulations!** You've built a professional-grade plugin system using polymorphism and interfaces. This architecture is used in real-world applications from browsers to game engines to development tools. You've mastered a core software engineering pattern!

---

*© C# Kickstart Part 2 Contributors*