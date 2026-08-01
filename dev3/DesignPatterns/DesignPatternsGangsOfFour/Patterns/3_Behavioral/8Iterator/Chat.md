Excellent. The **Iterator Pattern** is one of the simplest yet most widely used Behavioral Patterns.

In fact, you use it almost every day in C# without realizing it:

```csharp
foreach (var item in collection)
{
    Console.WriteLine(item);
}
```

Behind the scenes, the .NET Framework is using the **Iterator Pattern**.

***

# Iterator Pattern

## Definition

The Iterator Pattern provides a way to access elements of a collection sequentially without exposing its underlying implementation.

In simple words:

> Traverse a collection without knowing how the collection is internally stored.

***

# Real-World Analogy

Think of a TV remote.

You can move through channels:

```text
Next Channel
Previous Channel
```

You don't care whether channels are stored in:

* Array
* List
* Database
* File

The remote provides a standard way to navigate.

***

# Problem Statement

Suppose we have a collection:

```csharp
List<string> employees
```

Without Iterator:

```csharp
for (int i = 0; i < employees.Count; i++)
{
    Console.WriteLine(employees[i]);
}
```

This exposes:

* Indexing logic
* Internal structure

What if tomorrow the collection changes?

```text
Array → Linked List
List → Database
```

Client code may need changes.

***

# Solution

Provide a separate iterator object responsible for traversal.

```text
Collection
     ↓
 Iterator
     ↓
 Elements
```

The client only asks:

```csharp
MoveNext()
Current
```

***

# Structure

```text
          Iterator
              ▲
              |
      EmployeeIterator

              ▲
              |
      EmployeeCollection
```

***

# Console Application Example

## Step 1: Iterator Interface

### IIterator.cs

```csharp
namespace IteratorPatternDemo
{
    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
    }
}
```

***

## Step 2: Collection

### EmployeeCollection.cs

```csharp
using System.Collections.Generic;

namespace IteratorPatternDemo
{
    public class EmployeeCollection
    {
        private readonly List<string> _employees =
            new();

        public void Add(string employee)
        {
            _employees.Add(employee);
        }

        public IIterator<string> CreateIterator()
        {
            return new EmployeeIterator(_employees);
        }
    }
}
```

***

## Step 3: Concrete Iterator

### EmployeeIterator.cs

```csharp
using System.Collections.Generic;

namespace IteratorPatternDemo
{
    public class EmployeeIterator : IIterator<string>
    {
        private readonly List<string> _employees;
        private int _position = 0;

        public EmployeeIterator(
            List<string> employees)
        {
            _employees = employees;
        }

        public bool HasNext()
        {
            return _position < _employees.Count;
        }

        public string Next()
        {
            return _employees[_position++];
        }
    }
}
```

***

## Step 4: Client

### Program.cs

```csharp
using System;

namespace IteratorPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeCollection employees =
                new EmployeeCollection();

            employees.Add("Raghav");
            employees.Add("Amit");
            employees.Add("Priya");

            IIterator<string> iterator =
                employees.CreateIterator();

            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.Next());
            }

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Raghav
Amit
Priya
```

***

# How It Works

Execution:

```csharp
while(iterator.HasNext())
{
    Console.WriteLine(iterator.Next());
}
```

Flow:

```text
Iterator
   ↓
Gets Current Item
   ↓
Moves Forward
   ↓
Returns Next Item
```

The client never accesses the internal list directly.

***

# Real .NET Version

The .NET Framework already provides Iterator support through:

```csharp
IEnumerable<T>
IEnumerator<T>
```

***

## Example

```csharp
List<string> names =
[
    "Raghav",
    "Amit",
    "Priya"
];

foreach (var name in names)
{
    Console.WriteLine(name);
}
```

Behind the scenes:

```csharp
IEnumerator<string> enumerator =
    names.GetEnumerator();

while (enumerator.MoveNext())
{
    Console.WriteLine(
        enumerator.Current);
}
```

This is the Iterator Pattern.

***

# Implementing Iterator with IEnumerable

A more realistic C# implementation.

## Collection

```csharp
using System.Collections;
using System.Collections.Generic;

public class EmployeeCollection
    : IEnumerable<string>
{
    private readonly List<string> _employees =
        new();

    public void Add(string employee)
    {
        _employees.Add(employee);
    }

    public IEnumerator<string> GetEnumerator()
    {
        return _employees.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
```

***

## Client

```csharp
var employees =
    new EmployeeCollection();

employees.Add("Raghav");
employees.Add("Amit");
employees.Add("Priya");

foreach (var employee in employees)
{
    Console.WriteLine(employee);
}
```

Output:

```text
Raghav
Amit
Priya
```

***

# Using yield return

C# makes Iterator implementation even easier.

```csharp
public IEnumerable<int> GetNumbers()
{
    yield return 1;
    yield return 2;
    yield return 3;
}
```

Usage:

```csharp
foreach(var number in GetNumbers())
{
    Console.WriteLine(number);
}
```

Output:

```text
1
2
3
```

The compiler automatically generates the iterator.

***

# Real-World Example: Book Collection

## Collection

```csharp
public class Library
{
    private readonly List<string> _books =
        new();

    public void AddBook(string book)
    {
        _books.Add(book);
    }

    public IEnumerator<string> GetEnumerator()
    {
        return _books.GetEnumerator();
    }
}
```

***

## Client

```csharp
foreach(var book in library)
{
    Console.WriteLine(book);
}
```

The client doesn't care whether books are stored in:

* List
* Array
* Linked List
* Database

***

# UML (Simplified)

```text
       IIterator
           ▲
           |
   EmployeeIterator

           ▲
           |
  EmployeeCollection
```

***

# Iterator vs Composite

A common interview question.

### Composite

Represents hierarchical structures.

```text
Folder
 ├── File
 └── File
```

Focus:

✅ Part-whole relationship

***

### Iterator

Traverses collections.

```text
Collection
     ↓
 Iterator
```

Focus:

✅ Sequential access

***

# Iterator vs Observer

### Observer

```text
Subject
   ↓
Observers
```

Focus:

✅ Event notification

***

### Iterator

```text
Collection
   ↓
Iterator
```

Focus:

✅ Collection traversal

***

# Iterator vs Visitor

### Iterator

Traverses data.

```text
Next()
Current
MoveNext()
```

***

### Visitor

Performs operations on object structures.

```text
Accept(visitor)
```

Focus:

✅ Adding operations

***

# Advantages

✅ Hides collection implementation

✅ Provides a standard traversal mechanism

✅ Supports multiple traversal strategies

✅ Simplifies client code

✅ Works with different collection types

***

# Disadvantages

❌ Adds extra classes in classic implementations

❌ May be unnecessary since .NET already provides IEnumerable/IEnumerator

❌ Multiple iterators can add complexity

***

# When to Use

Use Iterator when:

* You need sequential access to collection items.
* Internal collection details should be hidden.
* Multiple traversal mechanisms are needed.
* You want to support `foreach`.

Examples:

* Collections
* Tree Traversals
* File Systems
* Database Results
* Custom Data Structures

***

# Interview Question

### Q: How is the Iterator Pattern implemented in C#?

**Answer:**

Through:

```csharp
IEnumerable<T>
IEnumerator<T>
```

and the `foreach` statement.

Example:

```csharp
foreach(var item in collection)
{
    Console.WriteLine(item);
}
```

Internally C# uses an iterator to traverse the collection.

***

# Iterator Pattern in One Line

> **Provide a way to sequentially access elements of a collection without exposing its internal structure.**

✅ **Behavioral Pattern #8 Complete: Iterator Pattern**

### Behavioral Patterns Progress

1. ✅ Chain of Responsibility
2. ✅ Observer
3. ✅ Strategy
4. ✅ Command
5. ✅ State
6. ✅ Template Method
7. ✅ Mediator
8. ✅ Iterator
9. ⏭️ Memento
10. ⏭️ Visitor
11. ⏭️ Interpreter

**Next Pattern: Memento Pattern**, which is commonly used for undo/redo functionality by capturing and restoring an object's previous state.
