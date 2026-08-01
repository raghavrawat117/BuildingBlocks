Excellent. The **Composite Pattern** is one of the most practical Structural Patterns because it allows us to treat **individual objects and groups of objects in the same way**.

> The key idea:
>
> **A leaf and a collection of leaves should be usable through the same interface.**

***

# Composite Pattern

## Definition

The Composite Pattern composes objects into tree structures and allows clients to treat individual objects and compositions of objects uniformly.

***

# Real-World Example

Think about a file system.

```text
Root
│
├── Documents
│   ├── Resume.docx
│   └── Notes.txt
│
└── Pictures
    ├── Photo1.jpg
    └── Photo2.jpg
```

Here:

* Files are individual objects (Leaf)
* Folders are collections (Composite)

The client can perform the same operation on both:

```csharp
Display()
```

Whether it's a file or a folder.

***

# Problem Statement

Without Composite:

```csharp
if (object is File)
{
    ...
}
else if (object is Folder)
{
    ...
}
```

As the hierarchy grows, the client becomes full of type checks.

***

# Solution Structure

```text
           IFileSystemItem
                 ▲
        ┌────────┴────────┐
        │                 │
      File           Folder
                        │
                Contains Files
                and Folders
```

Both files and folders implement the same interface.

***

# Console Application Example

## Step 1: Component

The common abstraction.

### IFileSystemItem.cs

```csharp
namespace CompositePatternDemo
{
    public interface IFileSystemItem
    {
        void Display(int depth);
    }
}
```

***

## Step 2: Leaf

A file cannot contain other items.

### File.cs

```csharp
using System;

namespace CompositePatternDemo
{
    public class File : IFileSystemItem
    {
        private readonly string _name;

        public File(string name)
        {
            _name = name;
        }

        public void Display(int depth)
        {
            Console.WriteLine(
                new string('-', depth) + _name);
        }
    }
}
```

***

## Step 3: Composite

A folder can contain files and folders.

### Folder.cs

```csharp
using System;
using System.Collections.Generic;

namespace CompositePatternDemo
{
    public class Folder : IFileSystemItem
    {
        private readonly string _name;

        private readonly List<IFileSystemItem> _items =
            new();

        public Folder(string name)
        {
            _name = name;
        }

        public void Add(IFileSystemItem item)
        {
            _items.Add(item);
        }

        public void Remove(IFileSystemItem item)
        {
            _items.Remove(item);
        }

        public void Display(int depth)
        {
            Console.WriteLine(
                new string('-', depth) + _name);

            foreach (var item in _items)
            {
                item.Display(depth + 2);
            }
        }
    }
}
```

***

## Step 4: Client

### Program.cs

```csharp
using System;

namespace CompositePatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Folder root = new Folder("Root");

            Folder documents =
                new Folder("Documents");

            documents.Add(
                new File("Resume.docx"));

            documents.Add(
                new File("Notes.txt"));

            Folder pictures =
                new Folder("Pictures");

            pictures.Add(
                new File("Photo1.jpg"));

            pictures.Add(
                new File("Photo2.jpg"));

            root.Add(documents);
            root.Add(pictures);

            root.Display(1);

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
-Root
---Documents
-----Resume.docx
-----Notes.txt
---Pictures
-----Photo1.jpg
-----Photo2.jpg
```

Notice that:

```csharp
root.Display()
```

invokes the same operation on:

* folders
* files

without special handling.

***

# Recursive Nature of Composite

A composite can contain:

```text
Leaf
```

or

```text
Another Composite
```

Example:

```text
Company
│
├── IT Department
│   ├── Developer 1
│   ├── Developer 2
│
└── HR Department
    ├── HR 1
    └── HR 2
```

A department is itself composed of employees or sub-departments.

***

# Another Realistic Example: Organization Structure

## Component

```csharp
public interface IEmployee
{
    void ShowDetails();
}
```

***

## Leaf

```csharp
public class Developer : IEmployee
{
    private readonly string _name;

    public Developer(string name)
    {
        _name = name;
    }

    public void ShowDetails()
    {
        Console.WriteLine($"Developer: {_name}");
    }
}
```

***

## Composite

```csharp
public class Manager : IEmployee
{
    private readonly string _name;

    private readonly List<IEmployee> _team =
        new();

    public Manager(string name)
    {
        _name = name;
    }

    public void Add(IEmployee employee)
    {
        _team.Add(employee);
    }

    public void ShowDetails()
    {
        Console.WriteLine($"Manager: {_name}");

        foreach (var employee in _team)
        {
            employee.ShowDetails();
        }
    }
}
```

***

## Client

```csharp
Manager teamLead =
    new Manager("Raghav");

teamLead.Add(
    new Developer("Amit"));

teamLead.Add(
    new Developer("Priya"));

teamLead.ShowDetails();
```

Output:

```text
Manager: Raghav
Developer: Amit
Developer: Priya
```

The manager and developer are treated as the same abstraction:

```csharp
IEmployee
```

***

# UML (Simplified)

```text
          IFileSystemItem
                ▲
        ┌───────┴────────┐
        │                │
      File            Folder
                         │
              List<IFileSystemItem>
```

***

# Real-World Examples

### File Systems

```text
Folder
 ├── Folder
 ├── Folder
 └── File
```

### Organization Structures

```text
CEO
 ├── Manager
 ├── Manager
 └── Employee
```

### UI Controls

```text
Window
 ├── Panel
 │    ├── Button
 │    └── TextBox
 └── Menu
```

### Menu Systems

```text
Menu
 ├── Menu Item
 └── Sub Menu
```

***

# Advantages

✅ Treats individual and grouped objects uniformly

✅ Simplifies client code

✅ Supports recursive tree structures

✅ Follows Open/Closed Principle

✅ Easy to add new leaf types

***

# Disadvantages

❌ Can make the design overly generic

❌ Harder to enforce rules about what can be added

❌ Debugging deep trees can be challenging

***

# Composite vs Decorator

A very common interview question.

### Composite

Represents a tree structure.

```text
Folder
 ├── File
 └── File
```

Focus:

✅ Part-whole hierarchy

***

### Decorator

Adds behavior to an object.

```text
Coffee
   ↓
MilkDecorator
   ↓
SugarDecorator
```

Focus:

✅ Additional responsibilities

***

# Composite vs Bridge

### Composite

Models hierarchical structures.

```text
Company
 ├── Department
 └── Employee
```

### Bridge

Separates two independent dimensions.

```text
Notification
      +
Sender
```

***

# When to Use

Use Composite when:

* Data naturally forms a tree.
* Clients should treat individual and grouped objects the same way.
* Recursive structures exist.

Examples:

* File systems
* Menus
* Organization charts
* Product categories
* UI component trees

***

# Interview Question

### Q: What is the main benefit of the Composite Pattern?

**Answer:**

The Composite Pattern allows clients to work with single objects and groups of objects through the same interface.

Example:

```csharp
IFileSystemItem item = new File("Notes.txt");

IFileSystemItem folder = new Folder("Documents");
```

Both are used identically:

```csharp
item.Display();

folder.Display();
```

without type checking or special handling.

***

## Composite Pattern in One Line

> **Treat individual objects and collections of objects uniformly through a common interface.**

✅ **Structural Pattern #3 Complete: Composite Pattern**

### Structural Patterns Progress

1. ✅ Adapter
2. ✅ Bridge
3. ✅ Composite
4. ⏭️ Decorator
5. ⏭️ Facade
6. ⏭️ Flyweight
7. ⏭️ Proxy

**Next Pattern: Decorator Pattern**, one of the most important patterns in .NET, used heavily in ASP.NET Core middleware, streams, logging pipelines, and dependency injection.
