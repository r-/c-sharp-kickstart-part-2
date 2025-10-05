# Mini Project: Build YOUR Game Scoreboard

## Project Overview

Create YOUR own game scoreboard system that tracks players and their scores! This is YOUR first complete object-oriented program - while there are minimum requirements, you're encouraged to add your own creative features and design YOUR scoreboard system YOUR way.

## Why This Project?

This project lets you:
- Apply everything from Chapter 3 in one complete program
- See the power of classes for managing multiple similar entities
- Express your creativity in designing game mechanics
- Build something you can demonstrate and be proud of
- Practice making design decisions about how objects interact

## Learning Goals

- Create and use classes with fields, methods, and constructors
- Manage multiple objects using collections (List)
- Design object interactions and game logic
- Make independent decisions about program structure and features

## Minimum Requirements

Your scoreboard system MUST include these core features:

### 1. Player Class

Create a `Player` class with:
- Fields for player name and score
- Constructor to initialize players
- Method to add points
- Method to display player information

### 2. Multiple Players

Your program must:
- Create and track at least 3-4 players
- Store players in a `List<Player>`
- Allow updating individual player scores
- Display all players and their scores

### 3. Game Logic

Implement:
- Some way to add points to players (manual or automatic)
- Clear scoreboard display showing all players
- Identification of the winner (highest score)
- Clean program structure with organized methods

### 4. User Experience

Your program should:
- Have clear output messages
- Display formatted scoreboard
- Show winner announcement
- Run without crashes

## Expected Output Example

```
=== Game Scoreboard ===

Alex: 45 points
Riley: 60 points  
Jordan: 30 points
Sam: 55 points

=== Winner ===
🏆 Riley wins with 60 points!
```

## Implementation Steps

1. **Start with Player Class** - Get one player working first
2. **Add Multiple Players** - Create a List and add several players
3. **Basic Scoring** - Implement simple point addition
4. **Display Logic** - Make the scoreboard look good
5. **Game Mechanics** - Add YOUR unique game features
6. **Polish** - Clean up code, add comments, test thoroughly

## Hints

### Creating Your Player Class
```csharp
class Player
{
    public string Name;
    public int Score;
    
    public Player(string name)
    {
        Name = name;
        Score = 0;
    }
    
    public void AddPoints(int points)
    {
        Score += points;
    }
}
```

### Managing Multiple Players
```csharp
List<Player> players = new List<Player>();
players.Add(new Player("Alex"));
players.Add(new Player("Riley"));

// Add points
players[0].AddPoints(10);
players[1].AddPoints(15);

// Display all
foreach (Player player in players)
{
    Console.WriteLine($"{player.Name}: {player.Score}");
}
```

### Finding the Winner
```csharp
Player winner = players[0];
foreach (Player player in players)
{
    if (player.Score > winner.Score)
    {
        winner = player;
    }
}
```

## Make It YOUR Own!

After meeting minimum requirements, add YOUR creative features! Here are ideas:

### Feature Ideas

- 🎮 **Game Rounds**: Implement multiple rounds with point accumulation
- 🎲 **Random Events**: Random bonus points or penalties
- 👥 **Teams**: Organize players into teams with team scores
- 📊 **Statistics**: Track average score, highest round, win streaks
- 💫 **Power-Ups**: Special abilities or multipliers
- 🏆 **Rankings**: Display players sorted by score (leaderboard)
- 🎯 **Achievements**: Award badges for milestones
- ⚡ **Speed Rounds**: Time-based scoring bonus
- 🌈 **Color Coding**: Use console colors for different score ranges
- 💾 **Save System**: Write scores to file and load them back
- 🎪 **Different Games**: Create multiple game modes

### Design Decisions YOU Make

Think about:
- How do players earn points? (automatic, user input, random?)
- What makes your scoreboard unique?
- How should the winner be announced?
- What additional player information could you track?
- How can you make the display visually appealing?
- Should there be penalties or special events?

**Remember**: There's no single "correct" scoreboard. YOUR creativity and problem-solving approach are what matter!

## Self-Assessment Checklist

Test your scoreboard thoroughly:

### Core Functionality
- [ ] Player class exists with required fields and methods
- [ ] Multiple players can be created and tracked
- [ ] Points can be added to individual players
- [ ] Scoreboard displays all players correctly
- [ ] Winner is identified and announced
- [ ] Program runs without crashes

### Code Quality
- [ ] Code is organized into logical methods
- [ ] Player class is well-structured
- [ ] Variable names are clear and descriptive
- [ ] Comments explain complex logic
- [ ] No unnecessary code repetition

### YOUR Additions
- [ ] Added at least one unique feature
- [ ] Tested custom features thoroughly
- [ ] Code is clean and understandable
- [ ] Program is engaging to use

## What Makes a Great Project?

Your project will be evaluated on:

- **Meets Requirements** (40%): All core features work correctly
- **Code Quality** (30%): Clean Player class, organized structure, good naming
- **Functionality** (20%): Works reliably, handles edge cases
- **Creativity** (10%): YOUR unique features and improvements

## Common Pitfalls to Avoid

**Problem**: Creating player but not adding to list
```csharp
// ❌ Wrong - player not in list!
Player player1 = new Player("Alex");
// player1 is lost!

// ✅ Correct - add to list
players.Add(new Player("Alex"));
```

**Problem**: Forgetting to initialize Score in constructor
```csharp
// ❌ Score might be undefined
public Player(string name)
{
    Name = name;
    // Score not initialized!
}

// ✅ Set default value
public Player(string name)
{
    Name = name;
    Score = 0;
}
```

## Showcase YOUR Work!

When you're done:
- Be prepared to demonstrate YOUR scoreboard
- Explain YOUR design decisions
- Share what unique features YOU added
- Discuss challenges YOU overcame

**This is YOUR project** - make it something you're proud to show off!

## Resources

- Review Chapter 3 concepts if needed
- Look at exercises 01-03 for Player class examples
- Remember: classes let you create unlimited objects easily

## Extension Ideas for Advanced Students

**Level 1: Enhanced Players**
- Add player avatar/icon
- Track games played per player
- Calculate average score per game

**Level 2: Tournament Mode**
- Bracket-style elimination
- Best of 3/5 matches
- Championship rounds

**Level 3: Persistence**
- Save players to file
- Load previous game data
- High score history

**Level 4: Interactive Mode**
- Let user choose which player gets points
- Manual score entry
- Player vs Player mode

---

**Good luck!** Remember to test thoroughly, handle user inputs gracefully, and most importantly - make it YOURS!