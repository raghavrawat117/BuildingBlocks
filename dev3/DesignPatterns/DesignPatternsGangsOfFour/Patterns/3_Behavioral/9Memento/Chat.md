Excellent. The **Memento Pattern** is the pattern behind one of the most common application features:

* Undo / Redo
* Checkpoints in games
* Draft recovery
* Save/Load functionality
* Transaction rollback

> **Key idea:**
>
> Capture an object's state at a point in time and restore it later without exposing its internal details.

***

# Memento Pattern

## Definition

The Memento Pattern allows an object to save its internal state into a separate object (the Memento) so that the state can be restored later.

***

# Real-World Analogy

Think of a text editor.

You type:

```text
Hello
```

Save state.

Then edit:

```text
Hello World
```

Save state.

Then edit:

```text
Hello World !!!
```

If you press **Undo**, the editor restores the previous state.

The editor doesn't expose all its internal fields to the undo system.

***

# Problem Statement

Suppose we have a document:

```csharp
document.Content = "Version 1";
document.Content = "Version 2";
document.Content = "Version 3";
```

How can we return to:

```text
Version 1
```

or

```text
Version 2
```

without exposing the internals of the `Document` class?

***

# Solution

Store snapshots of the object's state.

```text
Document
   |
   +-- CreateMemento()
   |
   +-- Restore(Memento)
```

***

# Structure

```text
       Originator
         Document
             |
             ▼
          Memento

             ▲
             |
         Caretaker
```

***

# Participants

### Originator

The object whose state must be saved.

```csharp
Document
```

### Memento

Stores the snapshot.

```csharp
DocumentMemento
```

### Caretaker

Manages history.

```csharp
DocumentHistory
```

***

# Console Application Example

## Step 1: Memento

### DocumentMemento.cs

```csharp
namespace MementoPatternDemo
{
    public class DocumentMemento
    {
        public string Content { get; }

        public DocumentMemento(string content)
        {
            Content = content;
        }
    }
}
```

***

## Step 2: Originator

### Document.cs

```csharp
namespace MementoPatternDemo
{
    public class Document
    {
        public string Content { get; set; }

        public DocumentMemento Save()
        {
            return new DocumentMemento(Content);
        }

        public void Restore(DocumentMemento memento)
        {
            Content = memento.Content;
        }
    }
}
```

***

## Step 3: Caretaker

### DocumentHistory.cs

```csharp
using System.Collections.Generic;

namespace MementoPatternDemo
{
    public class DocumentHistory
    {
        private readonly Stack<DocumentMemento> _history =
            new();

        public void Save(DocumentMemento memento)
        {
            _history.Push(memento);
        }

        public DocumentMemento Undo()
        {
            return _history.Pop();
        }
    }
}
```

***

## Step 4: Client

### Program.cs

```csharp
using System;

namespace MementoPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Document document =
                new Document();

            DocumentHistory history =
                new DocumentHistory();

            document.Content = "Version 1";
            history.Save(document.Save());

            document.Content = "Version 2";
            history.Save(document.Save());

            document.Content = "Version 3";

            Console.WriteLine(
                $"Current: {document.Content}");

            document.Restore(history.Undo());

            Console.WriteLine(
                $"Undo 1 : {document.Content}");

            document.Restore(history.Undo());

            Console.WriteLine(
                $"Undo 2 : {document.Content}");

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Current: Version 3

Undo 1 : Version 2

Undo 2 : Version 1
```

***

# How It Works

When:

```csharp
history.Save(document.Save());
```

executes:

```text
Document
   ↓
Create Snapshot
   ↓
Store in History
```

History becomes:

```text
Version 1
Version 2
```

Later:

```csharp
document.Restore(history.Undo());
```

restores the last snapshot.

***

# Visual Representation

```text
Current State

Version 3

History Stack

Top
 ↓
Version 2
Version 1
```

Undo:

```text
Restore Version 2
```

Undo again:

```text
Restore Version 1
```

***

# More Realistic Example: Bank Account

## Originator

```csharp
public class BankAccount
{
    public decimal Balance { get; set; }

    public AccountMemento Save()
    {
        return new AccountMemento(Balance);
    }

    public void Restore(AccountMemento memento)
    {
        Balance = memento.Balance;
    }
}
```

***

## Memento

```csharp
public class AccountMemento
{
    public decimal Balance { get; }

    public AccountMemento(decimal balance)
    {
        Balance = balance;
    }
}
```

***

## Client

```csharp
account.Balance = 5000;

history.Push(account.Save());

account.Balance = 10000;

account.Restore(history.Pop());
```

Output:

```text
Balance = 5000
```

Useful for rollback scenarios.

***

# Undo/Redo Enhancement

A proper Undo/Redo system usually uses two stacks.

```text
Undo Stack
Redo Stack
```

***

## Example Flow

```text
Version 1
Version 2
Version 3
```

Undo:

```text
Version 3 -> Redo Stack
Restore Version 2
```

Undo:

```text
Version 2 -> Redo Stack
Restore Version 1
```

Redo:

```text
Restore Version 2
```

Many text editors use this approach.

***

# Real-World Example: Game Save Points

Player state:

```text
Health = 80
Level = 5
Position = (100,50)
```

Save checkpoint:

```csharp
var checkpoint = player.Save();
```

Player dies.

Restore checkpoint:

```csharp
player.Restore(checkpoint);
```

State returns exactly to the saved point.

***

# UML (Simplified)

```text
            Document
          (Originator)
                |
                ▼
        DocumentMemento

                ▲
                |
        DocumentHistory
          (Caretaker)
```

***

# Real-World .NET Examples

### Text Editors

```text
Undo
Redo
```

### Design Tools

```text
Visual Studio Designer
Photoshop
Figma
```

### Game Systems

```text
Save Game
Checkpoint
Load Game
```

### Workflow Rollback

```text
Restore previous process state
```

***

# Memento vs Prototype

A very common interview question.

## Prototype

Creates a copy of an object.

```csharp
var clone = order.Clone();
```

Focus:

✅ Object creation

***

## Memento

Stores state for later restoration.

```csharp
var snapshot = order.Save();
```

Focus:

✅ State recovery

***

# Memento vs Command

## Command

Stores an action.

```csharp
DepositCommand
DeleteOrderCommand
```

Focus:

✅ What happened

***

## Memento

Stores object state.

```csharp
DocumentMemento
```

Focus:

✅ What the object looked like

***

# Memento vs State

## State

Changes behavior based on current state.

```text
Draft
Published
Archived
```

***

## Memento

Stores historical state.

```text
Version 1
Version 2
Version 3
```

***

# Advantages

✅ Supports Undo/Redo

✅ Preserves encapsulation

✅ Simplifies rollback implementations

✅ Separates state history from business logic

✅ Easy checkpoint creation

***

# Disadvantages

❌ Large objects consume more memory

❌ Many snapshots can become expensive

❌ Caretaker may grow significantly

❌ Complex object graphs can make snapshots difficult

***

# When to Use

Use Memento when:

* Undo/Redo is required.
* State rollback is required.
* Checkpoints are needed.
* Object history should be preserved.
* Internal state must remain encapsulated.

Examples:

* Text editors
* Banking systems
* Games
* Workflow engines
* Form drafts
* Transaction rollback

***

# Interview Question

### Q: What problem does the Memento Pattern solve?

**Answer:**

The Memento Pattern allows an object's state to be captured and restored later without exposing its internal implementation.

Instead of manually tracking changes:

```csharp
oldValue = document.Content;
```

you create snapshots:

```csharp
var snapshot = document.Save();
```

and restore them when needed:

```csharp
document.Restore(snapshot);
```

***

# Memento Pattern in One Line

> **Capture and externalize an object's state so it can be restored later without violating encapsulation.**

✅ **Behavioral Pattern #9 Complete: Memento Pattern**

### Behavioral Patterns Progress

1. ✅ Chain of Responsibility
2. ✅ Observer
3. ✅ Strategy
4. ✅ Command
5. ✅ State
6. ✅ Template Method
7. ✅ Mediator
8. ✅ Iterator
9. ✅ Memento
10. ⏭️ Visitor
11. ⏭️ Interpreter

**Next Pattern: Visitor Pattern**, which is designed to add new operations to existing object structures without modifying those classes.
