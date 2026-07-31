Excellent. The **Flyweight Pattern** is probably the most misunderstood Structural Pattern because its goal is very specific:

> **Reduce memory usage by sharing common state among many objects.**

Unlike Adapter, Bridge, or Decorator, Flyweight is primarily an **optimization pattern**.

***

# Flyweight Pattern

## Definition

The Flyweight Pattern minimizes memory usage by sharing as much data as possible between similar objects.

Instead of storing duplicate data in every object, shared data is kept in a central location and reused.

***

# Real-World Example

Think of a text editor displaying:

```text
HELLO WORLD
```

Without Flyweight:

```text
H Object
E Object
L Object
L Object
O Object
...
```

Each character stores:

* Font
* Font Size
* Color
* Character

Result: lots of duplicate memory.

***

With Flyweight:

```text
Shared Character 'H'
Shared Character 'E'
Shared Character 'L'
```

Font information can be shared.

Only position information varies.

Memory consumption drops significantly.

***

# Key Concept

Flyweight separates state into:

### Intrinsic State (Shared)

Stored in Flyweight.

```text
Character
Font
Color
```

Common across many objects.

***

### Extrinsic State (Unique)

Passed from the client.

```text
X Position
Y Position
```

Changes per usage.

***

# Problem Statement

Suppose a game contains:

```text
10,000 Trees
```

Without Flyweight:

```csharp
new Tree("Oak", "Green", "OakTexture");
new Tree("Oak", "Green", "OakTexture");
new Tree("Oak", "Green", "OakTexture");
...
```

Thousands of duplicate copies of the same data.

Huge memory waste.

***

# Solution

Store shared tree information once.

```csharp
TreeType
```

and reuse it.

Each tree keeps only:

```csharp
X Position
Y Position
```

***

# Console Application Example

## Step 1: Flyweight

Shared state.

### TreeType.cs

```csharp
using System;

namespace FlyweightPatternDemo
{
    public class TreeType
    {
        public string Name { get; }
        public string Color { get; }

        public TreeType(
            string name,
            string color)
        {
            Name = name;
            Color = color;
        }

        public void Display(int x, int y)
        {
            Console.WriteLine(
                $"Tree: {Name}, Color: {Color}, Location: ({x},{y})");
        }
    }
}
```

***

## Step 2: Flyweight Factory

Caches shared objects.

### TreeFactory.cs

```csharp
using System.Collections.Generic;

namespace FlyweightPatternDemo
{
    public class TreeFactory
    {
        private readonly Dictionary<string, TreeType>
            _treeTypes = new();

        public TreeType GetTreeType(
            string name,
            string color)
        {
            string key = $"{name}_{color}";

            if (!_treeTypes.ContainsKey(key))
            {
                _treeTypes[key] =
                    new TreeType(name, color);
            }

            return _treeTypes[key];
        }
    }
}
```

***

## Step 3: Context Object

Stores unique state.

### Tree.cs

```csharp
namespace FlyweightPatternDemo
{
    public class Tree
    {
        private readonly int _x;
        private readonly int _y;

        private readonly TreeType _treeType;

        public Tree(
            int x,
            int y,
            TreeType treeType)
        {
            _x = x;
            _y = y;
            _treeType = treeType;
        }

        public void Display()
        {
            _treeType.Display(_x, _y);
        }
    }
}
```

***

## Step 4: Client

### Program.cs

```csharp
using System;
using System.Collections.Generic;

namespace FlyweightPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            TreeFactory factory =
                new TreeFactory();

            List<Tree> forest = new();

            for (int i = 0; i < 5; i++)
            {
                TreeType oakType =
                    factory.GetTreeType(
                        "Oak",
                        "Green");

                forest.Add(
                    new Tree(i * 10, i * 5, oakType));
            }

            foreach (var tree in forest)
            {
                tree.Display();
            }

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Tree: Oak, Color: Green, Location: (0,0)
Tree: Oak, Color: Green, Location: (10,5)
Tree: Oak, Color: Green, Location: (20,10)
Tree: Oak, Color: Green, Location: (30,15)
Tree: Oak, Color: Green, Location: (40,20)
```

***

# What's Being Shared?

Without Flyweight:

```text
Tree 1
  Oak
  Green

Tree 2
  Oak
  Green

Tree 3
  Oak
  Green
```

Every tree stores identical data.

***

With Flyweight:

```text
One Oak TreeType
       ↑
       ↑
       ↑
Tree1 Tree2 Tree3
```

All trees point to the same shared object.

***

# Visual Representation

```text
                 TreeType
             (Oak, Green)
                 ▲
        ┌────────┼────────┐
        │        │        │
      Tree1    Tree2    Tree3
     x,y      x,y      x,y
```

Shared:

```text
Oak
Green
```

Unique:

```text
Coordinates
```

***

# Real-World Example: Text Editor

Suppose a document contains:

```text
100,000 letters
```

Shared state:

```text
Font
Font Size
Color
Character Shape
```

Unique state:

```text
Position
Line Number
```

The editor shares font data instead of duplicating it for every character.

***

# Another Example: Game Development

Thousands of:

```text
Trees
Rocks
Enemies
Buildings
```

Shared:

```text
Texture
Model
Animation Data
```

Unique:

```text
Position
Health
Direction
```

This is one of the most common real-world uses of Flyweight.

***

# UML (Simplified)

```text
                  Flyweight
                  TreeType
                       ▲
                       |
                Shared Object

                       ▲
                       |
                     Tree
             (Extrinsic State)
```

***

# Flyweight vs Singleton

A common confusion.

### Singleton

```csharp
Logger.Instance
```

One shared instance for the entire application.

***

### Flyweight

```csharp
Oak TreeType
Pine TreeType
Mango TreeType
```

Many shared instances.

Each instance represents shared data for a group of objects.

***

# Flyweight vs Prototype

### Prototype

Creates objects by cloning.

```csharp
var clone = original.Clone();
```

Focus:

✅ Creation

***

### Flyweight

Shares existing instances.

```csharp
factory.GetTreeType("Oak");
```

Focus:

✅ Memory optimization

***

# Flyweight vs Cache

Flyweight often looks like a cache.

Example:

```csharp
Dictionary<string, TreeType>
```

Difference:

A Flyweight's purpose is specifically to share intrinsic state among many logical objects.

A cache's purpose is generally performance and retrieval speed.

***

# Advantages

✅ Reduces memory consumption

✅ Improves scalability

✅ Reuses common objects

✅ Helpful when working with thousands of similar objects

✅ Better performance for large object sets

***

# Disadvantages

❌ Increased complexity

❌ Requires separating intrinsic and extrinsic state

❌ May not help for small datasets

❌ Can make object design harder to understand

***

# When to Use

Use Flyweight when:

* Large numbers of similar objects exist.
* Memory consumption is a concern.
* A significant portion of object state can be shared.

Examples:

* Character rendering
* Text editors
* UI controls
* Map markers
* Game objects
* Icons and images
* Product catalogs

***

# Interview Question

### Q: What are Intrinsic and Extrinsic State?

**Answer:**

### Intrinsic State

Shared and stored inside the Flyweight.

```csharp
Name = "Oak";
Color = "Green";
```

***

### Extrinsic State

Unique and supplied by the client.

```csharp
x = 100;
y = 200;
```

The Flyweight pattern works by sharing intrinsic state while keeping extrinsic state separate.

***

# Flyweight Pattern in One Line

> **Share common state between many objects to reduce memory usage.**

✅ **Structural Pattern #6 Complete: Flyweight Pattern**

### Structural Patterns Progress

1. ✅ Adapter
2. ✅ Bridge
3. ✅ Composite
4. ✅ Decorator
5. ✅ Facade
6. ✅ Flyweight
7. ⏭️ Proxy

**Next Pattern: Proxy Pattern**, the final Structural Pattern, which provides a placeholder or surrogate object to control access to another object.
