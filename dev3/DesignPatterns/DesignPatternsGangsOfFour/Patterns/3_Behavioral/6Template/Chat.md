Excellent. The **Template Method Pattern** is often taught right after Strategy because both deal with algorithms, but they use very different approaches.

> **Strategy:** Change the entire algorithm using composition.
>
> **Template Method:** Keep the algorithm structure fixed but allow subclasses to customize certain steps.

***

# Template Method Pattern

## Definition

The Template Method Pattern defines the skeleton of an algorithm in a base class while allowing subclasses to override specific steps without changing the overall algorithm structure.

***

# Real-World Analogy

Think of making a beverage.

The process is generally:

```text
1. Boil Water
2. Brew Beverage
3. Pour into Cup
4. Add Condiments
```

For Tea:

```text
Boil Water
Steep Tea
Pour into Cup
Add Lemon
```

For Coffee:

```text
Boil Water
Brew Coffee
Pour into Cup
Add Sugar and Milk
```

The overall process stays the same.

Only certain steps differ.

***

# Problem Statement

Suppose we have:

```text
Tea Preparation
Coffee Preparation
```

Without Template Method:

```csharp
PrepareTea()
{
    BoilWater();
    SteepTea();
    PourIntoCup();
    AddLemon();
}

PrepareCoffee()
{
    BoilWater();
    BrewCoffee();
    PourIntoCup();
    AddMilk();
}
```

Notice:

```text
Boil Water
Pour Into Cup
```

are duplicated.

This duplication grows as more beverage types are added.

***

# Solution

Move the common algorithm into a base class.

Allow subclasses to implement the variable steps.

***

# Structure

```text
          Beverage
              ▲
      ----------------
      |              |
     Tea          Coffee
```

The base class owns the algorithm.

Subclasses provide implementation details.

***

# Console Application Example

## Step 1: Abstract Class

### Beverage.cs

```csharp
using System;

namespace TemplateMethodDemo
{
    public abstract class Beverage
    {
        // Template Method
        public void PrepareRecipe()
        {
            BoilWater();
            Brew();
            PourInCup();
            AddCondiments();
        }

        private void BoilWater()
        {
            Console.WriteLine("Boiling water");
        }

        private void PourInCup()
        {
            Console.WriteLine("Pouring into cup");
        }

        protected abstract void Brew();

        protected abstract void AddCondiments();
    }
}
```

***

## Step 2: Concrete Implementation

### Tea.cs

```csharp
using System;

namespace TemplateMethodDemo
{
    public class Tea : Beverage
    {
        protected override void Brew()
        {
            Console.WriteLine("Steeping tea");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("Adding lemon");
        }
    }
}
```

***

### Coffee.cs

```csharp
using System;

namespace TemplateMethodDemo
{
    public class Coffee : Beverage
    {
        protected override void Brew()
        {
            Console.WriteLine("Brewing coffee");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("Adding milk and sugar");
        }
    }
}
```

***

## Step 3: Client

### Program.cs

```csharp
using System;

namespace TemplateMethodDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Beverage tea = new Tea();

            Console.WriteLine("Preparing Tea");
            tea.PrepareRecipe();

            Console.WriteLine();

            Beverage coffee = new Coffee();

            Console.WriteLine("Preparing Coffee");
            coffee.PrepareRecipe();

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Preparing Tea

Boiling water
Steeping tea
Pouring into cup
Adding lemon

Preparing Coffee

Boiling water
Brewing coffee
Pouring into cup
Adding milk and sugar
```

***

# Why Is It Called "Template Method"?

The method:

```csharp
PrepareRecipe()
```

defines the template (the overall workflow).

```csharp
BoilWater();
Brew();
PourInCup();
AddCondiments();
```

Subclasses are not allowed to change the sequence.

They can only customize certain steps.

***

# Hook Methods

Template Method often includes optional hooks.

## Base Class

```csharp
public abstract class Beverage
{
    public void PrepareRecipe()
    {
        BoilWater();
        Brew();
        PourInCup();

        if (CustomerWantsCondiments())
        {
            AddCondiments();
        }
    }

    protected virtual bool CustomerWantsCondiments()
    {
        return true;
    }

    protected abstract void Brew();

    protected abstract void AddCondiments();
}
```

***

## Derived Class

```csharp
public class BlackCoffee : Coffee
{
    protected override bool CustomerWantsCondiments()
    {
        return false;
    }
}
```

Now condiments are skipped.

***

# Real-World Example: Report Generation

All reports follow:

```text
1. Fetch Data
2. Process Data
3. Export Report
```

***

## Template

```csharp
public abstract class ReportGenerator
{
    public void Generate()
    {
        FetchData();
        ProcessData();
        Export();
    }

    protected abstract void FetchData();
    protected abstract void ProcessData();
    protected abstract void Export();
}
```

***

## PDF Report

```csharp
public class PdfReport : ReportGenerator
{
    protected override void FetchData()
    {
        Console.WriteLine("Fetching PDF data");
    }

    protected override void ProcessData()
    {
        Console.WriteLine("Processing PDF data");
    }

    protected override void Export()
    {
        Console.WriteLine("Exporting PDF");
    }
}
```

***

## Excel Report

```csharp
public class ExcelReport : ReportGenerator
{
    protected override void FetchData()
    {
        Console.WriteLine("Fetching Excel data");
    }

    protected override void ProcessData()
    {
        Console.WriteLine("Processing Excel data");
    }

    protected override void Export()
    {
        Console.WriteLine("Exporting Excel");
    }
}
```

***

# Real-World .NET Examples

## ASP.NET Page Lifecycle (Classic ASP.NET)

Framework defines:

```text
Init
Load
Render
Unload
```

Developers override specific stages.

***

## Unit Test Frameworks

Methods such as:

```csharp
SetUp()
Test()
TearDown()
```

follow a predefined execution template.

***

## Background Services

Often:

```csharp
Start
Execute
Stop
```

is fixed while subclasses customize implementation details.

***

# UML (Simplified)

```text
            Beverage
                ▲
        ----------------
        |              |
       Tea         Coffee

Template Method:
PrepareRecipe()
```

***

# Template Method vs Strategy

This is the most common interview question.

## Template Method

Uses inheritance.

```csharp
abstract class Beverage
{
    public void PrepareRecipe()
    {
        ...
    }
}
```

Subclasses override steps.

Focus:

✅ Fixed algorithm structure

✅ Customizable steps

***

## Strategy

Uses composition.

```csharp
ShoppingCart
      |
IPaymentStrategy
```

Different algorithms can be swapped at runtime.

Focus:

✅ Runtime algorithm selection

***

### Quick Rule

**Template Method**

```text
Inheritance
Fixed workflow
```

**Strategy**

```text
Composition
Replaceable workflow
```

***

# Template Method vs State

### State

Behavior changes because state changes.

```text
Draft
Published
Archived
```

***

### Template Method

Behavior changes because subclasses implement steps differently.

```text
Tea
Coffee
```

***

# Advantages

✅ Eliminates duplicate code

✅ Enforces a standard workflow

✅ Easy to extend

✅ Supports Open/Closed Principle

✅ Centralizes algorithm structure

***

# Disadvantages

❌ Relies on inheritance

❌ Less flexible than Strategy

❌ Deep inheritance hierarchies can become difficult to maintain

***

# When to Use

Use Template Method when:

* Multiple classes follow the same workflow.
* Only a few steps vary.
* You want to enforce execution order.
* Common logic should exist in one place.

Examples:

* Report generation
* File import/export
* Document processing
* Payment workflows
* Data synchronization
* ETL pipelines

***

# Interview Question

### Q: What problem does Template Method solve?

**Answer:**

Template Method removes duplicated workflow logic by defining the algorithm structure in a base class and allowing subclasses to implement specific steps.

Instead of:

```csharp
PrepareTea();
PrepareCoffee();
```

having duplicate code, create:

```csharp
PrepareRecipe();
```

in the base class and let subclasses provide the beverage-specific details.

***

# Template Method Pattern in One Line

> **Define the skeleton of an algorithm in a base class and allow subclasses to customize specific steps without changing the algorithm structure.**

✅ **Behavioral Pattern #6 Complete: Template Method**

### Behavioral Patterns Progress

1. ✅ Chain of Responsibility
2. ✅ Observer
3. ✅ Strategy
4. ✅ Command
5. ✅ State
6. ✅ Template Method
7. ⏭️ Iterator
8. ⏭️ Mediator
9. ⏭️ Memento
10. ⏭️ Visitor
11. ⏭️ Interpreter

**Next Pattern:** **Mediator**, which centralizes communication between objects and is commonly demonstrated using a chat room or UI control interactions.
