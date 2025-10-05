# 🐙 Using GitHub to Save and Sync Your Work

As you work through the C# Kickstart Part 2 exercises, you'll want to save your progress and access it from different computers. This guide shows you how to use GitHub to keep your work synchronized across all your devices.

## 🤔 Why Use GitHub for Your Exercises?

- **Save your progress**: Never lose your work again
- **Work anywhere**: Access your exercises from home, school, or library computers
- **Track your learning**: See how your coding skills improve over time
- **Build your portfolio**: Show potential employers your learning journey

## 📋 Prerequisites

- A GitHub account (free at [github.com](https://github.com))
- Git installed on your computer
- Basic familiarity with terminal/command prompt

## 🍴 Step 1: Fork the Repository

**Forking** creates your own copy of the C# Kickstart Part 2 repository on your GitHub account.

1. **Go to the original repository**: https://github.com/r-/c-sharp-kickstart-part-2
2. **Click the "Fork" button** in the top-right corner
3. **Select your account** as the destination
4. **Wait for GitHub** to create your personal copy

✅ **Result**: You now have `https://github.com/YOUR-USERNAME/c-sharp-kickstart-part-2`

## 💻 Step 2: Clone Your Fork

Now download your personal copy to your computer:

```bash
# Navigate to where you want the project
cd Desktop

# Clone YOUR fork (replace YOUR-USERNAME)
git clone https://github.com/YOUR-USERNAME/c-sharp-kickstart-part-2.git

# Enter the project folder
cd c-sharp-kickstart-part-2

# Build the solution
dotnet build
```

## 📝 Step 3: Work on Exercises

Work on exercises normally:

1. **Read** the exercise instructions
2. **Edit** the `Program.cs` files
3. **Test** your solutions with `dotnet test`
4. **Continue** learning!

## 💾 Step 4: Save Your Progress

When you've completed some exercises and want to save your work:

### Check What You've Changed
```bash
git status
```
This shows which files you've modified.

### Save Your Changes Locally
```bash
# Add all your changes
git add .

# Commit with a descriptive message
git commit -m "Complete Chapter 3 exercises"
```

### Upload to GitHub
```bash
# Push your changes to your GitHub fork
git push origin master
```

✅ **Your work is now saved on GitHub!**

## 🔄 Step 5: Sync Across Computers

### On a New Computer

1. **Clone your fork** (same as Step 2):
   ```bash
   git clone https://github.com/YOUR-USERNAME/c-sharp-kickstart-part-2.git
   cd c-sharp-kickstart-part-2
   dotnet build
   ```

2. **Start working** on exercises

### On a Computer You've Used Before

**Before starting work**, get your latest changes:
```bash
# Navigate to your project folder
cd c-sharp-kickstart-part-2

# Download your latest changes from GitHub
git pull origin master
```

**After working**, save your progress:
```bash
# Save and upload your new work
git add .
git commit -m "Complete functions exercises"
git push origin master
```

## 🔄 Step 6: Getting Updates from the Original Course

Sometimes the course instructors might add new exercises or fix issues. Here's how to get those updates:

### One-Time Setup
```bash
# Add the original repository as a remote
git remote add upstream https://github.com/r-/c-sharp-kickstart-part-2.git
```

### Getting Updates
```bash
# Download updates from the original course
git fetch upstream

# Merge updates into your work
git merge upstream/master

# Upload the updates to your fork
git push origin master
```

## 🚨 Common Issues and Solutions

### "Permission denied" when pushing
- Make sure you're pushing to YOUR fork, not the original repository
- Check that you're logged into GitHub in your terminal

### Merge conflicts
- This happens when you and the instructors changed the same file
- Git will mark the conflicts - choose which version to keep
- Ask for help if you're unsure!

### Lost work
- If you committed your work (`git commit`), it's saved locally
- If you pushed (`git push`), it's saved on GitHub
- Use `git log` to see your commit history

## 📚 Quick Reference

| Action | Command |
|--------|---------|
| Check status | `git status` |
| Save changes locally | `git add . && git commit -m "message"` |
| Upload to GitHub | `git push origin master` |
| Download from GitHub | `git pull origin master` |
| Get course updates | `git fetch upstream && git merge upstream/master` |

## 🎯 Best Practices

1. **Commit often**: Save your work after completing each exercise
2. **Use clear messages**: Write commit messages that explain what you did
3. **Push regularly**: Upload your work to GitHub at least once per session
4. **Pull before working**: Always get your latest changes when switching computers

## 🆘 Need Help?

- **Git is confusing**: That's normal! It takes practice
- **Made a mistake**: Most Git mistakes can be undone
- **Ask for help**: Your instructor or classmates can assist
- **Online resources**: GitHub has excellent documentation

## 🎉 Congratulations!

You're now using the same tools that professional developers use every day. This workflow will serve you well throughout your programming journey!

---

**Remember**: The goal is to learn programming, not become a Git expert overnight. Start with the basics and gradually build your confidence with version control.